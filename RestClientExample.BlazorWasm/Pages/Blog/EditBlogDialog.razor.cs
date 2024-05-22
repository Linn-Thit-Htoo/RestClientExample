using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestClientExample.BlazorWasm.Models;
using RestClientExample.BlazorWasm.Services;
using RestSharp;
using static RestClientExample.BlazorWasm.Services.InjectService;

namespace RestClientExample.BlazorWasm.Pages.Blog;

public partial class EditBlogDialog
{
    private BlogResponseModel ResponseModel = new();

    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

    [Parameter] public long id { get; set; }

    private bool isButtonDisabled = true;

    private void Cancel() => MudDialog?.Close();

    #region Save Async

    private async Task SaveAsync()
    {
        if (string.IsNullOrEmpty(ResponseModel.Item.BlogTitle))
        {
            ShowWarning("Blog Title cannot be empty.");
            return;
        }

        if (string.IsNullOrEmpty(ResponseModel.Item.BlogAuthor))
        {
            ShowWarning("Blog Author cannot be empty.");
            return;
        }

        if (string.IsNullOrEmpty(ResponseModel.Item.BlogContent))
        {
            ShowWarning("Blog Content cannot be empty.");
            return;
        }

        BlogRequestModel requestModel = new()
        {
            BlogTitle = ResponseModel.Item.BlogTitle,
            BlogAuthor = ResponseModel.Item.BlogAuthor,
            BlogContent = ResponseModel.Item.BlogContent
        };

        var response = await RestClientService.ExecuteAsync<BlogResponseModel>(
            $"{Endpoints.Blog}/{id}",
            EnumHttpMethod.Patch,
            requestModel);

        if (response.IsSuccess)
        {
            InjectService.ShowMessage(response.Message!, EnumResponseType.Success);
            MudDialog?.Close();
            return;
        }

        InjectService.ShowMessage(response.Message!, EnumResponseType.Error);
    }

    #endregion

    #region On Initialized Async

    protected override async Task OnInitializedAsync()
    {
        if (id != 0)
        {
            await GetBlog();
        }
    }

    #endregion

    #region Get Blog

    private async Task GetBlog()
    {
        var response = await RestClientService.ExecuteAsync<BlogResponseModel>(
            $"{Endpoints.Blog}/{id}",
            EnumHttpMethod.Get);

        if (response.IsSuccess)
        {
            ResponseModel.Item = response.Item;
        }
    }

    #endregion

    #region Show Warning

    private void ShowWarning(string message)
    {
        InjectService.ShowMessage(message, EnumResponseType.Warning);
    }

    #endregion

    private void Validate()
    {
        if (string.IsNullOrEmpty(ResponseModel.Item.BlogTitle) || string.IsNullOrEmpty(ResponseModel.Item.BlogAuthor) || string.IsNullOrEmpty(ResponseModel.Item.BlogContent))
        {
            isButtonDisabled = true;
        }
        else
        {
            isButtonDisabled = false;
        }
        StateHasChanged();
    }
}