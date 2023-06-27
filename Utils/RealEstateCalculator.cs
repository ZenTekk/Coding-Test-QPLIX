using Coding_Test_QPLIX.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding_Test_QPLIX
{
    internal sealed class RealEstateCalculator : IRealEstateCalculator
    {
        public double GetValueOfRealEstateInvestments(IList<RealEstateInvestment> realEstateInvestments)
        {
            double valueOfAllRealEstateInvestments = 0;
            var realEstateGroupings = realEstateInvestments.GroupBy(i => i.InvestmentId).ToList();
            
            foreach (var realEstateGrouping in realEstateGroupings)
            {
                double valueOfRealEstate = realEstateGrouping.Sum(i => i.Value);
                valueOfAllRealEstateInvestments += valueOfRealEstate;
            }

            return Math.Round(valueOfAllRealEstateInvestments, 2);
        }
    }
}