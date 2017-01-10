using UnityEngine;
using System.Collections;

public class LogicOfGame {

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
    /// 初始化牌桌
    /// </summary>
    public void InitTable()
    {
        showPos[0] = new Vector3(-0.6f, 0, -1.13f);

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
    /// 发手牌
    /// </summary>
    public void HandDeal()
    {
        //NetWriter.SetUrl("127.0.0.1:9001");
        //for (int i = 0; i < 13; i++)
        //{
        //    Net.Instance.Send(2003, CallBack, null);
        //}

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
        }
        otherHandDeal();
    }

    /// <summary>
    /// 服务器回牌
    /// </summary>
    /// <param name="actionResult"></param>
    //public void CallBack(ActionResult actionResult)
    //{
    //    int num = actionResult.Get<int>("brand");
    //    MyPlayer.Instance.addBrnad(num);
    //}

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
    /// 发牌
    /// </summary>
    public void Deal(int num)
    {
        //NetWriter.SetUrl("127.0.0.1:9001");
        //Net.Instance.Send(2003, CallBack, null);

        Vector3 pos = new Vector3(-3.95f, 0.5f, -3.5f);
        Vector3 rot = new Vector3(0, 0, -90);
        Vector3 sca = new Vector3(1.5f, 1.5f, 1.5f);

        //int brand = MyPlayer.Instance.GetEndBrand();
        //int size = MyPlayer.Instance.getSize();

        pos.x += 0.45f * 13 + 0.7f;

        GameObject gb = ResourceMgr.Instance.CreateGameObject("Mahjong/mj" + num, true);
        gb.name = num.ToString();
        gb.transform.localPosition = pos;
        gb.transform.localRotation = Quaternion.Euler(rot);
        gb.transform.localScale = sca;
        gb.AddComponent<TBrand>();
        gb.AddComponent<BoxCollider>();
        

    }

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

    Vector3[] showPos = new Vector3[4];

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

        //服务器广播num
    }

    /// <summary>
    /// 玩家可操作展示
    /// </summary>
    /// <param name="num"></param>
    public void ShowOperation(int num)
    {
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
    /// 响应玩家操作
    /// </summary>
    /// <param name="Level">操作等级</param>
    public void RespondOperation(int Level)
    {
        //向服务器发送响应等级
    }

}
