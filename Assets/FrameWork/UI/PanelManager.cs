using System;
using System.Collections.Generic;
using UnityEngine;

public static class PanelManager
{
    // 存储已打开的窗口，Key 为 BasePanel 的类型，Value 为 BasePanel 实例
    private static Dictionary<Type, BasePanel> _dictWindow = new Dictionary<Type, BasePanel>();

    /// <summary>
    /// 打开一个面板
    /// </summary>
    public static void Open<T>(object msg = null, Action callback = null, bool isShow = true) where T : BasePanel
    {
        Type panelType = typeof(T);

        if (IsBasePanel(panelType))
        {
            Debug.LogWarning("重复打开窗口: " + panelType);
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
    /// 关闭指定窗口类的窗口
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
    /// 隐藏指定窗口
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
    /// 获取 BasePanel 实例
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
    /// 判断窗口是否打开
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
    /// 在屏幕中央显示一个警告弹窗
    /// </summary>
    public static void ShowCenterAlert(string msg, string btnName, Action onBtnFunc)
    {
        var data = new Dictionary<string, object>();
        data["btnCount"] = 1;
        data["msg"] = msg;
        data["btnName"] = btnName;
        data["onBtnFunc"] = onBtnFunc;

        // 这里可调用 PanelManager.Open<AlertPanel>(data);
    }

    /// <summary>
    /// 显示带有两个按钮的警告框
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

        // 这里可调用 PanelManager.Open<AlertPanel>(data);
    }

    /// <summary>
    /// 更新所有打开的面板
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
