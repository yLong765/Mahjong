using UnityEngine;
using System.Collections;
using System;

public class Action2003 : BaseAction
{
    private ActionResult actionResult;

    public Action2003() : base((int)ActionType.brandRadioResult)
    {
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult = new ActionResult();
        //actionResult["brand"] = reader.getInt();
        Debug.Log(reader.getInt());
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        Debug.Log("");
    }
}
