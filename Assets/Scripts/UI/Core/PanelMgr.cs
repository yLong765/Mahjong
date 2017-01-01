using UnityEngine;
using System.Collections;
using System;

public class PanelMgr : MonoBehaviour {

    #region 单例

    private static PanelMgr _Instance = new PanelMgr();
    public static PanelMgr Instance
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
    /// 打开Panel
    /// </summary>
    /// <param name="ps">panel类型</param>
    public void OpenPanel(PanelState ps)
    {
        string name = ps.ToString();
        GameObject panel = new GameObject(name);
        panel.transform.parent = parentObj;
        panel.transform.localEulerAngles = Vector3.zero;
        panel.transform.localPosition = Vector3.zero;
        panel.transform.localScale = Vector3.one;
        PanelBase pb = panel.AddComponent(Type.GetType(name)) as PanelBase;
        pb.OnInit();
    }

}
