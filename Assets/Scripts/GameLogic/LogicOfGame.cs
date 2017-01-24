using UnityEngine;
using System.Collections;
using System;

public class LogicOfGame : IEventListener {

    #region 单例

    private static LogicOfGame _Instance = new LogicOfGame();

    public static LogicOfGame Instance
    {
        get
        {
            return _Instance;
        }
    }

    #endregion

    /// <summary>
    /// 吃碰杠位置
    /// </summary>
    private Vector3[] Oppos = new Vector3[4];
    /// <summary>
    /// 吃碰杠修改坐标
    /// </summary>
    private Vector3[] OpC = new Vector3[4];

    /// <summary>
    /// 出牌位置
    /// </summary>
    private Vector3[] showPos = new Vector3[4];
    /// <summary>
    /// 出牌修改坐标
    /// </summary>
    private Vector3[] showC = new Vector3[4];

    /// <summary>
    /// 构造函数
    /// </summary>
    public LogicOfGame()
    {
        WebLogic.Instance.AddEventListener(2, this);
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void InitDate()
    {
        ActionParam param = new ActionParam();
        param["roomID"] = GameSetting.Instance.roomID;

        WebLogic.Instance.Send((int)ActionType.GetPlayerId, param);
        
        Oppos[0] = new Vector3(2.5f, 0, -3.5f);
        Oppos[1] = new Vector3(3.5f, 0, 2.5f);
        Oppos[2] = new Vector3(-2.5f, 0, 3.5f);
        Oppos[3] = new Vector3(-3.5f, 0, -2.5f);

        OpC[0] = new Vector3();
        OpC[1] = new Vector3();
        OpC[2] = new Vector3();
        OpC[3] = new Vector3();

        showPos[0] = new Vector3(-0.6f, 0, -1.13f);
        showPos[1] = new Vector3(-1.13f, 0, 0.6f);
        showPos[2] = new Vector3(0.6f, 0, 1.13f);
        showPos[3] = new Vector3(1.13f, 0, -0.6f);

        showC[0] = new Vector3();
        showC[1] = new Vector3();
        showC[2] = new Vector3();
        showC[3] = new Vector3();
    }

    /// <summary>
    /// 初始化牌桌
    /// </summary>
    public void InitTable()
    {
        Vector3[] pos = new Vector3[4];
        pos[0] = new Vector3(2.85f, 0, 0);
        pos[1] = new Vector3(-2.85f, 0, 0);
        pos[2] = new Vector3(0, 0, 2.85f);
        pos[3] = new Vector3(0, 0, -2.85f);

        Vector3[] rot = new Vector3[4];
        rot[0] = new Vector3(0, 90, 0);
        rot[1] = new Vector3(0, 90, 0);
        rot[2] = new Vector3(0, 0, 0);
        rot[3] = new Vector3(0, 0, 0);

        for (int i = 0; i < 4; i++)
        {
            GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/brandmotain", false);
            gb.transform.localPosition = pos[i];
            gb.transform.localRotation = Quaternion.Euler(rot[i]);
            gb.transform.localScale = Vector3.one;
        }
        
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
            GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + brand, true);
            pos.x += 0.45f;
            gb.name = brand.ToString();
            gb.transform.localPosition = pos;
            gb.transform.localRotation = Quaternion.Euler(rot);
            gb.transform.localScale = sca;
            gb.AddComponent<TBrand>();
            gb.AddComponent<BoxCollider>();
            MyPlayer.Instance.AddOb(i, gb);
        }
        otherHandDeal();
    }

    /// <summary>
    /// 其他玩家手牌
    /// </summary>
    private void otherHandDeal()
    {
        Vector3[] pos = new Vector3[3];
        pos[0] = new Vector3(-3.5f, 0.1f, 0);
        pos[1] = new Vector3(3.5f, 0.1f, 0);
        pos[2] = new Vector3(0, 0.1f, 3.5f);
        Vector3[] rot = new Vector3[3];
        rot[0] = new Vector3(0, 90, 0);
        rot[1] = new Vector3(0, -90, 0);
        rot[2] = new Vector3(0, 180, 0);
        for (int i = 0; i < 3; i++)
        {
            GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/handbrands", true);
            gb.transform.localPosition = pos[i];
            gb.transform.localRotation = Quaternion.Euler(rot[i]);
            gb.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// 向服务端发送发牌请求
    /// </summary>
    public void DealInWeb()
    {
        ActionParam param = new ActionParam();

        param["roomID"] = GameSetting.Instance.roomID;

        WebLogic.Instance.Send((int)ActionType.Logic, param);
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

        pos.x += 0.45f * MyPlayer.Instance.getSize() + 0.7f;

        GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + brand, true);
        gb.name = brand.ToString();
        gb.transform.localPosition = pos;
        gb.transform.localRotation = Quaternion.Euler(rot);
        gb.transform.localScale = sca;
        gb.AddComponent<TBrand>();
        gb.AddComponent<BoxCollider>();
        

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
            ShowBrand(num);
            gb.GetComponent<TBrand>().OnClickShow();
            LastObject = null;
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
        GameObject g = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + num.ToString(), true);
        g.transform.localPosition = showPos[0];
        g.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        g.transform.localScale = Vector3.one;
        if (showPos[0].x > 0.3f)
        {
            showPos[0].x = -0.6f;
            showPos[0].z -= 0.4f;
        }
        else showPos[0].x += 0.3f;

        ActionParam param = new ActionParam();

        param["roomID"] = GameSetting.Instance.roomID;
        param["brand"] = num;
        param["playerId"] = GameSetting.Instance;

        WebLogic.Instance.Send((int)ActionType.RadioBrand, param);
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
        GameObject g = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + num.ToString(), true);
        g.transform.localPosition = showPos[playerid];
        g.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        g.transform.localScale = Vector3.one;
        if (showPos[playerid].x > 0.3f)
        {
            showPos[playerid].x = -0.6f;
            showPos[playerid].z -= 0.4f;
        }
        else showPos[playerid].x += 0.3f;
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

        if (MyPlayer.Instance.isHu(num))
        {
            SceneGame.Instance.showButton("Hu");
        }
        if (MyPlayer.Instance.isTing())
        {
            SceneGame.Instance.showButton("Ting");
        }
        if (MyPlayer.Instance.isGang(num))
        {
            SceneGame.Instance.showButton("Gang");
        }
        if (MyPlayer.Instance.isPeng(num))
        {
            SceneGame.Instance.showButton("Peng");
        }
        if (MyPlayer.Instance.isChi(num))
        {
            SceneGame.Instance.showButton("Chi");
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

        switch (level)
        {
            case 1:
                
                Oppos[id] += OpC[id];
                
                break;
        }
    }

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

        if (cc[0] != -1 && cc[1] != -1)
        {
            c[g, 0] = cc[0];
            c[g, 1] = cc[1];
            g++;
        }

        if (cc[1] != -1 && cc[2] != -1)
        {
            c[g, 0] = cc[0];
            c[g, 1] = cc[1];
            g++;
        }

        if (cc[2] != -1 && cc[3] != -1)
        {
            c[g, 0] = cc[0];
            c[g, 1] = cc[1];
            g++;
        }

        if (g > 1)
        {
            for (int i = 0; i < g; i++)
            {
                MyPlayer.Instance.GetOb(c[i, 0]).GetComponent<TBrand>().OnClickUp();
                MyPlayer.Instance.GetOb(c[i, 1]).GetComponent<TBrand>().OnClickUp();
            }
        }

        else
        {
            if (c[0, 1] < numOp) numOp = c[0, 1];
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

        for (int i = 0; i < g; i++)
        {
            if (MyPlayer.Instance.GetOb(c[i,0]) == gb)
            {
                if (Click)
                {
                    if (c[i, 0] < numOp) numOp = c[i, 0];
                }
                else
                {
                    MyPlayer.Instance.GetOb(c[i, 0]).GetComponent<TBrand>().OnClickUp();
                    MyPlayer.Instance.GetOb(c[i, 1]).GetComponent<TBrand>().OnClickUp();
                }
            }
            else
            {
                MyPlayer.Instance.GetOb(c[i, 0]).GetComponent<TBrand>().Down();
                MyPlayer.Instance.GetOb(c[i, 1]).GetComponent<TBrand>().Down();
            }
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
            case (int)ActionType.Logic:
                int brand = (int)param["brand"];
                MyPlayer.Instance.Add(brand);

                break;

            case (int)ActionType.playerIdRadio:
                if (GameSetting.Instance.Playerid == (int)param["playerId"])
                {

                }
                else
                {

                }
                break;
        }

        return false;
    }

}