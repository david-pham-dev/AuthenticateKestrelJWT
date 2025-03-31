using CommunityToolkit.Mvvm.Input;
using FrontEnd.Models;
using FrontEnd.Service;
using Microsoft.Maui.Controls;
using System.Xml;

namespace FrontEnd.Views;

public partial class UserDetail : ContentPage
{	private readonly ApiService	_apiService;
	public UserDetail()
	{
		InitializeComponent();
		_apiService = new ApiService();
		LoadUserDetails();
	}
	private async void LoadUserDetails()
	{
		var user = await _apiService.GetUserDetailAsync();
		if (user != null)
		{
            nameLabel.Text = user.name;
            emailLabel.Text = user.email;
        }
        else
        {
            await DisplayAlert("Error", "Failed to load user details.", "OK");
        }
    }

    private async void LogoutAsync(object sender, EventArgs e)
    {
        UserAccountSession.Instance.ClearSession();
        await Navigation.PushAsync(new MainPage());
    }

}