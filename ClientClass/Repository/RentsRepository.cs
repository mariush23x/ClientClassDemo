using ClientClass.Excpetions;
using ClientClass.Model;
using System.Globalization;
using System.Text;

namespace ClientClass.Repository {
    public sealed class RentsRepository : BaseRepository<Rent> {
        private readonly List<Rent> _rents = [];
        private readonly string[] reportHeaders = [
            "Id wypożyczenia",
            "Id klienta",
            "Id pojazdu",
            "Data rozpoczecia",
            "Data zakończenia",
            "Czas trwania",
            "Wartość"];

        private Rent CreateRent(Rent rent) {
            if (_rents.Exists(r => r.Vehicle.Id.Equals(rent.Vehicle.Id) && r.IsRented)) {
                throw new ClientIsRentedException(rent.Vehicle);
            }
            if (_rents.Count + 1 > rent.Client.MaxRentVehicleCount) {
                throw new MaxClientRentCountException(rent.Client);
            }

            _rents.Add(rent);
            return rent;
        }

        private Rent RemoveRent(Rent rent) {
            _rents.Remove(rent);
            return rent;
        }

        public string GetClientForRentedVehicle(Vehicle vehicle) {
            var rent = _rents.Where(r => r.Vehicle.Id.Equals(vehicle.Id)).FirstOrDefault();
            return rent == null ? "Nie znaleziono" : rent.Client.GetClientInfo();
        }

        public string RentReport() {
            if (_rents.Count == 0) {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var header in reportHeaders) {
                sb.Append(header);
                sb.Append("\t\t");
            }
            sb.AppendLine();
            foreach (var rent in _rents) {
                sb.Append(rent.Id).Append("\t\t");
                sb.Append(rent.Client.GetPersonalId()).Append("\t\t");
                sb.Append(rent.Vehicle.Id).Append("\t\t");
                sb.Append(rent.StartDate).Append("\t\t");
                if (rent.EndDate != null) {
                    sb.Append(rent.EndDate).Append("\t\t");
                } else {
                    sb.Append("Brak").Append("\t\t");
                }
                sb.Append(rent.RentDuration).Append("\t\t");
                sb.Append(rent.Value.ToString("N1", CultureInfo.InvariantCulture)).Append("\t\t");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public override Rent Create(Rent rent) => CreateRent(rent);

        public override Rent Delete(Rent rent) => RemoveRent(rent);

        public override Rent? Find(string id) => _rents.FirstOrDefault(r => r.Id.ToString() == id);

        public override List<Rent> GetAll() => _rents;

        public override void Remove() => _rents.Clear();

        public override Rent Update(Rent rent) {
            var idx = _rents.IndexOf(rent);
            if (idx != -1) {
                _rents[idx] = rent;
            }
            return rent;
        }
    }
}
