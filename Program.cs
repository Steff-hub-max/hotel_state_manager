using App;

//programmet laddar filer för användare och rum, samt splittar
//raderna med komma, så listorna stämmer med hur programmet fungerar.
List<User> users = new List<User>();
string[] lines = File.ReadAllLines("Users.csv");
foreach (string line in lines)
{
    string[] userData = line.Split(',');

    string name = userData[0];
    string password = userData[1];

    User user = new(name, password);

    users.Add(user);
}
List<Room> rooms = new List<Room>();
string[] roomlines = File.ReadAllLines("Rooms.csv");
foreach (string line in roomlines)
{
    string[] userData = line.Split(',');

    string number = userData[0];
    string guest = userData[1];
    Status roomStatus = Enum.Parse<Status>(userData[2]);
    int roomnumber;
    int.TryParse(number, out roomnumber);
    Room room = new(roomnumber, guest, roomStatus);

    rooms.Add(room);
}

// här kommer en bool, som gör att om användaren inte är inloggad
// skickas man till inloggningen. när man har loggat in,
// sedan visas en meny, där man får välja vad man vill göra.

User? active_user = null;
bool running = true;
while (running)
{
    if (active_user != null)
    {
        try
        {
            Console.Clear();
        }
        catch { }
        Console.WriteLine($"--Hotel State Manager--");
        Console.WriteLine($"welcome {active_user.Name}. Please type what you want to do.\n\n");
        Console.WriteLine("1 - list of all Occupied rooms.");
        Console.WriteLine("2 - list of all Available rooms");
        Console.WriteLine("3 - book a guest to a room");
        Console.WriteLine("4 - checkout a guest from a room");
        Console.WriteLine("5 - mark a room as unavailable");
        Console.WriteLine("l - logout from this session");
        Console.WriteLine("q - exit the program\n\n");
        Console.Write("Enter command: ");
        switch (Console.ReadLine())
        {
            // här loopar programmet igenom rummen, och jämför om status är occupied.
            // om det är upptaget, visas rumsnummer, vem som är gäst där och statusen på rummet.
            case "1":
                Console.WriteLine("Here is a list of all available rooms: ");
                foreach (Room room in rooms)
                {
                    if (room.RoomStatus == Status.Occupied)
                    {
                        Console.WriteLine(
                            $"Room: {room.RoomNumber} Current guest: {room.Guest} Status: {room.RoomStatus}"
                        );
                    }
                }
                Console.WriteLine("Press ENTER to continue.");
                Console.ReadLine();
                break;

            //här händer nästan samma sak, skillnaden är att den kollar efter tomma rum.
            case "2":
                Console.WriteLine("Here is a list of all available rooms: ");
                foreach (Room room in rooms)
                {
                    if (room.RoomStatus == Status.Available)
                    {
                        Console.WriteLine(
                            $"Room: {room.RoomNumber} Current guest: {room.Guest} Status: {room.RoomStatus}"
                        );
                    }
                }
                Console.WriteLine("Press ENTER to continue.");
                Console.ReadLine();
                break;
            case "3":
                // här får användaren först en lista över lediga rum, och får sedan skriva in vilket rum gästen
                // ska bo i och namn på gästen. när användaren angett det, sparas det till fil.
                foreach (Room room in rooms)
                {
                    if (room.RoomStatus == Status.Available)
                        Console.WriteLine(
                            $"Room: {room.RoomNumber} Current guest: {room.Guest} Status: {room.RoomStatus}"
                        );
                }
                Console.WriteLine(
                    "Enter the room you want to change, type DONE to go back to menu:"
                );
                string? user_new = Console.ReadLine();
                if (user_new?.ToLower() == "done")
                {
                    break;
                }
                if (int.TryParse(user_new, out int result))
                {
                    foreach (Room room in rooms)
                    {
                        if (room.RoomNumber == result)
                        {
                            Console.WriteLine("Enter the name of the new guest");
                            Console.Write("Name:");
                            room.Guest = Console.ReadLine()!;
                            room.RoomStatus = Status.Occupied;
                            SaveRoom(rooms, "Rooms.csv");
                            Console.WriteLine(
                                $"Your guest {room.Guest} is now booked to room: {room.RoomNumber}"
                            );
                            Console.WriteLine("Press ENTER to go back to menu");
                            Console.ReadLine();
                        }
                    }
                }
                break;
            case "4":
                // Här kan användaren checka ut en gäst. Användaren får först en lista över upptagna rum
                // och får välja vilket rum som ska checkas ut.
                foreach (Room room in rooms)
                {
                    if (room.RoomStatus == Status.Occupied)
                        Console.WriteLine(
                            $"Room: {room.RoomNumber} Current guest: {room.Guest} Status: {room.RoomStatus}"
                        );
                }
                Console.WriteLine(
                    "Enter the room you want to change, type DONE to go back to menu:"
                );
                string? user_out = Console.ReadLine();
                if (user_out?.ToLower() == "done")
                {
                    break;
                }
                if (int.TryParse(user_out, out int out_result))
                {
                    foreach (Room room in rooms)
                    {
                        if (room.RoomNumber == out_result)
                        {
                            room.Guest = "none";
                            room.RoomStatus = Status.Available;
                            SaveRoom(rooms, "Rooms.csv");
                            Console.WriteLine(
                                $"Your guest has succesfully checked out from room: {room.RoomNumber}."
                            );
                            Console.WriteLine("Press ENTER to go back to menu");
                            Console.ReadLine();
                        }
                    }
                }

                break;
            case "5":
                //Här kan användaren välja att markera ett rum som otillgängligt genom
                // att skriva in rumsnummret , det gör att
                // det inte går att boka in en gäst på det rummet. användaren kan även välja att
                // öppna rummet igen genom att ange ett redan otillgängligt rum.
                bool closed = true;
                while (closed)
                {
                    try
                    {
                        Console.Clear();
                    }
                    catch { }
                    foreach (Room room in rooms)
                    {
                        Console.WriteLine($"Room: {room.RoomNumber} Status: {room.RoomStatus}");
                    }
                    Console.WriteLine(
                        "Enter the room you want to change, type DONE to go back to menu:"
                    );
                    string? user_input = Console.ReadLine();
                    if (user_input?.ToLower() == "done")
                    {
                        closed = false;
                        continue;
                    }
                    if (int.TryParse(user_input, out int input))
                    {
                        foreach (Room room in rooms)
                        {
                            if (room.RoomNumber == input)
                            {
                                if (room.RoomStatus == Status.Available)
                                {
                                    room.RoomStatus = Status.Unavailable;
                                    SaveRoom(rooms, "Rooms.csv");
                                }
                                else if (room.RoomStatus == Status.Unavailable)
                                {
                                    room.RoomStatus = Status.Available;
                                    SaveRoom(rooms, "Rooms.csv");
                                }
                            }
                        }
                    }
                }

                break;
            case "l":
                active_user = null;
                break;
            case "q":
                running = false;
                break;
            default:
                Console.WriteLine("You must enter a valid command.");
                break;
        }
    }
    else
    {
        Console.WriteLine("--Hotel State Manager--");
        Console.WriteLine("Vänligen logga in");
        Console.Write("Name: ");
        string? username = Console.ReadLine();
        Console.Write("Password: ");
        string? password = Console.ReadLine();
        foreach (User user in users)
        {
            if (username is null or "" || password is null or "")
            {
                Console.WriteLine("username or password is wrong.");
            }
            else if (user.TryLogin(username, password))
            {
                active_user = user;
                break;
            }
        }
    }
}
static void SaveRoom(List<Room> rooms, string path)
{
    string[] lines_to_save = new string[rooms.Count];
    for (int i = 0; i < rooms.Count; ++i)
    {
        lines_to_save[i] = rooms[i].ToSaveString();
    }
    File.WriteAllLines(path, lines_to_save);
}
