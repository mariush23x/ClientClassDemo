using ClientClass.Excpetions;
using ClientClass.Model;
using ClientClass.Model.Clients;

namespace ClientClass.Repository {
    public sealed class ClientRepository : BaseRepository<Client> {
        private readonly List<Client> _clients = [];

        public ClientRepository() => CurrentClientType = typeof(Client);

        public Type CurrentClientType { get; private set; }

        private Client AddClient(Client client, bool checkClientType = true) {
            ArgumentNullException.ThrowIfNull(client, nameof(client));
            if (checkClientType && client.GetType() != CurrentClientType) {
                throw new InvalidClientTypeException(client, CurrentClientType);
            }

            _clients.Add(client);
            return client;
        }

        private Client RemoveClient(Client client) {
            _clients.Remove(client);
            return client;
        }

        public Client Delete(int index) {
            if (index < 0 || index > _clients.Count - 1) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var client = _clients[index];
            _clients.RemoveAt(index);
            return client;
        }

        public void ChangeClientType(Type clientType) {
            if (clientType != CurrentClientType) {
                CurrentClientType = clientType;
                ChangeClientTypeCollection();
            }
        }

        private void ChangeClientTypeCollection() {
            var changedClientTypes = new Client[_clients.Count];
            for (var i = 0; i < _clients.Count; i++) {
                var client = _clients[i];
                Client? newClientType = null;

                if (CurrentClientType == typeof(Client)) {
                    newClientType = new Client(client.GetFirstName(), client.GetLastName(), client.GetPersonalId());
                } else if (CurrentClientType == typeof(Client1)) {
                    newClientType = new Client1(client);
                } else if (CurrentClientType == typeof(Client2)) {
                    newClientType = new Client2(client);
                } else if (CurrentClientType == typeof(Client3)) {
                    newClientType = new Client3(client);
                }

                if (newClientType != null) {
                    changedClientTypes[i] = newClientType;
                }
            }

            _clients.Clear();
            _clients.AddRange(changedClientTypes);
        }

        public override Client Create(Client client) {
            if (_clients.Count == 0 && CurrentClientType != client.GetType()) {
                CurrentClientType = client.GetType();
            }
            return AddClient(client);
        }

        public override Client Delete(Client client) => RemoveClient(client);

        public override Client? Find(string id) => _clients.FirstOrDefault(c => c.GetPersonalId() == id);

        public override List<Client> GetAll() => _clients;

        public override void Remove() => _clients.Clear();

        public override Client Update(Client client) {
            var idx = _clients.IndexOf(client);
            if (idx != -1) {
                _clients[idx] = client;
            }
            return client;
        }
    }
}
