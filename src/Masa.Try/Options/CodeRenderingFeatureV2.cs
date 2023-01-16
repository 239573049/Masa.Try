using Microsoft.AspNetCore.Razor.Language;

namespace Masa.Try;

public class CodeRenderingFeatureV2 : IConfigureRazorCodeGenerationOptionsFeature, ITagHelperFeature
{
    public int Order => 1;

    public RazorEngine Engine { get; set; }

    public void Configure(RazorCodeGenerationOptionsBuilder options)
    {
        options.RootNamespace = "Masa";
    }

    public IReadOnlyList<TagHelperDescriptor> TagHelpers { get; set; }

    public IReadOnlyList<TagHelperDescriptor> GetDescriptors()
    {
        return TagHelpers;

    }

}