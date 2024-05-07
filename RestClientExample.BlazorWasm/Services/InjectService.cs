using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace RestClientExample.BlazorWasm.Services;

public class InjectService
{
    private readonly IDialogService _dialogService;
    private readonly ISnackbar _snackbar;

    public InjectService(IDialogService dialogService, ISnackbar snackbar)
    {
        _dialogService = dialogService;
        _snackbar = snackbar;
    }

    public async Task<DialogResult> ShowDialogAsync<T>(string title, DialogParameters? parameters = null) where T : IComponent
    {
        DialogOptions options = new()
        {
            MaxWidth = MaxWidth.Small,
            FullWidth = true,
            DisableBackdropClick = true,
            CloseOnEscapeKey = false
        };

        IDialogReference dialog = null!;
        if (parameters is not null)
        {
            dialog = await _dialogService.ShowAsync<T>(title, parameters, options);
        }
        else
        {
            dialog = await _dialogService.ShowAsync<T>(title, options);
        }

        var result = await dialog.Result;

        return result;
    }

    public void ShowMessage(string message, EnumResponseType responseType)
    {
        switch (responseType)
        {
            case EnumResponseType.Success:
                _snackbar.Add(message, Severity.Success);
                break;
            case EnumResponseType.Info:
                _snackbar.Add(message, Severity.Info);
                break;
            case EnumResponseType.Warning:
                _snackbar.Add(message, Severity.Warning);
                break;
            case EnumResponseType.Error:
                _snackbar.Add(message, Severity.Error);
                break;
            default:
                break;
        }
    }

    public enum EnumResponseType
    {
        Success,
        Info,
        Warning,
        Error
    }
}