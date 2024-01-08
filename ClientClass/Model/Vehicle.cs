using ClientClass.Excpetions;

namespace ClientClass.Model {
    public class Vehicle {
        public string Id { get; }
        public decimal BaseRentPrice { get; protected set; }

        public Vehicle(string id, int baseRentPrice) {
            if (string.IsNullOrWhiteSpace(id)) {
                throw new NullReferenceException(nameof(id));
            }
            if (baseRentPrice < 0) {
                throw new ValueLessThanZeroException();
            }

            Id = id;
            BaseRentPrice = baseRentPrice;
        }

        public string VehicleInfo() => $"Numer rejestracyjny: {Id}, Cena wypożyczenia: {BaseRentPrice}";
    }
}
