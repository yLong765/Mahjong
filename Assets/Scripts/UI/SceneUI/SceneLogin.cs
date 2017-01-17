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

            WebLogic.Instance.Send((int)ActionType.Login, param);
        }
    }

    protected override bool Event(ActionParam param)
    {
        Debug.Log("Login");
        int id = (int)param["ActionType"];

        if (id == (int)ActionType.Login)
        {
            int admit = (int)param["success"];

            switch (admit)
            {
                case 1: //登陆成功
                    SceneMgr.Instance.SceneSwitch(SceneState.SceneRoom);
                    break;
                default:
                    Debug.LogError("用户名密码错误");
                    break;
            }
        }

        return false;
    }

}
