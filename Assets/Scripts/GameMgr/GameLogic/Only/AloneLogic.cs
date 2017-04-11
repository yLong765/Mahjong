using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AloneLogic : MonoBehaviour
{

    #region 单例

    private static AloneLogic _Instance = new AloneLogic();

    public static AloneLogic Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_AloneLogic").AddComponent<AloneLogic>();
            }
            DontDestroyOnLoad(_Instance.gameObject);
            return _Instance;
        }
    }

    void Awake()
    {
        _Instance = this;
    }

    #endregion

    #region 数据定义

    private GameObject tag;

    private bool Ting = false;

    private Player[] players = new Player[4];

    /// <summary>
    /// 吃碰杠位置
    /// </summary>
    private Vector3[] Oppos = new Vector3[4];
    /// <summary>
    /// 吃碰杠角度
    /// </summary>
    private Vector3[] Oprot = new Vector3[4];
    /// <summary>
    /// 吃碰杠修改坐标
    /// </summary>
    private Vector3[] OpC = new Vector3[4];
    private Vector3[] OpCC = new Vector3[4];

    /// <summary>
    /// 出牌位置
    /// </summary>
    private Vector3[] showPos = new Vector3[4];
    /// <summary>
    /// 出牌修改坐标
    /// </summary>
    private Vector3[] showC = new Vector3[4];
    private Vector3[] showCC = new Vector3[4];
    private Vector3[] showLastP = new Vector3[4];
    private int[] showTime = new int[4] { 0, 0, 0, 0 };

    /// <summary>
    /// 发牌位置
    /// </summary>
    private Vector3[] DealPos = new Vector3[4];
    /// <summary>
    /// 发牌角度
    /// </summary>
    private Vector3[] DealRot = new Vector3[4];

    private GameObject LastShowBrand = null;
    private int LastShowMan;

    private int PSpos = 0;

    private int[] Brands = new int[137];
    private int nowBrNum = 0;

    private int nowplayer = 0;

    #endregion

    #region 游戏逻辑

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitDate()
    {
        tag = GameObject.Find("tag");

        nowplayer = Random.Range(0, 4);

        players[0] = new Player();
        players[1] = new Player();
        players[2] = new Player();
        players[3] = new Player();

        ActionParam param = new ActionParam();
        param["roomID"] = GameSetting.Instance.roomID;

        WebLogic.Instance.Send((int)ActionType.GetPlayerId, param);

        Oppos[0] = new Vector3(3f, 0, -3.5f);
        Oppos[1] = new Vector3(3.5f, 0, 3f);
        Oppos[2] = new Vector3(-3f, 0, 3.5f);
        Oppos[3] = new Vector3(-3.5f, 0, -3f);

        Oprot[0] = new Vector3(0, 0, -90);
        Oprot[1] = new Vector3(0, -90, -90);
        Oprot[2] = new Vector3(0, 180, -90);
        Oprot[3] = new Vector3(0, 90, -90);

        OpC[0] = new Vector3(-0.3f, 0, 0);
        OpC[1] = new Vector3(0, 0, -0.3f);
        OpC[2] = new Vector3(0.3f, 0, 0);
        OpC[3] = new Vector3(0, 0, 0.3f);

        OpCC[0] = new Vector3(-0.1f, 0, 0);
        OpCC[1] = new Vector3(0, 0, -0.1f);
        OpCC[2] = new Vector3(0.1f, 0, 0);
        OpCC[3] = new Vector3(0, 0, 0.1f);

        showPos[0] = new Vector3(-0.6f, 0, -1.13f);
        showPos[3] = new Vector3(-1.13f, 0, 0.6f);
        showPos[2] = new Vector3(0.6f, 0, 1.13f);
        showPos[1] = new Vector3(1.13f, 0, -0.6f);

        showC[0] = new Vector3(0.3f, 0, 0);
        showC[1] = new Vector3(0, 0, 0.3f);
        showC[2] = new Vector3(-0.3f, 0, 0);
        showC[3] = new Vector3(0, 0, -0.3f);

        showCC[0] = new Vector3(0, 0, -0.4f);
        showCC[1] = new Vector3(0.4f, 0, 0);
        showCC[2] = new Vector3(0, 0, 0.4f);
        showCC[3] = new Vector3(-0.4f, 0, 0);

        DealPos[0] = new Vector3(0, 0.5f, -3.5f);

        DealRot[0] = new Vector3(0, 0, -90);

        int z = 0;
        for (int i = 0; i < 4; i++)
        {
            int p = 9;
            if (i == 3) p = 7;
            for (int j = 0; j < p; j++)
            {
                for (int k = 0; k < 4; k++)
                {
                    Brands[z++] = i * 10 + j;
                }
            }
        }

        for (int i = 0; i < z; i++)
        {
            int r = Random.Range(i, z);
            int t = Brands[r];
            Brands[r] = Brands[i];
            Brands[i] = t;
        }
    }

    /// <summary>
    /// 初始化牌桌
    /// </summary>
    public void InitTable()
    {
        Vector3[] pos = new Vector3[4];
        pos[0] = new Vector3(-2.4f, 0.2f, -2.85f);
        pos[1] = new Vector3(2.85f, 0.2f, -2.4f);
        pos[2] = new Vector3(2.4f, 0.2f, 2.85f);
        pos[3] = new Vector3(-2.85f, 0.2f, 2.4f);

        Vector3[] C = new Vector3[4];
        C[0] = new Vector3(0.3f, 0, 0);
        C[1] = new Vector3(0, 0, 0.3f);
        C[2] = new Vector3(-0.3f, 0, 0);
        C[3] = new Vector3(0, 0, -0.3f);

        Vector3[] rot = new Vector3[4];
        rot[0] = new Vector3(0, 0, 90);
        rot[1] = new Vector3(0, 90, 90);
        rot[2] = new Vector3(0, 0, 90);
        rot[3] = new Vector3(0, 90, 90);

        int num = 0;

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 17; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    Vector3 Tpos = pos[i] + C[i] * j;
                    if (k == 1) Tpos.y = 0;

                    GameObject gb = ResourceMgr.Instance.CreateBrand("Mahjong/mj36", Tpos, Quaternion.Euler(rot[i]));
                    gb.name = num.ToString();
                    num++;
                }
            }
        }
    }

    public void InitHand()
    {
        for (int i = 0; i < 13; i++)
        {
            players[0].Add(Brands[nowBrNum++]);
            players[1].Add(Brands[nowBrNum++]);
            players[2].Add(Brands[nowBrNum++]);
            players[3].Add(Brands[nowBrNum++]);
        }
    }

    /// <summary>
    /// 发手牌
    /// </summary>
    public void HandDeal()
    {
        Vector3[] pos = new Vector3[4];
        pos[0] = new Vector3(-3.45f, 0.5f, -3.4f);
        pos[1] = new Vector3(3.5f, 0.1f, -1.8f);
        pos[2] = new Vector3(1.8f, 0.1f, 3.5f);
        pos[3] = new Vector3(-3.5f, 0.1f, 1.8f);

        Vector3[] rot = new Vector3[4];
        rot[0] = new Vector3(-10, 0, -90);
        rot[1] = new Vector3(-90, 180, 0);
        rot[2] = new Vector3(-90, 90, 0);
        rot[3] = new Vector3(-90, 0, 0);

        Vector3[] C = new Vector3[4];
        C[0] = new Vector3(0.45f, 0, 0);
        C[1] = new Vector3(0, 0, 0.3f);
        C[2] = new Vector3(-0.3f, 0, 0);
        C[3] = new Vector3(0, 0, -0.3f);

        Vector3 sca = Vector3.one;

        players[0].Sort();
        players[1].Sort();
        players[2].Sort();
        players[3].Sort();

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                pos[i] += C[i];
                if (i == 0) sca = Vector3.one * 1.5f;
                else sca = Vector3.one;

                Brand mBrand = players[i].Get(j);
                mBrand.Init(pos[i], Quaternion.Euler(rot[i]), sca);
                if (i == 0) mBrand.AddBox();
            }

        }

        int[] StartNum = new int[4] { 68, 92, 0, 34 };

        Debug.Log(nowplayer);

        int Begin = StartNum[nowplayer] + Random.Range(2, 13) * 2;

        int f = 0;
        while (f <= 52)
        {
            PSpos = Begin + f;
            if (PSpos > 135) PSpos -= 136;
            Destroy(GameObject.Find(PSpos.ToString()).gameObject);
            f++;
        }

        Deal(nowplayer);

    }

    /// <summary>
    /// 发牌
    /// </summary>
    public void Deal(int id)
    {
        players[id].Add(Brands[nowBrNum++]);

        Vector3[] pos = new Vector3[4];
        pos[0] = new Vector3(-3.45f, 0.5f, -3.4f);
        pos[1] = new Vector3(3.5f, 0.1f, -1.8f);
        pos[2] = new Vector3(1.8f, 0.1f, 3.5f);
        pos[3] = new Vector3(-3.5f, 0.1f, 1.8f);

        Vector3[] rot = new Vector3[4];
        rot[0] = new Vector3(-10, 0, -90);
        rot[1] = new Vector3(-90, 180, 0);
        rot[2] = new Vector3(-90, 90, 0);
        rot[3] = new Vector3(-90, 0, 0);

        Vector3[] C = new Vector3[4];
        C[0] = new Vector3(0.55f, 0, 0);
        C[1] = new Vector3(0, 0, 0.4f);
        C[2] = new Vector3(-0.4f, 0, 0);
        C[3] = new Vector3(0, 0, -0.4f);

        Vector3[] C1 = new Vector3[4];
        C1[0] = new Vector3(0.45f, 0, 0);
        C1[1] = new Vector3(0, 0, 0.3f);
        C1[2] = new Vector3(-0.3f, 0, 0);
        C1[3] = new Vector3(0, 0, -0.3f);

        Vector3 sca = Vector3.one;
        if (id == 0) sca *= 1.5f;

        pos[id] += C1[id] * (players[id].getSize() - 1) + C[id];

        Brand mBrand = players[id].GetEnd();
        mBrand.Init(pos[id], Quaternion.Euler(rot[id]), sca);
        if (id == 0) mBrand.AddBox();

        Destroy(GameObject.Find(PSpos.ToString()).gameObject);
        PSpos++;

        SceneGame.Instance.ChangePS();

        if (id == 0)
        {
            if (!Ting)
            {
                if (players[0].isTing())
                    SceneGame.Instance.showButton("Ting");
                if (players[0].isHu(mBrand.id))
                    SceneGame.Instance.showButton("Hu");
            }
            else
            {
                StartCoroutine(Jtime(mBrand));
            }
        }
        else
        {
            OtherShowBrand(players[id].GetEnd().id, id);
        }

    }

    /// <summary>
    /// 听牌延时出牌
    /// </summary>
    /// <param name="mBrand"></param>
    /// <returns></returns>
    IEnumerator Jtime(Brand mBrand)
    {
        yield return new WaitForSeconds(1f);
        ShowBrand(mBrand);
    }

    /// <summary>
    /// 上一张点击牌
    /// </summary>
    private Brand LastBrand;

    /// <summary>
    /// 点击出牌
    /// </summary>
    /// <param name="gb">点击牌</param>
    public void MouseClick(Brand mBrand)
    {
        int num = mBrand.id;
        if (LastBrand == mBrand)
        {
            if (nowplayer == 0)
            {
                if (!Ting)
                    ShowBrand(mBrand);
            }
            else
            {
                mBrand.moveDown();
                LastBrand = null;
            }
        }
        else if (LastBrand != null)
        {
            LastBrand.moveDown();
            mBrand.moveUp();
            LastBrand = mBrand;
        }
        else
        {
            mBrand.moveUp();
            LastBrand = mBrand;
        }
    }

    /// <summary>
    /// 出牌
    /// </summary>
    public void ShowBrand(Brand mBrand)
    {
        OtherShowBrand(mBrand.id, 0);

        players[0].Remove(mBrand);

        players[0].Sort();

        Vector3 pos = new Vector3(-3.45f, 0.5f, -3.4f);

        for (int i = 0; i < players[0].getSize(); i++)
        {
            Brand br = players[0].Get(i);
            pos.x += 0.45f;
            br.setPos(pos);
        }

        LastBrand = null;

        SceneGame.Instance.NoActive();
    }

    /// <summary>
    /// 其他玩家出牌展示
    /// </summary>
    /// <param name="num"></param>
    /// <param name="playerid"></param>
    public void OtherShowBrand(int num, int id)
    {
        GameObject g = ResourceMgr.Instance.CreateBrand("Mahjong/mj" + num, showPos[id], Quaternion.Euler(Oprot[id]));

        AudioMgr.Instance.showAudio(num);

        tag.transform.position = g.transform.position + Vector3.up;

        showLastP[id] = showPos[id];
        showPos[id] += showC[id];
        showTime[id]++;
        if (showTime[id] % 5 == 0)
        {
            showPos[id] -= showC[id] * 5;
            showPos[id] += showCC[id];
        }

        LastShowBrand = g;
        LastShowMan = id;

        if (id != 0)
        {
            players[id].Remove(players[id].getSize() - 1);
            ShowOperation(num);
        }
    }

    /// <summary>
    /// 记录处理牌
    /// </summary>
    int numOp;

    /// <summary>
    /// 玩家可操作展示
    /// </summary>
    /// <param name="num"></param>
    public void ShowOperation(int num)
    {
        numOp = num;

        bool Guo = true;

        if (players[0].isHu(num))
        {
            Guo = false;
            SceneGame.Instance.showButton("Hu");
        }
        if (!Ting)
        {
            if (players[0].isGang(num))
            {
                Guo = false;
                SceneGame.Instance.showButton("Gang");
            }
            if (players[0].isPeng(num))
            {
                Guo = false;
                SceneGame.Instance.showButton("Peng");
            }
            if (players[0].isChi(num))
            {
                int o = GameSetting.Instance.Playerid - GameSetting.Instance.target;
                if (o == 1 || o == -3)
                {
                    Guo = false;
                    SceneGame.Instance.showButton("Chi");
                }
            }
        }

        if (Guo)
        {
            nowplayer++;
        }
    }

    /// <summary>
    /// 展示吃，碰，杠
    /// </summary>
    /// <param name="num"></param>
    /// <param name="playerid"></param>
    /// <param name="level"></param>
    public void ShowOp(int num, int id, int level)
    {
        AudioMgr.Instance.opSound(level);

        if (level > 0) tag.transform.position = new Vector3(0, -1, 0);

        switch (level)
        {
            case 1:
                for (int i = 2; i >= 0; i--)
                {
                    ResourceMgr.Instance.CreateBrand("Mahjong/mj" + (num + i), Oppos[id], Quaternion.Euler(Oprot[id]));
                    Oppos[id] += OpC[id];
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    ResourceMgr.Instance.CreateBrand("Mahjong/mj" + num, Oppos[id], Quaternion.Euler(Oprot[id]));
                    Oppos[id] += OpC[id];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    ResourceMgr.Instance.CreateBrand("Mahjong/mj" + num, Oppos[id], Quaternion.Euler(Oprot[id]));
                    Oppos[id] += OpC[id];
                }
                break;
            case 5:
                GameEnd();
                break;
        }

        if (level != 0)
        {
            Oppos[id] += OpCC[id];
            GameObject.Destroy(LastShowBrand);
            showPos[LastShowMan] = showLastP[LastShowMan];
            showTime[LastShowMan]--;
        }

        if (id == 0)
        {
            OpAnims(level);
            if (level == 0 || level == 3)
            {
                FlowOfGame.Instance.Deal = true;
            }
            else if (players[0].isTing())
            {
                SceneGame.Instance.showButton("Ting");
            }
        }
        else
        {
            players[id].webRemoveOperator(level);
            if (level == 0 || level == 3)
            {
                players[id].Add(36);
                Deal(GameSetting.Instance.target);
            }

        }

    }

    /// <summary>
    /// Chi位置
    /// </summary>
    int[] ChiPos = new int[2];

    /// <summary>
    /// Chi型临时数组
    /// </summary>
    int[,] c = new int[3, 2];

    /// <summary>
    /// 临时数组长度
    /// </summary>
    int g = 0;

    /// <summary>
    /// 选择吃什么型
    /// </summary>
    public void ChangeChi()
    {
        int[] cc = players[0].chi;

        g = 0;

        if (cc[0] != -1 && cc[1] != -1)
        {
            c[g, 0] = cc[0];
            c[g, 1] = cc[1];
            g++;
        }

        if (cc[1] != -1 && cc[2] != -1)
        {
            c[g, 0] = cc[1];
            c[g, 1] = cc[2];
            g++;
        }

        if (cc[2] != -1 && cc[3] != -1)
        {
            c[g, 0] = cc[2];
            c[g, 1] = cc[3];
            g++;
        }

        if (g > 1)
        {
            Mouse.Instance.Chi = true;
            for (int i = 0; i < g; i++)
            {
                players[0].Get(c[i, 0]).moveUp();
                players[0].Get(c[i, 1]).moveUp();
            }
        }
        else
        {
            if (players[0].Get(c[0, 0]).id < numOp) numOp = players[0].Get(c[0, 0]).id;
            ChiPos[0] = c[0, 0];
            ChiPos[1] = c[0, 1];
        }

    }

    /// <summary>
    /// 选择动画
    /// </summary>
    /// <param name="gb"></param>
    /// <param name="Click"></param>
    public void ChangeAnimation(Brand mBrand, bool Click)
    {
        if (mBrand == null)
        {
            for (int i = 0; i < g; i++)
            {
                players[0].Get(c[i, 0]).moveUp();
                players[0].Get(c[i, 1]).moveUp();
            }
            return;
        }

        int tt = -1;

        for (int i = 0; i < g; i++)
        {
            players[0].Get(c[i, 0]).moveDown();
            players[0].Get(c[i, 1]).moveDown();
            if (players[0].Get(c[i, 0]) == mBrand)
            {
                tt = i;
            }
        }

        if (tt != -1)
        {
            if (Click)
            {
                if (players[0].Get(c[tt, 0]).id < numOp) numOp = players[0].Get(c[tt, 0]).id;
                ChiPos[0] = c[tt, 0];
                ChiPos[1] = c[tt, 1];
            }
            else
            {
                players[0].Get(c[tt, 0]).moveUp();
                players[0].Get(c[tt, 1]).moveUp();
            }
        }
        else
        {
            for (int i = 0; i < g; i++)
            {
                players[0].Get(c[i, 0]).moveUp();
                players[0].Get(c[i, 1]).moveUp();
            }
        }
    }

    /// <summary>
    /// 吃碰杠消除
    /// </summary>
    /// <param name="level"></param>
    public void OpAnims(int level)
    {
        switch (level)
        {
            case 1:
                players[0].Remove(ChiPos[1]);
                players[0].Remove(ChiPos[0]);
                break;

            case 2:
                int[] P = players[0].peng;
                players[0].Remove(P[1]);
                players[0].Remove(P[0]);
                break;

            case 3:
                int[] G = players[0].gang;
                players[0].Remove(G[2]);
                players[0].Remove(G[1]);
                players[0].Remove(G[0]);
                break;
        }

        players[0].Sort();

        Vector3 pos = new Vector3(-3.45f, 0.5f, -3.4f);

        for (int i = 0; i < players[0].getSize() - 1; i++)
        {
            Brand br = players[0].Get(i);
            pos.x += 0.45f;
            br.setPos(pos);
        }

        if (level == 0)
        {
            Brand b = players[0].GetEnd();
            pos.x += 0.45f;
            b.setPos(pos);
        }
        else
        {
            Brand b = players[0].GetEnd();
            pos.x += 0.55f;
            b.setPos(pos);
        }



    }

    /// <summary>
    /// 选择听什么牌
    /// </summary>
    public void ChangeTing()
    {
        int size = players[0].tings.Count;

        for (int i = 0; i < size; i++)
        {
            if (players[0].tings[i] != null)
            {
                players[0].Get(i).moveUp();
            }
        }

        Mouse.Instance.Ting = true;
    }

    /// <summary>
    /// 听牌选择动画
    /// </summary>
    /// <param name="mbrand"></param>
    /// <param name="Click"></param>
    public void TingAniamtion(Brand mbrand, bool Click)
    {
        if (Click)
        {
            if (mbrand != null)
            {
                int j = players[0].GetId(mbrand);
                if (players[0].tings[j] != null)
                {
                    players[0].Hus = null;
                    players[0].Hus = players[0].tings[j];

                    for (int i = 0; i < players[0].getSize(); i++)
                    {
                        players[0].Get(i).moveDown();
                    }

                    Ting = true;
                    ShowBrand(mbrand);
                    Mouse.Instance.Ting = false;
                    AudioMgr.Instance.opSound(4);
                }
            }
        }
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    /// <returns></returns>
    public bool GameEnd()
    {
        SceneMgr.Instance.SceneSwitch(SceneState.SceneGameEnd);
        return true;
    }

    #endregion

}
