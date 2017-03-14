using UnityEngine;
using UnityEngine.UI;

public class SceneLogin : SceneBase {

    private InputField PlayerName;

    public override void OnInit()
    {
        setSkinPath("UI/Scenes/" + SceneState.SceneLogin.ToString());
        base.OnInit();
    }

    protected override void OnInitData()
    {
        PlayerName = skin.transform.Find("PlayerName").GetComponent<InputField>();
    }

    protected override void onClick(GameObject BtObject)
    {
        if (BtObject.name.Equals("LoginBt"))
        {
            GameSetting.Instance.PlayerName = PlayerName.text;
            SceneMgr.Instance.SceneSwitch(SceneState.SceneRoom);
        }
    }

}
