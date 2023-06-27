using System;

namespace Coding_Test_QPLIX
{
    public abstract class JoinedInvestmentBase : InvestmentBase
    {
        internal DateTime Date { get; set; }
        internal string Type { get; set; }
        internal double Value { get; set; }
    }
}