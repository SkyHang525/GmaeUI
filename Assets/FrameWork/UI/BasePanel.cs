using UnityEngine;
using System.Collections.Generic;

public class BasePanel
{
    // ��Դ·������
    protected string _resName;
    // �����ĸ��ڵ�
    protected GameObject _rootView;
    // �����Ĳ㼶
    protected UILayer _layer = UILayer.UiPanel;

    public BasePanel(object msg = null)
    {
        this._rootView = null;
    }

    /// <summary>
    /// ��ȡָ�����Ƶ��ӽڵ�
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
    /// �����ͼ����Ӷ���
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
    /// �Ӹ���ͼ���Ƴ�ָ�����Ӷ���
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
    /// ��ʾUI���
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
    /// ����ʱ���õķ���
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
    /// �ر���ͼ
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
    /// ������ͼ
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
    /// ��ʾʱ�Ļص����������ض���
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
    /// ����ʱ�Ļص�����
    /// </summary>
    protected virtual void OnHide()
    {
        // ���Զ�������ʱ�Ĳ���
    }

    /// <summary>
    /// ��ʼ����Ϣ
    /// </summary>
    protected void InitMessage()
    {
        // ���Զ����ʼ���߼�
    }

    /// <summary>
    /// ���ùرհ�ť
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
    /// ����رհ�ťʱ�Ļص�����
    /// </summary>
    private void CloseBtnOnClick()
    {
        Hide();
    }

    /// <summary>
    /// ����ʵ�����ͷ������Դ
    /// </summary>
    public void Dispose()
    {
        // ������Դ
    }

    public void Update(float dt)
    {
        // �����߼�
    }

    public bool IsShow()
    {
        return _rootView != null && _rootView.activeSelf;
    }
}
