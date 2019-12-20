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
            
            InitializeComponent();
            BindingContext = new CreateTransactionViewModel();
            UserEmail = userEmail;
            GetInformation();
            BudgetPicker.SelectedIndexChanged += SelectBudget;
            CreateNewTransaction.Clicked += CreateTransaction_Clicked;
            Title = "Create Transaction";
        }

        public async void CreateTransaction_Clicked(object sender, EventArgs e)
        {
            User user = await Core.GetUserByEmail(UserEmail);
            var budget = (Budget)BudgetPicker.SelectedItem;
            var budgetItem = (BudgetItem)ItemPicker.SelectedItem;
            var bankAccount = (BankAccount)BankPicker.SelectedItem;
            var amount = Convert.ToDecimal(TransactionAmount.Text);
            var memo = TransactionMemo.Text;

            Core.CreateTransaction(amount, memo, TransactionType.Withdrawal, user.Id, user.GroupId, budget.Id, budgetItem.Id, bankAccount.Id);

            Navigation.PopAsync();
        }

        private async void SelectBudget(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Budget selectedItem = (Budget)picker.SelectedItem;

            List<BudgetItem> budgetItems = await Core.GetBudgetItemsByBudgetId(selectedItem.Id);
            //viewModel.BudgetItems = budgetItems;
            ItemPicker.ItemsSource = budgetItems;
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