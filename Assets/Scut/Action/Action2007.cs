using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action2007 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action2007() : base((int)ActionType.GameInit)
    {
        actionResult["ActionType"] = (int)ActionType.GameInit;
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
    }
}
