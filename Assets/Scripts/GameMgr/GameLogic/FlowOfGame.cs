using UnityEngine;
using System.Collections;

public class FlowOfGame : MonoBehaviour {

    #region Instance

    private static FlowOfGame _Instance;

    public static FlowOfGame Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_FlowOfGame").AddComponent<FlowOfGame>();
            }
            return _Instance;
        }
    }

    #endregion

    void Awake()
    {
        _Instance = this;
    }

    void Start()
    {
        LogicOfGame.Instance.InitDate();
    }

    public void Init()
    {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);
        LogicOfGame.Instance.InitTable();
        LogicOfGame.Instance.HandDealInWeb();
    }

    public bool HandDeal = false;
    public bool Deal = false;
    public bool CanDeal = false;

    void Update()
    {
        if (HandDeal)
        {
            HandDeal = false;
            LogicOfGame.Instance.HandDeal();
        }
        if (Deal)
        {
            Deal = false;
            CanDeal = true;
            LogicOfGame.Instance.DealInWeb();
        }
    }

}
