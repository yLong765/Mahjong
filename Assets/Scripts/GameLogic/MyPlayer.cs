using UnityEngine;
using System.Collections;
using System;

public class MyPlayer {

    #region 单例

    private static MyPlayer _Instance = new MyPlayer();

    public static MyPlayer Instance
    {
        get
        {
            return _Instance;
        }
    }

    #endregion

    /// <summary>
    /// 手牌List
    /// </summary>
    private int[] brands = new int[20];

    /// <summary>
    /// 手牌大小
    /// </summary>
    private int size = 0;

    /// <summary>
    /// 特殊操作牌
    /// </summary>
    private int[] OperationBrnads = new int[20];

    /// <summary>
    /// 碰牌记录
    /// </summary>
    private int[] peng = new int[2];

    /// <summary>
    /// 杠牌记录
    /// </summary>
    private int[] gang = new int[3];

    /// <summary>
    /// 吃牌记录
    /// </summary>
    private int[] chi = new int[4];

    /// <summary>
    /// 听牌记录数组大小
    /// </summary>
    private int tingSize = 0;

    /// <summary>
    /// 听牌记录
    /// </summary>
    private int[] ting = new int[10];

    /// <summary>
    /// 听牌标记数组
    /// </summary>
    private int[] TingBJ = new int[20];

    /// <summary>
    /// 获得手牌数目
    /// </summary>
    /// <returns>数目</returns>
    public int getSize()
    {
        return size;
    }

    /// <summary>
    /// 检测能否胡
    /// </summary>
    /// <param name="num">检测牌</param>
    /// <returns>能否胡</returns>
    public bool isHu(int num)
    {
        for (int i = 0; i < tingSize; i++)
        {
            if (num == ting[i]) return true;
        }
        return false;
    }

    /// <summary>
    /// 检测能否听
    /// </summary>
    /// <returns>能否听</returns>
    public bool isTing()
    {
        bool bting = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int num = i * 10 + j;

                for (int k = 0; k < 20; k++)
                {
                    TingBJ[k] = 0;
                }

                if (Ting(num))
                {
                    bting = true;
                    ting[tingSize] = num;
                    tingSize++;
                }
            }
        }
        return bting;
    }

    /// <summary>
    /// 能听标记
    /// </summary>
    private bool CanTing = false;

    int[] T = null;

    /// <summary>
    /// 听牌判断函数
    /// </summary>
    /// <param name="num">想听的牌</param>
    /// <returns>是否能听</returns>
    private bool Ting(int num)
    {
        T = null;
        T = (int[])brands.Clone();
        T[size] = num;

        CanTing = false;

        Array.Sort(T, 0, size + 1);

        bool bo = false;

        for (int i = 0; i < size; i++)
        {
            if (T[i] == 0 || T[i] == 8 || T[i] == 10 || T[i] == 18 || T[i] == 20 || T[i] == 28 || T[i] == 34)
            {
                bo = true;
            }
            if (!bo) return bo;
        }

        BFSFG(1);

        if (CanTing)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 分割手牌
    /// </summary>
    private void BFSFG(int num)
    {
        switch (num)
        {
            case 1: //分割雀头
                for (int i = 0; i < size; i++)
                {
                    if (T[i] == T[i + 1])
                    {
                        TingBJ[i] = 1;
                        TingBJ[i + 1] = 1;
                        BFSFG(2);
                        TingBJ[i] = 0;
                        TingBJ[i + 1] = 0;
                    }
                }
                break;
            case 2: //分割碰牌
                for (int i = 0; i < size - 1; i++)
                {
                    if (TingBJ[i] == 0)
                    {
                        if (T[i] == T[i + 1] && T[i + 1] == T[i + 2])
                        {
                            TingBJ[i] = 2;
                            TingBJ[i + 1] = 2;
                            TingBJ[i + 2] = 2;
                        }
                    }
                }
                BFSFG(3);
                break;
            case 3: //分割吃牌
                for (int i = 0; i < size - 1; i++)
                {
                    if (TingBJ[i] == 0)
                    {
                        int p = T[i];
                        int p2 = p;
                        int pos = 0;
                        for (int j = i + 1; j <= size; j++)
                        {
                            if (p2 + 1 == T[j])
                            {
                                p2 += 1;
                                if (p2 - p == 2)
                                {
                                    TingBJ[i] = 3;
                                    TingBJ[pos] = 3;
                                    TingBJ[j] = 3;
                                    pos = 0;
                                    break;
                                }
                                pos = j;
                            }
                        }
                    }
                }
                BFSFG(4);
                break;
            case 4:
                bool bo = true;

                for (int i = 0; i < size; i++)
                {
                    if (TingBJ[i] == 0)
                    {
                        for (int j = 0; j <= size; j++)
                            TingBJ[j] = 0;
                        bo = false;
                        break;
                    }
                }

                if (bo) CanTing = true;

                break;
        }
    }

    /// <summary>
    /// 检测是否能碰
    /// </summary>
    /// <param name="num">检测牌数</param>
    /// <returns>是否能碰</returns>
    public bool isPeng(int num)
    {
        int t = 0;
        for (int i = 0; i < size; i++)
        {
            if (brands[i] == num)
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
        for (int i = 0; i < size; i++)
        {
            if (brands[i] == num)
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
        bool p = false;
        for (int i = 0; i < 4; i++)
        {
            chi[i] = -1;
        }
        for (int i = 0; i < size; i++)
        {
            if (brands[i] == num - 2)
                chi[0] = i;
            if (brands[i] == num - 1)
            {
                chi[1] = i;
                if (chi[0] != -1) p = true;
            }
            if (brands[i] == num + 1)
            {
                chi[2] = i;
                if (chi[1] != -1) p = true;
            }
            if (brands[i] == num + 2)
            {
                chi[3] = i;
                if (chi[1] != -1) p = true;
            }
        }
        return p;
    }

    /// <summary>
    /// 排序手牌
    /// </summary>
    public void Sort()
    {
        Array.Sort(brands,0,size);
    }

    /// <summary>
    /// 获得最后的牌
    /// </summary>
    /// <returns></returns>
    public int GetEnd()
    {
        return brands[size - 1];
    }

    /// <summary>
    /// 获得指定牌
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public int Get(int num)
    {
        return brands[num];
    }

    /// <summary>
    /// 添加手牌
    /// </summary>
    /// <param name="num"></param>
    public void Add(int num)
    {
        brands[size] = num;
        size++;
    }

    /// <summary>
    /// 删除手牌
    /// </summary>
    /// <param name="num"></param>
    public void Remove(int pos)
    {
        brands[pos] = brands[size];
        size--;
    }

}
