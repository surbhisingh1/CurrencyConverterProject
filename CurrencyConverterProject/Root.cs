using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterProject
{
    public class Root
    {
        //Root class is a main class. API return rates in a rates it return all currency name with Value.
       
            //Get all record in rates and set in Rate class as currency name wise
            public Rate rates { get; set; }
        
    }
}
