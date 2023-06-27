using Coding_Test_QPLIX.Models;
using Coding_Test_QPLIX.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding_Test_QPLIX
{
    internal sealed class StockCalculator : IStockCalculator
    {
        public double GetValueOfStockInvestments(IList<StockInvestment> stockInvestments, IDictionary<string, Quote> quotes)
        {
            double valueOfAllStockInvestments = 0;
            var stockInvestmentGroupings = stockInvestments.GroupBy(i => i.ISIN).ToList();

            foreach (var stockInvestmentGrouping in stockInvestmentGroupings)
            {
                string isin = stockInvestmentGrouping.Key;
                if (!quotes.ContainsKey(isin)) 
                {
                    Console.WriteLine($"No entry found for {isin} up to the requested date. -> Skipped");
                    continue; 
                }

                double countOfShares = stockInvestmentGrouping.Sum(si => si.Value);
                double mostRecentShareValue = quotes[isin].PricePerShare;
                double valueOfAllInvestmentsInThisShare = countOfShares * mostRecentShareValue;
                valueOfAllStockInvestments += valueOfAllInvestmentsInThisShare;
            }

            return Math.Round(valueOfAllStockInvestments, 2);
        }
    }
}