using UnityEngine;
using System.Collections.Generic;
using System;

public class Test : MonoBehaviour {

    public GameObject Object;

    private Vector3 pos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);

        MyPlayer.Instance.Add(0);
        MyPlayer.Instance.Add(1);
        MyPlayer.Instance.Add(2);
        MyPlayer.Instance.Add(10);
        MyPlayer.Instance.Add(11);
        MyPlayer.Instance.Add(12);
        MyPlayer.Instance.Add(20);
        MyPlayer.Instance.Add(21);
        MyPlayer.Instance.Add(22);
        MyPlayer.Instance.Add(34);
        MyPlayer.Instance.Add(34);
        MyPlayer.Instance.Add(34);
        MyPlayer.Instance.Add(18);

        //LogicOfGame.Instance.RespondOperation(0);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
