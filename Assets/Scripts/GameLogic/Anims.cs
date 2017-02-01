using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anims {

    #region 单例

    private static Anims _Instance = new Anims();

    public static Anims Instance
    {
        get
        {
            return _Instance;
        }
    }

    #endregion

    #region 定义

    /// <summary>
    /// 吃碰杠位置
    /// </summary>
    private Vector3[] opPos = new Vector3[4];
    /// <summary>
    /// 吃碰杠角度
    /// </summary>
    private Vector3[] opRot = new Vector3[4];
    /// <summary>
    /// 吃碰杠修改坐标
    /// </summary>
    private Vector3[] opC = new Vector3[4];



    /// <summary>
    /// 出牌位置
    /// </summary>
    private Vector3[] showPos = new Vector3[4];
    /// <summary>
    /// 出牌修改坐标
    /// </summary>
    private Vector3[] showC = new Vector3[4];


    /// <summary>
    /// 发牌位置
    /// </summary>
    private Vector3[] DealPos = new Vector3[4];
    /// <summary>
    /// 发牌角度
    /// </summary>
    private Vector3[] DealRot = new Vector3[4];

    #endregion

    public Anims()
    {
        InitDate();
    }

    /// <summary>
    /// 初始化所有数据
    /// </summary>
    public void InitDate()
    {

    }

}
