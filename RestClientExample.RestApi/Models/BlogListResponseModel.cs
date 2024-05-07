namespace RestClientExample.RestApi.Models;

public class BlogListResponseModel
{
    public int PageCount { get; set; }
    public int PageNo { get; set; }
    public int PageSize { get; set; }
    public List<BlogModel> Blogs { get; set; }
}