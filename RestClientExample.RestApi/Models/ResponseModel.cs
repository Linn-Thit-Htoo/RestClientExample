namespace RestClientExample.RestApi.Models;

public class ResponseModel
{
    public bool IsSuccess { get; set; }
    public bool IsError { get { return !IsSuccess; } }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public object? Item { get; set; }
    public PageSettingModel PageSetting { get; set; }
}