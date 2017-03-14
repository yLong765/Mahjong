using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLogic : WebBase {

    #region 单例

    private static WebLogic _Instance;

    public static WebLogic Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new WebLogic();
            }
            return _Instance;
        }
    }

    #endregion

    public WebLogic() : base()
    {
        
    }

    public void Send(int ActionId, ActionParam Param)
    {
        SendMessage(ActionId, Param);
    }

    protected override void CallBack(ActionResult actionResult)
    {
        ActionParam Param = new ActionParam();
        int actionID = actionResult.Get<int>("ActionType");
        Param["ActionType"] = actionID;

        switch (actionID)
        {
            case (int)ActionType.Login:
                Param["success"] = actionResult.Get<int>("success");
                break;

            case (int)ActionType.JoinRoom:
                Param["success"] = actionResult.Get<int>("success");
                GameSetting.Instance.Playerid = actionResult.Get<int>("id");
                break;

            case (int)ActionType.RoomMessage: 
                Param["Name"] = actionResult.Get<string>("Name");
                Param["Size"] = actionResult.Get<int>("Size");
                break;

            case (int)ActionType.RoomMessageRadioResult:
                Param["player1"] = actionResult.Get<string>("player1");
                Param["player2"] = actionResult.Get<string>("player2");
                Param["player3"] = actionResult.Get<string>("player3");
                Param["player4"] = actionResult.Get<string>("player4");
                GameSetting.Instance.roomName = actionResult.Get<string>("roomName");
                break;

            case (int)ActionType.Logic:
                Param["brand"] = actionResult.Get<int>("brand");
                break;

            case (int)ActionType.GetPlayerId:
                Param["target"] = actionResult.Get<int>("target");
                Param["StartNum"] = actionResult.Get<int>("StartNum");
                break;
        }

        if (actionID == 2001 || actionID == 2004) SendEvnet(EventCode.WebToLogic, Param);
        else SendEvnet(EventCode.WebToUI, Param);

        GameSetting.Instance.CanSend = true;
    }

    public void RadioCallBack(ActionResult actionResult)
    {
        ActionParam Param = new ActionParam();
        int actionID = actionResult.Get<int>("ActionType");
        Param["ActionType"] = actionID;

        switch (actionID)
        {
            case (int)ActionType.RoomMessageRadioResult:
                Param["player1"] = actionResult.Get<string>("player1");
                Param["player2"] = actionResult.Get<string>("player2");
                Param["player3"] = actionResult.Get<string>("player3");
                Param["player4"] = actionResult.Get<string>("player4");
                break;

            case (int)ActionType.brandRadioResult:
                Param["brand"] = actionResult.Get<int>("brand");
                Param["playerid"] = actionResult.Get<int>("playerid");
                break;

            case (int)ActionType.playerIdRadio:
                Param["num"] = actionResult.Get<int>("num");
                Param["level"] = actionResult.Get<int>("level");
                Param["target"] = actionResult.Get<int>("target");
                break;

            case (int)ActionType.GameEnd:
                Param["playerName"] = actionResult.Get<string>("playerName");
                break;
        }

        if (actionID == 3000 || actionID == 3002) SendEvnet(EventCode.WebToLogic, Param);
        else SendEvnet(EventCode.WebToUI, Param);
    }

}
