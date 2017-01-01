using UnityEngine;
using System.Collections;
using System;

public class Action2006 : BaseAction
{
    ActionResult actionResult;

    public Action2006() : base((int)ActionType.RoomMessage)
    {
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult = new ActionResult();
        actionResult["roomName"] = reader.readString();
        actionResult["size"] = reader.getInt();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
    }
}
