using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace xLuaStudy
{


    [LuaCallCSharp]
    public class MyGeneric 
    {
        public T GetMax<T>(T num1, T num2) where T : IComparable
        {

            if (num1.CompareTo(num2)<0)
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
