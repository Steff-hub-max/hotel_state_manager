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

    public string ToSaveString()
    {
        string result = $"{RoomNumber},{Guest},{RoomStatus},";
        return result;
    }
}

public enum Status
{
    Available,
    Occupied,
    Unavailable,
}
