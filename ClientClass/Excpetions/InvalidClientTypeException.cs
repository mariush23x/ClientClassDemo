using ClientClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientClass.Excpetions {
    public sealed class InvalidClientTypeException(Client client, Type expectedType) : Exception {
        public override string Message => $"Błędny typ klienta {client.GetType()}! Obsługiwany typ: {expectedType}";
    }
}
