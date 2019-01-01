using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// C# call Lua  Table
/// 方式一：
/// 映射到一个interface(简单版本)
/// 引用拷贝
/// 一般用于商业项目
/// 注意：
/// 1、添加    [CSharpCallLua]
/// 2、编辑器生成目录
/// </summary>
public class CallLuaByInterface : MonoBehaviour
{
    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
        //得到 Lua  中table的信息
        IGameLanguage gameLanguage = env.Global.Get<IGameLanguage>("gameLanguage");
        Debug.Log("str1===" + gameLanguage.str1);
        Debug.Log("str2===" + gameLanguage.str2);
        Debug.Log("str3===" + gameLanguage.str3);
        Debug.Log("str4===" + gameLanguage.str4);
        gameLanguage.str1 = "sdgdfghfhjg";

        env.DoString("print('修改后的='..gameLanguage.str1)");

    }
    /// 定义接口
    
       [CSharpCallLua]
    public interface IGameLanguage
    {
        string str1 { get; set; }
        string str2{ get; set; }
        string str3 { get; set; }
        string str4 { get; set; }
    }
}
