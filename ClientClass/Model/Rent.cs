using ClientClass.Excpetions;

namespace ClientClass.Model {
    public sealed class Rent(Client client, Vehicle vehicle, DateTime startDate) {
        private int rentDuration = 0;
        private DateTime? endDate;

        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime StartDate { get; private set; } = startDate;
        public DateTime? EndDate {
            get => endDate;
            set {
                if (!IsRented) {
                    return;
                }
                if (value < StartDate) {
                    throw new RentEndDateException(StartDate);
                }
                endDate = value;
                IsRented = false;
            }
        }
        public int RentDuration {
            get {
                if (!IsRented && EndDate.HasValue) {
                    rentDuration = EndDate.Value.Subtract(StartDate).Days;
                    if (rentDuration == 0) {
                        rentDuration = 1;
                    }
                }
                return rentDuration;
            }
        }
        public decimal Value => IsRented ? 0.0m : (RentDuration * Vehicle.BaseRentPrice) - Client.MaxDiscount;
        public bool IsRented { get; private set; } = true;
        public Vehicle Vehicle { get; } = vehicle;
        public Client Client { get; } = client;

        public string RentInfo() => $"Id wypożyczenia: {Id}, data wypożyczenia: {StartDate}, liczba dni wypożyczenia: {RentDuration}";
    }
}
