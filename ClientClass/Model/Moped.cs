namespace ClientClass.Model {
    public sealed class Moped(string id, int baseRentPrice, decimal engineDisplacement) : MotorVehicle(id, baseRentPrice, engineDisplacement) {
    }
}
