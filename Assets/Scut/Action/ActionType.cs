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


}
