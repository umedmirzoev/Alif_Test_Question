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

                decimal totalAmount = CalculateInstallmentPayment(product, amount, phoneNumber, rangeMonths);
                Console.WriteLine($"Total amount: {totalAmount} somoni");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadLine();
        }

        static decimal CalculateInstallmentPayment(string product, decimal amount, string phoneNumber, int rangeMonths)
        {
            Dictionary<string, int[]> installmentRange = new Dictionary<string, int[]>
            {
                { "Smartphone", new int[] { 3, 9 } },
                { "Computer", new int[] { 3, 12 } },
                { "TV", new int[] { 3, 18 } }
            };

            if (!installmentRange.ContainsKey(product))
            {
                throw new ArgumentException("Invalid product");
            }

            int[] range = installmentRange[product];
            if (rangeMonths < range[0] || rangeMonths > range[1])
            {
                throw new ArgumentException($"Invalid installment range for {product}");
            }

            Dictionary<string, decimal> additionalPercent = new Dictionary<string, decimal>
            {
                { "Smartphone", 0.03M },
                { "Computer", 0.04M },
                { "TV", 0.05M }
            };

            decimal additionalPercentage = additionalPercent[product] * (rangeMonths - range[0] + 1);
            decimal totalAmount = amount * (1 + additionalPercentage);

            string message = $"You have purchased a {product} for {amount} somoni with an installment plan for {rangeMonths} months. The total amount is {totalAmount} somoni.";
            SendSms(phoneNumber, message);

            return totalAmount;
        }

        static void SendSms(string phoneNumber, string message)
        {
            // Implement this function using an appropriate SMS API or library
            Console.WriteLine($"SMS sent to {phoneNumber}: {message}");
        }
    }
}
