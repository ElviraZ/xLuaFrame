using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;

using System;





/// <summary>
/// C# call Lua  function
/// 方式一：
///采用  delegate方式，官方推荐,但需要在编辑器生成代码
///
/// 
/// 注意：
/// 1、含有out  与  ref关键字的委托需要添加标签[CSharpCallLua]
/// 2、退出LuaEnv之前需要先释放所有委托
/// 3、对于Unity与C#种德复杂类型的API，必须加入xLua 的配置文件，经过生成代码之后才能正确应用
///                 如：   Action<int,int ,int>   Func<int,int,int>等
/// 
/// 
/// 
/// </summary>
public class CallLuaFunctionByDel : MonoBehaviour
{
    LuaEnv env = null;

    //自定义委托
    public delegate void DelAdding(int num1, int num2);
    DelAdding delAdding = null;
    Action act = null;

    //以下需要配置文件
    Action<int, int, int> act3 = null;
    Func<int, int, int> act4 = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
        //通过Action委托映射得到Lua中的函数信息
        //无参函数
        act= env.Global.Get<Action>("myProcFunc1");
        act.Invoke();
        //act();


        //通过委托映射得到Lua中的函数信息
      delAdding=    env.Global.Get<DelAdding>("myProcFunc2");
        delAdding(1,5);




        act3=env.Global.Get<Action<int, int, int>>("myProcFunc3");
        act3(20,30,40);


        act4 = env.Global.Get<Func<int, int, int>>("myProcFunc4");
     Debug.Log(  act4(20, 30));



    }
    private void OnDestroy()
    {
        delAdding = null;
        act = null;
        act3 = null;
        act4 = null;
        env.Dispose();
    }
}
