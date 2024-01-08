using ClientClass.Excpetions;
using ClientClass.Model;
using ClientClass.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClientClassTests {
    [TestClass]
    public class UnitTest {
        private static readonly List<Vehicle> defaultVehicles = [
            new Car("PO 12345", 100, 1500, 'A'),
            new Car("WX 23456", 500, 2500, 'E'),
            new Car("TK 23DX2", 220, 2200, 'B'),
            new Car("ZS DDD2S", 70, 900, 'D'),
            new Moped("DW 123", 20, 300),
            new Moped("PO A21", 30, 400),
            new Moped("SK 000", 60, 440),
            new Moped("WE A11", 100, 1000),
            new Bicycle("AB-CDEF-GH", 15),
            new Bicycle("IJ-KLMN-OP", 15)
        ];

        [TestMethod]
        public void FirstName_WithEmptyValue_SettingValue() {
            var client = new Client("Jan", "Kowalski", "89100192752");
            var expected = "Jan";

            Assert.AreEqual(client.GetFirstName(), expected);
            client.SetFirstName(string.Empty);
            Assert.AreEqual(client.GetFirstName(), expected);
        }

        [TestMethod]
        public void PersonalId_WithValidationId_CheckValidId() {
            var client = new Client("Jan", "Kowalski", "89100192752");
            Assert.IsTrue(client.ValidPersonalId());
        }

        [TestMethod]
        public void PersonalId_WithNotValidationId_CheckValidId() {
            var client = new Client("Jan", "Kowalski", "89081421445");
            Assert.IsFalse(client.ValidPersonalId());
        }

        [TestMethod]
        public void PersonalId_ThrowLengthNotEquals11_CheckValidId() {
            var client = new Client("Jan", "Kowalski", "8910019275");
            Assert.ThrowsException<PersonalIdException>(() => client.ValidPersonalId());
        }

        [TestMethod]
        public void RentDuration_EqualsZeroWithNotEndingDate_CalcRentDuration() {
            var client = new Client("Jan", "Kowalski", "89100192752");
            var vehicle = new Vehicle("PO12345", 10);
            var startDate = DateTime.Now.AddDays(-10);
            var rent = new Rent(client, vehicle, startDate);

            Assert.AreEqual(rent.RentDuration, 0);
            Assert.IsTrue(rent.IsRented);
        }

        [TestMethod]
        public void RentDuration_BiggerThanZeroWithEndingDate_CalcRentDuration() {
            var client = new Client("Jan", "Kowalski", "89100192752");
            var vehicle = new Vehicle("PO12345", 10);
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now;
            var rent = new Rent(client, vehicle, startDate) {
                EndDate = endDate
            };

            Assert.IsTrue(rent.RentDuration > 0);
            Assert.IsFalse(rent.IsRented);
        }

        [TestMethod]
        public void RentValue_CorrectCalculation_CheckRentValue() {
            var baseRentValue = 15;
            var client = new Client("Jan", "Kowalski", "89100192752");
            var vehicle = new Vehicle("PO12345", baseRentValue);
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now;
            var rent = new Rent(client, vehicle, startDate) {
                EndDate = endDate
            };

            var expectedValue = baseRentValue * endDate.Subtract(startDate).Days;
            Assert.IsFalse(rent.IsRented);
            Assert.AreEqual(rent.Value, expectedValue);
        }

        [TestMethod]
        public void RentId_Create10Rents_And_CheckDifferentsIdWithRentsManager() {
            var client = new Client("Jan", "Kowalski", "89100192752");
            var clientRepository = new ClientRepository();
            clientRepository.Create(client);

            var rents = new Rent[10];
            for (var i = 0; i < defaultVehicles.Count; i++) {
                rents[i] = new Rent(client, defaultVehicles[i], DateTime.Now.AddDays(-(i + 1)));
            }

            for (var i = 0; i < 10; i += 2) {
                var rent1 = rents[i];
                var rent2 = rents[i + 1];
                Assert.AreNotEqual(rent1.Id, rent2.Id);
            }
        }

        [TestMethod]
        public void EndDate_ThrowStartDateEarly_CheckRentIsRented() {
            var client = new Client("Jan", "Kowalski", "89100192752");
            var vehicle = new Vehicle("PO12345", 10);
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now.AddDays(-12);
            var rent = new Rent(client, vehicle, startDate);

            Assert.ThrowsException<RentEndDateException>(() => rent.EndDate = endDate);
        }
    }
}