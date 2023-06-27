using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding_Test_QPLIX.Utils
{
    internal sealed class InvestmentMatcher
    {
        private readonly string _investorId;
        private readonly IList<GeneralInvestment> _investments;
        private readonly IList<Transaction> _transactions;

        internal InvestmentMatcher(string investorId, IList<GeneralInvestment> investments, IList<Transaction> transactions)
        {
            if (string.IsNullOrEmpty(investorId))
            {
                throw new ArgumentNullException(nameof(investorId));
            }
            if (investments == null || investments.Count() == 0)
            {
                throw new ArgumentNullException(nameof(investments));
            }
            if (transactions == null || transactions.Count() == 0)
            {
                throw new ArgumentNullException(nameof(transactions));
            }

            _investorId = investorId;
            _investments = investments;
            _transactions = transactions;
        }

        internal IList<StockInvestment> GetStockInvestments()
        {
            var realEstateInvestments =
                from investment in _investments.Where(i => i.IsStockInvestment && i.InvestorId.Equals(_investorId, StringComparison.OrdinalIgnoreCase))
                join transaction in _transactions
                on investment.InvestmentId equals transaction.InvestmentId
                select new StockInvestment
                {
                    InvestorId = investment.InvestorId,
                    InvestmentId = investment.InvestmentId,
                    InvestmentType = investment.InvestmentType,
                    Date = transaction.Date,
                    Type = transaction.Type,
                    Value = transaction.Value,
                    ISIN = investment.ISIN,
                };

            return realEstateInvestments.ToList();
        }

        internal IList<RealEstateInvestment> GetRealEstateInvestments()
        {
            var stockInvestments =
                from investment in _investments.Where(i => i.IsRealEstateInvestment && i.InvestorId.Equals(_investorId, StringComparison.OrdinalIgnoreCase))
                join transaction in _transactions
                on investment.InvestmentId equals transaction.InvestmentId
                select new RealEstateInvestment
                {
                    InvestorId = investment.InvestorId,
                    InvestmentId = investment.InvestmentId,
                    InvestmentType = investment.InvestmentType,
                    Date = transaction.Date,
                    Type = transaction.Type,
                    Value = transaction.Value,
                    City = investment.City
                };
            return stockInvestments.ToList();
        }

        internal IList<FondInvestment> GetFondInvestments()
        {
            var fondsInvestments =
                from investment in _investments.Where(i => i.IsFondInvestment && i.InvestorId.Equals(_investorId, StringComparison.OrdinalIgnoreCase))
                join transaction in _transactions
                on investment.InvestmentId equals transaction.InvestmentId
                select new FondInvestment
                {
                    InvestorId = investment.InvestorId,
                    InvestmentId = investment.InvestmentId,
                    InvestmentType = investment.InvestmentType,
                    Date = transaction.Date,
                    Type = transaction.Type,
                    Value = transaction.Value,
                    FondId = investment.FondsInvestor
                };

            return fondsInvestments.ToList();
        }

        internal IList<FondInternalInvestment> GetFondInternalInvestments()
        {
            var fondInternalInvestments =
                from investment in _investments.Where(i => i.IsFondInternalInvestment)
                join transaction in _transactions
                on investment.InvestmentId equals transaction.InvestmentId
                select new FondInternalInvestment
                {
                    InvestorId = investment.InvestorId,
                    InvestmentId = investment.InvestmentId,
                    InvestmentType = investment.InvestmentType,
                    Date = transaction.Date,
                    Type = transaction.Type,
                    Value = transaction.Value,
                    City = investment.City,
                    ISIN = investment.ISIN,
                };

            return fondInternalInvestments.ToList();
        }
    }
}
