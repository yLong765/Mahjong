using UnityEngine;
using System.Collections;

public class TBrand : MonoBehaviour{

    private int BrandId;

    private Vector3 myPos;

    public void setPos(Vector3 pos)
    {
        myPos = pos;
    }

    public void setBrandId(int id)
    {
        BrandId = id;
    }

    public int getBrandId()
    {
        return BrandId;
    }

    public void OnClickUp()
    {
        Vector3 pos = myPos;
        pos.z += 0.2f;
        transform.localPosition = pos;
    }

    public void Down()
    {
        transform.localPosition = myPos;
    }

    public void OnClickShow()
    {
        Destroy(gameObject);
    }

}
