using UnityEngine;
using System.Collections;
using System;

public class Action2000 : BaseAction
{

    private ActionResult actionResult;

    public Action2000() : base((int)ActionType.Room)
    {
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult = new ActionResult();
        int b = 0;
        if ((b = reader.getInt()) == 0)
        {
            actionResult["callback"] = -1;
        }
        else
        {
            actionResult["callback"] = b;
        }
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
        writer.writeString("roomName", (string)actionParam["roomName"]);
        writer.writeInt32("roomOperation", (int)actionParam["roomOperation"]);
    }

    enum roomOperation : int
    {
        Join = 1,
        Leave = 2,
        Delete = 3,
    }
}
