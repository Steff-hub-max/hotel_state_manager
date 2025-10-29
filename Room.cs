namespace App;

class Room
{
    public string RoomNumber;
    public string Guest;
    public Status RoomStatus;

    public Room(string roomNumber, string guest, Status roomStatus)
    {
        RoomNumber = roomNumber;
        Guest = guest;
        RoomStatus = roomStatus;
    }
}

public enum Status
{
    Available,
    Occupied,
    Unavailable,
}
