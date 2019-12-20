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
    public partial class Dashboard : ContentPage
    {
        //string userId = "7ca42d98-b875-4760-85be-239cc365eed4";
        private string UserEmail { get; set; }
        public Dashboard(string email)
        {
            InitializeComponent();
            UserEmail = email;
            Title = "Financial Portal";
            InitTableView();
            CreateTransaction.Clicked += CreateTransaction_Clicked;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //your code here;
            await InitTableView();
        }

        private async void TableRow_Clicked(object sender, EventArgs e)
        {
            ViewCell vc = (ViewCell)sender;
            Transaction transaction = (Transaction)vc.BindingContext;
            await Navigation.PushAsync(new TransactionDetail(transaction));
        }

        private async void CreateTransaction_Clicked(object sender, EventArgs e)
        {
            var createView = new CreateTransaction(UserEmail);
            await Navigation.PushAsync(createView);
        }

        private async Task InitTableView()
        {
            User user = await Core.GetUserByEmail(UserEmail);
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

                var typeLabel = new Label();
                typeLabel.SetBinding(Label.TextProperty, "Type", BindingMode.TwoWay, null, null);
                typeLabel.Padding = new Thickness(20, 0, 0, 0);
                typeLabel.HorizontalOptions = LayoutOptions.FillAndExpand;

                var amountLabel = new Label();
                amountLabel.SetBinding(Label.TextProperty, "Amount", BindingMode.TwoWay, null, null);
                amountLabel.HorizontalOptions = LayoutOptions.FillAndExpand;

                var createdLabel = new Label();
                createdLabel.Text = transaction.Created.ToString("MM/dd/yy");
                createdLabel.HorizontalOptions = LayoutOptions.FillAndExpand;

                cellLayout.Children.Add(typeLabel, 0, 0);

                cellLayout.Children.Add(amountLabel, 1, 0);

                cellLayout.Children.Add(createdLabel, 2, 0);

                var viewCell = new ViewCell() { View = cellLayout };
                viewCell.BindingContext = transaction;
                viewCell.Tapped += TableRow_Clicked;

                tableSection.Add(
                    viewCell
                );
            }
            

            TransactionsTable.Root = new TableRoot()
            {
                tableSection
            };

        }
    }
}