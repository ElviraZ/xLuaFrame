using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

using System;


/// <summary>
/// C# call Lua  function
/// 方式一：
///采用  luaFunction，效率低，不推荐
///优点：不用生成代码也不用配置表
///
/// 
/// 
/// 
/// 
/// </summary>
public class CallLuaFunctionByLuaFunc : MonoBehaviour
{
    LuaEnv env = null;
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");



        LuaFunction luaFunction1 = env.Global.Get<LuaFunction>("myProcFunc1");
        luaFunction1.Call();

        LuaFunction luaFunction2= env.Global.Get<LuaFunction>("myProcFunc2");
        luaFunction2.Call(10,20);

        LuaFunction luaFunction3= env.Global.Get<LuaFunction>("myProcFunc3");
        luaFunction3.Call(10, 20,30);

        LuaFunction luaFunction4 = env.Global.Get<LuaFunction>("myProcFunc4");
      object[] resultArray=  luaFunction4.Call(10, 20);

        for (int i = 0; i < resultArray.Length; i++)
        {
            Debug.Log(resultArray[i]);
        }

    }
    private void OnDestroy()
    {
     
        env.Dispose();
    }
}

