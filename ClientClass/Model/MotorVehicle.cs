using ClientClass.Excpetions;

namespace ClientClass.Model {
    public class MotorVehicle : Vehicle {
        public decimal EngineDisplacement { get; private set; }

        public MotorVehicle(string id, int baseRentPrice, decimal engineDisplacement) : base(id, baseRentPrice) {
            if (engineDisplacement <= 0) {
                throw new ValueLessThanZeroException();
            }

            EngineDisplacement = engineDisplacement;
            if (EngineDisplacement is >= 1000 and <= 2000) {
                // Liniowy przrost bazowej ceny wypożyczenia 
                BaseRentPrice = ((0.0005m * EngineDisplacement) + 0.5m) * BaseRentPrice;
            } else {
                BaseRentPrice = 1.5m * BaseRentPrice;
            }
        }
    }
}
