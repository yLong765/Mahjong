using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

    private RaycastHit hit;
    private GameObject MouseObject;

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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                MouseObject = hit.collider.gameObject;
                LogicOfGame.Instance.MouseClick(MouseObject);
            }
        }
    }

}
