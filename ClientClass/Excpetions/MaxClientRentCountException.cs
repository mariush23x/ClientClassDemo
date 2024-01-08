using ClientClass.Model;

namespace ClientClass.Excpetions {
    public sealed class MaxClientRentCountException(Client client) : Exception {
        private readonly Client client = client;

        public override string Message => $"Osiągnięto limit wypożyczeń dla klienta ({client.GetType()}): Nr PESEL {client.GetPersonalId()}!";
    }
}
