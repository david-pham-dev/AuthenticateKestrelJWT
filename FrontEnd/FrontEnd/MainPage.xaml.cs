using CommunityToolkit.Mvvm.Input;
using FrontEnd.Views;
using FrontEnd.Service;

namespace FrontEnd
{
    public partial class MainPage : ContentPage
    {   
        public MainPage()
        {
            InitializeComponent();
        }
        private void registerButton_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
        private void signInButton_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInPage());
        }
    }   

}
