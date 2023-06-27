namespace Coding_Test_QPLIX
{
    public sealed class FondInternalInvestment : JoinedInvestmentBase
    {
        internal string City { get; set; }
        internal string ISIN { get; set; }
        internal StockInvestment ToStockInvestment()
        {
            return new StockInvestment
            {
                InvestorId = InvestorId,
                InvestmentId = InvestmentId,
                InvestmentType = InvestmentType,
                Date = Date,
                Type = Type,
                Value = Value,
                ISIN = ISIN
            };
        }

        internal RealEstateInvestment ToRealEstateInvestment()
        {
            return new RealEstateInvestment
            {
                InvestorId = InvestorId,
                InvestmentId = InvestmentId,
                InvestmentType = InvestmentType,
                Date = Date,
                Type = Type,
                Value = Value,
                City = City
            };
        }
    }
}