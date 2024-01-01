using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LAB6.TagHelpers
{
    [HtmlTargetElement("item-link")]
    public class ItemLinkTagHelper : TagHelper
    {
        [HtmlAttributeName("controller-name")]
        public string ControllerName { get; set; }

        [HtmlAttributeName("action-name")]
        public string ActionName { get; set; }

        [HtmlAttributeName("item-id")]
        public int ItemId { get; set; }

        [HtmlAttributeName("display-text")]
        public string DisplayText { get; set; }



        public override void Process(TagHelperContext context, TagHelperOutput output)
        {


            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"/{ControllerName}/{ActionName}/{ItemId}");
            output.Content.SetContent(DisplayText);
        }
    }
}
