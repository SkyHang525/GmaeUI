using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResMgr
{
    private static ResMgr m_Instance = null;

    private Dictionary<string, Sprite> m_SpriteMap = new Dictionary<string, Sprite>();
    private Dictionary<string, Font> m_FontMap = new Dictionary<string, Font>();
    private Dictionary<string, AudioClip> m_AudioClipMap = new Dictionary<string, AudioClip>();
    private Dictionary<string, GameObject> m_GameObjectMap = new Dictionary<string, GameObject>();

    public static ResMgr Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new ResMgr();
            }
            return m_Instance;
        }
    }

    public void Init()
    {
        // 初始化资源管理器
    }

    // 异步加载指定 Asset 的资源
    private async System.Threading.Tasks.Task<T> AwaitLoadAsset<T>(string assetPath) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadAssetAsync<T>(assetPath);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogError($"Failed to load asset: {assetPath}");
            return null;
        }
    }

    // 获取资源
    public T GetAsset<T>(string name) where T : UnityEngine.Object
    {
        if (typeof(T) == typeof(Sprite))
        {
            return (T)(object)(m_SpriteMap.ContainsKey(name) ? m_SpriteMap[name] : null);
        }
        else if (typeof(T) == typeof(Font))
        {
            return (T)(object)(m_FontMap.ContainsKey(name) ? m_FontMap[name] : null);
        }
        else if (typeof(T) == typeof(AudioClip))
        {
            return (T)(object)(m_AudioClipMap.ContainsKey(name) ? m_AudioClipMap[name] : null);
        }
        else if (typeof(T) == typeof(GameObject))
        {
            return (T)(object)(m_GameObjectMap.ContainsKey(name) ? m_GameObjectMap[name] : null);
        }
        return null;
    }

    // 设置资源
    public void SetAsset<T>(string name, T asset) where T : UnityEngine.Object
    {
        if (asset is Sprite)
        {
            m_SpriteMap[name] = asset as Sprite;
        }
        else if (asset is Font)
        {
            m_FontMap[name] = asset as Font;
        }
        else if (asset is AudioClip)
        {
            m_AudioClipMap[name] = asset as AudioClip;
        }
        else if (asset is GameObject)
        {
            m_GameObjectMap[name] = asset as GameObject;
        }
    }

    // 获取指定资源
    public async System.Threading.Tasks.Task<T> AwaitGetAsset<T>(string assetPath) where T : UnityEngine.Object
    {
        T asset = GetAsset<T>(assetPath);
        if (asset != null) return asset;

        asset = await AwaitLoadAsset<T>(assetPath);
        if (asset != null)
        {
            SetAsset(assetPath, asset);
        }
        return asset;
    }

    // 异步加载资源
    public async System.Threading.Tasks.Task LoadAsset( string assetPath, System.Type assetType)
    {
        UnityEngine.Object asset = await AwaitLoadAsset<UnityEngine.Object>(assetPath);
        if (asset != null)
        {
            SetAsset(assetPath, asset);
        }
    }

    // 同时加载多个资源
    public async System.Threading.Tasks.Task LoadAssetAll<T>(string assetPath) where T : UnityEngine.Object
    {
        var handle = Addressables.LoadAssetsAsync<T>(assetPath, (obj) => {
            SetAsset(obj.name, obj);
            Debug.Log($"Loaded asset: {obj.name}");
        });
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Loaded asset: {assetPath}");
        }
        else
        {
            Debug.LogError($"Failed to load asset: {assetPath}");
        }
    }
}
