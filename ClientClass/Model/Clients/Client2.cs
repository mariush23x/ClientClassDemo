namespace ClientClass.Model.Clients {
    public sealed class Client2 : Client {
        public Client2(string firstName, string lastName, string personalId) : base(firstName, lastName, personalId) {
            maxVehicleCount = 2;
            maxDiscount = 10;
        }

        public Client2(Client client) : this(client.GetFirstName(), client.GetLastName(), client.GetPersonalId()) {
        }
    }
}
