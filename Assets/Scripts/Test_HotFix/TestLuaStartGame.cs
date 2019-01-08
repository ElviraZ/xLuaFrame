/***
 *
 *  Title: "热更新流程设计"课程项目
 *         
 *         开发一个“年会倒计时”。
 *         
 *         通过ABFW框架，启动UI界面。
 *
 *  Description:
 *        功能：
 *            本脚本启动lua热补丁功能。
 *
 *  Date: 2018
 * 
 *  Version: 1.0
 *
 *  Modify Recorder:
 *     
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HotUpdateModel
{
    public class TestLuaStartGame : MonoBehaviour, IStartGame
    {
        public void ReceiveInfoStartRuning()
        {
            Debug.Log(GetType()+ "/ReceiveInfoStartRuning()/ 开始lua补丁调用");
            //调用lua脚本
            LuaHelper.GetInstance().DoString("require 'Test_ProjectHotFixListInfo'");                       //引入lua脚本
            LuaHelper.GetInstance().CallLuaFunction("Test_ProjectHotFixListInfo", "HotFixScriptsMethod");   //调用lua函数

        }
    }//Class_end
}


