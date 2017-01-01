using UnityEngine;
using System.Collections.Generic;
using System;

public class Action2001 : BaseAction
{
    private ActionResult actionResult;

    public Action2001() : base((int)ActionType.Logic)
    {
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult = new ActionResult();
        actionResult["brand"] = reader.getInt();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", GameSetting.Instance.roomID);
        writer.writeInt32("BrandOperation", GameSetting.Instance.BrandOperation);
    }
}
