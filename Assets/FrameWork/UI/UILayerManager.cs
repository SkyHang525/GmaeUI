using UnityEngine;

public enum UILayer
{
    Main,      // ������
    UiPanel,   // �������
    Second,    // ��Ч
    Tips       // ��ʾ
}

public static class UILayerManager
{
    // ͼ���ֵ�
    private static readonly System.Collections.Generic.Dictionary<UILayer, GameObject> _dictLayer = new System.Collections.Generic.Dictionary<UILayer, GameObject>();

    private static readonly int[] ZINDEX = { 0, 10, 50, 100 };

    // ��������ͼ��
    public static void Clear()
    {
        _dictLayer.Clear();
    }

    /// <summary>
    /// ����һ��UIͼ�����
    /// </summary>
    /// <param name="layer">Ҫ������ͼ��</param>
    private static void CreateUILayerPanel(UILayer layer)
    {
        Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (canvas.transform.Find(layer.ToString()) != null)
        {
            return; // ����Ѿ�������㣬ֱ�ӷ���
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
    /// ��Ӵ��������ָ����UI��
    /// </summary>
    /// <param name="layer">UI��</param>
    /// <param name="com">Ҫ��ӵ�����ڵ�</param>
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
    /// �������е�UI�����
    /// </summary>
    public static void CreateUILayerPanelAll()
    {
        try
        {
            for (int i = 0; i < 3; i++) // ��������ͼ��
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
