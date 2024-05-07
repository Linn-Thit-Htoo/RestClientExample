namespace RestClientExample.RestApi.Models;

public static class ChangeModel
{
    public static BlogModel Change(this BlogRequestModel requestModel)
    {
        return new BlogModel()
        {
            BlogTitle = requestModel.BlogTitle,
            BlogAuthor = requestModel.BlogAuthor,
            BlogContent = requestModel.BlogContent
        };
    }
}
