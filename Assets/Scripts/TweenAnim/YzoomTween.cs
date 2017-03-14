using UnityEngine;
using System.Collections;

public class YzoomTween : MonoBehaviour {

    public GameObject Target;

    public float speed = 2f;

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
                Target.transform.localScale = new Vector3(1, 0, 1);
            }
        }
    }
	
	void Update () {
        if (Target.transform.localScale.y <= 0.95f)
            Target.transform.localScale = Vector3.Lerp(Target.transform.localScale, Vector3.one, speed * Time.deltaTime);
        else
        {
            Target.transform.localScale = Vector3.one;
        }
	}
}
