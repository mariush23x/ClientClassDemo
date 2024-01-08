namespace BankAccountClass {
    public sealed class BankAccount {
        private readonly string customerName;
        private double balance;

        public BankAccount() {
            customerName = string.Empty;
            balance = 0;
        }

        public BankAccount(string customerName, double balance) {
            this.customerName = customerName;
            this.balance = balance;
        }

        public string GetCustomerName() => customerName;

        public double GetBalance() => balance;

        public void Debit(double amount) {
            if (amount < 0.0d) {
                throw new Exception("Kwota wypłaty jest mniejsza od 0!");
            }
            if (amount > balance) {
                throw new Exception("Kwota wypłaty jest większa od stanu konta!");
            }

            balance -= amount; // celowo należy zmienić na +, aby zobaczyć, że metoda nie działa poprawnie
        }

        public void Credit(double amount) {
            if (amount < 0.0d) {
                throw new Exception("Kwota wypłaty jest mniejsza od 0!");
            }

            balance += amount;
        }
    }
}
