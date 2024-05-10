using Microsoft.EntityFrameworkCore;
using RestClientExample.RestApi.Models;

namespace RestClientExample.RestApi.Features.Blog;

public class DataAccess_Blog
{
    private readonly AppDbContext _appDbContext;

    public DataAccess_Blog(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<BlogListResponseModel> GetBlogs(int pageNo, int pageSize)
    {
        var lst = await _appDbContext.Blogs
            .AsNoTracking()
            .OrderByDescending(x => x.BlogId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var rowCount = await _appDbContext.Blogs.CountAsync();
        var pageCount = rowCount / pageSize;

        if (rowCount % pageSize > 0)
        {
            pageCount++;
        }

        PageSettingModel pageSettingModel = new()
        {
            PageCount = pageCount,
            PageSize = pageSize,
            PageNo = pageNo
        };

        //BlogDataModel blogDataModel = new()
        //{
        //    Blogs = lst
        //};

        return new BlogListResponseModel()
        {
            Data = new BlogDataModel() { Blogs = lst },
            PageSetting = new PageSettingModel()
            {
                PageCount = pageCount,
                PageSize = pageSize,
                PageNo = pageNo
            }
        };
    }

    public async Task<BlogModel> GetBlog(long id)
    {
        var item = await _appDbContext.Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id);
        return item!;
    }

    public async Task<int> CreateBlog(BlogRequestModel requestModel)
    {
        await _appDbContext.Blogs.AddAsync(requestModel.Change());
        int result = await _appDbContext.SaveChangesAsync();
        return result;
    }

    public async Task<int> UpdateBlog(BlogRequestModel requestModel, long id)
    {
        BlogModel? item = await _appDbContext.Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id) ?? throw new Exception("No data found.");

        item.BlogTitle = requestModel.BlogTitle;
        item.BlogAuthor = requestModel.BlogAuthor;
        item.BlogContent = requestModel.BlogContent;
        _appDbContext.Entry(item).State = EntityState.Modified;
        int result = await _appDbContext.SaveChangesAsync();

        return result;
    }

    public async Task<int> PatchBlog(BlogRequestModel requestModel, long id)
    {
        BlogModel? item = await _appDbContext.Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id) ?? throw new Exception("No data found.");

        if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            item.BlogTitle = requestModel.BlogTitle;

        if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            item.BlogAuthor = requestModel.BlogAuthor;

        if (!string.IsNullOrEmpty(requestModel.BlogContent))
            item.BlogContent = requestModel.BlogContent;

        _appDbContext.Entry(item).State = EntityState.Modified;
        int result = await _appDbContext.SaveChangesAsync();

        return result;
    }

    public async Task<int> DeleteBlog(long id)
    {
        BlogModel? item = await _appDbContext.Blogs
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.BlogId == id) ?? throw new Exception("No data found.");

        _appDbContext.Blogs.Remove(item);
        int result = await _appDbContext.SaveChangesAsync();

        return result;
    }
}
