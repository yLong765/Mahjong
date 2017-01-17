using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNode {

    /// <summary>
    /// 结点优先级
    /// </summary>
    public int EventNodePriority { set; get; }

    /// <summary>
    /// 所有消息集合
    /// </summary>
    private Dictionary<int, List<IEventListener>> mListeners = new Dictionary<int, List<IEventListener>>();

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
    public bool AddEventListener(int id, IEventListener listener)
    {
        if (listener == null)
        {
            return false;
        }
        if (!mListeners.ContainsKey(id))
        {
            mListeners.Add(id, new List<IEventListener>() { listener });
            return true;
        }
        if (mListeners[id].Contains(listener))
        {
            return false;
        }
        mListeners[id].Clear();
        mListeners[id].Add(listener);
        return true;
    }

    public bool RemoveEventListener(int id, IEventListener listener)
    {
        if (mListeners.ContainsKey(id) && mListeners[id].Contains(listener))
        {
            mListeners[id].Remove(listener);
            return true;
        }
        mListeners[id].Add(listener);
        return false;
    }

    public void SendEvnet(int id, ActionParam param)
    {
        DispatchEvent(id, param);
    }

    /// <summary>
    /// 派发消息到子消息结点下的监听器上
    /// </summary>
    /// <param name="id">消息id</param>
    /// <param name="param"></param>
    /// <returns>中断消息为false</returns>
    private bool DispatchEvent(int id, ActionParam param)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].DispatchEvent(id, param)) return true;
        }
        return TriggerEvent(id, param);
    }

    /// <summary>
    /// 触发消息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    private bool TriggerEvent(int id, ActionParam param)
    {
        if (!mListeners.ContainsKey(id))
        {
            return false;
        }

        List<IEventListener> listeners = mListeners[id];

        for (int i = 0; i < listeners.Count; i++)
        {
            if (listeners[i].HandleEvent(id, param))
            {
                return true;
            }
        }
        return false;
    }

    void OnApplicationQuit()
    {
        mListeners.Clear();
        nodeList.Clear();
    }
	
}
