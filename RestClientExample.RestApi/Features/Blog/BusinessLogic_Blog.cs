using RestClientExample.RestApi.Models;

namespace RestClientExample.RestApi.Features.Blog;

public class BusinessLogic_Blog
{
    private readonly DataAccess_Blog _dataAccess_Blog;

    public BusinessLogic_Blog(DataAccess_Blog dataAccess_Blog)
    {
        _dataAccess_Blog = dataAccess_Blog;
    }

    public async Task<BlogListResponseModel> GetBlogs(int pageNo, int pageSize)
    {
        if (pageNo == 0 || pageSize == 0)
            throw new Exception("Invalid Request.");

        return await _dataAccess_Blog.GetBlogs(pageNo, pageSize);
    }

    public async Task<BlogModel> GetBlog(long id)
    {
        if (id == 0)
            throw new Exception("Id cannot be empty.");

        return await _dataAccess_Blog.GetBlog(id);
    }

    public async Task<int> CreateBlog(BlogRequestModel requestModel)
    {
        if (string.IsNullOrEmpty(requestModel.BlogTitle))
            throw new Exception("Blog Title cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.BlogAuthor))
            throw new Exception("Blog Author cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.BlogContent))
            throw new Exception("Blog Content cannot be empty.");

        int result = await _dataAccess_Blog.CreateBlog(requestModel);
        return result;
    }

    public async Task<int> UpdateBlog(BlogRequestModel requestModel, long id)
    {
        if (id == 0)
            throw new Exception("ID cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.BlogTitle))
            throw new Exception("Blog Title cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.BlogAuthor))
            throw new Exception("Blog Author cannot be empty.");

        if (string.IsNullOrEmpty(requestModel.BlogContent))
            throw new Exception("Blog Content cannot be empty.");

        int result = await _dataAccess_Blog.UpdateBlog(requestModel, id);
        return result;
    }

    public async Task<int> PatchBlog(BlogRequestModel requestModel, long id)
    {
        if (id == 0)
            throw new Exception("ID cannot be empty.");

        int result = await _dataAccess_Blog.PatchBlog(requestModel, id);
        return result;
    }

    public async Task<int> DeleteBlog(long id)
    {
        if (id == 0)
            throw new Exception("ID cannot be empty.");

        int result = await _dataAccess_Blog.DeleteBlog(id);
        return result;
    }
}