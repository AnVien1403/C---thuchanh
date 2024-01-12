using System;
using System.Collections.Generic;

public class BankAccount
{
    private const decimal MinimumBalance = 50000;
    private const decimal WithdrawalFeeRate = 0.00067m;
    public required string Username { get; set; }
    public required string Password { get; set; }
    public decimal Balance { get; set; }

    public bool Login(string enteredUsername, string enteredPassword)
    {
        return Username == enteredUsername && Password == enteredPassword;
    }

    public bool Withdraw(decimal amount)
    {
        if (IsValidTransaction(amount) && amount % 500 == 0)
        {
            decimal totalAmountWithFee = amount + CalculateTransactionFee(amount, TransactionType.Withdraw);
            Balance -= totalAmountWithFee;
            return true;
        }
        return false;
    }

    public bool Deposit(decimal amount)
    {
        if (IsValidTransaction(amount))
        {
            decimal totalAmountWithFee = amount + CalculateTransactionFee(amount, TransactionType.Deposit);
            Balance += totalAmountWithFee;
            return true;
        }
        return false;
    }

    public decimal CheckBalance()
    {
        return Balance;
    }

    private bool IsValidTransaction(decimal amount)
    {
        return amount > 0 && amount <= Balance - MinimumBalance;
    }

    private decimal CalculateTransactionFee(decimal amount, TransactionType transactionType)
    {
        if (transactionType == TransactionType.Withdraw)
        {
            return amount * WithdrawalFeeRate;
        }
        return 0;
    }
}

public enum TransactionType
{
    Withdraw,
    Deposit
}

public class Program
{
    public static void Main()
    {
        // Tạo một danh sách tài khoản ngân hàng để kiểm tra
        List<BankAccount> bankAccounts = new List<BankAccount>
        {
            new BankAccount { Username = "user1", Password = "pass1", Balance = 5000 },
            new BankAccount { Username = "user2", Password = "pass2", Balance = 10000 }
        };

        Console.WriteLine("Enter username:");
#pragma warning disable CS8600
        string enteredUsername = Console.ReadLine();
#pragma warning restore CS8600
        Console.WriteLine("Enter password:");
#pragma warning disable CS8600
        string enteredPassword = Console.ReadLine();
#pragma warning restore CS8600

#pragma warning disable CS8604
        BankAccount userAccount = AuthenticateUser(enteredUsername, enteredPassword, bankAccounts);
#pragma warning restore CS8604

        if (userAccount != null)
        {
            Console.WriteLine("Login successful!");

            while (true)
            {
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Exit");

                Console.Write("Choose an option (1-4): ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string option = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.



                switch (option)
                {
                    case "1":
                        Console.WriteLine($"Current balance: {userAccount.CheckBalance()}");
                        break;
                    case "2":
                        Console.Write("Enter withdrawal amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
                        {
                            if (userAccount.Withdraw(withdrawalAmount))
                            {
                                Console.WriteLine($"Withdrawal of {withdrawalAmount} successful!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid withdrawal amount!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case "3":
                        Console.Write("Enter deposit amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                        {
                            if (userAccount.Deposit(depositAmount))
                            {
                                Console.WriteLine($"Deposit of {depositAmount} successful!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid deposit amount!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Login failed!");
        }
    }

    static BankAccount AuthenticateUser(string enteredUsername, string enteredPassword, List<BankAccount> bankAccounts)
    {
        foreach (var account in bankAccounts)
        {
            if (account.Login(enteredUsername, enteredPassword))
            {
                return account;
            }
        }
#pragma warning disable CS8603
        return null;
#pragma warning restore CS8603
    }
}
