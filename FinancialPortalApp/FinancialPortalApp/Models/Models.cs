using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Web;

namespace FinancialPortalApp.Models
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int GroupId { get; set; }
        public decimal IncomeAmount { get; set; }
        
        public IncomeType IncomeType { get; set; }
        
        public string Email { get; set; }

    }
    
    public enum IncomeType
    {
        Hourly,
        Daily,
        Weekly,
        BiWeekly,
        SemiMonthly,
        Monthly,
        Annually
    }
    
    public class Budget
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Spent { get; set; }
        
        public decimal Target { get; set; }
        
        public int GroupId { get; set; }
    }
    
    public class BudgetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Spent { get; set; }
        public decimal Target { get; set; }
        public int BudgetId { get; set; }
        public double Percentage
        {
            get
            {
                var target = Decimal.ToDouble(Target);
                var spent = Decimal.ToDouble(Spent);

                if (Target == 0)
                {
                    return 0;
                }
                else
                {
                    return Math.Round(spent / target * 100);
                }
            }
        }
    }
    public class Group
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Balance { get; set; }
        
        public decimal StartAmount { get; set; }
       
        public decimal Spent
        {
            get
            {
                return StartAmount - Balance;
            }
        }
    }
    /// <summary>
    /// Transaction Model
    /// </summary>
    public class Transaction
    {
        
        public int Id { get; set; }
        
        public decimal Amount { get; set; }
        
        public string Memo { get; set; }
        
        public DateTime Created { get; set; }
        
        public TransactionType Type { get; set; }
        
        public string CreatorId { get; set; }
        
        public int GroupId { get; set; }
        
        public int? BudgetId { get; set; }
        
        public int? BudgetItemId { get; set; }
        
        public int BankAccountId { get; set; }
    }
    
    public enum TransactionType
    {
        
        Deposit,
        
        Withdrawal
    }
    
    public class BankAccount
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
        
        public string UserId { get; set; }
    }
    
    public enum AccountType
    {
        
        Checking,
        
        Savings
    }
}