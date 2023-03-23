using System;
using System.Collections.Generic;

namespace InstallmentPaymentProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter product (Smartphone/Computer/TV): ");
                string product = Console.ReadLine();

                Console.WriteLine("Enter amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter phone number: ");
                string phoneNumber = Console.ReadLine();

                Console.WriteLine("Enter installment range (in months): ");
                int rangeMonths = int.Parse(Console.ReadLine());

                // Create an instance of the Purchase class and use it to calculate the total payment amount
                Purchase purchase = new Purchase(product, amount, phoneNumber, rangeMonths);
                decimal totalAmount = purchase.CalculateInstallmentPayment();

                Console.WriteLine($"Total amount: {totalAmount} somoni");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }
    }

    class Purchase
    {
        // Define properties for the Purchase class
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public string PhoneNumber { get; set; }
        public int RangeMonths { get; set; }

        // Constructor for the Purchase class
        public Purchase(string product, decimal amount, string phoneNumber, int rangeMonths)
        {
            Product = product;
            Amount = amount;
            PhoneNumber = phoneNumber;
            RangeMonths = rangeMonths;
        }

        // Calculate the total payment amount for the purchase
        public decimal CalculateInstallmentPayment()
        {
            Dictionary<string, int[]> installmentRange = new Dictionary<string, int[]>
            {
                { "Smartphone", new int[] { 3, 9 } },
                { "Computer", new int[] { 3, 12 } },
                { "TV", new int[] { 3, 18 } }
            };

            if (!installmentRange.ContainsKey(Product))
            {
                throw new ArgumentException("Invalid product");
            }

            int[] range = installmentRange[Product];
            if (RangeMonths < range[0] || RangeMonths > range[1])
            {
                throw new ArgumentException($"Invalid installment range for {Product}");
            }

            Dictionary<string, decimal> additionalPercent = new Dictionary<string, decimal>
            {
                { "Smartphone", 0.03m },
                { "Computer", 0.04m },
                { "TV", 0.05m }
            };

            decimal additionalPercentage = additionalPercent[Product] * (RangeMonths - range[0] + 1);
            decimal totalAmount = Amount * (1 + additionalPercentage);

            string message = $"You have purchased a {Product} for {Amount} somoni with an installment plan for {RangeMonths} months. The total amount is {totalAmount} somoni.";
            SendSms(PhoneNumber, message);

            return totalAmount;
        }

        static void SendSms(string phoneNumber, string message)
        {
            // Implement this function using an appropriate SMS API or library
            Console.WriteLine($"SMS sent to {phoneNumber}: {message}");
        }
    }
}
