using UnityEngine;
using System.Collections;
using System;

public class Action2005 : BaseAction
{
    ActionResult actionResult;

    public Action2005() : base((int)ActionType.Login)
    {
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult = new ActionResult();
        actionResult["isSwitch"] = reader.getInt();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeString("UserName", (string)actionParam["UserName"]);
        writer.writeString("PassWord", (string)actionParam["PassWord"]);
    }
}
