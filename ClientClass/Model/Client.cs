using ClientClass.Excpetions;

namespace ClientClass.Model {
    public class Client {
        private readonly string personalId;
        private string firstName;
        private string lastName;
        protected int maxVehicleCount;
        protected decimal maxDiscount;

        public Address? Address { get; private set; }

        public int MaxRentVehicleCount => maxVehicleCount;

        public decimal MaxDiscount => maxDiscount;

        public Client() {
#if DEBUG
            Console.WriteLine("Stworzenie konstruktora bezparametrowego klasy: {0}", GetType().Name);
#endif
            personalId = firstName = lastName = string.Empty;
            maxVehicleCount = int.MaxValue;
            maxDiscount = 0m;
        }

        public Client(string firstName, string lastName, string personalId) {
#if DEBUG
            Console.WriteLine("Stworzenie konstruktora parametrowego klasy: {0}", GetType().Name);
#endif
            if (string.IsNullOrEmpty(firstName)) {
                throw new NullReferenceException(nameof(firstName));
            }
            if (string.IsNullOrEmpty(lastName)) {
                throw new NullReferenceException(nameof(lastName));
            }
            if (string.IsNullOrEmpty(personalId)) {
                throw new NullReferenceException(nameof(personalId));
            }

            this.firstName = firstName;
            this.lastName = lastName;
            this.personalId = personalId;
            maxVehicleCount = int.MaxValue;
            maxDiscount = 0m;
        }

        public void AddAddres(Address address) => Address = address ?? throw new NullReferenceException(nameof(address));

        public string GetClientInfo() =>
            Address != null
                ? $"Imię: {firstName}, Nazwisko: {lastName}, PESEL: {personalId}, Adres: {Address.Street} {Address.Number}"
                : $"Imię: {firstName}, Nazwisko: {lastName}, PESEL: {personalId}";

        public string GetFirstName() => firstName;

        public void SetFirstName(string firstName) {
            if (this.firstName != firstName && !string.IsNullOrWhiteSpace(firstName)) {
                this.firstName = firstName;
            }
        }
        
        public string GetLastName() => lastName;

        public void SetLastName(string lastName) {
            if (this.lastName != lastName && !string.IsNullOrWhiteSpace(lastName)) {
                this.lastName = lastName;
            }
        }

        public string GetPersonalId() => personalId;

        public bool ValidPersonalId() {
            if (personalId.Length != 11) {
                throw new PersonalIdException();
            }

            var weights = new int[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            var sum = 0;

            for (var i = 0; i < personalId.Length - 1; i++) {
                var value = personalId[i] - '0';
                value *= weights[i];
                sum += value;
            }

            var rem = sum % 10;
            if (rem != 0) {
                rem = 10 - rem;
            }
            return rem == personalId.Last() - '0';
        }

        #region Override Members
        public override bool Equals(object? obj) =>
            obj is Client client && client.GetFirstName().Equals(firstName) && client.GetLastName().Equals(lastName) && client.GetPersonalId().Equals(personalId);

        public override int GetHashCode() {
            var hash = 17;
            hash ^= GetFirstName().Length;
            hash ^= GetLastName().Length;
            hash ^= GetPersonalId().Length;
            return hash;
        }
        #endregion
    }
}
