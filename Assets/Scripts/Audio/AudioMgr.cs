using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour {

    private static AudioMgr _Instance;
    public static AudioMgr Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject go = new GameObject("_AudioMgr");
                DontDestroyOnLoad(go);
                _Instance = go.AddComponent<AudioMgr>();
            }
            return _Instance;
        }
    }

    private AudioSource AS;

    void Start()
    {
        AS = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 出牌音效
    /// </summary>
    /// <param name="num"></param>
    public void showAudio(int num)
    {
        int nump = num;
        string ppp = "";
        if (num < 10)
        {
            nump += 1;
            ppp = "wan";
        }
        else if (num >= 10 && num < 20)
        {
            nump = nump - 9;
            ppp = "tiao";
        }

        else if (num >= 20 && num < 30)
        {
            nump = nump - 19;
            ppp = "tong";
        }

        ppp = nump.ToString() + ppp;
        if (num == 30) ppp = "dongfeng";
        else if (num == 31) ppp = "nanfeng";
        else if (num == 32) ppp = "xifeng";
        else if (num == 33) ppp = "beifeng";
        else if (num == 34) ppp = "zhong";
        else if (num == 35) ppp = "fa";
        else if (num == 36) ppp = "bai";
        AS.clip = ResourceMgr.Instance.CreateSound("Sound/" + ppp, true);
        AS.Play();
    }

    /// <summary>
    /// 对局开始
    /// </summary>
    public void gameStart()
    {
        //AS.clip = ResourceMgr.Instance.CreateSound("Sound/duijukaishi", true);
        //AS.Play();
    }

    public void opSound(int level)
    {
        switch(level)
        {
            case 1:
                AS.clip = ResourceMgr.Instance.CreateSound("Sound/chi", true);
                break;
            case 2:
                AS.clip = ResourceMgr.Instance.CreateSound("Sound/peng", true);
                break;
            case 3:
                AS.clip = ResourceMgr.Instance.CreateSound("Sound/gang", true);
                break;
            case 4:
                AS.clip = ResourceMgr.Instance.CreateSound("Sound/ting2", true);
                break;
            case 5:
                AS.clip = ResourceMgr.Instance.CreateSound("Sound/hu", true);
                break;
        }
        if (level > 0) AS.Play();
    }

}
