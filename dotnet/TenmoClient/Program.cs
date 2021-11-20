using System;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient
{
    class Program
    {
        private static readonly ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();
        private static readonly ApiService apiService = new ApiService("https://localhost:44315/");

        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            while (true)
            {
                int loginRegister = -1;
                while (loginRegister != 1 && loginRegister != 2)
                {
                    Console.WriteLine("Welcome to TEnmo!");
                    Console.WriteLine("1: Login");
                    Console.WriteLine("2: Register");
                    Console.Write("Please choose an option: ");

                    if (!int.TryParse(Console.ReadLine(), out loginRegister))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }
                    else if (loginRegister == 1)
                    {
                        while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                        {
                            LoginUser loginUser = consoleService.PromptForLogin();
                            ApiUser user = authService.Login(loginUser);
                            if (user != null)
                            {
                                UserService.SetLogin(user);
                            }
                        }
                    }
                    else if (loginRegister == 2)
                    {
                        bool isRegistered = false;
                        while (!isRegistered) //will keep looping until user is registered
                        {
                            LoginUser registerUser = consoleService.PromptForLogin();
                            isRegistered = authService.Register(registerUser);
                            if (isRegistered)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Registration successful. You can now log in.");
                                loginRegister = -1; //reset outer loop to allow choice for login
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }

                MenuSelection();
            }
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: View your pending requests");
                Console.WriteLine("4: Send TE bucks");
                Console.WriteLine("5: Request TE bucks");
                Console.WriteLine("6: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                }
                else if (menuSelection == 1)
                {
                    int userId = UserService.GetUserId();
                    decimal balance = apiService.GetBalance(userId);
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine($"Your current balance is: ${balance}");
                    Console.WriteLine("----------------------------------");
                }
                else if (menuSelection == 2)
                {
                    int userId = UserService.GetUserId();
                    List<Transfer> transfers = apiService.TransferLookupUserId(userId);
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("My Transfers");
                    Console.WriteLine("----------------------------------");
                    foreach (Transfer transfer in transfers)
                    {
                        Console.WriteLine($"Transfer ID: {transfer.TransferId}");
                        Console.WriteLine($"Description: {transfer.TransferTypeDesc}");
                        Console.WriteLine($"Amount: ${transfer.Amount}");
                        Console.WriteLine("----------------------------------\n");
                    }
                    Console.Write("Enter Transfer ID for more details: ");
                    int transferIdSelection = int.Parse(Console.ReadLine());
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Transfer Details");
                    Console.WriteLine("----------------------------------");

                    Transfer transferDetails = apiService.GetTransferById(transferIdSelection);
                    Console.WriteLine($"Transfer ID: {transferDetails.TransferId}");
                    Console.WriteLine($"Description: {transferDetails.TransferTypeDesc}");
                    Console.WriteLine($"From: {transferDetails.AccountFrom}");
                    Console.WriteLine($"To: {transferDetails.AccountTo}");
                    Console.WriteLine($"Amount: ${transferDetails.Amount}");
                    Console.WriteLine("----------------------------------");
                }
                else if (menuSelection == 3)
                {
                    int userId = UserService.GetUserId();
                    List<Transfer> transfers = apiService.PendingTransferRequests(userId);
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Pending Transfers");
                    Console.WriteLine("----------------------------------");
                    foreach (Transfer transfer in transfers)
                    {
                        Console.WriteLine($"Transfer ID: {transfer.TransferId}");
                        Console.WriteLine($"Description: {transfer.TransferTypeDesc}");
                        Console.WriteLine($"From: {transfer.AccountFrom}");
                        Console.WriteLine($"To: {transfer.AccountTo}");
                        Console.WriteLine($"Amount: ${transfer.Amount}");
                        Console.WriteLine("----------------------------------");
                    }
                }
                else if (menuSelection == 4)
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("My Friends");
                    Console.WriteLine("----------------------------------");
                    
                    List<User> allUsers = apiService.GetAllUsers();
                    foreach (User user in allUsers)
                    {
                        Console.WriteLine($"ID: {user.UserId} | Name: {user.Username}");
                    }
                    
                    Console.Write("\nSelect a user ID account number to send TE bucks to: ");
                    int actTo = int.Parse(Console.ReadLine());
                    Console.Write("\nSelect the amount of money to send: ");
                    decimal amountToSend = decimal.Parse(Console.ReadLine());

                    int userId = UserService.GetUserId();
                    decimal myBalance = apiService.GetBalance(userId);
                    decimal toBalance = apiService.GetBalance(actTo);

                    if (amountToSend > myBalance)
                    {
                        Console.WriteLine("\nYou do not have enough funds to complete the process.");
                    }
                    else
                    {
                        Account accountFrom = apiService.GetAccount(userId);
                        int accountFromNumber = accountFrom.AccountId;
                        Account accountTo = apiService.GetAccount(actTo);
                        int accountToNumber = accountTo.AccountId;
                        
                        Transfer transferToAdd = new Transfer(2, 2, accountToNumber, accountFromNumber, amountToSend);
                        apiService.WriteTransferToDB(transferToAdd);

                        Account updateToAcct = new Account()
                        {
                            AccountId = accountToNumber,
                            UserId = actTo,
                            Balance = (toBalance + amountToSend),
                        };

                        Account updateFromAcct = new Account()
                        {
                            AccountId = accountFromNumber,
                            UserId = userId,
                            Balance = (myBalance - amountToSend),
                        };

                        apiService.UpdateAccount(updateToAcct);
                        apiService.UpdateAccount(updateFromAcct);

                        Console.WriteLine($"\nYour updated balance is: ${updateFromAcct.Balance}\n");
                    }
                }
                else if (menuSelection == 5)
                {


                    List<User> u = apiService.GetAllUsers();
                    Console.WriteLine("-----------------");
                    Console.WriteLine("My Friends");
                    Console.WriteLine("-----------------");
                    foreach (User user in u)
                    {
                        Console.WriteLine($"{user.Username}: {user.UserId}");
                    }

                    //Transfer transfer = apiService.CreateNewRequestObject();
                    //apiService.CreateTransfer(transfer);

                }
                else if (menuSelection == 6)
                {
                    Console.WriteLine("");
                    UserService.SetLogin(new ApiUser()); //wipe out previous login info
                    Console.Clear();
                    menuSelection = 0;
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }
            }
        }

    }
}
