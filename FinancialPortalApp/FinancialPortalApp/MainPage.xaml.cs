using Auth0.OidcClient;
using FinancialPortalApp.Data;
using FinancialPortalApp.Models;
using FinancialPortalApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinancialPortalApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string userId = "7ca42d98-b875-4760-85be-239cc365eed4";
        string userEmail = "mattpark102@gmail.com";
        public MainPage()
        {
            InitializeComponent();
            Title = "Financial Portal";
            InitTableView();
            CreateTransaction.Clicked += CreateTransaction_Clicked;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
            InitTableView();
        }

        private async void CreateTransaction_Clicked(object sender, EventArgs e)
        {
            var createView = new CreateTransaction(userEmail);
            await Navigation.PushAsync(createView);
        }

        private async Task InitTableView()
        {
            User user = await Core.GetUserByEmail(userEmail);
            List<Transaction> transactionsTemp = await Core.GetTransactionsByGroupId(user.GroupId);

            var transactions = transactionsTemp.OrderByDescending(t => t.Created);
            var layout = new Grid();

            layout.Children.Add(new Label()
            {
                Text = "Type",
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20, 0, 0, 0)
            }, 0, 0);

            layout.Children.Add(new Label()
            {
                Text = "Amount",
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 1, 0);

            layout.Children.Add(new Label()
            {
                Text = "Date",
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand
            }, 2, 0);

            var tableSection = new TableSection("Transactions")
            {
                new ViewCell() { View = layout }
            };

            foreach (var transaction in transactions)
            {
                var cellLayout = new Grid();
                cellLayout.HorizontalOptions = LayoutOptions.FillAndExpand;

                cellLayout.Children.Add(new Label()
                {
                    Text = $"{transaction.Type}",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = new Thickness(20, 0, 0, 0)
                }, 0, 0);

                cellLayout.Children.Add(new Label()
                {
                    Text = $"{transaction.Amount}",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    
                }, 1, 0);

                cellLayout.Children.Add(new Label()
                {
                    Text = transaction.Created.ToString("MM/dd/yy"),
                    HorizontalOptions = LayoutOptions.FillAndExpand
                }, 2, 0);

                tableSection.Add(
                    new ViewCell() { View = cellLayout }
                );
            }

            TansactionsTable.Root = new TableRoot()
            {
                tableSection
            };

        }
    }
}
