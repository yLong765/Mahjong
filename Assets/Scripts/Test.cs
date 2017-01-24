using UnityEngine;
using System.Collections.Generic;
using System;

public class Test :MonoBehaviour
{
   
    void Start()
    {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);

        MyPlayer.Instance.Add(1);
        MyPlayer.Instance.Add(1);
        MyPlayer.Instance.Add(1);
        MyPlayer.Instance.Add(2);
        MyPlayer.Instance.Add(2);
        MyPlayer.Instance.Add(2);
        MyPlayer.Instance.Add(3);
        MyPlayer.Instance.Add(3);
        MyPlayer.Instance.Add(3);
        MyPlayer.Instance.Add(4);
        MyPlayer.Instance.Add(4);
        MyPlayer.Instance.Add(11);
        MyPlayer.Instance.Add(12);
        //MyPlayer.Instance.Add(13);
    }

}
