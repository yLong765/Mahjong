using UnityEngine;
using System.Collections;

public class UpZoomTween : MonoBehaviour {

    public GameObject Target;

    public float speed = 1.5f;

    void Start()
    {
        if (Target == null)
        {
            if (gameObject == null)
            {
                Debug.LogError("Taget null");
            }
            else
            {
                Target = gameObject;
                Target.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            Target.transform.localScale = Vector3.zero;
        }
    }
	
	void Update () {
        Target.transform.localScale = Vector3.Lerp(Target.transform.localScale, Vector3.one, speed * Time.deltaTime);
	}
}
