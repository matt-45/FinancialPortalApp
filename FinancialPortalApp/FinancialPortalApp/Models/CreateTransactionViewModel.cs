using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialPortalApp.Models
{
    public class CreateTransactionViewModel
    {
        public List<Budget> Budgets { get; set; }
        public List<BudgetItem> BudgetItems { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
        public Budget SelectedBudget { get; set; }
    }
}
