using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class SavingAccount : Account
    {
        public SavingAccount()
        {
            this.accountType = Account.SAVINGS; // added just to maintain the existing structure
        }

        public override double InterestEarned()
        {
            return this.InterestEarned(null);
        }

        public override double InterestEarned(DateTime? asOfDate)
        {
            double amount = 0;
            double interest = 0;
            double numberOfdays = 0;
            foreach (Transaction transaction in this.transactions)
            {
                if (asOfDate.HasValue)
                    numberOfdays += Math.Abs( (asOfDate.Value - transaction.TransactionDate).Days);
                amount += transaction.amount;
            }

            if (!asOfDate.HasValue)
                numberOfdays = 365;


            // rate of 0.1% for first $1,000 then 0.2%
            if (amount <= 1000)
                interest = (amount * 0.001) * (numberOfdays / 365);
            else
                interest = (1 + (amount - 1000) * 0.002) * (numberOfdays / 365);
            return interest;
        }
    }
}
