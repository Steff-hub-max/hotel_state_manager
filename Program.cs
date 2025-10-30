using App;

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

    string roomnumber = userData[0];
    string guest = userData[1];
    Status roomStatus = Enum.Parse<Status>(userData[2]);

    Room room = new(roomnumber, guest, roomStatus);

    rooms.Add(room);
}

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
        Console.WriteLine("full - list of all booked rooms");
        Console.WriteLine("empty - list of all empty rooms");
        Console.WriteLine("new - book a guest to a room");
        Console.WriteLine("checkout - checkout a guest from a room");
        Console.WriteLine("closed - mark a room as unavailable");
        Console.WriteLine("logout - logout from this session");
        Console.WriteLine("quit - exit the program\n\n");
        Console.Write("Enter command: ");
        switch (Console.ReadLine())
        {
            case "full":
                Console.WriteLine("Here is a list of all available rooms: ");
                foreach (Room room in rooms)
                {
                    if (room.RoomStatus == Status.Occupied)
                    {
                        Console.WriteLine(room.RoomNumber + room.Guest + room.RoomStatus);
                    }
                }
                Console.ReadLine();
                break;
            case "empty":
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
                Console.ReadLine();
                break;
            case "new":
                break;
            case "checkout":
                break;
            case "closed":
                break;
            case "logout":
                active_user = null;
                break;
            case "quit":
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
