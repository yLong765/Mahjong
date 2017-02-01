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
        Init();
    }

    private void Init()
    {
        LogicOfGame.Instance.InitDate();
    }

    public void InitTable()
    {
        LogicOfGame.Instance.InitTable();
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);
        Invoke("InitHandDeal", 0.5f);
    }

    private void InitHandDeal()
    {
        LogicOfGame.Instance.HandDealInWeb();
        if (GameSetting.Instance.target != GameSetting.Instance.Playerid)
            LogicOfGame.Instance.otherDeal();
    }

    public bool Deal = false;
    public bool DealDone = false;
    public bool InitDone = false;

    void Update()
    {
        if (Deal)
        {
            LogicOfGame.Instance.DealInWeb();
            Deal = false;
            InitDone = true;
        }
        if (DealDone && InitDone)
        {
            LogicOfGame.Instance.Deal();
            DealDone = false;
        }
        else
        {
            DealDone = false;
        }
    }

}
