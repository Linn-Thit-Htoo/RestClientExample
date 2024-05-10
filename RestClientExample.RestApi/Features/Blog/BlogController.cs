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
            BlogListResponseModel lst = await _businessLogic_Blog.GetBlogs(pageNo, pageSize);
            return Ok(new ResponseModel()
            {
                IsSuccess = true,
                Data = lst.Data,
                PageSetting = lst.PageSetting
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseModel()
            {
                IsSuccess = false,
                Message = ex.Message
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(long id)
    {
        try
        {
            var item = await _businessLogic_Blog.GetBlog(id);
            return Ok(new ResponseModel()
            {
                IsSuccess = true,
                Item = item
            });
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

            ResponseModel responseModel = new();

            if (result > 0)
            {
                return StatusCode(201, responseModel = new()
                {
                    IsSuccess = true,
                    Message = "Creating Successful!",
                    Item = requestModel
                });
            }

            return BadRequest(responseModel = new()
            {
                Message = "Creating Fail!",
                IsSuccess = false
            });
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
            ResponseModel responseModel = new();

            if (result > 0)
            {
                return StatusCode(202, responseModel = new()
                {
                    IsSuccess = true,
                    Message = "Updating Successful!",
                    Item = requestModel
                });
            }

            return StatusCode(400, responseModel = new()
            {
                IsSuccess = true,
                Message = "Updating Fail!"
            });
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
            ResponseModel responseModel = new();

            if (result > 0)
            {
                return StatusCode(202, responseModel = new()
                {
                    IsSuccess = true,
                    Message = "Updating Successful!",
                    Item = requestModel
                });
            }

            return BadRequest(responseModel = new()
            {
                IsSuccess = false,
                Message = "Updating Fail."
            });
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
        ResponseModel responseModel = new();

        if (result > 0)
        {
            return StatusCode(202, responseModel = new()
            {
                IsSuccess = true,
                Message = "Deleting Successful."
            });
        }

        return BadRequest(responseModel = new()
        {
            IsSuccess = false,
            Message = "Deleting Fail."
        });
    }
}
