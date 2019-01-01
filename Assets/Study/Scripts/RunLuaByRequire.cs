
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// 
/// 
/// 
/// 
/// 
/// 缺点：只能使用Resource内置文件
/// </summary>
public class RunLuaByRequire : MonoBehaviour
{
    LuaEnv env = null;

    // Use this for initialization
    void Start()
    {

        env = new LuaEnv();

      
        env.DoString("require'Simplelua'");//不用加后缀
    //    env.DoString("require'main'");//不用加后缀  //推荐方式，main文件中加载其他lua文件
    }

    // Update is called once per frame
    void Update()
    {

    }
}
