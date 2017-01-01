using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelCreateRoom : PanelBase {

    private InputField roomName;


    public override void OnInit()
    {
        setSkinPath("UI/Panels/" + PanelState.PanelCreateRoom.ToString());
        base.OnInit();
    }

    protected override void OnInitData()
    {
        roomName = skin.transform.Find("MainPanel").Find("roomName").GetComponent<InputField>();
    }

    protected override void onClick(GameObject BtObject)
    {
        if (BtObject.name.Equals("ensureBt"))
        {
            ActionParam param = new ActionParam();
            param["roomID"] = -1;
            param["roomName"] = roomName.text;
            param["roomOperation"] = 1;

            Net.Instance.Send(2000, callback, param);
        }
        if (BtObject.name.Equals("cancleBt") || BtObject.name.Equals("OtherPanel"))
        {
            XzoomTween x = GetComponentInChildren<XzoomTween>();
            x.isBig = false;
            x.isSmall = true;
            Destroy(gameObject, 2f);
        }
    }

    private void callback(ActionResult actionResult)
    {
        int b = -1;
        if ((b = (int)actionResult["callback"]) != -1)
        {
            
            GameSetting.Instance.roomID = b;
            XzoomTween x = GetComponentInChildren<XzoomTween>();
            x.isBig = false;
            x.isSmall = true;
            Destroy(gameObject, 1f);
            SceneMgr.Instance.SceneSwitch(SceneState.SceneInRoom);
        }
    }

}
