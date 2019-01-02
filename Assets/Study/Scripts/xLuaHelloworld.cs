using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class xLuaHelloworld : MonoBehaviour {
    LuaEnv env = null;


    string str1Lua1 = "print('this is  the first hello world')";
    string str1Lua2 = "CS.UnityEngine.Debug.Log('2222222222222')";
    // Use this for initialization
    void Start () {

        env = new LuaEnv();
        env.DoString(str1Lua1);
        env.DoString(str1Lua2);
    }
	

    private void OnDisable()
    {
        env.Dispose();
    }
}
