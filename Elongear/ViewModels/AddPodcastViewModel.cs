using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elongear.LjsonConverters;
using Elongear.Pages.SnackBars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elongear.Http;
using Elongear.Services;
using System.Collections.ObjectModel;
using Elongear.Observables;

namespace Elongear.ViewModels;

public partial class AddPodcastViewModel: BaseViewModel
{
    public AddPodcastViewModel()
    {
        //HttpMaster.SendGetCategoriesCommand(OnGetCategoriesResponse, OnHttpErrorResponse);
    }
    public override async void OnHttpErrorResponse(string message)
    {
        base.OnHttpErrorResponse(message);
        if(message == "Потрібно авторизуватися")
        {
            await Navigate<LoginViewModel>();
        }
    }

    public override void OnHttpSuccessResponse(string message)
    {
        base.OnHttpSuccessResponse("Запис додано");
    }

    public void OnGetCategoriesResponse(string message)
    {
        var records = message.Split("\r\n");
        if (records[0] == "")
        {
            SnackHelper.ShowSnack<ErrorSnack>("Категорій немає");
            return;
        }
        var converter = new CategoryConverter();
        foreach(var record in records)
        {
            Categories.Add(converter.FromLjson(record));
        }
    }

    [ObservableProperty]
    private ObservableCollection<Category> categories = [];

    [ObservableProperty]
    private ObservableCollection<Category> filteredCategories = [];

    [ObservableProperty]
    private string imagePath = "";

    [ObservableProperty]
    private string podcastPath = "";

    [ObservableProperty]
    private string podcastName = "";

    [ObservableProperty]
    private string podcastDescription = "";

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(FilterCategoriesCommand))]
    private string categoryName = "";

    [RelayCommand]
    public void GetCategories()
    {
        var sent = HttpMaster.SendGetCategoriesCommand(OnGetCategoriesResponse, OnHttpErrorResponse);
        OnSentSnack(sent);
    }

    [RelayCommand]
    public void FilterCategories(string startName)
    {
        if (string.IsNullOrEmpty(startName)) return;
        if(startName.Length == 1)
        {
            GetCategories();
        }
        var filter = Categories.Where(x => x.Name.StartsWith(startName));
        FilteredCategories.Clear();
        foreach(var element in filter)
        {
            FilteredCategories.Add(element);
        }
    }

    private bool CheckInput()
    {
        if(string.IsNullOrEmpty(PodcastName))
        {
            SnackHelper.ShowSnack<ErrorSnack>("Введіть назву подкасту");
            return false;
        }
        if (string.IsNullOrEmpty(CategoryName))
        {
            SnackHelper.ShowSnack<ErrorSnack>("Введіть назву категорії");
            return false;
        }
        if (string.IsNullOrEmpty(PodcastPath))
        {
            SnackHelper.ShowSnack<ErrorSnack>("Вкажіть шлях до подкасту");
            return false;
        }
        return true;
    }

    [RelayCommand]
    public void AddPodcast()
    {
        if (!CheckInput()) return;
        var convert = new AddPodcastConverter();
        var ljson = convert.ToLjson(this);
        var progressModel = ServiceHelper.GetService<FileProgressViewModel>();
        var progress = progressModel.AddUploadRecord(PodcastName);
        var sent = HttpMaster.SendPodcastCommand
            (ljson, ImagePath, PodcastPath, OnHttpErrorResponse, OnHttpErrorResponse, progress);
        OnSentSnack(sent);
    }

    [RelayCommand]
    public async Task MoveToSelectPodcast()
    {
        await Navigate<PodcastSelectViewModel>();
    }
    
}
