using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebLogic {

    #region 单例

    private static WebLogic _Instance = new WebLogic();
    
    public static WebLogic Instance
    {
        get
        {
            return _Instance;
        }
    }

    #endregion

    private int actionId;

    public WebLogic()
    {
        NetWriter.SetUrl("127.0.0.1:9001");
        GameSetting.Instance.CanSend = true;
    }

    public void SendMessage(int ActionId, ActionParam actionParam)
    {
        actionId = ActionId;
        GameSetting.Instance.CanSend = false;
        Net.Instance.Send(ActionId, CallBack, actionParam);
    }

    public void CallBack(ActionResult actionResult)
    {
        if (actionId == (int)ActionType.Room)
        {

        }

        GameSetting.Instance.CanSend = true;
    }

}
