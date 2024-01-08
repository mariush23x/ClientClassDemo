namespace ClientClass.Excpetions {
    public sealed class BadCarSegmentException(char segment) : Exception {
        private readonly char segment = segment;

        public override string Message => $"Samochód w segmencie: {segment} nie jest obsługiwany!";
    }
}
