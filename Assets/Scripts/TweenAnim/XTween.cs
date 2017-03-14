using UnityEngine;
using System.Collections;

public class XTween : MonoBehaviour {

    public GameObject Target;

    private Vector2 pos;

    public float speed = 3f;

    public bool isBig = false;

    public bool isSmall = false;

    // Use this for initialization
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
        if (isBig)
        {
            pos = new Vector2(250f, 96f);
        }
        if (isSmall)
        {
            pos = new Vector2(119f, 96f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (isBig)
        {
            pos = new Vector2(250f, 96f);
        }
        if (isSmall)
        {
            pos = new Vector2(119f, 96f);
        }
        if (isBig || isSmall)
            Target.GetComponent<RectTransform>().sizeDelta = Vector2.Lerp(Target.GetComponent<RectTransform>().sizeDelta, pos, speed * Time.deltaTime);
	}
}
