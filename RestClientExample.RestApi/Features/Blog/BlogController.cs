using Microsoft.AspNetCore.Mvc;
using RestClientExample.RestApi.Models;

namespace RestClientExample.RestApi.Features.Blog;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly BusinessLogic_Blog _businessLogic_Blog;

    public BlogController(BusinessLogic_Blog businessLogic_Blog)
    {
        _businessLogic_Blog = businessLogic_Blog;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs(int pageNo, int pageSize)
    {
        try
        {
            return Ok(await _businessLogic_Blog.GetBlogs(pageNo, pageSize));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(long id)
    {
        try
        {
            return Ok(await _businessLogic_Blog.GetBlog(id));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromBody] BlogRequestModel requestModel)
    {
        try
        {
            int result = await _businessLogic_Blog.CreateBlog(requestModel);
            return result > 0 ? StatusCode(201, "Creating Successful.") : BadRequest("Creating Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog([FromBody] BlogRequestModel requestModel, long id)
    {
        try
        {
            int result = await _businessLogic_Blog.UpdateBlog(requestModel, id);
            return result > 0 ? StatusCode(202, "Updating Successful.") : BadRequest("Updating Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchBlog([FromBody] BlogRequestModel requestModel, long id)
    {
        try
        {
            int result = await _businessLogic_Blog.PatchBlog(requestModel, id);
            return result > 0 ? StatusCode(202, "Updating Successful.") : BadRequest("Updating Fail.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        int result = await _businessLogic_Blog.DeleteBlog(id);
        return result > 0 ? StatusCode(202, "Deleting Successful.") : BadRequest("Deleting Fail.");
    }
}
