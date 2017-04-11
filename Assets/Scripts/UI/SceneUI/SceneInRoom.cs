using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneInRoom : SceneBase {

    Dictionary<int, Text> playersName = new Dictionary<int, Text>();
    Dictionary<int, Image> playersBack = new Dictionary<int, Image>();

    /// <summary>
    /// 房间名
    /// </summary>
    private Text roomName;

    /// <summary>
    /// 背景图
    /// </summary>
    private Image BgImage;

    public override void OnInit()
    {
        setSkinPath("UI/Scenes/" + SceneState.SceneInRoom.ToString());
        base.OnInit();
    }

    protected override void OnInitData()
    {
        for (int i = 1; i < 5; i++)
        {
            playersBack[i - 1] = skin.transform.Find("playerback" + i).GetComponent<Image>();
            playersName[i - 1] = playersBack[i - 1].transform.Find("Name").GetComponent<Text>();
        }
        BgImage = skin.transform.Find("BgImage").GetComponent<Image>();
        roomName = skin.transform.Find("roomName").GetComponent<Text>();
    }

    int playernum = 0;

    protected override bool Event(ActionParam param)
    {
        int id = (int)param["ActionType"];

        if (id == (int)ActionType.RoomMessageRadioResult)
        {
            playernum = 0;
            UpdateData(0, (string)param["player1"]);
            UpdateData(1, (string)param["player2"]);
            UpdateData(2, (string)param["player3"]);
            UpdateData(3, (string)param["player4"]);
            roomName.text = "房间:" + GameSetting.Instance.roomName;
            if (playernum == 4)
            {
                playernum = 0;

                BgImage.gameObject.SetActive(true);

                ActionParam Param = new ActionParam();
                Param["roomID"] = GameSetting.Instance.roomID;

                WebLogic.Instance.Send((int)ActionType.GameInit, Param);

                AudioMgr.Instance.gameStart();

                Invoke("Switch", 2f);
            }
        }

        return false;
    }

    private void UpdateData(int id, string name)
    {
        if (!name.Equals("1"))
        {
            playernum++;
            playersName[id].text = name;
            playersName[id].gameObject.SetActive(true);
            playersBack[id].GetComponent<XTween>().isBig = true;
            playersBack[id].GetComponent<XTween>().isSmall = false;
        }
        else
        {
            playersName[id].text = "";
            playersName[id].gameObject.SetActive(false);
            playersBack[id].GetComponent<XTween>().isBig = false;
            playersBack[id].GetComponent<XTween>().isSmall = true;
        }
    }

    private void Switch()
    {
        SceneManager.Instance.SceneSwitch(SceneType.Type.Game);
    }
}
