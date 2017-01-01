using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneGame : SceneBase {

    private Vector3 pos = new Vector3(420, -330, 0);

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

    public void showButton(string name)
    {
        pos.x -= 120;
        Transform tf = skin.transform.Find(name);
        tf.gameObject.SetActive(true);
        tf.GetComponent<RectTransform>().localPosition = pos;
        
    }

    protected override void onClick(GameObject BtObject)
    {
        if (BtObject.name.Equals("Chi"))
        {
            
        }
        else if (BtObject.name.Equals("Peng"))
        {

        }
        else if (BtObject.name.Equals("Gang"))
        {
            
        }
        else if (BtObject.name.Equals("Ting"))
        {

        }
        else if (BtObject.name.Equals("Hu"))
        {

        }
        else if (BtObject.name.Equals("Guo"))
        {

        }
    }

}
