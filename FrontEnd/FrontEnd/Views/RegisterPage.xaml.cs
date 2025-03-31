using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using FrontEnd.Service;

namespace FrontEnd.Views;

public partial class RegisterPage : ContentPage
{	
	
	private ApiService _apiService;
	private RegisterPageViewModel _viewModel;
	public RegisterPage()
    {
		InitializeComponent();
        BindingContext = new PasswordViewModel();
        _apiService = new ApiService();
    }
    private void OnLabelTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SignInPage());
    }
    private async void OnClickRegister(object sender, EventArgs e)
	{
		var userRegisterDetail = new RegisterPageViewModel
		{
			Name = NameEntry.Text,
			Email = EmailEntry.Text,
			Password = PasswordEntry.Text,
		};
        bool isRegistered = await _apiService.Register(userRegisterDetail);
        if (isRegistered)
        {
           await DisplayAlert("Success", "User registered successfully!", "OK");
            ClearFields();
          await  Navigation.PushAsync(new SignInPage());
        }
        else
        {
            await DisplayAlert("Error", "Registration failed.", "OK");
        }

    }
    private void ClearFields()
    {
        NameEntry.Text = null;
        EmailEntry.Text = null;
        PasswordEntry.Text = null;
    }
}
