using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class UIBase : MonoBehaviour {

    /// <summary>
    /// 皮肤对象
    /// </summary>
    private GameObject _skin;
    /// <summary>
    /// 皮肤公开
    /// </summary>
    public GameObject skin
    {
        get
        {
            return _skin;
        }
        set
        {
            _skin = value;
        }
    }

    /// <summary>
    /// 皮肤地址
    /// </summary>
    private string skinPath = null;
    /// <summary>
    /// 设置皮肤地址
    /// </summary>
    /// <param name="Path"></param>
    public void setSkinPath(string Path)
    {
        skinPath = Path;
    }

    void Awake() { OnAwake(); }
    void Start() { OnStart(); }
    void Update() { OnUpdate(); }
    void OnDestroy() { }

    protected void Init()
    {
        InitSkin();

        Button[] bts = transform.Find("").GetComponentsInChildren<Button>();
        foreach (Button bt in bts)
        {
            EventTriggerListener.Get(bt.gameObject).onClick = onClick;
        }

        OnInitData();
    }

    #region 虚方法

    /// <summary>
    /// 初始化数据
    /// </summary>
    protected virtual void OnInitData() { }
    /// <summary>
    /// 点击事件
    /// </summary>
    protected virtual void onClick(GameObject BtObject) { }
    /// <summary>
    /// Awake
    /// </summary>
    protected virtual void OnAwake() { }
    /// <summary>
    /// Start
    /// </summary>
    protected virtual void OnStart() { }
    /// <summary>
    /// Update
    /// </summary>
    protected virtual void OnUpdate() { }
    /// <summary>
    /// 初始化皮肤
    /// </summary>
    protected virtual void InitSkin()
    {
        if (!string.IsNullOrEmpty(skinPath))
        {
            skin = ResourceMgr.Instance.CreateGameObject(skinPath, false);
        }
        skin.transform.parent = transform;
        skin.transform.localEulerAngles = Vector3.zero;
        skin.transform.localPosition = Vector3.zero;
        skin.transform.localScale = Vector3.one;
    }

    #endregion

}
