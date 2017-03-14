using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Action2008 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action2008() : base((int)ActionType.SendGameEndMessage)
    {
        actionResult["ActionType"] = (int)ActionType.SendGameEndMessage;
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
        writer.writeInt32("roomID", (int)actionResult["roomID"]);
        writer.writeString("playerName", (string)actionResult["playerName"]);
    }
}

