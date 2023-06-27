using Coding_Test_QPLIX.Models;
using System.Collections.Generic;

namespace Coding_Test_QPLIX.Utils.Interfaces
{
    public interface IStockCalculator
    {
        double GetValueOfStockInvestments(IList<StockInvestment> stockInvestments, IDictionary<string, Quote> quotes);
    }
}
