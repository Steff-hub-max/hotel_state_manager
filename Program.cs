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

    string number = userData[0];
    string guest = userData[1];
    Status roomStatus = Enum.Parse<Status>(userData[2]);
    int roomnumber;
    int.TryParse(number, out roomnumber);
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
                break;
            case "5":
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
                                }
                                else if (room.RoomStatus == Status.Unavailable)
                                {
                                    room.RoomStatus = Status.Available;
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
