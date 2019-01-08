/***
 *
 *  Title: "热更新流程设计"课程项目
 *          学习HotFix 热补丁技术，小示例
 *
 *  Description:
 *        功能：[本脚本的主要功能描述] 
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
using XLua;

namespace HotUpdateModel
{
    [Hotfix]
    public class TestHotFix2 : MonoBehaviour
    {
        LuaEnv luaenv = new LuaEnv();

        private void Start()
        {
            Debug.Log("测试HotFix技术，示例2");
        }
        //调用函数
        public void InvokeInCsharp()
        {
            Debug.Log("这是在C#中执行的方法");
        }
        //lua语言执行的内容
        public void InvokeInLua()
        {
            luaenv.DoString(@"xlua.hotfix(
                                           CS.HotUpdateModel.TestHotFix2,'InvokeInCsharp',function()
                                             print('这是在lua中执行的方法')    
                                           end
                                         )");
        }
    }//Class_end
}