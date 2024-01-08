using ClientClass.Excpetions;

namespace ClientClass.Model {
    public sealed class Car : MotorVehicle {
        public char Segment { get; private set; }

        public Car(string id, int baseRentPrice, decimal engineDisplacement, char segment) : base(id, baseRentPrice, engineDisplacement) {
            Segment = segment;

            BaseRentPrice *= Segment switch {
                'A' => 1.0m,
                'B' => 1.1m,
                'C' => 1.2m,
                'D' => 1.3m,
                'E' => 1.4m,
                _ => throw new BadCarSegmentException(segment)
            };
        }
    }
}
