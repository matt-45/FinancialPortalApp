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
    public partial class CreateTransaction : ContentPage
    {
        private string UserEmail { get; set; }
        private CreateTransactionViewModel viewModel = new CreateTransactionViewModel();
        public CreateTransaction(string userEmail)
        {
            BindingContext = new CreateTransactionViewModel();
            UserEmail = userEmail;
            InitializeComponent();
            GetInformation();
            BudgetPicker.SelectedIndexChanged += SelectBudget;
            Title = "Create Transaction";
        }
        private async void SelectBudget(object sender, EventArgs e)
        {
            
            Picker picker = sender as Picker;
            Budget selectedItem = (Budget)picker.SelectedItem;

            List<BudgetItem> budgetItems = await Core.GetBudgetItemsByBudgetId(selectedItem.Id);
            viewModel.BudgetItems = budgetItems;

            this.ApplyBindings();
        }

        private async Task GetInformation()
        {
            User user = await Core.GetUserByEmail(UserEmail);
            List<Budget> budgets = await Core.GetBudgetsByGroupId(user.GroupId);
            List<BankAccount> bankAccounts = await Core.GetBankAccountsByUserId(user.Id);

            viewModel.Budgets = budgets;
            viewModel.BankAccounts = bankAccounts;

            this.BindingContext = viewModel;
        }


    }
}