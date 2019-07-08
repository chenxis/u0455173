using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TipsCalculator
{
    class Bill
    {
       private double bill;
       private double Tip;
        public Bill(double Bill)
        {
            bill = Bill;
            Tip = 0.2*Bill;
        }


       
      

        public double getTip()
        {
            return Tip;
        }
        public double getBill()
        {
            return bill;
        }









    }
}
