using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestClientExample.BlazorWasm.Services;
using RestSharp;
using static RestClientExample.BlazorWasm.Services.InjectService;

namespace RestClientExample.BlazorWasm.Pages.Blog;

public partial class DeleteBlogDialog
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    [Parameter] public long id { get; set; }

    void Cancel() => MudDialog.Cancel();

    #region Delete Async

    public async Task DeleteAsync()
    {
        if (id != 0)
        {
            RestRequest request = new($"{Endpoints.Blog}/{id}", Method.Delete);
            RestResponse response = await RestClient.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                InjectService.ShowMessage(response.Content!.Substring(1, response.Content!.Length - 2), EnumResponseType.Success);
                MudDialog.Close();
                return;
            }

            InjectService.ShowMessage(response.Content!.Substring(1, response.Content!.Length - 2), EnumResponseType.Error);
        }
    }

    #endregion
}
