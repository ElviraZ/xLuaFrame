using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

using System;





/// <summary>
/// Lua   Call C#
/// 
/// 本脚本是启动Lua文件
/// </summary>
public class LuaCallCSharpBase : MonoBehaviour
{

    LuaEnv env = null;
    // Start is called before the first frame update
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require 'LuaCallCSharp'  ");//require不用加后缀
    }

    private void OnDestroy()
    {
        env.Dispose();
    }
}
