using ClientClass.Model;
using ClientClass.Model.Clients;
using ClientClass.Repository;

internal class Program {
    private static readonly List<Client> defaultClients = [
        new("Jan", "Kowalski", "891001132752"),
        new("Arkadiusz", "Nowak", "88080601948")
    ];

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

    private static void ConsoleNextLine() {
        Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować");
        Console.ReadLine();
    }

    private static void Excercise1() {
        Console.Clear();
        Console.WriteLine("Stworzenie obiektu klient wraz konstruktorem podstawowym i parametrycznym" +
            "\nPróba zmiany danych osobowych oraz dodania pustego ciągu do nazwiska klienta");
        ConsoleNextLine();
        // 1. Jakimi wartościami zaincjalizowane zostały pola obiektu gdy został on stworzony za pomocą bezparametrowego konstruktora?
        // Odp: Wartości mają domyślną wartość stringa - w tym przypadku ciąg pusty
        //
        // 2. Czy przy obecnej definicji klasy możesz powołać obiekt tej klasy tak, aby posiadał on konkretne wartości pól?
        //    Jeżeli nie jesteś pewna/pewien - spróbuj dodać odpowiedni kod do funkcji main(). Czy wobec tego można użyć obiektów tej klasy w sposób dający praktyczny skutek?
        // Odp: Domyślne wartości pól mają ustawionego tylko "getter", tzn. są tylko do odczytu. Oznacz to, że obiekt nie daje się używać do celów praktycznych
        var client1 = new Client();
        Console.WriteLine(client1.GetClientInfo());

        // 3. Czy znając argumenty konstruktora w funkcji main() oraz analizując komunikaty możesz wskazać, z działań na którym obiekcie pochodzą wszystkie komunikaty?
        // Odp: Tak
        //
        // 4. Czy nadal możliwe jest powołanie obiektu klasy Client używając konstruktora bezparametrowego?
        //    Jeśli tak, to zmień definicję klasy tak, aby było to niemożliwe (wskazówka: pamiętaj o konstruktorze domyślnym; istnienie składowej w klasie nie oznacza, że musi być ona dostępna).
        //    Następnie zmień sposób powoływania drugiego obiektu w funkcji main() tak, aby także wykorzystać konstruktor parametrowy
        // Odp: Jendym ze sposobów, aby uniemożliwić tworzenie kontruktora bezparametrowego jest zamiania publicznego konstruktora na prywatny
        //
        // 5. Czy przy obecnej definicji klasy możesz ustawić wartości pól, które uważasz za zmienialne?
        // Odp: Nie.
        //
        // 6.Czy przy obecnej definicji klasy możesz odczytać bezpośrednio wartości pól?
        // Odp: Nie.
        //
        // 7. Czy takie ograniczenie byłoby możliwe do zrealizowania, gdybyśmy w klasie Client zrezygnowali z hermetyzacji?
        // Odp. Nie
        //
        // 8. Czy ustawianie wartości pól poprzez konstruktor parametrowy z listą inicjalizacyjną podlega wprowadzonym ograniczeniom
        // Odp. Nie, kontruktor nie sprawdza podanych ograniczeń. Dochodzi tylko do przypisania wartości.
        var client2 = new Client("Jan", "Kowalski", "891001132752");
        Console.WriteLine(client2.GetClientInfo());
        client2.SetFirstName("Adam");
        client2.SetLastName("Nowak");
        Console.WriteLine(client2.GetClientInfo());
        client2.SetLastName(string.Empty);
        Console.WriteLine(client2.GetClientInfo());

        ConsoleNextLine();
    }

    private static void Excercise3() {
        Console.Clear();
        Console.WriteLine("Stworzenie wypożyczenia i dodanie czasu oddania pojazdu");
        ConsoleNextLine();

        var client = new Client("Jan", "Kowalski", "89100192752");
        var vehicle = new Vehicle("PO12345", 10);
        var startDate = DateTime.Now.AddDays(-10);
        var rent = new Rent(client, vehicle, startDate);
        Console.WriteLine(rent.RentInfo());
        rent.EndDate = DateTime.Now;
        Console.WriteLine(rent.RentInfo());

        ConsoleNextLine();
    }

