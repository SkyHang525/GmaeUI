using System;
using System.Collections.Generic;
using UnityEngine;

public static class PanelManager
{
    // �洢�Ѵ򿪵Ĵ��ڣ�Key Ϊ BasePanel �����ͣ�Value Ϊ BasePanel ʵ��
    private static Dictionary<Type, BasePanel> _dictWindow = new Dictionary<Type, BasePanel>();

    /// <summary>
    /// ��һ�����
    /// </summary>
    public static void Open<T>(object msg = null, Action callback = null, bool isShow = true) where T : BasePanel
    {
        Type panelType = typeof(T);

        if (IsBasePanel(panelType))
        {
            Debug.LogWarning("�ظ��򿪴���: " + panelType);
            return;
        }

        BasePanel panel;
        if (!_dictWindow.ContainsKey(panelType))
        {
            panel = (T)Activator.CreateInstance(panelType, new object[] { msg });
            _dictWindow[panelType] = panel;
        }
        else
        {
            panel = _dictWindow[panelType];
        }

        panel.Show(msg, callback, isShow);
    }

    /// <summary>
    /// �ر�ָ��������Ĵ���
    /// </summary>
    public static void Close<T>(Action callback = null) where T : BasePanel
    {
        Type panelType = typeof(T);

        if (!_dictWindow.TryGetValue(panelType, out BasePanel panel))
        {
            return;
        }

        panel.Close(callback);
        _dictWindow.Remove(panelType);
    }

    /// <summary>
    /// ����ָ������
    /// </summary>
    public static void Hide<T>(Action callback = null) where T : BasePanel
    {
        Type panelType = typeof(T);

        if (!_dictWindow.TryGetValue(panelType, out BasePanel panel) || !panel.IsShow())
        {
            return;
        }

        panel.Hide(callback);
    }

    /// <summary>
    /// ��ȡ BasePanel ʵ��
    /// </summary>
    public static BasePanel GetBasePanel<T>() where T : BasePanel
    {
        Type panelType = typeof(T);

        if (!_dictWindow.TryGetValue(panelType, out BasePanel panel) || !panel.IsShow())
        {
            return null;
        }

        return panel;
    }

    /// <summary>
    /// �жϴ����Ƿ��
    /// </summary>
    public static bool IsBasePanel(Type panelType)
    {
        if (!_dictWindow.TryGetValue(panelType, out BasePanel panel))
        {
            return false;
        }

        return panel != null && panel.IsShow();
    }

    /// <summary>
    /// ����Ļ������ʾһ�����浯��
    /// </summary>
    public static void ShowCenterAlert(string msg, string btnName, Action onBtnFunc)
    {
        var data = new Dictionary<string, object>();
        data["btnCount"] = 1;
        data["msg"] = msg;
        data["btnName"] = btnName;
        data["onBtnFunc"] = onBtnFunc;

        // ����ɵ��� PanelManager.Open<AlertPanel>(data);
    }

    /// <summary>
    /// ��ʾ����������ť�ľ����
    /// </summary>
    public static void ShowAlert(string msg, string btnName1, Action onBtnFunc1, string btnName2, Action onBtnFunc2)
    {
        var data = new Dictionary<string, object>();
        data["btnCount"] = 2;
        data["msg"] = msg;
        data["btnName1"] = btnName1;
        data["onBtnFunc1"] = onBtnFunc1;
        data["btnName2"] = btnName2;
        data["onBtnFunc2"] = onBtnFunc2;

        // ����ɵ��� PanelManager.Open<AlertPanel>(data);
    }

    /// <summary>
    /// �������д򿪵����
    /// </summary>
    public static void Update(float dt)
    {
        foreach (var panel in _dictWindow.Values)
        {
            if (panel.IsShow())
            {
                panel.Update(dt);
            }
        }
    }
}
