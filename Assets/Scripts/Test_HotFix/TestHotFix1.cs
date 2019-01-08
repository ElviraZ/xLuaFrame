/***
 *
 *  Title: "热更新流程设计"课程项目
 *       
 *         测试“HotFix 技术”
 *
 *  Description:
 *        功能：
 *            仿照Xlua插件，定义自己的hotfix 小Demo
 *            
 *            本质就是 lua中定义的方法，来替换出现bug的C#脚本（类）
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
    public class TestHotFix1 :MonoBehaviour
    {
        LuaEnv luaenv = new LuaEnv();

        private void Start()
        {
            Debug.Log("测试lua中的‘热补丁’ 技术");
            //执行一个调用函数
            InvokeRepeating("InvokeInCsharp",1F,2F);
        }

        //调用函数
        void InvokeInCsharp()
        {
            Debug.Log("在C#中执行的方法");
        }

        //lua语言执行的内容
        public void InvokeInLua()
        {
            Debug.Log("准备lua调用");
            luaenv.DoString(@"xlua.hotfix(
                                           CS.HotUpdateModel.TestHotFix1,'InvokeInCsharp',function()
                                             print('在lua中执行的方法')    
                                           end
                                         )");
        }

    }//Class_end
}


