using System;
using Bank_Library;

namespace Bank_Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> Bank = new Bank<Account>("Беларусь банк");
            bool isOpen = true;

            while (isOpen)
            {
                ConsoleColor baseColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1.To open new wallet.\t2.To withdraw money.\t3.To deposite money.\n4.To close wallet.\t5.To show all accounts.\t6.To exit the programm.");
                Console.WriteLine("\nChose the operation: ");
                Console.ForegroundColor = baseColor;

                try
                {
                    int operation = int.Parse(Console.ReadLine());

                    switch(operation)
                    {
                        case 1:
                            OpenAccount(Bank);
                            break;
                        case 2:
                            Withdraw(Bank);
                            break;
                        case 3:
                            Put(Bank);
                            break;
                        case 4:
                            CloseAccount(Bank);
                            break;
                        case 5:
                            ShowAccounts(Bank);
                            break;
                        case 6:
                            isOpen = false;
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unknown operation!");
                            Console.ForegroundColor = baseColor;
                            break;
                    }

                    Console.WriteLine();
                    Bank.CalculatePercantage();
                }
                catch(Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = baseColor;
                }
            }
        }
        public static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Add sum to deposite: ");
            decimal sum = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Select type of wallet:\t1.Ordinary(Demand) wallet.\t2.Deposite Account.");
            var accType = int.Parse(Console.ReadLine());
            AccountType accountType;

            if (accType == 1)
            {
                accountType = AccountType.Ordinary;
            }
            else accountType = AccountType.Deposit;

            bank.Open(accountType, sum, AddSumHandler, WithdrawSumHandler, CalculationHandler, CloseAccountHandler, OpenAccountHandler);

        }

        public static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Enter sum to withdraw from your wallet: ");
            decimal sum = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter you Id: ");
            int Id = int.Parse(Console.ReadLine());

            bank.Withdraw(sum, Id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Enter sum to deposite you wallet: ");
            decimal sum = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter you Id: ");
            int Id = int.Parse(Console.ReadLine());

            bank.Put(sum, Id);
        }

        public static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Enter account Id to remove: ");
            int id = int.Parse(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Do you seriasle want to delete your wallet.(This action cannot be returned!)\nEnter 'CONFIRM' to confirm: ");
            string confirm = Console.ReadLine().ToUpper();
            if (confirm == "CONFIRM") bank.Close(id);
            else return;
        }

        public static void ShowAccounts(Bank<Account> bank)
        {
            Account[] Accounts = bank.ShowAccounts();
            
            if(Accounts != null)
            {
                foreach (var account in Accounts)
                {
                    Console.WriteLine(account);
                }
            }
            else Console.WriteLine("Bank currently has no accounts.");
            
        }

        //AddSumHandler, WithdrawSumHandler, CalculationHandler, CloseAccountHandler.
        public static void OpenAccountHandler(object sender, AccountEventArgs accountEvent)
        {
            Console.WriteLine(accountEvent.Messages);
        }

        public static void AddSumHandler(object sender, AccountEventArgs accountEvent)
        {
            Console.WriteLine(accountEvent.Messages);
        }

        public static void WithdrawSumHandler(object sender, AccountEventArgs accountEvent)
        {
            Console.WriteLine(accountEvent.Messages);
            if(accountEvent.Sum > 0)
            {
                Console.WriteLine("Successful withdrawed. Let's spend all money!");
            }
        }
        public static void CalculationHandler(object sender, AccountEventArgs accountEvent)
        {
            Console.WriteLine(accountEvent.Messages);
        }
        public static void CloseAccountHandler(object sender, AccountEventArgs accountEvent)
        {
            Console.WriteLine(accountEvent.Messages);
        }

    }
}
