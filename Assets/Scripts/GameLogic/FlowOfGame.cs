using UnityEngine;
using System.Collections;

public class FlowOfGame : MonoBehaviour {

    private bool isHandDeal; //是否发全部手牌
    private bool isDeal; //是否发牌

    void Start()
    {
        isHandDeal = true;
        isDeal = false;
        Invoke("Init", 1f);
    }

    private void Init()
    {
        LogicOfGame.Instance.InitTable();
        if (isHandDeal)
        {
            Invoke("InitHandDeal", 0.5f);
        }
    }

    private void InitHandDeal()
    {
        LogicOfGame.Instance.HandDeal();
        isDeal = true;
    }

    void Update()
    {
        if (isDeal)
        {
            LogicOfGame.Instance.Deal(18);
            LogicOfGame.Instance.ShowOperation(18);
            isDeal = false;
        }
    }

}
