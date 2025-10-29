namespace App;

class User
{
    public string Name;
    public string Password;

    public User(string name, string password)
    {
        Name = name;
        Password = password;
    }

    public bool TryLogin(string username, string password)
    {
        return username == Name && password == Password;
    }
}
