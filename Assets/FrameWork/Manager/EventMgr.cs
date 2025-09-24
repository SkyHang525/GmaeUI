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
        // ��Ҫ��ʼ���߼��Ļ�������д������
    }

    /// <summary>
    /// ����¼�������
    /// </summary>
    /// <param name="eventName">�¼�����</param>
    /// <param name="callback">�ص�����</param>
    /// <param name="self">�ص������е�thisָ��</param>
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
    /// ����ָ���¼������������ݸ��¼�������
    /// </summary>
    /// <param name="eventName">�¼�����</param>
    /// <param name="udata">���ݸ��¼�������������</param>
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
    /// �Ƴ��¼�������
    /// </summary>
    /// <param name="eventName">�¼�����</param>
    /// <param name="callback">�ص�����</param>
    /// <param name="self">�󶨶���</param>
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
    /// ���¼�ӳ�����Ƴ���ָ������������¼�
    /// </summary>
    /// <param name="self">Ҫ���¼�ӳ�����Ƴ��Ķ���</param>
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
