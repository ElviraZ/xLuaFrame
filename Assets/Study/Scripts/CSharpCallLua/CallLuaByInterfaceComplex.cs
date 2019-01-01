using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// C# call Lua  Table
/// 方式一：
/// 映射到一个interface(复杂版本)
/// 引用拷贝
/// 一般用于商业项目
/// 注意：
/// 1、添加    [CSharpCallLua]
/// 2、编辑器生成目录
/// </summary>
public class CallLuaByInterfaceComplex : MonoBehaviour 
{
    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
        //得到 Lua  中table的信息
        IGameUser gameUser = env.Global.Get<IGameUser>("gameUser");
        Debug.Log("str1===" + gameUser.name);
        Debug.Log("str2===" + gameUser.age);
        Debug.Log("str3===" + gameUser.ID);
      gameUser.Speak();
        gameUser.Walking();
        Debug.Log(gameUser.Calculaition(100, 200)); 
    }

  
    /// 定义接口

    [CSharpCallLua]
    public interface IGameUser
    {
        string name { get; set; }
        int age { get; set; }
        string ID { get; set; }
        void Speak();
        void Walking();
        int Calculaition(int  num1,int num2);
    }
}
