using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

/// <summary>
/// Lua   Call 自定义C#脚本
/// 子类
/// 提供Lua   调用C#的各种示例
/// </summary>
namespace xLuaStudy
{

    /// <summary>
    /// 定义结构体
    /// </summary>
    public struct MyStruct
    {
        public string x;
        public string y;
    }


    /// <summary>
    /// 定义一个接口
    /// </summary>
    [CSharpCallLua]
    public interface MyInterface
    {
        int x { get; set; }

        int y { get; set; }

        void Speak();
    }


    /// <summary>
    /// 定义一个委托
    /// </summary>
    [CSharpCallLua]
    public delegate void MyDelegate(int num);








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

        public void Method2(float num1, float num2)
        {
            Debug.Log("Method2(float形式)");
        }

        public void Method2(string num1, string num2)
        {
            Debug.Log("Method2(string形式)");
        }


        public void Method3(int a, params string[] strs)
        {
            Debug.Log("Method3(可变参数形式)");
            for (int i = 0; i < strs.Length; i++)
            {
                Debug.Log(strs[i]);
            }

        }

        /// <summary>
        /// 参数为结构体的方法
        /// </summary>
        /// <param name="p"></param>
        public void Method4(MyStruct p)
        {
            Debug.Log("Method4(调用结构体)");
            Debug.Log("p.x===" + p.x);
            Debug.Log("p.y===" + p.y);
        }


        /// <summary>
        /// 参数为接口的方法，
        /// 注意接口必须         [CSharpCallLua]
        /// </summary>
        /// <param name="p"></param>
        public void Method5(MyInterface inter)
        {
            Debug.Log("Method5(调用接口)");
            Debug.Log("inter.x===" + inter.x);
            Debug.Log("inter.y===" + inter.y);
            inter.Speak();
        }

        /// <summary>
        /// 参数为委托的方法，
        /// 注意委托必须         [CSharpCallLua]
        /// </summary>
        /// <param name="p"></param>
        public void Method6(MyDelegate del)
        {
            Debug.Log("Method6(参数为委托)");
            del(88);
        }




        /// <summary>
        /// 定义一个具有多返回值的方法
        /// </summary>
        /// <param name="p"></param>
        public int Method7(int num1, out int num2, ref int num3)
        {
            Debug.Log("Method7(多返回值)");
            num2 = 3000;
            num3 = 999;
            return num1 + 100;
        }




        /// <summary>
        /// 普通的参数为泛型的方法
        /// </summary>
        public void Method8(List<string> strArray)
        {
            foreach (var item in strArray)
            {
                Debug.Log(item);
            }
        }



        /// <summary>
        /// 调用其他自定义的C#脚本的中的泛型方法
        /// </summary>
        public void Method9()
        {
            int maxNum = 0;
            int num1 = 100;
            int num2 = 200;


            MyGeneric myGeneric = new MyGeneric();

            maxNum = myGeneric.GetMax<int>(num1, num2);

            Debug.Log("最大的值是====" + maxNum);

        }
        /// <summary>
        /// 在C#中调用C#扩展方法
        /// </summary>
        public void Method10()
        {
            int maxNum = 0;
            int num1 = 300;
            int num2 = 200;

            MyGeneric myGeneric = new MyGeneric();
            //应用扩展方法
            maxNum = myGeneric.ExtGetMax(num1, num2);
            Debug.Log("最大的值是====" + maxNum);
        }



    }
}
