using ClientClass.Model.Clients;
using ClientClass.Repository;

namespace ClientClass.Model {
    public sealed class RentsManager(RentsRepository rentsRepository, ClientRepository clientRepository) {
        private readonly RentsRepository rentsRepository = rentsRepository ?? throw new NullReferenceException(nameof(rentsRepository));
        private readonly ClientRepository clientRepository = clientRepository ?? throw new NullReferenceException(nameof(clientRepository));
        private readonly List<Rent> _currentRents = [];
        private readonly List<Rent> _archiveRents = [];

        public Rent CreateRent(Rent rent) {
            if (_currentRents.Count == 0) {
                _currentRents.AddRange(rentsRepository.GetAll());
            }

            try {
                rentsRepository.Create(rent);
                _currentRents.Add(rent);
            } catch {
                throw;
            }
            return rent;
        }

        public Rent RemoveRent(Rent rent) => rentsRepository.Delete(rent);

        public Vehicle ReturnVehicle(Rent rent) {
            var idx = _currentRents.IndexOf(rent);
            rent.EndDate = DateTime.Now;

            if (idx != -1) {
                _currentRents[idx] = rent;
            }
            rentsRepository.Delete(rent);
            _archiveRents.Add(rent);

            /*if (CheckClientRentBallance(client) < 0.0m) {
                ChangeClientType()
            }*/
            return rent.Vehicle;
        }

        public Rent[] GetAllClientRents() {
            var rents = new List<Rent>();
            foreach (var rent in _currentRents) {
                if (rent.Client.GetType() == clientRepository.CurrentClientType && !rent.IsRented) {
                    rents.Add(rent);
                }
            }
            return [.. rents];
        }

        public decimal CheckClientRentBallance(Client client) {
            var sum = 0.0m;
            foreach (var rent in _currentRents.Where(r => r.Client == client)) {
                sum += rent.Value;
            }
            return sum;
        }

        public void ChangeClientType(Type clientType) {
            var currentClientRentsCount = rentsRepository.GetAll().Count;
            if (Activator.CreateInstance(clientType) is Client1 c1 && c1.MaxRentVehicleCount > currentClientRentsCount) {
                throw new Exception($"Nie można przekonwertować klienta na typ: {clientType}, ponieważ osiągnięto by maksymalną liczbę wypożyczęń");
            } else if (Activator.CreateInstance(clientType) is Client2 c2 && c2.MaxRentVehicleCount > currentClientRentsCount) {
                throw new Exception($"Nie można przekonwertować klienta na typ: {clientType}, ponieważ osiągnięto by maksymalną liczbę wypożyczęń");
            } else if (Activator.CreateInstance(clientType) is Client3 c3 && c3.MaxRentVehicleCount > currentClientRentsCount) {
                throw new Exception($"Nie można przekonwertować klienta na typ: {clientType}, ponieważ osiągnięto by maksymalną liczbę wypożyczęń");
            }
            clientRepository.ChangeClientType(clientType);
        }
    }
}
