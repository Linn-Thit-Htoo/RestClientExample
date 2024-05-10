using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestClientExample.BlazorWasm.Models;
using RestClientExample.BlazorWasm.Services;
using RestSharp;
using static RestClientExample.BlazorWasm.Services.InjectService;

namespace RestClientExample.BlazorWasm.Pages.Blog;

public partial class DeleteBlogDialog
{
    [CascadingParameter] MudDialogInstance? MudDialog { get; set; }

    [Parameter] public long id { get; set; }

    void Cancel() => MudDialog?.Cancel();

    #region Delete Async

    public async Task DeleteAsync()
    {
        if (id != 0)
        {
            var response = await RestClientService.ExecuteAsync<BlogResponseModel>(
                $"{Endpoints.Blog}/{id}",
                EnumHttpMethod.Delete);

            if (response.IsSuccess)
            {
                InjectService.ShowMessage(response.Message!, EnumResponseType.Success);
                MudDialog?.Close();
                return;
            }

            InjectService.ShowMessage(response.Message!, EnumResponseType.Error);
        }
    }
    #endregion
}