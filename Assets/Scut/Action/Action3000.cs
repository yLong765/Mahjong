using UnityEngine;
using System.Collections;
using System;

public class Action3000 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action3000() : base((int)ActionType.brandRadioResult)
    {
        actionResult["ActionType"] = (int)ActionType.brandRadioResult;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["brand"] = reader.getInt();
        actionResult["playerid"] = reader.getInt();
        WebLogic.Instance.RadioCallBack(actionResult);
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
    }
}
