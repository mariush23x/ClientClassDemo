namespace ClientClass.Excpetions {
    public sealed class RentEndDateException(DateTime startDate) : Exception {
        private readonly DateTime startDate = startDate;

        public override string Message => $"Data oddania nie może być wcześniejsza od daty wypożyczenia: {startDate.Date}";
    }
}
