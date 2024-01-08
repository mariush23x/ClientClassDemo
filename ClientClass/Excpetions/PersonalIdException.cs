namespace ClientClass.Excpetions {
    public sealed class PersonalIdException : Exception {
        public override string Message => "Długość numeru PESEL musi być równa 11!";
    }
}
