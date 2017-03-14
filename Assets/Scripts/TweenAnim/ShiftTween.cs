using UnityEngine;
using System.Collections;

public class ShiftTween : MonoBehaviour {

    public GameObject Target;

    public float speed;

    public Vector3 EndPos;

	void Start () {
        if (Target == null)
        {
            if (gameObject == null)
            {
                Debug.LogError("Target null");
            }
            else
            {
                Target = gameObject;
            }
        }
	}
	
	void Update () {
        Target.transform.localPosition = Vector3.Lerp(Target.transform.localPosition, EndPos, speed * Time.deltaTime);
	}
}
