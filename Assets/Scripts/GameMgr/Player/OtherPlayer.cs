using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayer {

    private int size = 13;

    private GameObject[] brandGBs = new GameObject[20];

    public OtherPlayer()
    {
        for (int i = 0; i < size; i++)
        {
            brandGBs[i] = ResourceMgr.Instance.CreateGameObject("Mahjong/mj36", true);
        }
    }

    public int GetSize()
    {
        return size;
    }

    public GameObject GetEndOb()
    {
        return brandGBs[size - 1];
    }

    public GameObject GetOb(int num)
    {
        return brandGBs[num];
    }

    public void AddOb()
    {
        brandGBs[size] = ResourceMgr.Instance.CreateGameObject("Mahjong/mj36", true);
        size++;
    }

    public void RemoveEndGb()
    {
        GameObject.Destroy(brandGBs[size - 1]);
        size--;
    }

    public void Operation(int level)
    {
        switch (level)
        {
            case 1:
            case 2:
                RemoveEndGb();
                RemoveEndGb();
                break;
            case 3:
                RemoveEndGb();
                RemoveEndGb();
                RemoveEndGb();
                break;
        }
    }

}
