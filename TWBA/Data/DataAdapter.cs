using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeakestBankOfAntarctica.Model;

namespace TheWeakestBankOfAntarctica.Data
{
    public static class DataAdapter
    {
        private static TWBA twba = null;

        public static void Init(TWBA model)
        {
            twba = model;
        }

        static void ConnectToRemoteDB()
        {
            string server = "Bank.db";
            string database = "TWBA";
            string username = "Bob";
            string password = "Banana";

            string connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};";
            try
            {

                Console.WriteLine("Connected to MySQL server!");

                // We arent using the Remote DB here; however, this is a valid code in the App

            }
            catch (Exception ex) { }
        }


        public static Account GetAccountByAccountNumber(string accountNumber)
        {
            Account account = (from a in twba.GetAllAccounts()
                       where a.AccountNumber == accountNumber
                       select a).FirstOrDefault();

            return account; 
        }

        public static List<Account> GetAccountOwners(string customerId)
        {
            List<Account> customerAccounts = (from account in twba.GetAllAccounts()
                                    where account.AccountOwners.Contains(customerId)
                                    select account).ToList();

            return customerAccounts;
        }
        /* CWE-476 NULL Pointer Dereference
        * Patched by : Kaimo Li
        * Description: 1. As GetAccountByAccountNumber may not have a valid account, I have added a null check to ensure that the account is not null before removing it
        *              2. If the deleting accountNumber does not match an account object, I return "Account not found"
        *              3. If the account is found, I remove it from the list of accounts and return a success message.
        */
        public static string CloseAccount(string accountNumber)
        {
            Account account = GetAccountByAccountNumber(accountNumber);
            if (account == null) {
                return "Account not found";
            }
            twba.GetAllAccounts().Remove(account);
            return account.AccountNumber + " has been removed successfully!";
        }
    }
}
