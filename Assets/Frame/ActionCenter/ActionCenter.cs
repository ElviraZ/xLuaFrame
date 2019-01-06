using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElviraFrame.ActionCenter
{
    /// <summary>
    /// 事件处理中心
    /// 
    /// 使用方法
    /// 
    /// 1、在想要监听的物体上添加脚本  ，脚本上添加   
    /// private void Awake()
    ///  {
    ///   ActionCenter.AddListener<string>(ActionType.Show, CallBackMethod);
    ///}
    ///private void OnDisable()
    ///{
    ///ActionCenter.RemoveListener<string>(ActionType.Show, CallBackMethod);
    ///}
    ///private void CallBackMethod(string)
    ///{
    ///
    ///}
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 2、然后在发布事件的上使用
    /// 
    ///   ActionCenter.Broadcast<string>(ActionType.Show, CallBack);
    /// 
    /// 
    /// 
    /// 
    /// 
    /// 注意：保持参数一致
    /// 
  
    /// </summary>
    public class ActionCenter
    {
        private static Dictionary<ActionType, Delegate> m_EventDic = new Dictionary<ActionType, Delegate>();

        private static void OnListenerAdding(ActionType eventType, Delegate callBack)
        {
            if (!m_EventDic.ContainsKey(eventType))
            {
                m_EventDic.Add(eventType, null);
            }
            Delegate d = m_EventDic[eventType];
            if (d != null && d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        private static void OnListenerRemoving(ActionType eventType, Delegate callBack)
        {
            if (m_EventDic.ContainsKey(eventType))
            {
                Delegate d = m_EventDic[eventType];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}", eventType, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
            }
        }
        private static void OnListenerRemoved(ActionType eventType)
        {
            if (m_EventDic[eventType] == null)
            {
                m_EventDic.Remove(eventType);
            }
        }
        //no parameters
        public static void AddListener(ActionType eventType, ActionCallBack callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack)m_EventDic[eventType] + callBack;
        }
        //Single parameters
        public static void AddListener<T>(ActionType eventType, ActionCallBack<T> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T>)m_EventDic[eventType] + callBack;
        }
        //two parameters
        public static void AddListener<T, X>(ActionType eventType, ActionCallBack<T, X> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X>)m_EventDic[eventType] + callBack;
        }
        //three parameters
        public static void AddListener<T, X, Y>(ActionType eventType, ActionCallBack<T, X, Y> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X, Y>)m_EventDic[eventType] + callBack;
        }
        //four parameters
        public static void AddListener<T, X, Y, Z>(ActionType eventType, ActionCallBack<T, X, Y, Z> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X, Y, Z>)m_EventDic[eventType] + callBack;
        }
        //five parameters
        public static void AddListener<T, X, Y, Z, W>(ActionType eventType, ActionCallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerAdding(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X, Y, Z, W>)m_EventDic[eventType] + callBack;
        }

        //no parameters
        public static void RemoveListener(ActionType eventType, ActionCallBack callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack)m_EventDic[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //single parameters
        public static void RemoveListener<T>(ActionType eventType, ActionCallBack<T> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T>)m_EventDic[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //two parameters
        public static void RemoveListener<T, X>(ActionType eventType, ActionCallBack<T, X> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X>)m_EventDic[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //three parameters
        public static void RemoveListener<T, X, Y>(ActionType eventType, ActionCallBack<T, X, Y> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X, Y>)m_EventDic[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //four parameters
        public static void RemoveListener<T, X, Y, Z>(ActionType eventType, ActionCallBack<T, X, Y, Z> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X, Y, Z>)m_EventDic[eventType] - callBack;
            OnListenerRemoved(eventType);
        }
        //five parameters
        public static void RemoveListener<T, X, Y, Z, W>(ActionType eventType, ActionCallBack<T, X, Y, Z, W> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            m_EventDic[eventType] = (ActionCallBack<T, X, Y, Z, W>)m_EventDic[eventType] - callBack;
            OnListenerRemoved(eventType);
        }


        //no parameters
        public static void Broadcast(ActionType eventType)
        {

            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                ActionCallBack callBack = d as ActionCallBack;
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //single parameters
        public static void Broadcast<T>(ActionType eventType, T arg)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                ActionCallBack<T> callBack = d as ActionCallBack<T>;
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //two parameters
        public static void Broadcast<T, X>(ActionType eventType, T arg1, X arg2)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                ActionCallBack<T, X> callBack = d as ActionCallBack<T, X>;
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //three parameters
        public static void Broadcast<T, X, Y>(ActionType eventType, T arg1, X arg2, Y arg3)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                ActionCallBack<T, X, Y> callBack = d as ActionCallBack<T, X, Y>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //four parameters
        public static void Broadcast<T, X, Y, Z>(ActionType eventType, T arg1, X arg2, Y arg3, Z arg4)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                ActionCallBack<T, X, Y, Z> callBack = d as ActionCallBack<T, X, Y, Z>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
        //five parameters
        public static void Broadcast<T, X, Y, Z, W>(ActionType eventType, T arg1, X arg2, Y arg3, Z arg4, W arg5)
        {
            Delegate d;
            if (m_EventDic.TryGetValue(eventType, out d))
            {
                ActionCallBack<T, X, Y, Z, W> callBack = d as ActionCallBack<T, X, Y, Z, W>;
                if (callBack != null)
                {
                    callBack(arg1, arg2, arg3, arg4, arg5);
                }
                else
                {
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型", eventType));
                }
            }
        }
    }

}
