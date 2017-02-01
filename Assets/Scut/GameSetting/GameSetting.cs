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
    public string roomName = "";
    public string PlayerName = "";
    public int Playerid = -1;
    public int target = -1;
    public int StartNum = -1;

    public bool CanSend = false;

}
