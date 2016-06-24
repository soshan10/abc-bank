using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using abc_bank;

namespace abc_bank_tests
{
    [TestClass]
    public class BankTest
    {

        private static readonly double DOUBLE_DELTA = 1e-15;
        private static readonly double FOUR_DECIMALS = 1e-4;

        [TestMethod]
        public void CustomerSummary() 
        {
            Bank bank = new Bank();
            Customer john = new Customer("John");
            john.OpenAccount(new CheckingAccount());
            bank.AddCustomer(john);

            Assert.AreEqual("Customer Summary\n - John (1 account)", bank.CustomerSummary());
        }

        [TestMethod]
        public void CheckingAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(100.0);

            Assert.AreEqual(0.1, bank.totalInterestPaid(), DOUBLE_DELTA);
        }
        
        [TestMethod]
        public void ComputeCheckingAccountAccruedInterest()
        {
            Bank bank = new Bank();
            Account checkingAccount = new CheckingAccount();
            Customer bill = new Customer("Bill").OpenAccount(checkingAccount);
            bank.AddCustomer(bill);

            checkingAccount.Deposit(1000.0);
            double amt = bank.totalInterestPaid(DateTime.Now.AddDays(100));
            Assert.AreEqual(0.2739, amt, FOUR_DECIMALS); 

        }

        [TestMethod]
        public void Savings_account() {
            Bank bank = new Bank();
            Account savingAccount = new SavingAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(savingAccount));

            savingAccount.Deposit(1500.0);

            Assert.AreEqual(2.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        public void ComputeSavingAccountAccruedInterest()
        {
            Bank bank = new Bank();
            Account savingAccount = new SavingAccount();
            Customer bill = new Customer("Bill").OpenAccount(savingAccount);
            bank.AddCustomer(bill);

            savingAccount.Deposit(1000.0);
            double amt = bank.totalInterestPaid(DateTime.Now.AddDays(100));
            Assert.AreEqual(0.2739, amt, FOUR_DECIMALS);

        }


        [TestMethod]
        public void MaxiSavingsAccount() {
            Bank bank = new Bank();
            Account checkingAccount = new MaxiSavingAccount();
            bank.AddCustomer(new Customer("Bill").OpenAccount(checkingAccount));

            checkingAccount.Deposit(3000.0);

            Assert.AreEqual(170.0, bank.totalInterestPaid(), DOUBLE_DELTA);
        }

        [TestMethod]
        public void ComputeMaxSavingAccountAccruedInterest()
        {
            Bank bank = new Bank();
            Account maxiSavingAccount = new MaxiSavingAccount();
            Customer bill = new Customer("Bill").OpenAccount(maxiSavingAccount);
            bank.AddCustomer(bill);

            maxiSavingAccount.Deposit(3000.0);
            double amt = bank.totalInterestPaid(DateTime.Now.AddDays(100));
            Assert.AreEqual(46.5753, amt, FOUR_DECIMALS);
        }


        [TestMethod]
        public void ComputeMaxSavingAccountAccruedInterestWithin10days()
        {
            Bank bank = new Bank();
            Account maxiSavingAccount = new MaxiSavingAccount();
            Customer bill = new Customer("Bill").OpenAccount(maxiSavingAccount);
            bank.AddCustomer(bill);

            maxiSavingAccount.Deposit(6000.0);
            maxiSavingAccount.Withdraw(3000.0);
            double amt = bank.totalInterestPaid(DateTime.Now.AddDays(8));
            Assert.AreEqual(5.6986, amt, FOUR_DECIMALS);
        }
    }
}
