using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterProject
{
    public class Rate
    {
        //Make sure API return value that name and where you want to store that name are same.
        //Like in API get response INR then set it with INR Name.
        public double INR { get; set; }
            public double JPY { get; set; }
            public double USD { get; set; }
            public double NZD { get; set; }
            public double EUR { get; set; }
            public double CAD { get; set; }
            public double ISK { get; set; }
            public double PHP { get; set; }
            public double DKK { get; set; }
            public double SAR { get; set; }
            public double GBP{ get; set; }

    }
}
