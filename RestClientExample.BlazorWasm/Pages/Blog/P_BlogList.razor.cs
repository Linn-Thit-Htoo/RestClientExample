﻿using MudBlazor;
using Newtonsoft.Json;
using RestClientExample.BlazorWasm.Models;
using RestClientExample.BlazorWasm.Services;
using RestSharp;

namespace RestClientExample.BlazorWasm.Pages.Blog;

public partial class P_BlogList
{
    private BlogListResponseModel? ResponseModel;
    private int _pageNo = 1;
    private int _pageSize = 10;

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await List(_pageNo, _pageSize);
        }
    }

    private async Task List(int pageNo, int pageSize = 10)
    {
        _pageNo = pageNo;
        _pageSize = pageSize;

        RestRequest request = new($"{Endpoints.Blog}?pageNo={_pageNo}&pageSize={_pageSize}", Method.Get);
        RestResponse response = await RestClient.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            ResponseModel = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr)!;
            StateHasChanged();
        }
    }

    private async Task PageChanged(int i)
    {
        _pageNo = i;
        await List(_pageNo);
    }

    private async Task Popup()
    {
        DialogResult result = await InjectService.ShowDialogAsync<CreateBlogDialog>("Create Blog");

        if (!result.Canceled)
            await List(_pageNo, _pageSize);
    }

    private async Task EditPopup(long id)
    {

        var parameters = new DialogParameters<EditBlogDialog>
        {
            { "id", id }
        };

        DialogResult result = await InjectService.ShowDialogAsync<EditBlogDialog>("Edit Blog", parameters);

        if (!result.Canceled)
            await List(_pageNo, _pageSize);
    }
}