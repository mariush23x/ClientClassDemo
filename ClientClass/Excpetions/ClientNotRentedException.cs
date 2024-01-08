using ClientClass.Model;

namespace ClientClass.Excpetions {
    public sealed class ClientIsRentedException(Vehicle vehicle) : Exception {
        public override string Message => $"Pojazd o numerach rejestracyjnych: {vehicle.Id} jest już wypożyczony!";
    }
}
