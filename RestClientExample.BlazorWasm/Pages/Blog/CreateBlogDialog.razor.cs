using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestClientExample.BlazorWasm.Models;
using RestClientExample.BlazorWasm.Services;
using static RestClientExample.BlazorWasm.Services.InjectService;

namespace RestClientExample.BlazorWasm.Pages.Blog;

public partial class CreateBlogDialog
{
    private BlogRequestModel requestModel = new();

    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

    private bool isButtonDisabled = true;

    #region Save Async

    private async Task SaveAsync()
    {

        if (string.IsNullOrEmpty(requestModel.BlogTitle))
        {
            ShowWarning("Title cannot be empty");
            return;
        }

        if (string.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            ShowWarning("Author cannot be empty.");
            return;
        }

        if (string.IsNullOrEmpty(requestModel.BlogContent))
        {
            ShowWarning("Content cannot be empty.");
            return;
        }

        var response = await RestClientService.ExecuteAsync<BlogResponseModel>(
            Endpoints.Blog,
            EnumHttpMethod.Post,
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

    #region Show Warning

    private void ShowWarning(string message)
    {
        InjectService.ShowMessage(message, EnumResponseType.Warning);
    }

    #endregion

    private void Cancel() => MudDialog?.Close();

    private void Validate()
    {
        if (string.IsNullOrEmpty(requestModel.BlogTitle) || string.IsNullOrEmpty(requestModel.BlogAuthor) || string.IsNullOrEmpty(requestModel.BlogContent))
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