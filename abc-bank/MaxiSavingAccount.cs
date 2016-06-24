using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class MaxiSavingAccount : Account
    {
        public MaxiSavingAccount()
        {
            this.accountType = Account.MAXI_SAVINGS; // added just to maintain the existing structure
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
            double rate = 0;
            bool hasWithdrawalsInPast10Days = false;

            foreach (Transaction transaction in this.transactions)
            {
                if (asOfDate.HasValue && !hasWithdrawalsInPast10Days)
                    hasWithdrawalsInPast10Days = ((transaction.amount < 0) && (asOfDate.Value - transaction.TransactionDate).Days <= 10);

                if (asOfDate.HasValue)
                    numberOfdays += Math.Abs((asOfDate.Value - transaction.TransactionDate).Days);

                amount += (transaction.amount);
            }

            if (!asOfDate.HasValue)
                numberOfdays = 365;

            // rate of 2% for first $1,000 then 5% for next $1,000 then 10%
            if (amount <= 1000)
            {
                rate = 0.02;
                interest = (amount * rate * (numberOfdays / 365));
            }
            if (amount <= 2000)
            {
                if (hasWithdrawalsInPast10Days)
                    rate = 0.01;
                else
                    rate = 0.05;
                interest = ((1000 * 0.02) + (amount - 1000) * rate) * (numberOfdays / 365);
            }
            rate = 0.1;
            if (hasWithdrawalsInPast10Days)
                interest = ((1000 * 0.02) + (1000 * 0.01) + (amount - 2000) * rate) * (numberOfdays / 365);
            else
                interest = ((1000 * 0.02) + (1000 * 0.05) + (amount - 2000) * rate) * (numberOfdays / 365);

            return interest;
        }
    }
}
