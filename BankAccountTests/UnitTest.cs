using BankAccountClass;

namespace BankAccountTests {
    [TestClass]
    public class UnitTest {
        [TestMethod]
        public void Debit_WithValidAmount_UpdateBalance() {
            var beginningBalance = 11.99;
            var debitAmount = 4.55;
            var expected = 7.44;
            var account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            account.Debit(debitAmount);
            Assert.AreEqual(expected, account.GetBalance(), "Account not debited correctly");
        }
    }
}