using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneGame : SceneBase {

    private Vector3 pos = new Vector3(420, -270, 0);

    private Text BrandNum;

    private static SceneGame _Instance;
    public static SceneGame Instance
    {
        get
        {
            return _Instance;
        }
    }

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

        skin.transform.Find("Guo").gameObject.SetActive(false);
        skin.transform.Find("Chi").gameObject.SetActive(false);
        skin.transform.Find("Peng").gameObject.SetActive(false);
        skin.transform.Find("Gang").gameObject.SetActive(false);
        skin.transform.Find("Ting").gameObject.SetActive(false);
        skin.transform.Find("Hu").gameObject.SetActive(false);
    }

    private bool GuoShow = true;

    public void showButton(string name)
    {
        if (GuoShow)
        {
            skin.transform.Find("Guo").gameObject.SetActive(true);
            GuoShow = false;
        }
        pos.x -= 100;
        Transform tf = skin.transform.Find(name);
        tf.gameObject.SetActive(true);
        tf.GetComponent<RectTransform>().localPosition = pos;
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
            LogicOfGame.Instance.RespondOperation(4);
        }
        else if (BtObject.name.Equals("Hu"))
        {
            LogicOfGame.Instance.RespondOperation(5);
        }
        else if (BtObject.name.Equals("Guo"))
        {
            LogicOfGame.Instance.RespondOperation(0);
        }

        GuoShow = true;

        pos = new Vector3(420, -270, 0);

        skin.transform.Find("Guo").gameObject.SetActive(false);
        skin.transform.Find("Chi").gameObject.SetActive(false);
        skin.transform.Find("Peng").gameObject.SetActive(false);
        skin.transform.Find("Gang").gameObject.SetActive(false);
        skin.transform.Find("Ting").gameObject.SetActive(false);
        skin.transform.Find("Hu").gameObject.SetActive(false);
    }

}
