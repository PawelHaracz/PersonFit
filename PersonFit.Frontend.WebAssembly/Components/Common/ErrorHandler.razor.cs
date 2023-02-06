using MudBlazor;

namespace PersonFit.Frontend.WebAssembly.Components.Common;

public partial class ErrorHandler
{
    public List<Exception> _receivedExceptions = new();

    protected override async Task OnErrorAsync(Exception exception)
    {
        _receivedExceptions.Add(exception);
        switch (exception)
        {
            case UnauthorizedAccessException:
                Snackbar.Add("Authentication Failed", Severity.Error);
                break;
        }
    }

    public new void Recover()
    {
        _receivedExceptions.Clear();
        base.Recover();
    }
}