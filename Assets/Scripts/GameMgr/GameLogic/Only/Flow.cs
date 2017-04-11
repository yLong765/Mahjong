using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour {
    
	void Start () {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);
        AloneLogic.Instance.InitDate();
        AloneLogic.Instance.InitTable();
        AloneLogic.Instance.InitHand();
        AloneLogic.Instance.HandDeal();
	}
	
	void Update () {
		
	}
}
