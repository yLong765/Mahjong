using UnityEngine;
using System.Collections;
using System;

public class Action2006 : BaseAction
{
    ActionResult actionResult = new ActionResult();

    public Action2006() : base((int)ActionType.RoomMessage)
    {
        actionResult["ActionType"] = (int)ActionType.RoomMessage;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["Name"] = reader.readString();
        actionResult["Size"] = reader.getInt();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
    }
}
