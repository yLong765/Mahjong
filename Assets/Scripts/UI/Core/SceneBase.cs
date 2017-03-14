using UnityEngine;
using System.Collections;
using System;

public class SceneBase : UIBase {

    public virtual void OnInit()
    {
        WebLogic.Instance.AddEventListener(EventCode.WebToUI, this);
        Init();
    }

}

public enum SceneState
{
    /// <summary>
    /// 登陆界面
    /// </summary>
    SceneLogin,

    /// <summary>
    /// 房间界面
    /// </summary>
    SceneRoom,

    /// <summary>
    /// 房间里界面
    /// </summary>
    SceneInRoom,

    /// <summary>
    /// 游戏界面
    /// </summary>
    SceneGame,

    /// <summary>
    /// 游戏结束
    /// </summary>
    SceneGameEnd,
}
