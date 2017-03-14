using UnityEngine;
using System.Collections;

public class XzoomTween : MonoBehaviour {

    public GameObject Target;

    public float speed = 3f;

    public bool isBig = false;

    public bool isSmall = false;

    void Start()
    {
        if (Target == null)
        {
            if (gameObject == null)
            {
                Debug.LogError("Target null");
            }
            else
            {
                Target = gameObject;
                if (isBig)
                    Target.transform.localScale = new Vector3(0, 1, 1);
                if (isSmall)
                    Target.transform.localScale = Vector3.one;
            }
        }
    }

    void Update()
    {
        if (isBig)
        {
            if (Target.transform.localScale.x <= 0.95f)
                Target.transform.localScale = Vector3.Lerp(Target.transform.localScale, Vector3.one, speed * Time.deltaTime);
            else
            {
                Target.transform.localScale = Vector3.one;
            }
        }

        if (isSmall)
        {
            if (Target.transform.localScale.x >= 0.05f)
                Target.transform.localScale = Vector3.Lerp(Target.transform.localScale, new Vector3(0, 1, 1), speed * Time.deltaTime);
            else
            {
                Target.transform.localScale = Vector3.zero;
            }
        }
    }
}
