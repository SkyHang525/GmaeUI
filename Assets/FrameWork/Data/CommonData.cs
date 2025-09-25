/*
 * @Description: 数据模块基类
 */
using System.Collections.Generic;

public class CommonData
{
    // 数据内部刷新事件id
    private static int EVENT_ID = 0;

    // 数据内部刷新事件
    private Dictionary<int, HashSet<IDataListener>> allListenerList = new Dictionary<int, HashSet<IDataListener>>();

    /// <summary>
    /// 添加监听器
    /// </summary>
    /// <param name="eventKey">事件键，用于标识监听的事件</param>
    /// <param name="listener">监听器对象，实现了 IDataListener 接口</param>
    public void AddListener(int eventKey, IDataListener listener)
    {
        if (!allListenerList.ContainsKey(eventKey))
        {
            allListenerList[eventKey] = new HashSet<IDataListener>();
        }

        if (listener != null)
        {
            allListenerList[eventKey].Add(listener);
        }
    }

    /// <summary>
    /// 从监听列表中移除指定的监听器
    /// </summary>
    /// <param name="eventKey">事件键</param>
    /// <param name="listener">要移除的监听器</param>
    public void RemoveListener(int eventKey, IDataListener listener)
    {
        if (!allListenerList.ContainsKey(eventKey))
        {
            return;
        }

        var bigList = allListenerList[eventKey];
        if (bigList.Contains(listener))
        {
            bigList.Remove(listener);
        }
    }

    /// <summary>
    /// 通知数据变更
    /// </summary>
    /// <param name="eventKey">事件键</param>
    /// <param name="args">任意数量的参数</param>
    public void NotifyDataChange(int eventKey, params object[] args)
    {
        if (!allListenerList.ContainsKey(eventKey))
        {
            return;
        }

        var bigList = allListenerList[eventKey];
        if (bigList == null) return;

        foreach (var listener in bigList)
        {
            listener?.NotifyDataChange(eventKey, args);
        }
    }

    /// <summary>
    /// 获取事件ID，并自动递增
    /// </summary>
    public static int GetEventID()
    {
        return ++EVENT_ID;
    }
}
