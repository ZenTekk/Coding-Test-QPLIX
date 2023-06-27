using Coding_Test_QPLIX.Models;
using Coding_Test_QPLIX.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding_Test_QPLIX
{
    internal sealed class FondCalculator : IFondCalculator
    {
        private readonly IStockCalculator _stockCalculator;
        private readonly IRealEstateCalculator _realEstateCalculator;

        internal FondCalculator(IStockCalculator stockCalculator, IRealEstateCalculator realEstateCalculator)
        {
            _stockCalculator = stockCalculator ?? throw new ArgumentNullException(nameof(stockCalculator));
            _realEstateCalculator = realEstateCalculator ?? throw new ArgumentNullException(nameof(realEstateCalculator));
        }

        public double GetValueOfFondInvestments(IList<FondInvestment> fondInvestments, IList<FondInternalInvestment> fondInternalInvestments, IDictionary<string, Quote> quotes)
        {
            double valueOfAllFondInvestments = 0;
            var fondGroupings = fondInvestments.GroupBy(i => i.FondId).ToList();
            foreach (var fondGrouping in fondGroupings)
            {
                string fondId = fondGrouping.Key;
                var singleFondInternalInvestments = fondInternalInvestments.Where(i => i.InvestorId.Equals(fondId)).ToList();
                double portionOfFond = fondGrouping.Sum(i => i.Value) * 0.01;
                double wholeFondValue = GetValueOfSingleFond(singleFondInternalInvestments, quotes);
                double investorsFondValue = portionOfFond * wholeFondValue;

                valueOfAllFondInvestments += investorsFondValue;
            }
            return Math.Round(valueOfAllFondInvestments, 2);
        }

        private double GetValueOfSingleFond(IList<FondInternalInvestment> fondInternalInvestments, IDictionary<string, Quote> quotes)
        {
            var stockInvestments = fondInternalInvestments.Where(i => i.IsStockInvestment).Select(i => i.ToStockInvestment()).ToList();
            var realEstateInvestments = fondInternalInvestments.Where(i => i.IsRealEstateInvestment).Select(i => i.ToRealEstateInvestment()).ToList();

            var valueOfStockInvestments = _stockCalculator.GetValueOfStockInvestments(stockInvestments, quotes);
            var valueOfRealEstateInvestments = _realEstateCalculator.GetValueOfRealEstateInvestments(realEstateInvestments);

            return valueOfStockInvestments + valueOfRealEstateInvestments;
        }
    }
}