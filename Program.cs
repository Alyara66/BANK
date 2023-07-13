using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bank
{
    public class Client
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string IBAN { get; set; }
        public decimal Amount { get; set; }
        public string Password { get; set; }
    }

    class Program
    {
        static List<Client> clients;
        static Client currentClient;

        static void Main(string[] args)
        {
            LoadClientsFromFile();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Welcome to the Banking System");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1. Log in");
                Console.WriteLine("2. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        LogIn();
                        break;
                    case "2":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }
        static void LogIn()
        {
            Console.Write("Enter your ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            foreach (Client client in clients)
            {
                if (client.ID == id && client.Password == password)
                {
                    currentClient = client;
                    ShowMainMenu();
                    return;
                }
            }

            Console.WriteLine("Invalid ID or password. Please try again.");

        }
        static void ShowMainMenu()
        {
            bool logout = false;
            while (!logout)
            {
                Console.WriteLine();
                Console.WriteLine("Main Menu");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("1. Deposit money");
                Console.WriteLine("2. Withdraw money");
                Console.WriteLine("3. Transfer money");
                Console.WriteLine("4. Change password");
                Console.WriteLine("5. Log out");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DepositMoney();
                        break;
                    case "2":
                        WithdrawMoney();
                        break;
                    case "3":
                        TransferMoney();
                        break;
                    case "4":
                        ChangePassword();
                        break;
                    case "5":
                        logout = true;
                        currentClient = null;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DepositMoney()
        {
            Console.Write("Enter amount to deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());
            currentClient.Amount += amount;
            UpdateClientFile(currentClient);

            Console.WriteLine($"Your new balance is: {currentClient.Amount}");
        }
        static void WithdrawMoney()
        {
            Console.Write("Enter the amount to withdraw: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            if (currentClient.Amount >= amount)
            {
                currentClient.Amount -= amount;
              
                Console.WriteLine($"Current balance: {currentClient.Amount}");
            }
            else
            {
                Console.WriteLine("Insufficient funds. Cannot withdraw.");
            }
        }
        static void TransferMoney()
        {
            Console.Write("Enter the recipient's IBAN: ");
            string recipientIBAN = Console.ReadLine();
            Console.Write("Enter the amount to transfer: ");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            Customer recipient= FindCustomerByIBAN(recipientIBAN);
            if (recipient != null)
            {
                if (currentCustomer.Amount>=amount)
                {
                    currentCustomer.Amount -= amount;
                    recipient.Amount += amount;
                    UpdateCustomerFile(currentCustomer);
                    UpdateCustomerFile(recipient);
                    Console.WriteLine("Transfer succesful. Current balance: {0}", currentCustomer.Amount);
                }
                else
                {
                    Console.WriteLine("Insufficient funds. Cannot transfer.");
                }
            }
             else
            {
                Console.WriteLine("Recipient with the specified IBAN not found.");
            }
        }
        static void ChangePassword()
        {
            Console.Write("Enter your new password: ");
            string newPassword = Console.ReadLine();

            currentClient.Password = newPassword;
           UpdateClientFile(currentClient)

            Console.WriteLine("Password changed successfully.");
        }

        static void LoadClientsFromFile()
        {
            clients = new List<Client>();

            using (StreamReader reader = new StreamReader("clients.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] clientData = line.Split(',');

                    Client client = new Client()
                    {
                        ID = int.Parse(clientData[0]),
                        FirstName = clientData[1],
                        LastName = clientData[2],
                        Age = int.Parse(clientData[3]),
                        IBAN = clientData[4],
                        Amount = decimal.Parse(clientData[5]),
                        Password = clientData[6]
                    };

                    clients.Add(client);
                }
            }
        }
        static void UpdateClientFile(Client client)
        {
            string tempFile = Path.GetTempFileName();

            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                foreach (Client c in clients)
                {
                    if (c.ID == client.ID)
                    {
                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
                            c.ID, c.FirstName, c.LastName, c.Age, c.IBAN, c.Amount, client.Password);
                    }
                    else
                    {
                        writer.WriteLine("{0},{1},{2},{3},{4},{5},{6}",
                            c.ID, c.FirstName, c.LastName, c.Age, c.IBAN, c.Amount, c.Password);
                    }
                }
            }

            File.Delete("clients.txt");
            File.Move(tempFile, "clients.txt");
        }

        static Client FindClientByIBAN(string iban)
        {
            foreach (Client client in clients)
            {
                if(client.IBAN == iban)
                {
                    return client;
                }
            }
        }
        return null;
    }

   
    















