using System;

namespace Coding_Test_QPLIX
{
    public abstract class InvestmentBase
    {
        public string InvestorId { get; set; }
        public string InvestmentId { get; set; }
        public string InvestmentType { get; set; }

        internal bool IsStockInvestment => string.Equals(InvestmentType, "Stock", StringComparison.OrdinalIgnoreCase);

        internal bool IsRealEstateInvestment => string.Equals(InvestmentType, "RealEstate", StringComparison.OrdinalIgnoreCase);

        internal bool IsFondInvestment => string.Equals(InvestmentType, "Fonds", StringComparison.OrdinalIgnoreCase);

        internal bool IsFondInternalInvestment => InvestorId.StartsWith("Fond", StringComparison.OrdinalIgnoreCase);
    }
}