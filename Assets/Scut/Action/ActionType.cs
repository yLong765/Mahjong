using System;

public enum ActionType
{
    /// <summary>
    /// 房间操作
    /// </summary>
    Room = 2000,

    /// <summary>
    /// 逻辑操作
    /// </summary>
    Logic = 2001,

    /// <summary>
    /// 广播出牌
    /// </summary>
    RadioBrand = 2002,

    /// <summary>
    /// 相应玩家操作等级
    /// </summary>
    RespondOperation = 2003,

    /// <summary>
    /// 获取玩家id
    /// </summary>
    GetPlayerId = 2004,

    /// <summary>
    /// 接收出牌
    /// </summary>
    brandRadioResult = 3000,

    /// <summary>
    /// 接收房间信息
    /// </summary>
    RoomMessageRadioResult = 3001,

    /// <summary>
    /// 登陆
    /// </summary>
    Login = 2005,

    /// <summary>
    /// 获取房间信息
    /// </summary>
    RoomMessage = 2006,

    GameInit = 2007,

    /// <summary>
    /// 玩家id广播
    /// </summary>
    playerIdRadio = 3002,
}
