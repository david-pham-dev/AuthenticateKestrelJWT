﻿using FrontEnd.Views;

namespace FrontEnd
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            MainPage = new NavigationPage(new MainPage());
        }
    }
}
