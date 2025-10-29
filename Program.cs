using App;

List<User> users = new List<User>();
users.Add(new User("a", "a"));
User? active_user = null;
bool running = true;
while (running)
{
    if (active_user != null)
    {
        Console.WriteLine($"welcome {active_user.Name}");
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
