using Masa.Try;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

await CodeRenderingHelper.InitializedAsync(await GetReference(),await GetRazorExtension());

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


async Task<List<PortableExecutableReference>?> GetReference()
{
    var refs = new List<PortableExecutableReference>();
    foreach (var v in AppDomain.CurrentDomain.GetAssemblies())
    {
        try
        {
            // Server是在服务器运行可以直接获取文件
            refs?.Add(MetadataReference.CreateFromFile(v.Location));
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