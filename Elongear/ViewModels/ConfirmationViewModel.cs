using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elongear.Pages.SnackBars;

using System.Timers;
using Elongear.Http;
namespace Elongear.ViewModels;

public partial class ConfirmationViewModel: BaseViewModel, IDisposable
{
    public ConfirmationViewModel()
    {
        RefreshTimers();
    }

    public void RefreshTimers()
    {
        timer = new(TimeSpan.FromSeconds(1));
        timer.Elapsed += UpdateDate;
        timer.Start();
        finishTimer = new(TimeSpan.FromMinutes(3));
        finishTimer.Elapsed += FinishDate;
        finishTimer.AutoReset = false;
        finishTimer.Start();
    }

    private void FinishDate(object? sender, ElapsedEventArgs e)
    {
        SnackHelper.ShowSnack<ErrorSnack>("Час вийшов. Отримайте наступний код або скористайтесь посиланням з електронного листа");
        timer.Stop();
    }

    private void UpdateDate(object? sender, ElapsedEventArgs e)
    {
        Time = Time.Subtract(TimeSpan.FromSeconds(1));
    }

    public override async void OnHttpSuccessResponse(string message)
    {
        base.OnHttpSuccessResponse("Вас підтверджено");
        await Navigation.GoBackAsync();
        await Navigation.GoBackAsync();
    }

    public void OnResendSuccessResponse(string message)
    {
        RefreshTimers();
        SnackHelper.ShowSnack<SuccessSnack>("Код оновлено, на пошту надіслано новий лист");
    }

    [ObservableProperty]
    private string digits = "";

    [ObservableProperty]
    private TimeSpan time =TimeSpan.FromMinutes(3);

    private System.Timers.Timer timer;
    private System.Timers.Timer finishTimer;

    [RelayCommand]
    public void ReceiveCode()
    {
        var sent = HttpMaster.SendResendActivationCommand(OnResendSuccessResponse, OnHttpErrorResponse);
        OnSentSnack(sent);
    }
    
    [RelayCommand]
    public void SendCode()
    {
        var sent = HttpMaster.SendConfirmActivationCommand(Digits, OnHttpSuccessResponse, OnHttpErrorResponse);
        OnSentSnack(sent);
    }

    public void Dispose()
    {
        timer.Dispose();
        finishTimer.Dispose();
    }
}
