using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneRoom : SceneBase {

    private Transform Content;
    private Transform Viewport;
    private GameObject id;
    private int roomID = 1;

    public override void OnInit()
    {
        setSkinPath("UI/Scenes/" + SceneState.SceneRoom.ToString());
        base.OnInit();
    }

    protected override void OnInitData()
    {
        Viewport = skin.transform.Find("Scroll View").Find("Viewport");
        Content = Viewport.Find("Content");
        id = skin.transform.Find("id").gameObject;
        roomID = 1;

        ActionParam param = new ActionParam();
        param["roomID"] = roomID;

        Net.Instance.Send(2006, callback, param);
    }

    protected override void onClick(GameObject BtObject)
    {
        if (BtObject.name.Equals("joinBt"))
        {
            Debug.Log("Join");
        }
        if (BtObject.name.Equals("createBt"))
        {
            PanelMgr.Instance.OpenPanel(PanelState.PanelCreateRoom);
        }
        if (BtObject.name.Equals("Btback"))
        {
            int roomID = int.Parse(BtObject.transform.parent.gameObject.name);
            string roomName = BtObject.transform.parent.Find("roomName").GetComponent<Text>().text;

            ActionParam param = new ActionParam();
            param["roomID"] = roomID;
            param["roomName"] = roomName;
            param["roomOperation"] = 1;

            Net.Instance.Send((int)ActionType.Room, JoinCallBack, param);

        }
    }

    private void JoinCallBack(ActionResult actionResult)
    {
        if (actionResult.Get<int>("callback") != -1)
        {
            GameSetting.Instance.roomID = actionResult.Get<int>("callback");
            SceneMgr.Instance.SceneSwitch(SceneState.SceneInRoom);
        }
    }

    private void callback(ActionResult actionResult)
    {
        string roomName = actionResult.Get<string>("roomName");
        int size = actionResult.Get<int>("size");
        CreateRoom(roomName,size);
    }

    private void CreateRoom(string roomName, int size)
    {
        if (size != 0)
        {
            GameObject room = Instantiate(id);
            room.transform.Find("roomName").GetComponent<Text>().text = roomName;
            room.transform.Find("peopleSize").GetComponent<Text>().text = size.ToString() + "人";
            room.name = roomID.ToString();
            room.transform.parent = Content;
            room.transform.localPosition = new Vector3(0, -(roomID - 1) * 60, 0);
            room.transform.localScale = Vector3.one;
            room.transform.localEulerAngles = Vector3.zero;
            room.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50);
            room.SetActive(true);
            EventTriggerListener.Get(room.transform.Find("Btback").gameObject).onClick = onClick;
        }
        roomID++;
        if (roomID < 10)
        {
            ActionParam param = new ActionParam();
            param["roomID"] = roomID;
            Net.Instance.Send(2006, callback, param);
        }
    }

}
