using UnityEngine;
using System.Collections.Generic;
using System;

public class Test :MonoBehaviour
{

    Player player = new Player();

    void Start()
    {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);
        player.Add(12);
        player.Add(12);
        player.Add(13);
        player.Add(14);
        player.Add(24);
        if (player.isTing()) Debug.Log("Ting");
        if (player.isHu(24)) Debug.Log("Hu");
    }



}
