﻿using Newtonsoft.Json;
using RestSharp;

namespace RestClientExample.BlazorWasm.Services;

public class RestClientService
{
    private RestClient _restClient;

    public RestClientService(RestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<T> ExecuteAsync<T>(string endpoint, EnumHttpMethod enumHttpMethod, object? requestModel = null)
    {
        RestRequest request = new(endpoint);
        RestResponse response = null;

        if (requestModel is not null)
        {
            string jsonStr = JsonConvert.SerializeObject(requestModel);
            request.AddJsonBody(jsonStr);
        }

        switch (enumHttpMethod)
        {
            case EnumHttpMethod.Get:
                response = await _restClient.GetAsync(request);
                break;
            case EnumHttpMethod.Post:
                response = await _restClient.PostAsync(request);
                break;
            case EnumHttpMethod.Put:
                response = await _restClient.PutAsync(request);
                break;
            case EnumHttpMethod.Patch:
                response = await _restClient.PatchAsync(request);
                break;
            case EnumHttpMethod.Delete:
                response = await _restClient.DeleteAsync(request);
                break;
            case EnumHttpMethod.None:
            default:
                break;
        }

        var jsonResponse = response?.Content!;
        var returnModel = JsonConvert.DeserializeObject<T>(jsonResponse)!;

        return returnModel;
    }
}

public enum EnumHttpMethod
{
    None,
    Get,
    Post,
    Put,
    Patch,
    Delete
}