    private static void Excercise4() {
        Console.Clear();
        // 1. Jaka jest kolejność wywoływania konstruktorów dla stworzonej hierarchii klas?
        //    Czy klasa Vehicle powinna posiadać bezparametrowy konstruktor?
        // Odp: Na początku jest wykonywanych kontruktor bazowy, poźniej wzystkie potomne konstruktory. Przykład (Vehicle->MotorVehicle->Car)
        //      Klasa Vehicle nie powinna posiadać konstruktora bezparametrowego jako klasa bazowa.
        //      Brak informacji podanej przy inicjalizacji klasy była by niemożiwa w każdej klasie potomnej
        var car = new Car("PO12345", 10, 1200, 'A');
        var bicycle = new Bicycle("XYZ", 2);

        // 1. Co to jest polimorfizm obiektowy?
        // Odp: Polimorfizm obiektowy jest powiązany z funkcjami wirtualnymi (virtual) oraz abstrakcyjnymi (abstract) i jest to tak zwane nadpisywanie funkcji (override)
        //
        // 2. Czym różni się nadpisanie metody (override) od przeciążenia metody (overload)?
        // Odp: Nadpisanie metody może posiadać inne ciało fukncji a przeciążona metoda ma zawsze takie samo; rózni się tylko argumentami.

        // 3. Jak w metodzie klasy potomnej wywołać nadpisaną metodę klasy bazowej?
        // Odp: Wystarczy użyć słowa base. Przykład: public void override Test() { base.Test(); }.
        //      Można się odwołać do tej metody w dowolnym momencie w ciele funkcji. Występuje tylko przy funkcjach wirtualnych
        var testRents = new List<Rent>() {
            new(defaultClients[0], defaultVehicles[0], DateTime.Now.AddDays(-15)),
            new(defaultClients[0], defaultVehicles[1], DateTime.Now.AddDays(-1)),
            new(defaultClients[1], defaultVehicles[2], DateTime.Now.AddDays(-12)),
            new(defaultClients[0], defaultVehicles[3], DateTime.Now.AddDays(-10)),
            new(defaultClients[1], defaultVehicles[4], DateTime.Now.AddDays(-8)),
            new(defaultClients[1], defaultVehicles[5], DateTime.Now.AddDays(-25)),
            new(defaultClients[0], defaultVehicles[6], DateTime.Now.AddDays(-3)),
            new(defaultClients[1], defaultVehicles[7], DateTime.Now.AddDays(-4))
        };

        Console.WriteLine("Stworzenie repozytorium RentsRepository.\nDodanie wypożyczeń testowych oraz próba wypożyecznia wypożyczonego pojazdu");

        var rentsRepository = new RentsRepository();
        foreach (var rent in testRents) {
            rentsRepository.Create(rent);
        }

        Console.WriteLine($"Ilość wypożyczeń: {rentsRepository.GetAll().Count}");
        Console.WriteLine(rentsRepository.RentReport());
        Console.WriteLine(rentsRepository.GetClientForRentedVehicle(defaultVehicles[2]));

        try {
            rentsRepository.Create(new(defaultClients[0], defaultVehicles[7], DateTime.Now.AddDays(-4)));
        }
        catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            ConsoleNextLine();
        }

        rentsRepository.Remove();

        Console.WriteLine("Stworzenie repozytoirum VehicleRepository.\nDodanie pojazdów testowych oraz próba uzyskania dostępu do pojazdu z poza listy");
        ConsoleNextLine();

        Console.WriteLine($"Ilość wypożyczeń: {rentsRepository.GetAll().Count}");
        Console.WriteLine(rentsRepository.GetClientForRentedVehicle(defaultVehicles[2]));

        var vehicleRepository = new VehicleRepository();
        foreach (var vehicle in defaultVehicles) {
            vehicleRepository.Create(vehicle);
        }

        ConsoleNextLine();

        Console.Clear();
        Console.WriteLine($"Ilość pojazdów: {vehicleRepository.GetAll().Count}");
        Console.WriteLine(vehicleRepository.VehicleReport());

        try {
            Console.WriteLine($"Pojazd na pozycji 5: {vehicleRepository[5].VehicleInfo()}");
            Console.WriteLine($"Pojazd na pozycji 100: {vehicleRepository[100].VehicleInfo()}");
        }
        catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować");
            Console.ReadLine();
        }

        vehicleRepository.Remove();
        Console.WriteLine($"Ilość pojazdów: {vehicleRepository.GetAll().Count}");

        ConsoleNextLine();
    }

    private static void Excercise5() {
        Console.Clear();

        var client1 = new Client1(defaultClients[0]);
        var client2 = new Client2(defaultClients[1]);

        Console.WriteLine("Stworzenie repozytorium ClientRepository." +
            $"\nDodanie klienta o typie {client1.GetType()} oraz próba wymuszenia dodania klienta o typie {client2.GetType()}");

        var clientRepository = new ClientRepository();
        clientRepository.Create(client1);

        Console.WriteLine($"Ilość klientów: {clientRepository.GetAll().Count}");

        try {
            clientRepository.Create(client2);
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            ConsoleNextLine();
        }

        Console.WriteLine($"Dodanie klienta o typie: {client2.GetType()}");

        try {
            clientRepository.ChangeClientType(client2.GetType());
            clientRepository.Create(client2);
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            ConsoleNextLine();
        }

        Console.WriteLine($"Ilość klientów: {clientRepository.GetAll().Count}");
        ConsoleNextLine();

        clientRepository.ChangeClientType(client1.GetType());

        var rents = new Rent[10];
        for (var i = 0; i < defaultVehicles.Count; i++) {
            rents[i] = new Rent(client1, defaultVehicles[i], DateTime.Now.AddDays(-(i + 1)));
        }

        var rentsRepository = new RentsRepository();
        foreach (var rent in rents) {
            rentsRepository.Create(rent);
        }

        Console.Clear();
        Console.WriteLine("Ćwiczenia na obiekcie RentsManager.\nWymuszenie wypożyczenia pojazdu, który nie jest dostępny." +
            $"\nSprawdzenie czy klient {client1.GetType()} nie osiągnał max: {client1.MaxRentVehicleCount}");
        var rentsManager = new RentsManager(rentsRepository, clientRepository);
        try {
            var newRent = new Rent(client2, defaultVehicles[0], DateTime.Now);
            rentsManager.CreateRent(newRent);
        }
        catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            ConsoleNextLine();
        }

        try {
            var newRent = new Rent(client1, new Vehicle("XYZ", 1), DateTime.Now);
            rentsManager.CreateRent(newRent);
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
            ConsoleNextLine();
        }

        var returnVehicle = rentsManager.ReturnVehicle(rents[0]);
        Console.WriteLine($"Czas trwania wypożyczenia pojazdu {returnVehicle.Id}: {rents[0].RentDuration} dni");

        var rentedVehicles = rentsManager.GetAllClientRents();
        foreach (var rentedVehicle in rentedVehicles) {
            Console.WriteLine(rentedVehicle.RentInfo());
        }

        ConsoleNextLine();
    }

    private static void Main() {
        Excercise1();
        Excercise3();
        Excercise4();
        Excercise5();
    }
}