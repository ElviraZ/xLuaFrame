using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// C# call Lua  Table
/// 方式一：
/// 使用class或者struct
/// 注意:此方法是值拷贝，修改之一不会同步到另外一个
/// </summary>
public class CallLuaByClass : MonoBehaviour {
    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.DoString("require'CSharpCallLua/CSharpCallLua'");
        //得到 Lua  中table的信息
   GameLanguage  gameLanguage=     env.Global.Get<GameLanguage>("gameLanguage");
        Debug.Log("str1===" + gameLanguage.str1);
        Debug.Log("str2===" + gameLanguage.str2);
        Debug.Log("str3===" + gameLanguage.str3);
        Debug.Log("str4===" + gameLanguage.str4);
        gameLanguage.str1 = "sdgdfghfhjg";

        env.DoString("print('修改后的='..gameLanguage.str1)");

    }
    // Update is called once per frame
    void Update () {
		
	}




    /// <summary>
    /// 定义内部类
    /// </summary>
    public class GameLanguage
    {
        public string str1;
        public string str2;
        public string str3;
        public string str4;
    }
}
