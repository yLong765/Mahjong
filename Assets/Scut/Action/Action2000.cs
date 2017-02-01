using UnityEngine;
using System.Collections;
using System;

public class Action2000 : BaseAction
{

    private ActionResult actionResult = new ActionResult();

    public Action2000() : base((int)ActionType.Room)
    {
        actionResult["ActionType"] = (int)ActionType.Room;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["success"] = reader.getInt();
        actionResult["playerName"] = reader.readString();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
        writer.writeString("roomName", (string)actionParam["roomName"]);
        writer.writeString("playerName", GameSetting.Instance.PlayerName);
        writer.writeInt32("roomOperation", (int)actionParam["roomOperation"]);
    }

    enum roomOperation : int
    {
        Join = 1,
        Leave = 2,
        Delete = 3,
    }
}
