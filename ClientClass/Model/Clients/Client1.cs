namespace ClientClass.Model.Clients {
    public sealed class Client1 : Client {
        public Client1(string firstName, string lastName, string personalId) : base(firstName, lastName, personalId) {
            maxVehicleCount = 10;
            maxDiscount = 5;
        }

        public Client1(Client client) : this(client.GetFirstName(), client.GetLastName(), client.GetPersonalId()) {
        }
    }
}
