using System;

namespace Coding_Test_QPLIX
{
    public sealed class Transaction
    {
        public string InvestmentId { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
    }
}