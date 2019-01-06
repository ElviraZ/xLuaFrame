using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace xLuaStudy
{




    /// <summary>
    /// MyGeneric 类的扩展
    /// 
    /// 
    /// 注意：
    /// A、扩展方法必须是静态类
    /// B、定义的扩展方法的参数第一个参数必须是this，然后加上需要扩展的类的名称
    /// </summary>
    [LuaCallCSharp]
    public static class Extension_MyGeneric
    {

        /// <summary>
        /// 定义扩展方法
        /// </summary>
        /// <param name="gen"></param>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns></returns>
        public static int ExtGetMax(this   xLuaStudy.MyGeneric   gen,int num1,int  num2)
        {
            if (num1<num2)
            {
                return num2;
            }
            else
            {
                return num1;
            }
        }

    }
}
