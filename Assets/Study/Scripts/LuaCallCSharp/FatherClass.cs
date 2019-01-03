using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// Lua   Call 自定义C#脚本
/// 父类
/// 
/// </summary>
namespace xLuaStudy
{
    public class FatherClass
    {


        public string fatherClassName = "父类字段";
        public FatherClass()
        {
            Debug.Log("父类构造函数");

        }

        /// <summary>
        /// 父类的方法
        /// </summary>
        public void ShowFatherInfo()
        {

            Debug.Log("父类函数ShowFatherInfo");


        }


    }
}
