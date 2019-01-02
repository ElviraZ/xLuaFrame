using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// C# call Lua  Table
/// 方式三：
/// 映射到一个Dictionary<>或者List<>
/// 
/// 
/// 适用于超简单的lua table
/// 
/// 
/// </summary>
public class CallLuaTableByDicOrList : MonoBehaviour
{
    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
        Dictionary<string, object> m_dic = env.Global.Get<Dictionary<string, object>>("gameLanguage");
        Debug.Log("键值对Table输出如下：\n");
        foreach (KeyValuePair<string, object> item in m_dic)
        {
            Debug.Log(item.Key + "----" + item.Value);
        }


        List<string> m_List = env.Global.Get<List<string>>("simplegameLanguage");
        Debug.Log("简单Table输出如下：\n");
        foreach (var item in m_List)
        {
            Debug.Log("----" + item);
        }


    }

    private void OnDestroy()
    {
        env.Dispose();
    }

}
