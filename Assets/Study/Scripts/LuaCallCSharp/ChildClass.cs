using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Lua   Call 自定义C#脚本
/// 子类
/// 
/// </summary>
namespace xLuaStudy
{
    public class ChildClass : FatherClass
    {
        public string childClassName = "子类字段";
        public ChildClass()
        {
            Debug.Log("子类构造函数");
        }

        /// <summary>
        /// 父类的方法
        /// </summary>
        public void ShowChildInfo()
        {
            Debug.Log("子类函数ShowChildInfo");
        }

        public void Method2(int num1, int num2)
        {
            Debug.Log("Method2(int形式)");
        }

        public  void   Method2(float num1,float  num2)
        {
            Debug.Log("Method2(float形式)");
        }

        public void Method2(string num1, string num2)
        {
            Debug.Log("Method2(string形式)");
        }


        public void Method3(int a, params  string [] strs)
        {
            Debug.Log("Method3(可变参数形式)");
            for (int i = 0; i < strs.Length; i++)
            {
    Debug.Log(strs[i]);
            }
        
        }



    }
}
