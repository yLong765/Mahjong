using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action2004 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action2004() : base((int)ActionType.GetPlayerId)
    {
        actionResult["ActionType"] = (int)ActionType.GetPlayerId;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["playerId"] = reader.getInt();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
    }
}
