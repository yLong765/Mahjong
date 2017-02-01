using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UItest : MonoBehaviour {

    private InputField roomID;
    private InputField Action;
    private Button Begin;
    private Button Ready;
    private Text result;
    private string Url = "127.0.0.1:9001";

    void Awake()
    {
        roomID = GameObject.Find("roomID").GetComponent<InputField>();
        Action = GameObject.Find("Action").GetComponent<InputField>();
        Begin = GameObject.Find("Begin").GetComponent<Button>();
        Ready = GameObject.Find("Ready").GetComponent<Button>();
        result = GameObject.Find("Result").GetComponent<Text>();
        Begin.onClick.AddListener(Click);
        Ready.onClick.AddListener(ReadyClick);
    }

    private void ReadyClick()
    {
        NetWriter.SetUrl(Url);
        GameSetting.Instance.roomID = int.Parse(roomID.text);
        Net.Instance.Send(2002, RoomCallback, null);
    }

    private void Click()
    {
        GameSetting.Instance.roomID = int.Parse(roomID.text);
        NetWriter.SetUrl(Url);

        if (Action.text.Equals("2001"))
        {
            for (int i = 0; i < 14; i++)
            {
                Net.Instance.Send(int.Parse(Action.text), RoomCallback, null);
            }

        }
        else
        {
            Net.Instance.Send(int.Parse(Action.text), RoomCallback, null);
        }
    }

    private void RoomCallback(ActionResult actionResult)
    {
        Debug.Log(actionResult.Get<int>("brand"));
    }

}
