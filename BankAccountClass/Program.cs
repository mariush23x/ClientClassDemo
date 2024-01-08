using BankAccountClass;

internal class Program {
    private static void Main() {
        var ba = new BankAccount("XYZ", 1000d);

        Console.WriteLine("Obecny stan konta klienta {0}: {1}", ba.GetCustomerName(), ba.GetBalance());
        ba.Debit(500);
        Console.WriteLine("Obecny stan konta klienta {0}: {1}", ba.GetCustomerName(), ba.GetBalance());
        ba.Credit(500);
        Console.WriteLine("Obecny stan konta klienta {0}: {1}", ba.GetCustomerName(), ba.GetBalance());
    }
}