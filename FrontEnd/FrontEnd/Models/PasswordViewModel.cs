using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace FrontEnd.Models
{   
    public partial class PasswordViewModel : ObservableObject
    {
        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string passwordToggleIcon ="\uf06e";

        [ObservableProperty]
        private bool isPasswordHidden = true;
        public PasswordViewModel()
        {
            IsPasswordHidden = true;
        }
        [RelayCommand]
        void TogglePassword()
        {
            IsPasswordHidden = !IsPasswordHidden;
            PasswordToggleIcon = IsPasswordHidden ? "\uf06e" : "\uf070";
        }
    }
}
