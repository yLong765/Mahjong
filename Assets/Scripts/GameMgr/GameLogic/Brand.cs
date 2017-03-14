using UnityEngine;
using System.Collections;

public class Brand : MonoBehaviour{
    
    /// <summary>
    /// 牌ID
    /// </summary>
    private int brandId;
    /// <summary>
    /// 公开牌ID
    /// </summary>
    public int id { get { return brandId; } }

    /// <summary>
    /// 公开牌GameObject
    /// </summary>
    public GameObject Object { get { return gameObject; } }

    #region 牌自身操作

    /// <summary>
    /// 无ID初始化
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="rot"></param>
    /// <param name="sca"></param>
    public void Init(Vector3 pos, Quaternion rot, Vector3 sca)
    {
        transform.localPosition = pos;
        transform.localRotation = rot;
        transform.localScale = sca;
    }

    public void AddBox()
    {
        gameObject.AddComponent<BoxCollider>();
    }

    /// <summary>
    /// 设置id
    /// </summary>
    /// <param name="num"></param>
    public void setID(int num)
    {
        brandId = num;
    }

    public void setPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    private bool CanUp = true;
    private bool CanDown = false;

    /// <summary>
    /// 向上移动
    /// </summary>
    public void moveUp()
    {
        if (CanUp)
        {
            CanUp = false;
            CanDown = true;
            transform.localPosition += transform.forward * 0.2f;
        }
    }

    /// <summary>
    /// 向下移动
    /// </summary>
    public void moveDown()
    {
        if (CanDown)
        {
            CanUp = true;
            CanDown = false;
            transform.localPosition -= transform.forward * 0.2f;
        }
    }

    #endregion

}
