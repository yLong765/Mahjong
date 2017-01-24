using UnityEngine;
using System.Collections;

public class TBrand : MonoBehaviour{

    private int BrandId;

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
        Vector3 pos = transform.localPosition;
        pos.z += 0.2f;
        transform.localPosition = pos;
    }

    public void Down()
    {
        Vector3 pos = transform.localPosition;
        pos.z -= 0.2f;
        transform.localPosition = pos;
    }

    public void OnClickShow()
    {
        Destroy(gameObject);
    }

}
