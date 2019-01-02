using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;








using XLua;
/// <summary>
/// C# call Lua  by   LuaTable
/// 方式四：
/// 使用LuaTable映射table
/// 注意:功能强大但是不推荐使用，因效率低
/// </summary>
public class CallLuaTableByLuaTable : MonoBehaviour
{
    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
        XLua.LuaTable   tabGameUser= env.Global.Get<XLua.LuaTable>("gameUser");
        //调用普通属性
        Debug.Log(tabGameUser.Get<string>("name"));
        Debug.Log(tabGameUser.Get<string>("ID"));
        Debug.Log(tabGameUser.Get<int>("age"));

        //调用函数
        //不带参数的函数
        LuaFunction  speak= tabGameUser.Get<LuaFunction>("Speak");
        speak.Call();
        LuaFunction walking = tabGameUser.Get<LuaFunction>("Walking");
        walking.Call();

        //带参数的函数
        LuaFunction cal = tabGameUser.Get<LuaFunction>("Calculaition");
     object[] resultArray =  cal.Call(tabGameUser, 100,200);
        Debug.Log(resultArray[0]);


    }

    private void OnDestroy()
    {
        env.Dispose();
    }
}
