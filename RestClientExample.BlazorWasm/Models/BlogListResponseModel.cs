namespace RestClientExample.BlazorWasm.Models;

public class BlogListResponseModel : ResponseModel
{
    public BlogDataModel Data {  get; set; }
    public PageSettingModel PageSetting { get; set; }
}