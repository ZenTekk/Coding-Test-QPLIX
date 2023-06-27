using Coding_Test_QPLIX.Models;
using Coding_Test_QPLIX.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Coding_Test_QPLIX
{
    internal sealed class Program
    {
        private static readonly string exePath = Assembly.GetEntryAssembly().Location;
        private static readonly string investmentsFilePath = Path.Combine(exePath, @"..\Data\Investments.csv");
        private static readonly string quotesFilePath = Path.Combine(exePath, @"..\Data\Quotes.csv");
        private static readonly string transactionsFilePath = Path.Combine(exePath, @"..\Data\Transactions.csv");

        static void Main(string[] args)
        {
            Console.WriteLine("Reading Investments...");
            var startTime = DateTime.Now;
            IList<GeneralInvestment> investments = new List<GeneralInvestment>();
            investments = CSVParser.Parse<GeneralInvestment>(investmentsFilePath);
            var duration = DateTime.Now - startTime;
            Console.WriteLine($"Done in {Math.Round(duration.TotalSeconds, 2)} seconds");

            Console.WriteLine("Reading Transactions...");
            startTime = DateTime.Now;
            IList<Transaction> transactions = CSVParser.Parse<Transaction>(transactionsFilePath);
            duration = DateTime.Now - startTime;
            Console.WriteLine($"Done in {Math.Round(duration.TotalSeconds, 2)} seconds");

            Console.WriteLine("Reading Quotes...");
            startTime = DateTime.Now;
            IList<Quote> quotes = CSVParser.Parse<Quote>(quotesFilePath, new CultureInfo("de-DE"));
            duration = DateTime.Now - startTime;
            Console.WriteLine($"Done in {Math.Round(duration.TotalSeconds, 2)} seconds");

            var line = Console.ReadLine();
            while (!string.IsNullOrWhiteSpace(line))
            {
                var input = line.Split(';');
                var date = DateTime.Parse(input[0]);
                var investorId = input[1];

                // Filter out dates
                transactions = transactions.Where(t => DateTime.Compare(t.Date, date) <= 0).ToList();
                quotes = quotes.Where(q => DateTime.Compare(q.Date, date) <= 0).ToList();

                // For each ISIN only the last date is needed -> Turn list of quotes into dictionary for better performance
                IDictionary<string, Quote> mostRecentQuotes =
                    quotes.GroupBy(q => q.ISIN).ToDictionary(
                        gp => gp.Key,
                        gp => gp.OrderBy(q => q.Date).LastOrDefault());


                var investmentMatcher = new InvestmentMatcher(investorId, investments, transactions);
                // Create 4 separate lists by joining investments and transactions
                // 1) customer investments of type Stock
                var stockInvestments = investmentMatcher.GetStockInvestments();
                // 2) customer investments of type RealEstate
                var realEstateInvestments = investmentMatcher.GetRealEstateInvestments();
                // 3) customer investments of type Fonds
                var fondInvestments = investmentMatcher.GetFondInvestments();
                // 4) investments done by the fonds themselves
                var fondInternalInvestments = investmentMatcher.GetFondInternalInvestments();

                // Get the value of the investor's stock investments
                var stockCalculator = new StockCalculator();
                var valueOfStockInvestments = stockCalculator.GetValueOfStockInvestments(stockInvestments, mostRecentQuotes);
                Console.WriteLine($"Value of stock investments: {valueOfStockInvestments}");

                // Get the value of the investor's real estate investments
                var realEstateCalculator = new RealEstateCalculator();
                var valueOfRealEstateInvestments = realEstateCalculator.GetValueOfRealEstateInvestments(realEstateInvestments);
                Console.WriteLine($"Value of real estate investments: {valueOfRealEstateInvestments}");

                // Get the value of the investor's fond investments (which internally are stock or real estate investments)
                var fondCalculator = new FondCalculator(stockCalculator, realEstateCalculator);
                var valueOfFondsInvestments = fondCalculator.GetValueOfFondInvestments(fondInvestments, fondInternalInvestments, mostRecentQuotes);
                Console.WriteLine($"Value of fonds investments: {valueOfFondsInvestments}");

                var totalValueOfInvestments = Math.Round(valueOfStockInvestments + valueOfRealEstateInvestments + valueOfFondsInvestments, 2);
                Console.WriteLine($"Total value across all investments: {totalValueOfInvestments}");

                line = Console.ReadLine();
            }

        }



    }
}
