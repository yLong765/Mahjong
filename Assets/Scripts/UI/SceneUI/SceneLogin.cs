using UnityEngine;
using UnityEngine.UI;

public class SceneLogin : SceneBase {

    private InputField UserName;
    private InputField PassWord;


    public override void OnInit()
    {
        setSkinPath("UI/Scenes/" + SceneState.SceneLogin.ToString());
        base.OnInit();
    }

    protected override void OnInitData()
    {
        UserName = skin.transform.Find("UserName").GetComponent<InputField>();
        PassWord = skin.transform.Find("PassWord").GetComponent<InputField>();
    }

    protected override void onClick(GameObject BtObject)
    {
        if (BtObject.name.Equals("LoginBt"))
        {
            ActionParam param = new ActionParam();
            param["UserName"] = UserName.text;
            param["PassWord"] = PassWord.text;

            Net.Instance.Send(2005, callback, param);
        }
    }

    private void callback(ActionResult actionResult)
    {
        int s = actionResult.Get<int>("isSwitch");
        if (s == 1) Swith(true); else Swith(false);
    }

    private void Swith(bool admit)
    {
        if (admit)
        {
            SceneMgr.Instance.SceneSwitch(SceneState.SceneRoom);
        }
        else 
        {
            Debug.LogError("用户名密码错误");
        }
    }

}
