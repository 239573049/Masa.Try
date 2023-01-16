using Masa.Try;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Masa.Try.WebAssemlby;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await CodeRenderingHelper.InitializedAsync(await GetReference(builder.Services.BuildServiceProvider()),await GetRazorExtension());

await builder.Build().RunAsync();

async Task<List<PortableExecutableReference>?> GetReference(IServiceProvider service)
{
    
    var httpClient = service.GetService<HttpClient>();

    var refs = new List<PortableExecutableReference>();
    foreach (var v in AppDomain.CurrentDomain.GetAssemblies())
    {
        try
        {
            // web Assembly 需要通过网络获取程序集
            var stream = await httpClient!.GetStreamAsync("_framework/" + v.GetName().Name + ".dll");
            if(stream.Length > 0)
            {
                refs?.Add(MetadataReference.CreateFromStream(stream));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    return refs; 
}

async Task<List<RazorExtension>> GetRazorExtension()
{
    var exits = new List<RazorExtension>();
    
    foreach (var asm in typeof(Program).Assembly.GetReferencedAssemblies())
    {
        exits.Add(new AssemblyExtension(asm.FullName, AppDomain.CurrentDomain.Load(asm.FullName)));
    }

    return exits;
}