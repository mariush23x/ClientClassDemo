using ClientClass.Model;
using System.Globalization;
using System.Text;

namespace ClientClass.Repository {
    public sealed class VehicleRepository : BaseRepository<Vehicle> {
        private readonly List<Vehicle> _vehicles = [];
        private readonly string[] reportHeaders = [
            "Id pojazdu",
            "Cena wypożyczenia"
        ];

        public Vehicle this[int index] => index < 0 || index > _vehicles.Count - 1 
            ? throw new IndexOutOfRangeException($"Indeks : {index} jest poza rozmiarem kolekcji lub jest ujemny!") 
            : _vehicles[index];

        public string VehicleReport() {
            if (_vehicles.Count == 0) {
                return string.Empty;
            }

            var sb = new StringBuilder();
            foreach (var header in reportHeaders) {
                sb.Append(header);
                sb.Append("\t\t");
            }
            sb.AppendLine();
            foreach (var vehicle in _vehicles) {
                sb.Append(vehicle.Id).Append("\t\t");
                sb.Append(vehicle.BaseRentPrice.ToString("N1", CultureInfo.InvariantCulture)).Append("\t\t");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public override Vehicle Create(Vehicle vehicle) {
            ArgumentNullException.ThrowIfNull(vehicle, nameof(vehicle));
            _vehicles.Add(vehicle);
            return vehicle;
        }

        public override Vehicle Delete(Vehicle vehicle) {
            _vehicles.Remove(vehicle);
            return vehicle;
        }

        public override Vehicle? Find(string id) => _vehicles.FirstOrDefault(v => v.Id == id);

        public override List<Vehicle> GetAll() => _vehicles;

        public override void Remove() => _vehicles.Clear();

        public override Vehicle Update(Vehicle vehicle) {
            var idx = _vehicles.IndexOf(vehicle);
            if (idx != -1) {
                _vehicles[idx] = vehicle;
            }
            return vehicle;
        }
    }
}
