using UnityEngine;
using System.Collections;
using System;

public class Action2004 : BaseAction
{
    ActionResult actionResult;

    public Action2004() : base((int)ActionType.RoomMessageRadioResult)
    {
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult = new ActionResult();
        GameSetting.Instance.player1 = reader.readString();
        GameSetting.Instance.player2 = reader.readString();
        GameSetting.Instance.player3 = reader.readString();
        GameSetting.Instance.player4 = reader.readString();
        GameSetting.Instance.roomName = reader.readString();
        
        GameSetting.Instance.RoomData = true;
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        
    }
}
