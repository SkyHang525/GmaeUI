using UnityEngine;

public enum UILayer
{
    Main,      // 主界面
    UiPanel,   // 界面面板
    Second,    // 特效
    Tips       // 提示
}

public static class UILayerManager
{
    // 图层字典
    private static readonly System.Collections.Generic.Dictionary<UILayer, GameObject> _dictLayer = new System.Collections.Generic.Dictionary<UILayer, GameObject>();

    private static readonly int[] ZINDEX = { 0, 10, 50, 100 };

    // 清理所有图层
    public static void Clear()
    {
        _dictLayer.Clear();
    }

    /// <summary>
    /// 创建一个UI图层面板
    /// </summary>
    /// <param name="layer">要创建的图层</param>
    private static void CreateUILayerPanel(UILayer layer)
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (canvas.transform.Find(layer.ToString()) != null)
        {
            return; // 如果已经有这个层，直接返回
        }

        GameObject layerGO = new GameObject(layer.ToString());
        layerGO.AddComponent<RectTransform>();
        layerGO.transform.SetParent(canvas.transform, false);

        RectTransform rectTransform = layerGO.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(canvas.GetComponent<RectTransform>().rect.width, canvas.GetComponent<RectTransform>().rect.height);

        _dictLayer[layer] = layerGO;
        layerGO.transform.SetSiblingIndex(ZINDEX[(int)layer]);
    }

    /// <summary>
    /// 添加窗口组件到指定的UI层
    /// </summary>
    /// <param name="layer">UI层</param>
    /// <param name="com">要添加的组件节点</param>
    public static void AddWindow(UILayer layer, GameObject com)
    {
        if (_dictLayer.ContainsKey(layer) == false || _dictLayer[layer] == null)
        {
            CreateUILayerPanel(layer);
        }

        GameObject layerGO = _dictLayer[layer];
        com.transform.SetParent(layerGO.transform, false);
    }

    /// <summary>
    /// 创建所有的UI层面板
    /// </summary>
    public static void CreateUILayerPanelAll()
    {
        try
        {
            for (int i = 0; i < 3; i++) // 创建三个图层
            {
                CreateUILayerPanel((UILayer)i);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("UILayerManager.CreateUILayerPanelAll error: " + e.ToString());
        }
    }
}
