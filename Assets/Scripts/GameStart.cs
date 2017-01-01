using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

    private string Url = "127.0.0.1:9001";

    void Start () {
        NetWriter.SetUrl(Url);
        SceneMgr.Instance.SceneSwitch(SceneState.SceneLogin);
	}

    void OnDestroy()
    {

        ActionParam param = new ActionParam();
        param["roomID"] = GameSetting.Instance.roomID;
        param["roomName"] = "";
        param["roomOperation"] = 2;

        Net.Instance.Send((int)ActionType.Room, null, param);

    }
	
}
