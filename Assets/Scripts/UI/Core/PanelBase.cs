using UnityEngine;
using System.Collections;

public class PanelBase : UIBase {

    public virtual void OnInit()
    {
        WebLogic.Instance.AddEventListener(1, this);
        Init();
    }

}

public enum PanelState
{
    /// <summary>
    /// 创建房间提示窗
    /// </summary>
    PanelCreateRoom,

    /// <summary>
    /// 房间界面
    /// </summary>
    //SceneRoom,
}
