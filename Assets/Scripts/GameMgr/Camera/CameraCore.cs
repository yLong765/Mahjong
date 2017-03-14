using UnityEngine;
using System.Collections;

public class CameraCore : MonoBehaviour {

    private Vector3 OnePos;
    private Quaternion OneRot;

    private Vector3 TwoPos;
    private Vector3 TwoRot;

	
	void Start () {
        OnePos = new Vector3(0.02f, 2.624f, -0.815f);
        OneRot = Quaternion.Euler(new Vector3(60, 0, 0));
	}
	
	
	void Update () {
	    
	}
}
