using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Action3003 : BaseAction
{
    private ActionResult actionResult = new ActionResult();

    public Action3003() : base((int)ActionType.GameEnd)
    {
        actionResult["ActionType"] = (int)ActionType.GameEnd;
    }

    public override ActionResult GetResponseData()
    {
        return actionResult;
    }

    protected override void DecodePackage(NetReader reader)
    {
        actionResult["playerName"] = reader.readString();
        WebLogic.Instance.RadioCallBack(actionResult);
    }

    protected override void SendParameter(NetWriter writer, ActionParam actionParam)
    {
        
    }
}

