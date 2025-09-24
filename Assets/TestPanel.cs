using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPanel : BasePanel
{
    public TestPanel(object msg = null)
    {
        _resName = "TestPanel";
        _layer = UILayer.UiPanel;
    }
    public override void OnCreate(object msg = null)
    {
        base.OnCreate(msg);
        Debug.Log("TestPanel OnCreate");
    }
    protected override void OnShow(object msg = null) { 
        base.OnShow(msg);
        Debug.Log("TestPanel OnShow"+msg);
    }
}
