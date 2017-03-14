using UnityEngine;
using System.Collections;

public class hyalinizeTween : MonoBehaviour {

    public Material Target;

    public float speed = 0.05f;

    public bool Up = false;

    public bool Down = false;

	void Start () {
        Target = ResourceMgr.Instance.LoadResource<Material>("Material/UIMaterial", true);
        if (Up)
        {
            Color color = Target.GetColor("_Color");
            color.a = 0.1f;
            Target.SetColor("Color", color);
        }
	}
	
	void Update () {
        if (Up)
        {
            Color color = Target.GetColor("_Color");
            if (color.a < 1) color.a += speed * Time.deltaTime;
            else Up = false;
            Target.SetColor("_Color", color);
        }
        if (Down)
        {
            Color color = Target.GetColor("_Color");
            if (color.a > 0) color.a -= speed * Time.deltaTime;
            Target.SetColor("_Color", color);
        }
	}
}
