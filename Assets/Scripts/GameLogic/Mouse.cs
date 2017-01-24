using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    private RaycastHit hit;
    private GameObject MouseObject;

    public bool Chi = false;

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

    /// <summary>
    /// 返回选中物体
    /// </summary>
    /// <returns>选中物体</returns>
    public GameObject getObject()
    {
        return MouseObject;
    }

    void Update()
    {
        //射线定位
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!Chi)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MouseObject = hit.collider.gameObject;
                    LogicOfGame.Instance.MouseClick(MouseObject);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    MouseObject = hit.collider.gameObject;
                    LogicOfGame.Instance.ChangeAnimation(MouseObject, true);
                }
                else
                {
                    MouseObject = hit.collider.gameObject;
                    LogicOfGame.Instance.ChangeAnimation(MouseObject, false);
                }
            }
        }
    }

}
