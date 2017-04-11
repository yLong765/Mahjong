using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Action2008 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action2008() : base((int)ActionType.getWinName)
    {
        actionResult["ActionType"] = (int)ActionType.getWinName;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["WinName"] = reader.readString();
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        writer.writeInt32("roomID", (int)actionParam["roomID"]);
    }
}

