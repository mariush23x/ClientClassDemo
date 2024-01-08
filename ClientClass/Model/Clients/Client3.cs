namespace ClientClass.Model.Clients {
    public sealed class Client3 : Client {
        public Client3(string firstName, string lastName, string personalId) : base(firstName, lastName, personalId) {
            maxVehicleCount = 15;
            maxDiscount = 8;
        }

        public Client3(Client client) : this(client.GetFirstName(), client.GetLastName(), client.GetPersonalId()) {
        }
    }
}
