using UnityEngine;
using System.Collections;

public class GameSetting
{

    private static GameSetting _Instance = new GameSetting();
    public static GameSetting Instance
    {
        get
        {
            return _Instance;
        }
    }

    public int roomID = 0;
    public int BrandOperation;
    public int RadioOperation;
    public int Brand;

    public bool RoomData = false;
    public string player1 = "1";
    public string player2 = "1";
    public string player3 = "1";
    public string player4 = "1";
    public string roomName = "";
    public int NowPlayer = 0;

    public bool CanSend = false;

}
