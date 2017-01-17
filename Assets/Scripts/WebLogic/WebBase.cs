using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBase : EventNode {

    public WebBase()
    {
        Init();
    }

    /// <summary>
    /// 初始化网络
    /// </summary>
    private void Init()
    {
        NetWriter.SetUrl("127.0.0.1:9001");
        GameSetting.Instance.CanSend = true;
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    protected virtual void InitDate()
    {

    }

    /// <summary>
    /// 发送函数
    /// </summary>
    /// <param name="ActionId">ID</param>
    /// <param name="Param">包</param>
    protected virtual void SendMessage(int ActionId, ActionParam Param)
    {
        GameSetting.Instance.CanSend = false;
        Net.Instance.Send(ActionId, CallBack, Param);
    }

    /// <summary>
    /// 回调函数
    /// </summary>
    /// <param name="actionParam">包</param>
    protected virtual void CallBack(ActionResult actionResult)
    {
        GameSetting.Instance.CanSend = true;
    }

}
