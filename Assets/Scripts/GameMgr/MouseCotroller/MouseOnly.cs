using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOnly : MonoBehaviour {

    private RaycastHit hit;

    public bool Chi = false;

    public bool Ting = false;

    #region 单例

    private static MouseOnly _Instance;
    public static MouseOnly Instance
    {
        get
        {
            return _Instance;
        }
    }

    #endregion

    void Awake()
    {
        _Instance = this;
    }

    void Update()
    {
        //射线定位
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!Chi && !Ting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>();
                    AloneLogic.Instance.MouseClick(mBrand);
                }
            }
            else if (Chi)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Chi = false;
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>(); ;
                    AloneLogic.Instance.ChangeAnimation(mBrand, true);
                }
                else
                {
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>();
                    AloneLogic.Instance.ChangeAnimation(mBrand, false);
                }
            }
            else if (Ting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ting = false;
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>(); ;
                    AloneLogic.Instance.TingAniamtion(mBrand, true);
                }
                else
                {
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>();
                    AloneLogic.Instance.TingAniamtion(mBrand, false);
                }
            }
        }
    }
}
