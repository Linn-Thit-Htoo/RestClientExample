using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using RestClientExample.BlazorWasm.Models;
using RestSharp;
using static RestClientExample.BlazorWasm.Services.InjectService;

namespace RestClientExample.BlazorWasm.Pages.Blog;

public partial class CreateBlogDialog
{
    private BlogRequestModel requestModel = new();

    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

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

        RestRequest request = new("/api/Blog", Method.Post);
        string jsonBody = JsonConvert.SerializeObject(requestModel);
        request.AddJsonBody(jsonBody);
        RestResponse response = await RestClient.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!.Substring(1, response.Content.Length - 2);
            InjectService.ShowMessage(jsonStr, EnumResponseType.Success);
            MudDialog?.Close();
            return;
        }

        InjectService.ShowMessage(response.Content!, EnumResponseType.Error);
    }


    private void Cancel() => MudDialog?.Cancel();

    private void Validate()
    {
    }

    private void ShowWarning(string message)
    {
        InjectService.ShowMessage(message, EnumResponseType.Warning);
    }
}
