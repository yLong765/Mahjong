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

            case (int)ActionType.Room:
                Param["success"] = actionResult.Get<int>("success");
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
                break;
        }

        SendEvnet(1, Param);

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
        }

        SendEvnet(1, Param);
    }

}
