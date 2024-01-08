namespace ClientClass.Excpetions {
    public sealed class ValueLessThanZeroException : Exception {
        public override string Message => "Wartość nie może być ujemna!";
    }
}
