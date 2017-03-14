using UnityEngine;
using System.Collections;

public class SceneManager {

    #region 单例

    private static SceneManager _Instance = new SceneManager();

    public static SceneManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    #endregion

    public void SceneSwitch(SceneType.Type type)
    {
        string name = type.ToString();
        Application.LoadLevel(name);
    }

}
