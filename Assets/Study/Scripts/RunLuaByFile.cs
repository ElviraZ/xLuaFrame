using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class RunLuaByFile : MonoBehaviour
{

    LuaEnv env = null;
    
    // Use this for initialization
    void Start()
    {

        env = new LuaEnv();

        TextAsset textAsset = Resources.Load<TextAsset>("Simplelua.lua");//lua文件必须加txt后缀
        env.DoString(textAsset.text);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
