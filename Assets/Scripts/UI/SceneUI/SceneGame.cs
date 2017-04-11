using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneGame : SceneBase {

    private static SceneGame _Instance;
    public static SceneGame Instance { get { return _Instance; } }

    /// <summary>
    /// 初始化按钮位置
    /// </summary>
    private Vector3 pos = new Vector3(420, -250, 0);

    /// <summary>
    /// 剩余牌数TextUI
    /// </summary>
    private Text BrandNum;

    protected override void OnAwake()
    {
        _Instance = this;
    }

    public override void OnInit()
    {
        setSkinPath("UI/Scenes/" + SceneState.SceneGame.ToString());
        base.OnInit();
    }

    protected override void OnInitData()
    {
        BrandNum = skin.transform.Find("BrandNum").GetComponent<Text>();
        poss[0] = new Vector3(-5, -150, 0);
        poss[1] = new Vector3(200, 50, 0);
        poss[2] = new Vector3(-5, 230, 0);
        poss[3] = new Vector3(-200, 50, 0);

        NoActive();
    }

    public void NoActive()
    {
        skin.transform.Find("Guo").gameObject.SetActive(false);
        skin.transform.Find("Chi").gameObject.SetActive(false);
        skin.transform.Find("Peng").gameObject.SetActive(false);
        skin.transform.Find("Gang").gameObject.SetActive(false);
        skin.transform.Find("Ting").gameObject.SetActive(false);
        skin.transform.Find("Hu").gameObject.SetActive(false);

        pos = new Vector3(420, -250, 0);
    }

    public void showButton(string name)
    {
        skin.transform.Find("Guo").gameObject.SetActive(true);

        pos.x -= 100;
        Transform tf = skin.transform.Find(name);
        tf.gameObject.SetActive(true);
        tf.GetComponent<RectTransform>().localPosition = pos;
    }

    private Vector3[] poss = new Vector3[4];

    public void showOpB(string name,int id)
    {
        Transform tf = skin.transform.Find(name);
        tf.gameObject.SetActive(true);
        tf.GetComponent<RectTransform>().localPosition = poss[id];
        tf.GetComponent<RectTransform>().localScale = Vector3.one * 1.2f;
        StartCoroutine(Jtime(tf));
    }

    IEnumerator Jtime(Transform tf)
    {
        yield return new WaitForSeconds(1f);
        tf.GetComponent<RectTransform>().localScale = Vector3.one * 0.7f;
        tf.gameObject.SetActive(false);
    }

    public void ChangePS()
    {
        int t = int.Parse(BrandNum.text) - 1;
        BrandNum.text = t.ToString();
    }

    protected override void onClick(GameObject BtObject)
    {
        if (BtObject.name.Equals("Chi"))
        {
            LogicOfGame.Instance.ChangeChi();
        }
        else if (BtObject.name.Equals("Peng"))
        {
            LogicOfGame.Instance.RespondOperation(2);
        }
        else if (BtObject.name.Equals("Gang"))
        {
            LogicOfGame.Instance.RespondOperation(3);
        }
        else if (BtObject.name.Equals("Ting"))
        {
            LogicOfGame.Instance.ChangeTing();
        }
        else if (BtObject.name.Equals("Hu"))
        {
            LogicOfGame.Instance.RespondOperation(5);
        }
        else if (BtObject.name.Equals("Guo"))
        {
            LogicOfGame.Instance.RespondOperation(0);
        }

        NoActive();
    }

}
