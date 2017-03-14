using UnityEngine;
using System.Collections;
using System;

public class Action3001 : BaseAction
{
    ActionResult actionResult = new ActionResult();

    public Action3001() : base((int)ActionType.RoomMessageRadioResult)
    {
        actionResult["ActionType"] = (int)ActionType.RoomMessageRadioResult;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["player1"] = reader.readString();
        actionResult["player2"] = reader.readString();
        actionResult["player3"] = reader.readString();
        actionResult["player4"] = reader.readString();
        WebLogic.Instance.RadioCallBack(actionResult);
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        
    }
}
