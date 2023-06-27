using System;

namespace Coding_Test_QPLIX.Models
{
    public sealed class Quote
    {
        public string ISIN { get; set; }
        public DateTime Date{ get; set; }
        public double PricePerShare { get; set; }
    }
}
