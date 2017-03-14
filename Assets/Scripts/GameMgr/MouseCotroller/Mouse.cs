using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    private RaycastHit hit;

    public bool Chi = false;

    public bool Ting = false;

    #region 单例

    private static Mouse _Instance;
    public static Mouse Instance
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
                    LogicOfGame.Instance.MouseClick(mBrand);
                }
            }
            else if (Chi)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Chi = false;
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>(); ;
                    LogicOfGame.Instance.ChangeAnimation(mBrand, true);
                }
                else
                {
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>();
                    LogicOfGame.Instance.ChangeAnimation(mBrand, false);
                }
            }
            else if (Ting)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Ting = false;
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>(); ;
                    LogicOfGame.Instance.TingAniamtion(mBrand, true);
                }
                else
                {
                    Brand mBrand = hit.collider.gameObject.GetComponent<Brand>();
                    LogicOfGame.Instance.TingAniamtion(mBrand, false);
                }
            }
        }
    }

}
