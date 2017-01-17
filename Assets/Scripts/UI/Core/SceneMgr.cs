using UnityEngine;
using System;

public class SceneMgr {

    #region 单例

    private static SceneMgr _Instance = new SceneMgr();
    public static SceneMgr Instance
    {
        get
        {
            return _Instance;
        }
    }

    #endregion

    /// <summary>
    /// 默认父类
    /// </summary>
    private Transform parentObj = GameObject.Find("Canvas").transform;

    /// <summary>
    /// 上一个界面
    /// </summary>
    private GameObject pervious = null;

    /// <summary>
    /// 切换Scene
    /// </summary>
    /// <param name="state">Scene</param>
    public void SceneSwitch(SceneState state)
    {
        string name = state.ToString();
        GameObject scene = new GameObject(name);
        scene.transform.parent = parentObj;
        scene.transform.localEulerAngles = Vector3.zero;
        scene.transform.localPosition = Vector3.zero;
        scene.transform.localScale = Vector3.one;
        SceneBase sb = scene.AddComponent(Type.GetType(name)) as SceneBase;
        sb.OnInit();

        if (pervious != null)
        {
            UnityEngine.Object.Destroy(pervious);
        }
        pervious = scene;
    }

}
