/*
 * @Description: ����ģ��������ӿ�
 */
public interface IDataListener
{
    /// <summary>
    /// ֪ͨ���ݱ��
    /// </summary>
    /// <param name="eventKey">�¼��������ڱ�ʶ�������¼�</param>
    /// <param name="args">���������Ĳ����������ݸ��������� notifyDataChange ����</param>
    void NotifyDataChange(int eventKey, params object[] args);
}
