using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

using System;





/// <summary>
/// C# call Lua  function
/// 方式一：
///采用  delegate方式(续)，调用Lua中具备多个返回值的函数
///
/// 1、使用out关键字的
/// 2、使用ref关键字的
/// 
/// </summary>
public class CallLuaFunctionByDelMultiReturn : MonoBehaviour
{
    LuaEnv env = null;

    //自定义委托
    [CSharpCallLua]
    public delegate void DelAddingMulti(int num1, int num2,out int  res1,out  int res2,out int  res3);
    DelAddingMulti delAddingMulti = null;
    [CSharpCallLua]
    public delegate void DelAddingMultiRef(ref int num1,ref int num2, out int res);
    DelAddingMultiRef delAddingMultiRef = null;


    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
     
        //通过委托映射得到Lua中的函数信息(使用out关键字)
        delAddingMulti = env.Global.Get<DelAddingMulti>("myProcFunc4");
       
        //输出返回结果
        int resResult1 = 0;
        int resResult2 = 0;
        int resResult3= 0;
     delAddingMulti(100, 80, out resResult1, out resResult2, out resResult3);
      
            Debug.Log(resResult1+"==="+resResult2+"==="+resResult3);

        //通过委托映射得到Lua中的函数信息(使用ref关键字)
        delAddingMultiRef = env.Global.Get<DelAddingMultiRef>("myProcFunc4");
        int intnum1 = 10;
        int intnum2 = 20;
        int result = 0;
        delAddingMultiRef(ref intnum1, ref intnum2, out result);


        Debug.Log(intnum1 + "===" + intnum2 + "===" + result);

    }
    private void OnDestroy()
    {
        delAddingMulti = null;
        delAddingMultiRef = null;
        env.Dispose();
    }
}
