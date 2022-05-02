namespace AppointmentAppBackend.Model;

public class Users
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public Users(int id, string username, string password)
    {
        Id = id;
        Username = username;
        Password= password;
    }
}
