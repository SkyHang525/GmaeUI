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
        // 注册事件
        EventMgr.Instance.AddListener("OnPlayerDead", (data) =>
        {
            UnityEngine.Debug.Log("玩家死亡: " + data);
        }, this);

        // 派发事件
        EventMgr.Instance.Emit("OnPlayerDead", "掉血过多");


        // 移除某个对象所有事件
        //EventMgr.Instance.RemoveSelfEvent(this);
        // 派发事件
        EventMgr.Instance.Emit("OnPlayerDead", "123132");

        // 初始化音效管理器
        SoundManager.Instance.Init();

        PanelManager.Open<TestPanel>(11);

    }
}
