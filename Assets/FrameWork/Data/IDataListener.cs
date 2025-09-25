/*
 * @Description: 数据模块监听器接口
 */
public interface IDataListener
{
    /// <summary>
    /// 通知数据变更
    /// </summary>
    /// <param name="eventKey">事件键，用于标识监听的事件</param>
    /// <param name="args">任意数量的参数，将传递给监听器的 notifyDataChange 方法</param>
    void NotifyDataChange(int eventKey, params object[] args);
}
