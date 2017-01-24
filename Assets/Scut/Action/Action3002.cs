using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action3002 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action3002() : base((int)ActionType.playerIdRadio)
    {
        actionResult["ActionType"] = (int)ActionType.playerIdRadio;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["num"] = reader.getInt();
        actionResult["level"] = reader.getInt();
        actionResult["PlayerId"] = reader.getInt();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        
    }
}
