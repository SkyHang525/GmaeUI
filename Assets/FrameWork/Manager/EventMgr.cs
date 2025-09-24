using System;
using System.Collections.Generic;

public class Caller
{
    public Action<object> Callback;
    public object Self;
}

public class EventMgr
{
    private static EventMgr _instance;
    public static EventMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EventMgr();
            }
            return _instance;
        }
    }

    private Dictionary<string, List<Caller>> eventNameMap = new Dictionary<string, List<Caller>>();

    public void Init()
    {
        // 需要初始化逻辑的话，可以写在这里
    }

    /// <summary>
    /// 添加事件监听器
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="callback">回调函数</param>
    /// <param name="self">回调函数中的this指向</param>
    public void AddListener(string eventName, Action<object> callback, object self)
    {
        if (!eventNameMap.TryGetValue(eventName, out var eventList))
        {
            eventList = new List<Caller>();
            eventNameMap[eventName] = eventList;
        }

        Caller caller = new Caller
        {
            Callback = callback,
            Self = self
        };
        eventList.Add(caller);
    }

    /// <summary>
    /// 触发指定事件，并传递数据给事件监听器
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="udata">传递给事件监听器的数据</param>
    public void Emit(string eventName, object udata = null)
    {
        if (!eventNameMap.TryGetValue(eventName, out var eventList))
        {
            return;
        }

        for (int i = 0; i < eventList.Count; i++)
        {
            var caller = eventList[i];
            if (caller == null || caller.Callback == null)
                continue;

            caller.Callback.Invoke(udata);
        }
    }

    /// <summary>
    /// 移除事件监听器
    /// </summary>
    /// <param name="eventName">事件名称</param>
    /// <param name="callback">回调函数</param>
    /// <param name="self">绑定对象</param>
    public void RemoveEvent(string eventName, Action<object> callback, object self)
    {
        if (!eventNameMap.TryGetValue(eventName, out var eventList))
        {
            return;
        }

        for (int i = 0; i < eventList.Count; i++)
        {
            var caller = eventList[i];
            if (caller.Callback == callback && caller.Self == self)
            {
                eventList.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// 从事件映射中移除与指定对象关联的事件
    /// </summary>
    /// <param name="self">要从事件映射中移除的对象</param>
    public void RemoveSelfEvent(object self)
    {
        foreach (var kv in eventNameMap)
        {
            var eventList = kv.Value;
            for (int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].Self == self)
                {
                    eventList.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
