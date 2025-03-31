
using FrontEnd.Models;
using FrontEnd.Models.ViewModels;
using FrontEnd.Service;
using System.Runtime.CompilerServices;

namespace FrontEnd.Views;

public partial class SignInPage : ContentPage
{	private readonly ApiService _apiService;
	public SignInPage()
	{
		InitializeComponent();
		_apiService = new ApiService();
	}
	private async void OnLoginClicked(object sender, EventArgs e)
	{
		string email = EmailEntry.Text;
		string password = PasswordEntry.Text;
		UserAccountSession? userAccountsession = await _apiService.LoginAsync(email, password);
		string? token = userAccountsession?.accessToken;
		string? loggedInEmail = userAccountsession?.email;
		if(token != null)
		{
			Preferences.Set("auth_token", token);
			var authToken = Preferences.Get("auth_token", string.Empty);
			UserAccountSession.Instance.SetSession(token, loggedInEmail);
            await DisplayAlert("Success", authToken, "Ok");
			await Navigation.PushAsync(new UserDetail()); // change to userDetail Page later
		}
		else
		{
			await DisplayAlert("Error", "Invalid Credentials", "Ok");
		}

	}
    private void OnLabelTapped(object sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }
}