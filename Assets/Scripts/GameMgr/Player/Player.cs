using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    #region 变量定义

    /// <summary>
    /// 手牌字典
    /// </summary>
    private Dictionary<int, Brand> brands = new Dictionary<int, Brand>();

    /// <summary>
    /// 碰牌记录
    /// </summary>
    public int[] peng = new int[2];

    /// <summary>
    /// 杠牌记录
    /// </summary>
    public int[] gang = new int[3];

    /// <summary>
    /// 吃牌记录
    /// </summary>
    public int[] chi = new int[4];

    /// <summary>
    /// 听牌记录数组大小
    /// </summary>
    private int tingSize = 0;

    /// <summary>
    /// 听牌记录
    /// </summary>
    public Dictionary<int, List<int>> tings = new Dictionary<int, List<int>>();

    /// <summary>
    /// 听牌标记数组
    /// </summary>
    private int[] TingBJ = new int[20];

    /// <summary>
    /// 胡牌记录
    /// </summary>
    public List<int> Hus = new List<int>();

    #endregion

    #region 方法

    /// <summary>
    /// 获得手牌数目
    /// </summary>
    /// <returns>数目</returns>
    public int getSize()
    {
        return brands.Count;
    }

    /// <summary>
    /// 排序手牌
    /// </summary>
    public void Sort()
    {
        for (int i = 0; i < brands.Count - 1; i++)
        {
            for (int j = i; j < brands.Count; j++)
            {
                if (brands[i].id > brands[j].id)
                {
                    Brand T = brands[i];
                    brands[i] = brands[j];
                    brands[j] = T;
                }
            }
        }
    }

    /// <summary>
    /// 添加手牌
    /// </summary>
    /// <param name="num"></param>
    public void Add(int num)
    {
        GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + num, true);
        Brand mBrand = gb.AddComponent<Brand>();
        mBrand.setID(num);
        brands.Add(brands.Count, mBrand);
    }

    /// <summary>
    /// 删除手牌
    /// </summary>
    /// <param name="num"></param>
    public void Remove(int pos)
    {
        MonoBehaviour.Destroy(brands[pos].gameObject);
        brands[pos] = brands[brands.Count - 1];
        brands.Remove(brands.Count - 1);
    }

    /// <summary>
    /// 删除手牌
    /// </summary>
    /// <param name="mBrand"></param>
    public void Remove(Brand mBrand)
    {
        int pos = -1;
        for (int i = 0; i < brands.Count; i++)
        {
            if (mBrand == brands[i])
            {
                pos = i;
                break;
            }
        }
        if (pos != -1)
        {
            MonoBehaviour.Destroy(brands[pos].gameObject);
            brands[pos] = brands[brands.Count - 1];
            brands.Remove(brands.Count - 1);
        }
    }

    /// <summary>
    /// 返回指定位置手牌
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public Brand Get(int pos)
    {
        return brands[pos];
    }

    public int GetId(Brand mbrand)
    {
        for (int i = 0; i < brands.Count; i++)
        {
            if (mbrand == brands[i]) return i;
        }
        return -1;
    }

    /// <summary>
    /// 返回最后的手牌
    /// </summary>
    /// <returns></returns>
    public Brand GetEnd()
    {
        return brands[brands.Count - 1];
    }

    /// <summary>
    /// 网络防作弊初始化
    /// </summary>
    public void webInit()
    {
        for (int i = 0; i < 13; i++)
        {
            Add(36);
        }
    }

    /// <summary>
    /// 网络版特殊处理
    /// </summary>
    /// <param name="level"></param>
    public void webRemoveOperator(int level)
    {
        switch (level)
        {
            case 1:
            case 2:
                Remove(brands.Count - 1);
                Remove(brands.Count - 1);
                break;
            case 3:
                Remove(brands.Count - 1);
                Remove(brands.Count - 1);
                Remove(brands.Count - 1);
                break;
        }
    }

    #endregion

    #region 判断特殊操作

    private bool CanTing = false;
    bool bo = false;
    bool pppp = false;

    /// <summary>
    /// 检测能否听
    /// </summary>
    /// <returns></returns>
    public bool isTing()
    {
        tingSize = 0;
        tings.Clear();

        int[] g = new int[brands.Count];

        for (int i = 0; i < brands.Count; i++)
        {
            g[i] = brands[i].id;
        }

        int[] p = (int[])g.Clone();

        for (int i = 0; i < brands.Count; i++)
        {
            bool b = false;
            for (int j = 0; j < 4; j++)
            {
                for (int k = 0; k < 9; k++)
                {
                    p = (int[])g.Clone();

                    int num = 0;
                    if (j < 4) num = j * 10 + k;
                    else if (k > 6) continue;
                    p[i] = num;

                    Array.Sort(p);

                    for (int l = 0; l < 20; l++)
                    {
                        TingBJ[l] = 0;
                    }

                    BFSFG(1, p);

                    if (bo)
                    {
                        if (!tings.ContainsKey(tingSize))
                            tings.Add(tingSize, new List<int>());
                        tings[tingSize].Add(num);
                        bo = false;
                        CanTing = true;
                        b = true;
                    }
                }
            }
            if (!b) tings[tingSize] = null;
            if (i == brands.Count && b)
            {
                Hus = null;
                Hus = tings[i];
            }
            tingSize++;
        }

        return CanTing;
    }

    /// <summary>
    /// 分割手牌
    /// </summary>
    private void BFSFG(int num,int[] T)
    {
        switch (num)
        {
            case 1: //分割雀头
                for (int i = 0; i < brands.Count - 1; i++)
                {
                    if (T[i] == T[i + 1])
                    {
                        TingBJ[i] = 1;
                        TingBJ[i + 1] = 1;
                        BFSFG(2,T);
                    }
                    if (bo) break;
                }
                break;
            case 2: //分割碰牌
                for (int i = 0; i < brands.Count - 2; i++)
                {
                    if (TingBJ[i] == 0 && TingBJ[i + 1] == 0 && TingBJ[i + 2] == 0)
                    {
                        if (T[i] == T[i + 1] && T[i + 1] == T[i + 2])
                        {
                            TingBJ[i] = 2;
                            TingBJ[i + 1] = 2;
                            TingBJ[i + 2] = 2;
                        }
                    }
                    if (bo) break;
                }
                BFSFG(3,T);
                break;
            case 3: //分割吃牌
                for (int i = 0; i < brands.Count - 2; i++)
                {
                    if (TingBJ[i] == 0 && T[i] < 30)
                    {
                        int p = T[i] + 1;
                        int m = 0;
                        for (int j = i + 1; j < brands.Count; j++)
                        {
                            if (p == T[j] && TingBJ[j] == 0)
                            {
                                if (m != 0)
                                {
                                    TingBJ[i] = 1;
                                    TingBJ[m] = 1;
                                    TingBJ[j] = 1;
                                    break;
                                }
                                p = T[j] + 1;
                                m = j;
                            }
                        }
                    }
                    if (bo) break;
                }
                BFSFG(4,T);
                break;
            case 4:
                bo = true;

                for (int i = 0; i < brands.Count; i++)
                {
                    if (TingBJ[i] == 0)
                    {
                        for (int j = 0; j < brands.Count; j++)
                            TingBJ[j] = 0;
                        bo = false;
                        break;
                    }
                }

                break;
        }
    }

    /// <summary>
    /// 检测能否胡
    /// </summary>
    /// <param name="num">检测牌</param>
    /// <returns>能否胡</returns>
    public bool isHu(int num)
    {
        for (int i = 0; i < Hus.Count; i++)
        {
            if (Hus.Contains(num)) return true;
        }
        return false;
    }

    /// <summary>
    /// 检测是否能碰
    /// </summary>
    /// <param name="num">检测牌数</param>
    /// <returns>是否能碰</returns>
    public bool isPeng(int num)
    {
        int t = 0;
        for (int i = 0; i < brands.Count; i++)
        {
            if (brands[i].id == num)
            {
                peng[t] = i;
                t++;
            }
            if (t == 2) return true;
        }
        return false;
    }

    /// <summary>
    /// 检测是否能杠
    /// </summary>
    /// <param name="num">检测牌</param>
    /// <returns>是否能杠</returns>
    public bool isGang(int num)
    {
        int t = 0;
        for (int i = 0; i < brands.Count; i++)
        {
            if (brands[i].id == num)
            {
                gang[t] = i;
                t++;
            }
            if (t == 3) return true;
        }
        return false;
    }

    /// <summary>
    /// 检测是否能吃
    /// </summary>
    /// <param name="num">检测牌</param>
    /// <returns>是否能吃</returns>
    public bool isChi(int num)
    {
        if (num >= 30) return false;

        bool p = false;
        for (int i = 0; i < 4; i++)
        {
            chi[i] = -1;
        }

        for (int i = 0; i < brands.Count; i++)
        {
            if (brands[i].id == num - 2)
                chi[0] = i;
            if (brands[i].id == num - 1)
            {
                chi[1] = i;
                if (chi[0] != -1) p = true;
            }
            if (brands[i].id == num + 1)
            {
                chi[2] = i;
                if (chi[1] != -1) p = true;
            }
            if (brands[i].id == num + 2)
            {
                chi[3] = i;
                if (chi[2] != -1) p = true;
            }
        }
        return p;
    }

    #endregion

}
