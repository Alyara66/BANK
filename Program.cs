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
        public string Pass { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Client> clients = LoadClientsFromFile("clients.txt");

            Console.Write("Enter your ID: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            Client client = clients.FirstOrDefault(c => c.ID == id && c.Pass == password);

            if (client == null)
            {
                Console.WriteLine("Invalid ID or password.");
                return;
            }
            Console.WriteLine($"Welcome, {client.FirstName}!");

            while (true)
            {
                Console.Write("Enter amount to deposit: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                client.Amount += amount;

                Console.WriteLine($"Your new balance is: {client.Amount}");
            }
        }
        static List<Client> LoadClientsFromFile(string filename)
        {
            var clients = new List<Client>();

            foreach (string line in File.ReadLines(filename))
            {
                string[] parts = line.Split(',');

                clients.Add(new Client()
                {
                    ID = int.Parse(parts[0]),
                    FirstName = parts[1],
                    LastName = parts[2],
                    Age = int.Parse(parts[3]),
                    IBAN = parts[4],
                    Amount = decimal.Parse(parts[5]),
                    Pass = parts[6]
                });
            }
            return clients;
        }
    }
}
    








        

    

