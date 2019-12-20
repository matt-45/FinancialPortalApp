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
    public partial class TransactionDetail : ContentPage
    {
        private Transaction transaction { get; set; }
        public TransactionDetail(Transaction trans)
        {
            InitializeComponent();
            transaction = trans;
            InitValues();
        }
        private async void InitValues()
        {
            Budget budget = await Core.GetBudgetById((int)transaction.BudgetId);
            BudgetItem budgetItem = await Core.GetBudgetItemById((int)transaction.BudgetItemId);
            BankAccount bankAccount = await Core.GetBankAccountById(transaction.BankAccountId);

            BudgetPicker.SelectedItem = budget.Name;
            ItemPicker.SelectedItem = budgetItem.Name;
            BankPicker.SelectedItem = bankAccount.Name;
            TransactionAmount.Text = $"{transaction.Amount}";
            TransactionMemo.Text = $"{transaction.Memo}";
        }
    }
}