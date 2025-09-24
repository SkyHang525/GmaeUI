using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class test : MonoBehaviour
{
    [SerializeField] private Image icon;
    async void Start()
    {
        await ResMgr.Instance.LoadAssetAll<Sprite>("img");
        await ResMgr.Instance.LoadAssetAll<GameObject>("prefab");
        await ResMgr.Instance.LoadAssetAll<AudioClip>("sound");
        icon.sprite = ResMgr.Instance.GetAsset<Sprite>("home_ui_bg");
        GameObject obj = ResMgr.Instance.GetAsset<GameObject>("Image");
        //GameObject go1 = Instantiate(obj, icon.transform);
        // ע���¼�
        EventMgr.Instance.AddListener("OnPlayerDead", (data) =>
        {
            UnityEngine.Debug.Log("�������: " + data);
        }, this);

        // �ɷ��¼�
        EventMgr.Instance.Emit("OnPlayerDead", "��Ѫ����");


        // �Ƴ�ĳ�����������¼�
        //EventMgr.Instance.RemoveSelfEvent(this);
        // �ɷ��¼�
        EventMgr.Instance.Emit("OnPlayerDead", "123132");

        // ��ʼ����Ч������
        SoundManager.Instance.Init();

        PanelManager.Open<TestPanel>(11);

    }
}
