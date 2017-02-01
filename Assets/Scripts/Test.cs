using UnityEngine;
using System.Collections.Generic;
using System;

public class Test :MonoBehaviour
{

    /// <summary>
    /// 吃碰杠位置
    /// </summary>
    private Vector3[] Oppos = new Vector3[4];
    /// <summary>
    /// 吃碰杠角度
    /// </summary>
    private Vector3[] Oprot = new Vector3[4];
    /// <summary>
    /// 吃碰杠修改坐标
    /// </summary>
    private Vector3[] OpC = new Vector3[4];

    void Start()
    {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);

    }

}
