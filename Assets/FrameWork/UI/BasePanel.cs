using UnityEngine;
using System.Collections.Generic;

public class BasePanel
{
    // 资源路径名称
    protected string _resName;
    // 弹窗的根节点
    protected GameObject _rootView;
    // 弹窗的层级
    protected UILayer _layer = UILayer.UiPanel;

    public BasePanel(object msg = null)
    {
        this._rootView = null;
    }

    /// <summary>
    /// 获取指定名称的子节点
    /// </summary>
    public GameObject GetChild(string name)
    {
        if (_rootView == null)
        {
            return null;
        }
        return _rootView.transform.Find(name)?.gameObject;
    }

    public Transform GetParent()
    {
        if (_rootView == null)
        {
            return null;
        }
        return _rootView.transform.parent;
    }

    /// <summary>
    /// 向根视图添加子对象
    /// </summary>
    public GameObject AddChild(GameObject obj)
    {
        if (_rootView == null)
        {
            return null;
        }
        obj.transform.SetParent(_rootView.transform, false);
        return obj;
    }

    /// <summary>
    /// 从根视图中移除指定的子对象
    /// </summary>
    public GameObject RemoveChild(GameObject obj)
    {
        if (_rootView == null)
        {
            return null;
        }
        obj.transform.SetParent(null);
        return obj;
    }

    public int GetSiblingIndex()
    {
        if (_rootView == null)
        {
            return 0;
        }
        return _rootView.transform.GetSiblingIndex();
    }

    /// <summary>
    /// 显示UI组件
    /// </summary>
    public async void Show(object msg = null, System.Action callback = null, bool isShow = true)
    {
        if (_rootView == null || !_rootView.activeSelf)
        {
            var uiPrefab = await ResMgr.Instance.AwaitGetAsset<GameObject>(_resName);
            _rootView = GameObject.Instantiate(uiPrefab);
            OnCreate(msg);
        }
        _rootView.SetActive(true);
        _rootView.transform.SetAsLastSibling();
        if (isShow)
        {
            OnShow(msg);
            InitMessage();
        }
        callback?.Invoke();
    }

    /// <summary>
    /// 创建时调用的方法
    /// </summary>
    public virtual void OnCreate(object msg = null)
    {
        if (_rootView.transform.parent == null)
        {
            UILayerManager.AddWindow(_layer, _rootView);
        }
        _rootView.SetActive(true);
    }

    /// <summary>
    /// 关闭视图
    /// </summary>
    public void Close(System.Action callback = null)
    {
        if (_rootView == null)
        {
            return;
        }

        EventMgr.Instance.RemoveSelfEvent(this);
        GameObject.Destroy(_rootView);
        _rootView = null;
        callback?.Invoke();
    }

    /// <summary>
    /// 隐藏视图
    /// </summary>
    public void Hide(System.Action callback = null)
    {
        if (_rootView == null)
        {
            return;
        }
        OnHide();
        _rootView.SetActive(false);
        EventMgr.Instance.RemoveSelfEvent(this);
        callback?.Invoke();
    }

    /// <summary>
    /// 显示时的回调方法，可重定义
    /// </summary>
    protected virtual void OnShow(object msg = null)
    {
        ShowAnimator();
    }

    protected void ShowAnimator()
    {
        Transform mainNode = GetChild("mainNode")?.transform;
        if (mainNode != null)
        {
            mainNode.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            LeanTween.scale(mainNode.gameObject, new Vector3(1f, 1f, 1f), 0.2f);
        }
    }

    /// <summary>
    /// 隐藏时的回调方法
    /// </summary>
    protected virtual void OnHide()
    {
        // 可自定义隐藏时的操作
    }

    /// <summary>
    /// 初始化消息
    /// </summary>
    protected void InitMessage()
    {
        // 可自定义初始化逻辑
    }

    /// <summary>
    /// 设置关闭按钮
    /// </summary>
    public void SetCloseButton(string name)
    {
        GameObject closeBtn = GetChild(name);
        if (closeBtn != null)
        {
            closeBtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(CloseBtnOnClick);
        }
    }

    /// <summary>
    /// 点击关闭按钮时的回调函数
    /// </summary>
    private void CloseBtnOnClick()
    {
        Hide();
    }

    /// <summary>
    /// 销毁实例并释放相关资源
    /// </summary>
    public void Dispose()
    {
        // 清理资源
    }

    public void Update(float dt)
    {
        // 更新逻辑
    }

    public bool IsShow()
    {
        return _rootView != null && _rootView.activeSelf;
    }
}
