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

            WebLogic.Instance.Send((int)ActionType.Room, param);
        }
        if (BtObject.name.Equals("cancleBt") || BtObject.name.Equals("OtherPanel"))
        {
            XzoomTween x = GetComponentInChildren<XzoomTween>();
            x.isBig = false;
            x.isSmall = true;
            Destroy(gameObject, 2f);
        }
    }

    protected override bool Event(ActionParam param)
    {
        int id = (int)param["ActionType"];

        if (id == (int)ActionType.Room)
        {
            int b = -1;
            if ((b = (int)param["success"]) != -1)
            {
                GameSetting.Instance.roomID = b;
                GameSetting.Instance.roomName = roomName.text;
                XzoomTween x = GetComponentInChildren<XzoomTween>();
                x.isBig = false;
                x.isSmall = true;
                Destroy(gameObject, 1f);
                SceneMgr.Instance.SceneSwitch(SceneState.SceneInRoom);
            }
        }

        return false;
    }

}
