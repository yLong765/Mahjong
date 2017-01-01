using UnityEngine;
using System.Collections;
using System;

public class Action2002 : BaseAction
{
    private ActionResult actionResult;

    public Action2002() : base((int)ActionType.Radio)
    {
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult = new ActionResult();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
        writer.writeInt32("RadioOperation", (int)actionParam["rO"]);
        writer.writeInt32("brand", (int)actionParam["brand"]);
    }
}
