using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using RestClientExample.BlazorWasm.Models;
using RestClientExample.BlazorWasm.Services;
using RestSharp;
using static RestClientExample.BlazorWasm.Services.InjectService;

namespace RestClientExample.BlazorWasm.Pages.Blog;

public partial class EditBlogDialog
{
    private BlogModel Model = new();

    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

    [Parameter] public long id { get; set; }

    private void Cancel() => MudDialog?.Close();

    #region Save Async

    private async Task SaveAsync()
    {
        if (string.IsNullOrEmpty(Model.BlogTitle))
        {
            ShowWarning("Blog Title cannot be empty.");
            return;
        }

        if (string.IsNullOrEmpty(Model.BlogAuthor))
        {
            ShowWarning("Blog Author cannot be empty.");
            return;
        }

        if (string.IsNullOrEmpty(Model.BlogContent))
        {
            ShowWarning("Blog Content cannot be empty.");
            return;
        }

        BlogRequestModel requestModel = new()
        {
            BlogTitle = Model.BlogTitle,
            BlogAuthor = Model.BlogAuthor,
            BlogContent = Model.BlogContent
        };

        RestRequest request = new($"{Endpoints.Blog}/{id}", Method.Patch);
        string jsonBody = JsonConvert.SerializeObject(requestModel);
        request.AddJsonBody(jsonBody);
        RestResponse response = await RestClient.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            InjectService.ShowMessage(response.Content!.Substring(1, response.Content!.Length - 2), EnumResponseType.Success);
            MudDialog?.Close();
            return;
        }

        InjectService.ShowMessage(response.Content!.Substring(1, response.Content!.Length - 2), EnumResponseType.Error);
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
        RestRequest request = new($"/api/blog/{id}", Method.Get);
        RestResponse response = await RestClient.GetAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            Model = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
        }
    }

    #endregion

    #region Show Warning

    private void ShowWarning(string message)
    {
        InjectService.ShowMessage(message, EnumResponseType.Warning);
    }

    #endregion
}