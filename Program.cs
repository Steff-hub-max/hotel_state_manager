using App;

List<User> users = new List<User>();
users.Add(new User("a", "a"));
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
                break;
            case "empty":
                break;
            case "new":
                break;
            case "checkout":
                break;
            case "closed":
                break;
            case "logout":
                break;
            case "quit":
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
