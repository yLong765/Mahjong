using UnityEngine;
using System.Collections;
using System;

public class Action2000 : BaseAction
{

    private ActionResult actionResult = new ActionResult();

    public Action2000() : base((int)ActionType.JoinRoom)
    {
        actionResult["ActionType"] = (int)ActionType.JoinRoom;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["success"] = reader.getInt();
        actionResult["id"] = reader.getInt();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
        writer.writeString("roomName", (string)actionParam["roomName"]);
        writer.writeString("playerName", (string)actionParam["playerName"]);
    }
}
