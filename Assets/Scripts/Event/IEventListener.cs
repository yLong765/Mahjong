using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消息监听接口
/// </summary>
public interface IEventListener {

    /// <summary>
    /// 处理消息
    /// </summary>
    /// <param name="id">消息id</param>
    /// <param name="param">参数</param>
    /// <returns>是否终止消息派发</returns>
    bool HandleEvent(int id, ActionParam param);

}
