using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventCode
{
    /// <summary>
    /// UI发送给Web
    /// </summary>
    UIToWeb = 0,
    /// <summary>
    /// Web发送给UI
    /// </summary>
    WebToUI,
    /// <summary>
    /// UI发送给Logic
    /// </summary>
    UIToLogic,
    /// <summary>
    /// Logic发送给UI
    /// </summary>
    LogicToUI,
    /// <summary>
    /// Logic发送给Web
    /// </summary>
    LogicToWeb,
    /// <summary>
    /// Web发送给Logic
    /// </summary>
    WebToLogic,
}

public class EventNode {

    /// <summary>
    /// 所有消息集合
    /// </summary>
    private Dictionary<int, IEventListener> mListeners = new Dictionary<int, IEventListener>();

    /// <summary>
    /// 消息结点
    /// </summary>
    private List<EventNode> nodeList = new List<EventNode>();

    /// <summary>
    /// 挂载消息结点到当前结点上
    /// </summary>
    /// <param name="node">消息结点</param>
    /// <returns>是否挂载成功</returns>
    public bool AddEventNode(EventNode node)
    {
        if (node == null)
        {
            return false;
        }

        if (nodeList.Contains(node))
        {
            return false;
        }
        else
        {
            nodeList.Add(node);
            return true;
        }
    }

    /// <summary>
    /// 卸载消息结点
    /// </summary>
    /// <param name="node">消息结点</param>
    /// <returns>是否卸载成功</returns>
    public bool RemoveEventNode(EventNode node)
    {
        if (!nodeList.Contains(node))
        {
            return false;
        }
        else
        {
            nodeList.Remove(node);
            return true;
        }
    }

    /// <summary>
    /// 挂载消息监听器到当前结点上
    /// </summary>
    /// <param name="id">消息id</param>
    /// <param name="listener">消息监听器</param>
    /// <returns>是否能挂接</returns>
    public bool AddEventListener(EventCode eventCode, IEventListener listener)
    {
        if (listener == null)
        {
            return false;
        }
        if (!mListeners.ContainsKey((int)eventCode))
        {
            mListeners.Add((int)eventCode, listener);
            return true;
        }
        mListeners.Remove((int)eventCode);
        mListeners.Add((int)eventCode, listener);
        return true;
    }

    public bool RemoveEventListener(EventCode eventCode)
    {
        if (mListeners.ContainsKey((int)eventCode))
        {
            mListeners.Remove((int)eventCode);
            return true;
        }
        return false;
    }

    public void SendEvnet(EventCode eventCode, ActionParam param)
    {
        DispatchEvent(eventCode, param);
    }

    /// <summary>
    /// 派发消息到子消息结点下的监听器上
    /// </summary>
    /// <param name="id">消息id</param>
    /// <param name="param"></param>
    /// <returns>中断消息为false</returns>
    private bool DispatchEvent(EventCode eventCode, ActionParam param)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].DispatchEvent(eventCode, param)) return true;
        }
        return TriggerEvent(eventCode, param);
    }

    /// <summary>
    /// 触发消息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    private bool TriggerEvent(EventCode eventCode, ActionParam param)
    {
        if (!mListeners.ContainsKey((int)eventCode))
        {
            return false;
        }

        if (mListeners[(int)eventCode].HandleEvent((int)eventCode, param))
        {
            return true;
        }

        return false;
    }

    void OnApplicationQuit()
    {
        mListeners.Clear();
        nodeList.Clear();
    }
	
}
