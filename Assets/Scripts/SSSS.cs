using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSSS : MonoBehaviour {

    public GameObject cube;

	// Use this for initialization
	void Start () {
        Test T = cube.AddComponent<TTTT>() as Test;
        T.GG();
        T = cube.AddComponent<EEEE>() as Test;
        T.GG();
    }
}
