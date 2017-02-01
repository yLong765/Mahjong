using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action2003 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action2003() : base((int)ActionType.RespondOperation)
    {
        actionResult["ActionType"] = (int)ActionType.RespondOperation;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {        

    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
        writer.writeInt32("playerId", (int)actionParam["playerId"]);
        writer.writeInt32("num", (int)actionParam["num"]);
        writer.writeInt32("level", (int)actionParam["level"]);
    }
}
