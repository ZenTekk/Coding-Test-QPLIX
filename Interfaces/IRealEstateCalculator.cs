using System.Collections.Generic;

namespace Coding_Test_QPLIX.Utils.Interfaces
{
    public interface IRealEstateCalculator
    {
        double GetValueOfRealEstateInvestments(IList<RealEstateInvestment> realEstateInvestments);
    }
}
