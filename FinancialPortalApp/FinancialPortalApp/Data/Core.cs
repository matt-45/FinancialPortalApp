using FinancialPortalApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialPortalApp.Data
{
    public class Core
    {
        // User
        public static async Task<dynamic> GetUserByEmail(string email)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/GetUserByEmail?Email={email}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                User user = new User();
                user = JsonConvert.DeserializeObject<User>(results);

                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return user;
            }
            else
            {
                return null;
            }
        }

        // Transactions
        public static void CreateTransaction(decimal Amount, string Memo, TransactionType Type, string CreatorId, int GroupId, int BudgetId, int BudgetItemId, int BankAccountId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Transactions/AddTransaction?Amount={Amount}&Memo={Memo}&Type={Type}&CreatorId={CreatorId}&GroupId={GroupId}&BudgetId={BudgetId}&BudgetItemId={BudgetId}&BankAccountId={BankAccountId}";
            DataService.PostDataServiceAsync(queryString);
        }
        public static void CalculateTransaction(int TransactionId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Transactions/CalculateTransaction?Id={TransactionId}";
            DataService.PutDataServiceAsync(queryString);
        }
        public static async Task<dynamic> GetTransactionsByGroupId(int Id)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Transactions/GetTransactionsByGroup?GroupId={Id}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                List<Transaction> transactions = new List<Transaction>();
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(results);
                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return transactions;
            }
            else
            {
                return null;
            }
        }
        public static async Task<dynamic> GetTransactionsByBankAccountId(int Id)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Transactions/GetTransactionsByBank?BankId={Id}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                List<Transaction> transactions = new List<Transaction>();
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(results);

                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return transactions;
            }
            else
            {
                return null;
            }
        }
        public static async Task<dynamic> GetTransactionsByUserId(string UserId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Transactions/GetTransactionsByUser?UserId={UserId}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                List<Transaction> Transactions = new List<Transaction>();
                Transactions = JsonConvert.DeserializeObject<List<Transaction>>(results);

                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return Transactions;
            }
            else
            {
                return null;
            }

        }
        public static void DeleteTransaction(int GroupId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Transactions/DeleteTransaction?Id={GroupId}";
            DataService.DeleteDataServiceAsync(queryString);
        }

        // Group
        public static void CreateGroup(string Name)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Groups/AddGroup?Name={Name}";
            DataService.PostDataServiceAsync(queryString);
        }
        public static void EditGroup(int GroupId, string Name, decimal Balance, decimal StartAmount)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Groups/EditGroup?Id={GroupId}&Name={Name}&Balance={Balance}&StartAmount={StartAmount}";
            DataService.PutDataServiceAsync(queryString);
        }
        public static async Task<dynamic> GetGroupById(int Id)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/Groups/GetGroupDetails?Id={Id}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                Group Group = new Group();
                Group = JsonConvert.DeserializeObject<Group>(results);

                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return Group;
            }
            else
            {
                return null;
            }
        }

        // Budgets
        public static void CreateBudget(string Name, int GroupId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/AddBudget?Name={Name}&GroupId={GroupId}";
            DataService.PostDataServiceAsync(queryString);
        }
        public static void EditBudget(int BudgetId, string Name, decimal Spent, decimal Target)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/EditBudget?Id={BudgetId}&Name={Name}&Spent={Spent}&Target={Target}";
            DataService.PutDataServiceAsync(queryString);
        }
        public static async Task<dynamic> GetBudgetsByGroupId(int Id)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/GetBudgetsByGroup?GroupId={Id}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                List<Budget> budgets = new List<Budget>();
                budgets = JsonConvert.DeserializeObject<List<Budget>>(results);

                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return budgets;
            }
            else
            {
                return null;
            }
        }
        public static void DeleteBudget(int BudgetId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/DeleteBudget?Id={BudgetId}";
            DataService.DeleteDataServiceAsync(queryString);
        }

        // Budget Items
        public static void CreateBudgetItem(string Name, int BudgetId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BudgetItems/AddBudgetItem?Name={Name}&BudgetId={BudgetId}";
            DataService.PostDataServiceAsync(queryString);
        }
        public static void EditBudgetItem(int BudgetItemId, string Name, decimal Spent, decimal Target)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BudgetItems/EditBudgetItem?Id={BudgetItemId}&Name={Name}&Spent={Spent}&Target={Target}";
            DataService.PutDataServiceAsync(queryString);
        }
        public static async Task<dynamic> GetBudgetItemsByBudgetId(int Id)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BudgetItems/GetBudgetItemsByBudget?BudgetId={Id}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                List<BudgetItem> budgetItems = new List<BudgetItem>();
                budgetItems = JsonConvert.DeserializeObject<List<BudgetItem>>(results);

                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return budgetItems;
            }
            else
            {
                return null;
            }
        }
        public static void DeleteBudgetItem(int BudgetItemId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BudgetItems/DeleteBudgetItem?Id={BudgetItemId}";
            DataService.DeleteDataServiceAsync(queryString);
        }

        // Bank Accounts
        public static void CreateBankAccount(string Name, decimal Balance, AccountType Type, string UserId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BankAccounts/AddAccount?Name={Name}&Balance={Balance}&Type={Type}&UserId={UserId}";
            DataService.PostDataServiceAsync(queryString);
        }
        public static void EditBankAccount(int BankAccountId, string UserId, string Name, decimal Balance, AccountType Type)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BankAccounts/EditBankAccount?Id={BankAccountId}&UserId={UserId}&Name={Name}&Balance={Balance}&Type={Type}";
            DataService.PutDataServiceAsync(queryString);
        }
        public static async Task<dynamic> GetBankAccountsByUserId(int Id)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BankAccounts/GetBankAccountsByUser?UserId={Id}";
            dynamic results = await DataService.GetDataFromServiceAsync(queryString).ConfigureAwait(false);

            if (results != null)
            {
                List<BankAccount> bankAccounts = new List<BankAccount>();
                bankAccounts = JsonConvert.DeserializeObject<List<BankAccount>>(results);

                //OpenWeather openWeather = new OpenWeather();
                //openWeather = JsonConvert.DeserializeObject<OpenWeather>(results);

                return bankAccounts;
            }
            else
            {
                return null;
            }
        }
        public static void DeleteBankAccount(int BankAccountId)
        {
            string queryString = $"https://financialwebapi.azurewebsites.net/api/BankAccounts/DeleteBankAccount?Id={BankAccountId}";
            DataService.DeleteDataServiceAsync(queryString);
        }
    }
}
