/*
 * @Description: ����ģ�����
 */
using System.Collections.Generic;

public class CommonData
{
    // �����ڲ�ˢ���¼�id
    private static int EVENT_ID = 0;

    // �����ڲ�ˢ���¼�
    private Dictionary<int, HashSet<IDataListener>> allListenerList = new Dictionary<int, HashSet<IDataListener>>();

    /// <summary>
    /// ��Ӽ�����
    /// </summary>
    /// <param name="eventKey">�¼��������ڱ�ʶ�������¼�</param>
    /// <param name="listener">����������ʵ���� IDataListener �ӿ�</param>
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
    /// �Ӽ����б����Ƴ�ָ���ļ�����
    /// </summary>
    /// <param name="eventKey">�¼���</param>
    /// <param name="listener">Ҫ�Ƴ��ļ�����</param>
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
    /// ֪ͨ���ݱ��
    /// </summary>
    /// <param name="eventKey">�¼���</param>
    /// <param name="args">���������Ĳ���</param>
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
    /// ��ȡ�¼�ID�����Զ�����
    /// </summary>
    public static int GetEventID()
    {
        return ++EVENT_ID;
    }
}
