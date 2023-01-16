using Microsoft.AspNetCore.Razor.Language;

namespace Masa.Try;

public class CodeRenderingProject : RazorProjectFileSystem
{
    /// <summary>
    /// 全局设置Using
    /// </summary>
    public static string GlobalUsing = @"@using Masa.Try 
@using Microsoft.AspNetCore.Components.Web";
    
    public override IEnumerable<RazorProjectItem> EnumerateItems(string basePath)
    {
        throw new NotImplementedException();
    }

    public override RazorProjectItem GetItem(string path)
    {
        throw new NotImplementedException();
    }

    public override RazorProjectItem GetItem(string path, string fileKind)
    {
        if (path == "/_Imports.razor")
            return new CodeRenderingProjectItem()
            {
                Name = "_Imports.razor",
                Code = GlobalUsing
            };
        throw new NotImplementedException(fileKind + ":" + path);
    }
}