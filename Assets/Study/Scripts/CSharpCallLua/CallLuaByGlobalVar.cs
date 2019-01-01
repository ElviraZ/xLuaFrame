using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// C# call Lua
/// </summary>
public class CallLuaByGlobalVar : MonoBehaviour {

    LuaEnv env = null;

	// Use this for initialization
	void Start () {
        env = new LuaEnv();

        env.DoString("require'CSharpCallLua/CSharpCallLua'");
   string  str1=     env.Global.Get<string>("str");
        Debug.Log(env.Global.Get<string>("str"));
        Debug.Log(env.Global.Get<int>("number"));
//        Debug.Log(env.Global.Get<int>("num"));//读取局部变量失败
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
