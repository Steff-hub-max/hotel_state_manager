namespace App;

class Room
{
    public int RoomNumber;
    public string Guest;
    public Status RoomStatus;

    public Room(int roomNumber, string guest, Status roomStatus)
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
