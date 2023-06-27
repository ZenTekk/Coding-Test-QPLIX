using Coding_Test_QPLIX.Models;
using System.Collections.Generic;

namespace Coding_Test_QPLIX
{
    public interface IFondCalculator
    {
        double GetValueOfFondInvestments(IList<FondInvestment> fondInvestments, IList<FondInternalInvestment> fondInternalInvestments, IDictionary<string, Quote> quotes);
    }
}