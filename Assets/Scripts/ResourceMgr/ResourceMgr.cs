using UnityEngine;
using System.Collections;

public class ResourceMgr : MonoBehaviour {

    #region 单例

    private static ResourceMgr _Instance;
    public static ResourceMgr Instance
    {
        get
        {
            if (_Instance == null)
            {
                GameObject gb = new GameObject("_ResourceMgr");
                DontDestroyOnLoad(gb);
                _Instance = gb.AddComponent<ResourceMgr>();
            }
            return _Instance;
        }
    }

    #endregion

    /// <summary>
    /// 资源缓存哈希表
    /// </summary>
    private static Hashtable cacheHash = new Hashtable();

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">资源地址</param>
    /// <param name="isCache">是否缓存</param>
    /// <returns>资源或者空</returns>
    public T LoadResource<T>(string path, bool isCache) where T : Object
    {
        if (cacheHash.Contains(path))
        {
            return cacheHash[path] as T;
        }

        T resouceObj = Resources.Load<T>(path);

        if (resouceObj == null)
        {
            Debug.LogError("资源不存在 path = " + path);
            return null;
        }
        if (isCache)
        {
            cacheHash.Add(path, resouceObj);
        }
        return resouceObj;
    }

    /// <summary>
    /// 实例化资源
    /// </summary>
    /// <param name="path">资源地址</param>
    /// <param name="isCache">是否缓存</param>
    /// <returns>实例化物体</returns>
    public GameObject CreateGameObject(string path, bool isCache)
    {
        GameObject gb = LoadResource<GameObject>(path, isCache);
        return Instantiate(gb) as GameObject;
    }

}
