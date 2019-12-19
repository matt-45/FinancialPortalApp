﻿using FinancialPortalApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinancialPortalApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CreateTransaction());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}