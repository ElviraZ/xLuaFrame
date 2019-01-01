using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// C# call Lua  Table
/// 方式一：
/// 映射到一个Dictionary<>或者List<>
/// 
/// 
/// 适用于超简单的lua table
/// 
/// 
/// </summary>
public class CallLuaByValue : MonoBehaviour 
{
    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
        Dictionary<string, object>  m_dic= env.Global.Get<Dictionary<string, object>>("gameLanguage");
        foreach (KeyValuePair<string,object> item in m_dic)
        {
            Debug.Log(item.Key+"----"+ item.Value);
        }


        List<string> m_List = env.Global.Get<List<string>>("simplegameLanguage");
        foreach (var item in m_List)
        {
 Debug.Log( "----" + item);
        }
           
        
    }
    /// 定义接口

    [CSharpCallLua]
    public interface IGameLanguage
    {
        string str1 { get; set; }
        string str2 { get; set; }
        string str3 { get; set; }
        string str4 { get; set; }
    }
}
