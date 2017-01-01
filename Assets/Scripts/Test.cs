using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    public GameObject Object;

    private Vector3 pos = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGame);
        LogicOfGame.Instance.Operation(0);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
