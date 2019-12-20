using FinancialPortalApp.Data;
using FinancialPortalApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FinancialPortalApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            InitializeComponent();
            Title = "Sign In";
            SignInButton.Clicked += SignInButton_Clicked;
        }

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            var email = EmailEditor.Text;
            User user = await Core.GetUserByEmail(email);
            if (user != null)
            {
                ErrorHeader.Text = "Email";
                Navigation.PushAsync(new Dashboard(email));
            } 
            else
            {
                ErrorHeader.Text = "Email does not exist.";
            }
        }

    }
}