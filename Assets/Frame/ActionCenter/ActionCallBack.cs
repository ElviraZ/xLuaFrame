namespace ElviraFrame.ActionCenter
{


    public delegate void ActionCallBack();
    public delegate void ActionCallBack<T>(T arg);
    public delegate void ActionCallBack<T, X>(T arg1, X arg2);
    public delegate void ActionCallBack<T, X, Y>(T arg1, X arg2, Y arg3);
    public delegate void ActionCallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);
    public delegate void ActionCallBack<T, X, Y, Z, W>(T arg1, X arg2, Y arg3, Z arg4, W arg5);
}