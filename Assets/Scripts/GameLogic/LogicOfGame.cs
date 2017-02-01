using UnityEngine;
using System.Collections;
using System;

public class LogicOfGame : MonoBehaviour, IEventListener {

    #region 单例

    private static LogicOfGame _Instance = new LogicOfGame();

    public static LogicOfGame Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new GameObject("_LogicOfGame").AddComponent<LogicOfGame>();
            }
            DontDestroyOnLoad(_Instance.gameObject);
            return _Instance;
        }
    }

    void Awake()
    {
        _Instance = this;
        WebLogic.Instance.AddEventListener(2, this);
        Debug.Log("Awake Done");
    }

    #endregion

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
    private int[] showTime = new int[4];

    /// <summary>
    /// 发牌位置
    /// </summary>
    private Vector3[] DealPos = new Vector3[4];
    /// <summary>
    /// 发牌角度
    /// </summary>
    private Vector3[] DealRot = new Vector3[4];

    private OtherPlayer[] Players = new OtherPlayer[4];

    private GameObject LastShowBrand = null;
    private int LastShowMan;

    private int PSpos = 0;

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitDate()
    {
        ActionParam param = new ActionParam();
        param["roomID"] = GameSetting.Instance.roomID;

        WebLogic.Instance.Send((int)ActionType.GetPlayerId, param);

        Players[1] = new OtherPlayer();
        Players[2] = new OtherPlayer();
        Players[3] = new OtherPlayer();

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

        Debug.Log("InitDate Done");
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

        int target = GameSetting.Instance.target;

        int id = ChangePlayerId(target);

        for (int i = 0; i < 4; i++)
        {
            int p = (id + i) % 4;
            for (int j = 0; j < 17; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    Vector3 Tpos = pos[p] + C[p] * j;
                    if (k == 1) Tpos.y = 0;

                    GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj36", true);
                    gb.gameObject.name = num.ToString();
                    gb.transform.localPosition = Tpos;
                    gb.transform.localRotation = Quaternion.Euler(rot[p]);
                    gb.transform.localScale = Vector3.one;
                    num++;
                }
            }
        }

        Debug.Log("InitTable Done");
    }

    /// <summary>
    /// 向服务端发送发牌请求
    /// </summary>
    public void HandDealInWeb()
    {
        ActionParam param = new ActionParam();

        param["roomID"] = GameSetting.Instance.roomID;

        for (int i = 0; i < 13; i++)
        {
            WebLogic.Instance.Send((int)ActionType.Logic, param);
        }

        Debug.Log("HandDealInWeb Done");

        StartCoroutine(CheckHandDeal());
    }

    /// <summary>
    /// 发手牌
    /// </summary>
    public void HandDeal()
    {

        Vector3 pos = new Vector3(-3.95f, 0.5f, -3.5f);
        Vector3 rot = new Vector3(0, 0, -90);
        Vector3 sca = new Vector3(1.5f, 1.5f, 1.5f);

        MyPlayer.Instance.Sort();

        for (int i = 0; i < 13; i++)
        {
            int brand = MyPlayer.Instance.Get(i);
            GameObject gb = MyPlayer.Instance.GetOb(i);
            pos.x += 0.45f;
            gb.name = brand.ToString();
            gb.transform.localPosition = pos;
            gb.transform.localRotation = Quaternion.Euler(rot);
            gb.transform.localScale = sca;
            gb.AddComponent<TBrand>();
            gb.AddComponent<BoxCollider>();
            gb.GetComponent<TBrand>().setPos(pos);
        }

        int Begin = GameSetting.Instance.StartNum * 2 - 1;

        for (int i = Begin; i < Begin + 52; i++)
        {
            Destroy(GameObject.Find(i.ToString()).gameObject);
        }

        PSpos = Begin + 52;

        Debug.Log("HandDeal Done");

        otherHandDeal();
    }

    /// <summary>
    /// 其他玩家手牌
    /// </summary>
    private void otherHandDeal()
    {
        Vector3[] pos = new Vector3[3];
        pos[0] = new Vector3(3.5f, 0.1f, -1.8f);
        pos[1] = new Vector3(1.8f, 0.1f, 3.5f);
        pos[2] = new Vector3(-3.5f, 0.1f, 1.8f);
        Vector3[] rot = new Vector3[3];
        rot[0] = new Vector3(-90, 180, 0);
        rot[1] = new Vector3(-90, 90, 0);
        rot[2] = new Vector3(-90, 0, 0);
        Vector3[] C = new Vector3[3];
        C[0] = new Vector3(0, 0, 0.3f);
        C[1] = new Vector3(-0.3f, 0, 0);
        C[2] = new Vector3(0, 0, -0.3f);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                GameObject gb = Players[i + 1].GetOb(j);
                gb.transform.localPosition = pos[i];
                gb.transform.localRotation = Quaternion.Euler(rot[i]);
                gb.transform.localScale = Vector3.one;
                pos[i] += C[i];
            }
        }

        Debug.Log("otherHandDeal Done");

        if (GameSetting.Instance.Playerid == GameSetting.Instance.target)
            FlowOfGame.Instance.Deal = true;
    }

    /// <summary>
    /// 向服务端发送发牌请求
    /// </summary>
    public void DealInWeb()
    {
        ActionParam param = new ActionParam();

        param["roomID"] = GameSetting.Instance.roomID;

        WebLogic.Instance.Send((int)ActionType.Logic, param);

        Debug.Log("DealInWeb Done");
    }

    /// <summary>
    /// 发牌
    /// </summary>
    public void Deal()
    {
        Vector3 pos = new Vector3(-3.95f, 0.5f, -3.5f);
        Vector3 rot = new Vector3(0, 0, -90);
        Vector3 sca = new Vector3(1.5f, 1.5f, 1.5f);

        int brand = MyPlayer.Instance.GetEnd();

        pos.x += 0.45f * (MyPlayer.Instance.getSize() - 1) + 0.55f;

        GameObject gb = MyPlayer.Instance.GetObEnd();
        gb.name = brand.ToString();
        gb.transform.localPosition = pos;
        gb.transform.localRotation = Quaternion.Euler(rot);
        gb.transform.localScale = sca;
        gb.AddComponent<TBrand>();
        gb.AddComponent<BoxCollider>();
        gb.GetComponent<TBrand>().setPos(pos);

        Destroy(GameObject.Find(PSpos.ToString()).gameObject);
        PSpos++;

        SceneGame.Instance.ChangePS();

        Debug.Log("Deal Done");
    }

    /// <summary>
    /// 其他玩家发牌
    /// </summary>
    public void otherDeal()
    {
        Vector3[] pos = new Vector3[4];
        pos[1] = new Vector3(3.5f, 0.1f, -1.8f);
        pos[2] = new Vector3(1.8f, 0.1f, 3.5f);
        pos[3] = new Vector3(-3.5f, 0.1f, 1.8f);

        Vector3[] rot = new Vector3[4];
        rot[1] = new Vector3(-90, 180, 0);
        rot[2] = new Vector3(-90, 90, 0);
        rot[3] = new Vector3(-90, 0, 0);

        Vector3[] C = new Vector3[4];
        C[1] = new Vector3(0, 0, 0.4f);
        C[2] = new Vector3(-0.4f, 0, 0);
        C[3] = new Vector3(0, 0, -0.4f);

        Vector3[] C1 = new Vector3[4];
        C1[1] = new Vector3(0, 0, 0.3f);
        C1[2] = new Vector3(-0.3f, 0, 0);
        C1[3] = new Vector3(0, 0, -0.3f);

        int target = GameSetting.Instance.target;

        int id = ChangePlayerId(target);

        int size = Players[id].GetSize() - 1;

        pos[id] += C1[id] * size;
        pos[id] += C[id];

        Players[id].AddOb();

        GameObject gb = Players[id].GetEndOb();
        gb.transform.localPosition = pos[id];
        gb.transform.localRotation = Quaternion.Euler(rot[id]);
        gb.transform.localScale = Vector3.one;

        Destroy(GameObject.Find(PSpos.ToString()).gameObject);
        PSpos++;

        SceneGame.Instance.ChangePS();

    }

    /// <summary>
    /// 上一张点击牌
    /// </summary>
    private GameObject LastObject;

    /// <summary>
    /// 点击出牌
    /// </summary>
    /// <param name="gb">点击牌</param>
    public void MouseClick(GameObject gb)
    {
        int num = int.Parse(gb.name);
        if (LastObject == gb)
        {
            if (GameSetting.Instance.target == GameSetting.Instance.Playerid)
            {
                ShowBrand(num);
                gb.GetComponent<TBrand>().OnClickShow();
            }
            else
            {
                LastObject.GetComponent<TBrand>().Down();
                LastObject = null;
            }            
        }
        else if (LastObject != null)
        {
            LastObject.GetComponent<TBrand>().Down();
            gb.GetComponent<TBrand>().OnClickUp();
            LastObject = gb;
        }
        else
        {
            gb.GetComponent<TBrand>().OnClickUp();
            LastObject = gb;
        }
    }

    /// <summary>
    /// 出牌
    /// </summary>
    public void ShowBrand(int num)
    {
        ActionParam param = new ActionParam();

        param["roomID"] = GameSetting.Instance.roomID;
        param["brand"] = num;
        param["playerId"] = GameSetting.Instance.Playerid;

        WebLogic.Instance.Send((int)ActionType.RadioBrand, param);

        int p = -1;

        if ((p = MyPlayer.Instance.FindId(LastObject)) != -1)
        {
            MyPlayer.Instance.Remove(p);
        }

        MyPlayer.Instance.Sort();

        Vector3 pos = new Vector3(-3.95f, 0.5f, -3.5f);

        for (int i = 0; i < MyPlayer.Instance.getSize(); i++)
        {
            GameObject gb = MyPlayer.Instance.GetOb(i);
            pos.x += 0.45f;
            gb.transform.localPosition = pos;
            gb.GetComponent<TBrand>().setPos(pos);
        }

        GameSetting.Instance.target = -1;

        LastObject = null;

        RespondOperation(0);
    }

    /// <summary>
    /// 转换玩家id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private int ChangePlayerId(int id)
    {
        int change = GameSetting.Instance.Playerid;
        int k = id - change;
        if (k < 0) { k += 4; }
        return k;
    }

    /// <summary>
    /// 其他玩家出牌展示
    /// </summary>
    /// <param name="num"></param>
    /// <param name="playerid"></param>
    public void OtherShowBrand(int num, int playerid)
    {
        int id = ChangePlayerId(playerid);

        GameObject g = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + num.ToString(), true);
        g.transform.localPosition = showPos[id];
        g.transform.localRotation = Quaternion.Euler(Oprot[id]);
        g.transform.localScale = Vector3.one;
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

        if (playerid != GameSetting.Instance.Playerid)
        {
            Players[id].RemoveEndGb();
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

        if (MyPlayer.Instance.isHu(num))
        {
            Guo = false;
            SceneGame.Instance.showButton("Hu");
        }
        if (MyPlayer.Instance.isTing())
        {
            Guo = false;
            SceneGame.Instance.showButton("Ting");
        }
        if (MyPlayer.Instance.isGang(num))
        {
            Guo = false;
            SceneGame.Instance.showButton("Gang");
        }
        if (MyPlayer.Instance.isPeng(num))
        {
            Guo = false;
            SceneGame.Instance.showButton("Peng");
        }
        if (MyPlayer.Instance.isChi(num))
        {
            Guo = false;
            SceneGame.Instance.showButton("Chi");
        }

        if (Guo)
        {
            RespondOperation(0);
        }
    }

    /// <summary>
    /// 展示吃，碰，杠
    /// </summary>
    /// <param name="num"></param>
    /// <param name="playerid"></param>
    /// <param name="level"></param>
    public void ShowOp(int num, int playerid, int level)
    {
        int id = ChangePlayerId(playerid);

        Debug.Log(num);

        switch (level)
        {
            case 1:
                for (int i = 2; i >= 0; i--)
                {
                    GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + (num + i), true);
                    gb.transform.localPosition = Oppos[id];
                    gb.transform.localRotation = Quaternion.Euler(Oprot[id]);
                    gb.transform.localScale = Vector3.one;
                    Oppos[id] += OpC[id];
                }
                break;
            case 2:
                for (int i = 0; i < 3; i++)
                {
                    GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + num, true);
                    gb.transform.localPosition = Oppos[id];
                    gb.transform.localRotation = Quaternion.Euler(Oprot[id]);
                    gb.transform.localScale = Vector3.one;
                    Oppos[id] += OpC[id];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + num, true);
                    gb.transform.localPosition = Oppos[id];
                    gb.transform.localRotation = Quaternion.Euler(Oprot[id]);
                    gb.transform.localScale = Vector3.one;
                    Oppos[id] += OpC[id];
                }
                break;
        }

        if (level != 0)
        {
            Oppos[id] += OpCC[id];
            GameObject.Destroy(LastShowBrand);
            showPos[LastShowMan] = showLastP[LastShowMan];
        }

        if (GameSetting.Instance.Playerid == GameSetting.Instance.target)
        {
            OpAnims(level);
            if (level == 0) FlowOfGame.Instance.Deal = true;
        }
        else
        {
            Players[id].Operation(level);
        }

    }

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
        int[] cc = MyPlayer.Instance.chi;

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
                MyPlayer.Instance.GetOb(c[i, 0]).GetComponent<TBrand>().OnClickUp();
                MyPlayer.Instance.GetOb(c[i, 1]).GetComponent<TBrand>().OnClickUp();
            }
        }
        else
        {
            if (MyPlayer.Instance.Get(c[0, 0]) < numOp) numOp = MyPlayer.Instance.Get(c[0, 0]);
            ChiPos[0] = c[0, 0];
            ChiPos[1] = c[0, 1];
            RespondOperation(1);
        }

    }

    /// <summary>
    /// 选择动画
    /// </summary>
    /// <param name="gb"></param>
    /// <param name="Click"></param>
    public void ChangeAnimation(GameObject gb, bool Click)
    {
        if (gb == null)
        {
            for (int i = 0; i < g; i++)
            {
                MyPlayer.Instance.GetOb(c[i, 0]).GetComponent<TBrand>().OnClickUp();
                MyPlayer.Instance.GetOb(c[i, 1]).GetComponent<TBrand>().OnClickUp();
            }
            return;
        }

        int tt = -1;

        for (int i = 0; i < g; i++)
        {
            MyPlayer.Instance.GetOb(c[i, 0]).GetComponent<TBrand>().Down();
            MyPlayer.Instance.GetOb(c[i, 1]).GetComponent<TBrand>().Down();
            if (MyPlayer.Instance.GetOb(c[i, 0]) == gb)
            {
                tt = i;
            }
        }

        if (tt != -1)
        {
            if (Click)
            {
                if (MyPlayer.Instance.Get(c[tt, 0]) < numOp) numOp = MyPlayer.Instance.Get(c[tt, 0]);
                ChiPos[0] = c[tt, 0];
                ChiPos[1] = c[tt, 1];
                RespondOperation(1);
            }
            else
            {
                MyPlayer.Instance.GetOb(c[tt, 0]).GetComponent<TBrand>().OnClickUp();
                MyPlayer.Instance.GetOb(c[tt, 1]).GetComponent<TBrand>().OnClickUp();
            }
        }
        else
        {
            for (int i = 0; i < g; i++)
            {
                MyPlayer.Instance.GetOb(c[i, 0]).GetComponent<TBrand>().OnClickUp();
                MyPlayer.Instance.GetOb(c[i, 1]).GetComponent<TBrand>().OnClickUp();
            }
        }
    }

    public void OpAnims(int level)
    {
        switch (level)
        {
            case 1:
                MyPlayer.Instance.Remove(ChiPos[1]);
                MyPlayer.Instance.Remove(ChiPos[0]);
                break;

            case 2:
                int[] P = MyPlayer.Instance.peng;
                MyPlayer.Instance.Remove(P[1]);
                MyPlayer.Instance.Remove(P[0]);
                break;

            case 3:
                int[] G = MyPlayer.Instance.gang;
                MyPlayer.Instance.Remove(G[2]);
                MyPlayer.Instance.Remove(G[1]);
                MyPlayer.Instance.Remove(G[0]);
                break;
        }

        MyPlayer.Instance.Sort();

        Vector3 pos = new Vector3(-3.95f, 0.5f, -3.5f);

        for (int i = 0; i < MyPlayer.Instance.getSize(); i++)
        {
            GameObject gb = MyPlayer.Instance.GetOb(i);
            pos.x += 0.45f;
            gb.transform.localPosition = pos;
            gb.GetComponent<TBrand>().setPos(pos);
        }

    }

    /// <summary>
    /// 响应玩家操作
    /// </summary>
    /// <param name="Level">操作等级</param>
    public void RespondOperation(int Level)
    {
        ActionParam param = new ActionParam();

        param["roomID"] = GameSetting.Instance.roomID;
        param["playerId"] = GameSetting.Instance.Playerid;
        param["num"] = numOp;
        param["level"] = Level;

        WebLogic.Instance.Send((int)ActionType.RespondOperation, param);
    }

    /// <summary>
    /// 事件响应
    /// </summary>
    /// <param name="id">事件id</param>
    /// <param name="param">事件包</param>
    /// <returns></returns>
    public bool HandleEvent(int id, ActionParam param)
    {
        
        int key = (int)param["ActionType"];

        switch (key)
        {
            case (int)ActionType.brandRadioResult:
                int brand = (int)param["brand"];
                int playerid = (int)param["playerid"];
                OtherShowBrand(brand, playerid);
                break;

            case (int)ActionType.Logic:
                brand = (int)param["brand"];
                MyPlayer.Instance.Add(brand);
                FlowOfGame.Instance.DealDone = true;
                break;

            case (int)ActionType.playerIdRadio:
                int num = (int)param["num"];
                int level = (int)param["level"];
                int PlayerId = (int)param["PlayerId"];
                GameSetting.Instance.target = PlayerId;
                ShowOp(num, PlayerId, level);
                otherDeal();
                break;

            case (int)ActionType.GetPlayerId:
                GameSetting.Instance.Playerid = (int)param["playerId"];
                GameSetting.Instance.target = (int)param["target"];
                GameSetting.Instance.StartNum = (int)param["StartNum"];
                FlowOfGame.Instance.InitTable();
                break;
        }

        return false;
    }

    /// <summary>
    /// 检查手牌获取
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckHandDeal()
    {
        yield return new WaitForSeconds(1f);
        if (MyPlayer.Instance.getSize() == 13)
        {
            HandDeal();
        }
        else
        {
            StartCoroutine(CheckHandDeal());
        }
    }
}