using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
/// <summary>
/// 商业项目通用方式
/// 
/// 
/// 
/// 
/// 解释：
/// 
/// 通过AddLoader注册一个回调（byte[] CustomMyLoader(ref string fileName)），回调的参数是字符串（文件名），
///  env.DoString("require'CustomLuaFile'")的时候参数会自动传给回调，回调根据参数去加载指定文件
///  如果需要支持调试，需要将fileName修改为真实路径传出
///  
/// 
/// lua文件可以是lua后缀文件
/// 
/// 
/// 
/// 
/// </summary>
public class RunLuaByLoader : MonoBehaviour {


    LuaEnv env = null;
    // Use this for initialization
    void Start()
    {
        env = new LuaEnv();
        env.AddLoader(CustomMyLoader);
        env.DoString("require'CustomLuaFile'");
    }
    /// <summary>
    /// 定义回调方法
    /// 
    /// 用于自定义Lua文件路径
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private  byte[] CustomMyLoader(ref string fileName)
        {
        //定义lua路径
        string luaPath = Application.dataPath + "/Scripts/Lua/" + fileName+".lua";
        //读取lua文件内容
        string strLuaContent = File.ReadAllText(luaPath);


        //数据类型转换
        byte[] array = System.Text.Encoding.UTF8.GetBytes(strLuaContent);
        return array;

    }
}
