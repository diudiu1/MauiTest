using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Configs;
using MauiApp3.Services.AccountServices;
using MauiApp3.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp3.ViewModels
{
    public partial class MyIndexPageViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        public MyIndexPageViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [ObservableProperty]
        public string id;
        [ObservableProperty]
        public string userName;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string avatarUrl;
        [ObservableProperty]
        public bool isRunning = false;
        [RelayCommand]
        async void GetMyInfo()
        {
            var resp = await _accountService.MyInfoAsync();
            if (resp != null)
            {
                avatarUrl = Appsettings.BaseAddress + resp.AvatarUrl;
                name = resp.Name;
                userName=resp.UserName;
                id=resp.Id;
                this.OnPropertyChanged("AvatarUrl");
                this.OnPropertyChanged("Name");
                this.OnPropertyChanged("UserName");
                this.OnPropertyChanged("Id");
            }
        }
        [RelayCommand]
        async Task Clear()
        {
            await _accountService.ClearAsync();
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        [RelayCommand]
        async Task Test()
        {
            await _accountService.TestAsync();
            
        }
        [RelayCommand]
        async Task CheckFile()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo != null)
                {
                    this.SetProperty(ref isRunning, true, "IsRunning");

                    using Stream sourceStream = await photo.OpenReadAsync();
                    var resp = await _accountService.UpdateAvatarAsync(sourceStream, photo.FileName);

                    await Task.Delay(1000);
                    this.SetProperty(ref avatarUrl, Appsettings.BaseAddress + resp.AvatarUrl, "AvatarUrl");
                    this.SetProperty(ref isRunning, false, "IsRunning");

                }
            }
        }
    }
}
