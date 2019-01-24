using System.IO;
using ABFW;
using UnityEditor;
using UnityEngine;

namespace ElviraFrame.AB
{
    public class ElviraEditor:EditorWindow
    {
        static ElviraEditor windos;

        [MenuItem("Elvira/Editor")]
        private static void Init()
        {
            windos = (ElviraEditor)EditorWindow.GetWindow(typeof(ElviraEditor), false, "Elvira编辑器帮助工具", false);
            windos.minSize = new UnityEngine.Vector2(620f, 480f);
            windos.Show();
        }

        string _packageResPath = "/AB_Res/";
      
        string _packageAndroidPath = "Assets/StreamingAssets/Android/";
        string _packageIosPath = "Assets/StreamingAssets/iOS/";
        string _assetBundleName;
        void OnGUI()
        {

            _packageResPath = EditorGUILayout.TextField("需要打AB 的原资源路径", _packageResPath);
            _packageAndroidPath = EditorGUILayout.TextField("Android   AB 包保存路径", _packageAndroidPath);
            _packageIosPath = EditorGUILayout.TextField("Ios    AB 包保存路径", _packageIosPath);
            _assetBundleName = EditorGUILayout.TextField("单独资源包的名字", _assetBundleName);

            if (GUI.Button(new Rect(60, 100, 230, 40), "给指定目录下设置AB Label"))
            {
                SetABLabel();
            }
            if (GUI.Button(new Rect(320, 100, 230, 40), "给指定目录下删除AB Label"))
            {
                DeleteABLabel();
            }



            if (GUI.Button(new Rect(60, 220, 230, 40), "所有选择的文件创建为一个iOS包"))
            {
                CreateBundleALLiOS();
            }
        if (GUI.Button(new Rect(320, 220, 230, 40), "所有选择的文件创建为一个android包"))
            {
                CreateBundleALLAndroid();
            }

            if (GUI.Button(new Rect(60, 300, 230, 40), "删除android AB包"))
            {
                DeleteAndroidAB();


            }


            if (GUI.Button(new Rect(320, 300, 230, 40), "删除Ios AB包"))
            {
                DeleteiOSAB();


            }
            if (GUI.Button(new Rect(60, 380, 230, 40), "删除所有PlayerPrefs"))
            {
              DeleteAllPlayerPrefs();

            }


            if (GUI.Button(new Rect(320, 380, 230, 40), "加密所有lua脚本"))
            {
                AESAllLuaFile();


            }


        }

      
        public static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }




        #region 设置和删除AB      Label
        static bool isSet = true;
        #region 设置AB 的包名
        /// <summary>
        /// 设置AB 的包名
        /// </summary>

        public  void SetABLabel()
        {
            isSet = true;
            FindResources();
        }

        private  void FindResources()
        {
            //  1：定义文件夹根目录
            string root = string.Empty;
            //目录信息
            DirectoryInfo[] dirArray = null;
            //清空无用Labels
            AssetDatabase.RemoveUnusedAssetBundleNames();
            //得到根目录
            //  root = Application.dataPath + "/"+ "AB_Res";
            // root = Application.dataPath + "/" + PathTools.GetABResourcesPath();
            root =Application.dataPath+"/"+ _packageResPath;
            DirectoryInfo directoryInfo = new DirectoryInfo(root);
            dirArray = directoryInfo.GetDirectories();

            // 2：遍历文件夹
            foreach (DirectoryInfo curreentDir in dirArray)
            {
                //       a、如果是目录则继续递归深入
                string tmpdir = root + "/" + curreentDir.Name;//全路径
                                                              //DirectoryInfo tmpdirinfo = new DirectoryInfo(tmpdir);//
                int tmpindex = tmpdir.LastIndexOf("/");//
                string tmpSceneName = tmpdir.Substring(tmpindex + 1);//得到场景名称
                                                                     //       b、找到后使用   AssetImporter标记包名和后缀名
                DirOrFile(curreentDir, tmpSceneName);
            }
            //3、刷新编辑器
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 判断是否是目录或者文件，修改label
        /// </summary>
        /// <param name="currentDir">当前文件信息</param>
        /// <param name="sceneName">场景名称</param>
        private  void DirOrFile(FileSystemInfo fileSystemInfo, string sceneName)
        {
            //参数检查
            if (!fileSystemInfo.Exists)
            {
                Debug.LogError("目录不存在" + fileSystemInfo);
            }
            //得到下一级
            DirectoryInfo dirinfoObj = fileSystemInfo as DirectoryInfo;
            FileSystemInfo[] filesysArray = dirinfoObj.GetFileSystemInfos();
            foreach (FileSystemInfo fileinfo in filesysArray)
            {
                FileInfo fileinfoObj = fileinfo as FileInfo;
                //文件类型
                if (fileinfoObj != null)
                {//修改AB Label
                    SetFileABLabel(fileinfoObj, sceneName);
                }
                //目录类型
                else
                {
                    DirOrFile(fileinfo, sceneName);
                }
            }
            if (isSet)
            {
                Debug.Log("设置完成");
            }
            else
            {
                Debug.Log("取消设置完成");
            }
        }

        /// <summary>
        /// 设置Label
        /// </summary>
        /// <param name="fileinfo">文件信息（包含绝对路径）</param>
        /// <param name="sceneName">场景名称</param>
        private  void SetFileABLabel(FileInfo fileinfoobj, string sceneName)
        {
            string ABName = string.Empty;
            string assetFilePath = string.Empty;
            if (fileinfoobj.Extension == ".meta") return;
            ABName = GetABName(fileinfoobj, sceneName);
            //获取文件的相对路径
            int tmpindex = fileinfoobj.FullName.IndexOf("Assets");
            assetFilePath = fileinfoobj.FullName.Substring(tmpindex);

            AssetImporter tmpImporter = AssetImporter.GetAtPath(assetFilePath);
            if (isSet == true)
            {
                tmpImporter.assetBundleName = ABName;
                if (fileinfoobj.Extension == ".unity")
                {
                    tmpImporter.assetBundleVariant = "u3d";
                }
                else
                {
                    tmpImporter.assetBundleVariant = "ab";
                }

            }
            else
            {
                tmpImporter.assetBundleVariant = null;
                tmpImporter.assetBundleName = null;

            }


        }

        private static string GetABName(FileInfo fileinfoobj, string sceneName)
        {
            string ABName = string.Empty;
            //Windows 路径
            string tmpwinPath = fileinfoobj.FullName;
            //Unity路径
            string tmpunityPath = tmpwinPath.Replace("\\", "/");
            //定位“场景名称”后面字符位置
            int tmpSceneNamePositon = tmpunityPath.IndexOf(sceneName) + sceneName.Length;
            string ABFileNameArea = tmpunityPath.Substring(tmpSceneNamePositon + 1);
            if (ABFileNameArea.Contains("/"))
            {
                string[] tmpArray = ABFileNameArea.Split('/');
                ABName = sceneName + "/" + tmpArray[0];
            }
            else
            {
                ABName = sceneName + "/" + sceneName;
            }
            return ABName;
        }
        #endregion
        #region 删除所有的AB 的包名

    
        public  void DeleteABLabel()
        {
            isSet = false;
            FindResources();
        }


        #endregion
        #endregion
        #region 打AB包

        //  0 BuildAssetBundleOptions.None --构建AssetBundle没有任何特殊的选项

        //  1 BuildAssetBundleOptions.UncompressedAssetBundle --不进行数据压缩。如果使用该项，因为没有压缩\解压缩的过程， AssetBundle的发布和加载会很快，但是AssetBundle也会更大，下载变慢

        //  2 BuildAssetBundleOptions.CollectDependencies  --包含所有依赖关系

        //  4 BuildAssetBundleOptions.CompleteAssets  --强制包括整个资源

        //  8 BuildAssetBundleOptions.DisableWriteTypeTree --在AssetBundle中不包含类型信息。发布web平台时，不能使用该项

        // 16 BuildAssetBundleOptions.DeterministicAssetBundle --使每个Object具有唯一不变的hash ID，可用于增量式发布AssetBundle

        // 32 BuildAssetBundleOptions.ForceRebuildAssetBundle --强制重新Build所有的AssetBundle

        // 64 BuildAssetBundleOptions.IgnoreTypeTreeChanges --忽略TypeTree的变化，不能与DisableTypeTree同时使用

        //128 BuildAssetBundleOptions.AppendHashToAssetBundleName --附加hash到AssetBundle名称中

        //256 BuildAssetBundleOptions.ChunkBasedCompression --Assetbundle的压缩格式为lz4。默认的是lzma格式，下载Assetbundle后立即解压。而lz4格式的Assetbundle会在加载资源的时候才进行解压，只是解压资源的时机不一样
        //Unfiltered 返回整个选择
        //TopLevel 只返回最上面选择的transform。另一个选定的transform的选定子物体将被过滤掉。
        //Deep 返回选择的物体和它所有的子代
        //ExcludePrefab 排除选择里的所有预制体
        //Editable 排除任何不被修改的对象。
        //Assets 只返回Asset文件夹的资源
        //DeepAssets 如果选择里包含文件夹，则也包括文件夹里的文件和子文件夹。
        void CreateBundleALLAndroid()
        {
            if (string.IsNullOrEmpty(_assetBundleName))
            {

                windos.ShowNotification(new GUIContent("单独资源包的名字不能为空"));
                //  EditorUtility.DisplayDialog("单独资源包的名字", "单独资源包的名字不能为空","Yes");
                return;
            }


            if (string.IsNullOrEmpty(_packageAndroidPath))
            {
                windos.ShowNotification(new GUIContent("请设置android   AB 包保存路径"));
                //  EditorUtility.DisplayDialog("AB包保存路径", "请设置android   AB 包保存路径", "Yes");
                return;
            }



            if (!Directory.Exists(_packageAndroidPath))
            {
                Directory.CreateDirectory(_packageAndroidPath);
            }
            //将选中对象一起打包
            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
            //NewMethod(buildMap);
            Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (SelectedAsset.Length == 0)
            {
                //   EditorUtility.DisplayDialog("选择资源", "没有选择需要打包的资源", "Yes");
                windos.ShowNotification(new GUIContent("没有选择需要打包的资源"));
                return;
            }


         //   Debug.Log("SelectedAsset[0].name===" + SelectedAsset[0].name);
            buildMap[0].assetBundleName = _assetBundleName + ".ab";
            //GameObject[] objs = Selection.gameObjects; //获取当前选中的所有对象
            string[] itemAssets = new string[SelectedAsset.Length];
            for (int i = 0; i < SelectedAsset.Length; i++)
            {
                itemAssets[i] = AssetDatabase.GetAssetPath(SelectedAsset[i]); //获取对象在工程目录下的相对路径
            }
            buildMap[0].assetNames = itemAssets;
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(_packageAndroidPath, buildMap, BuildAssetBundleOptions.None, BuildTarget.Android);
            AssetDatabase.Refresh(); //刷新
            if (manifest == null)
                Debug.LogError("Package AssetBundles Faild.");
            else
                Debug.Log("Package AssetBundles Success.");
        }
        void CreateBundleALLiOS()
        {
            if (string.IsNullOrEmpty(_assetBundleName))
            {
                windos.ShowNotification(new GUIContent("单独资源包的名字不能为空"));
                //  EditorUtility.DisplayDialog("单独资源包的名字", "单独资源包的名字不能为空","Yes");
                return;
            }


            if (string.IsNullOrEmpty(_packageIosPath))
            {
                windos.ShowNotification(new GUIContent("请设置android   AB 包保存路径"));
                //  EditorUtility.DisplayDialog("AB包保存路径", "请设置android   AB 包保存路径", "Yes");
                return;
            }


            if (!Directory.Exists(_packageIosPath))
            {
                Directory.CreateDirectory(_packageIosPath);
            }
            //将选中对象一起打包
            AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
            //NewMethod(buildMap);
            Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (SelectedAsset.Length == 0)
            {
                //   EditorUtility.DisplayDialog("选择资源", "没有选择需要打包的资源", "Yes");
                windos.ShowNotification(new GUIContent("没有选择需要打包的资源"));
                return;
            }
            buildMap[0].assetBundleName =_assetBundleName + ".ab";
            //GameObject[] objs = Selection.gameObjects; //获取当前选中的所有对象
            string[] itemAssets = new string[SelectedAsset.Length];
            for (int i = 0; i < SelectedAsset.Length; i++)
            {
                itemAssets[i] = AssetDatabase.GetAssetPath(SelectedAsset[i]); //获取对象在工程目录下的相对路径
            }
            buildMap[0].assetNames = itemAssets;
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(_packageIosPath, buildMap, BuildAssetBundleOptions.None, BuildTarget.iOS);
            AssetDatabase.Refresh(); //刷新
            if (manifest == null)
                Debug.LogError("Package AssetBundles Faild.");
            else
                Debug.Log("Package AssetBundles Success.");
        }


 
        public static void BuildAllABAndroid()
        {
            //打包AB输出路径
            string strABOutPathDIR = PathTools.GetABOutPath();
            Debug.Log("strABOutPathDIR==" + strABOutPathDIR);
            //判断生成输出目录文件夹
            if (!Directory.Exists(strABOutPathDIR))
            {
                Directory.CreateDirectory(strABOutPathDIR);
            }
            //打包生成
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.Android);
        }

    
        public static void BuildAllABIOS()
        {
            //打包AB输出路径
            string strABOutPathDIR = PathTools.GetABOutPath();
            //判断生成输出目录文件夹
            if (!Directory.Exists(strABOutPathDIR))
            {
                Directory.CreateDirectory(strABOutPathDIR);
            }
            //打包生成
            BuildPipeline.BuildAssetBundles(strABOutPathDIR, BuildAssetBundleOptions.None, BuildTarget.iOS);
        }

        void _packageBuddleSelected_IOS()
        {

            if (_packageIosPath.Length <= 0)
                return;

            if (!Directory.Exists(_packageIosPath))
            {
                Directory.CreateDirectory(_packageIosPath);

            }
            GameObject[] objs = Selection.gameObjects;
            AssetBundleBuild[] buildMap = new AssetBundleBuild[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                string[] itemAsset = new string[] { AssetDatabase.GetAssetPath(objs[i]) };
                buildMap[i].assetBundleName = objs[i].name + ".ab";
                buildMap[i].assetNames = itemAsset;
            }
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(_packageIosPath, buildMap, BuildAssetBundleOptions.None, BuildTarget.iOS);
            AssetDatabase.Refresh();
            if (manifest == null)
                Debug.LogError("Error:Package Failed");
            else
                Debug.Log("Package Success.");
        }
        void _packageBuddleSelected_Android()
        {
            if (_packageAndroidPath.Length <= 0)
                return;

            if (!Directory.Exists(_packageAndroidPath))
            {
                Directory.CreateDirectory(_packageAndroidPath);

            }



        //    GameObject[] objs = Selection.gameObjects;
            //NewMethod(buildMap);
            Object[] objs = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            AssetBundleBuild[] buildMap = new AssetBundleBuild[objs.Length];
          //  Debug.Log(objs.Length);
            for (int i = 0; i < objs.Length; i++)
            {
                string[] itemAsset = new string[] { AssetDatabase.GetAssetPath(objs[i]) };
                buildMap[i].assetBundleName = objs[i].name + ".ab";
              //  Debug.Log(buildMap[i].assetBundleName);
                buildMap[i].assetNames = itemAsset;
            }
            AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(_packageAndroidPath, buildMap, BuildAssetBundleOptions.None, BuildTarget.Android);
            AssetDatabase.Refresh();
            if (manifest == null)
                Debug.LogError("Error:Package Failed");
            else
                Debug.Log("Package Success.");
        }
        #endregion
        #region 删除AB包
        public void DeleteAndroidAB()
        {
            string deleteDir = _packageAndroidPath;
            Debug.Log(deleteDir);

            if (!string.IsNullOrEmpty(deleteDir))
            {
                Directory.Delete(deleteDir, true);//true表示可以删除非空目录

                File.Delete(deleteDir + ".meta");
                AssetDatabase.Refresh();
                Debug.Log("删除完成");
            }



        }
        public void DeleteiOSAB()
        {
            string deleteDir = _packageIosPath;
            if (File.Exists(deleteDir))
            {
                if (!string.IsNullOrEmpty(deleteDir))
                {
                    Directory.Delete(deleteDir, true);//true表示可以删除非空目录

                    File.Delete(deleteDir + ".meta");
                    AssetDatabase.Refresh();
                    Debug.Log("删除完成");
                }
            }

        }
        #endregion
#region      加密所有lua脚本

public static void AESAllLuaFile()
{
    string luaSrcPath = Application.dataPath + "/LuaScripts/";
            Debug.Log(luaSrcPath);
//目录信息
    DirectoryInfo[] dirArray = null;
    DirectoryInfo directoryInfo = new DirectoryInfo(luaSrcPath);
    dirArray = directoryInfo.GetDirectories();
            // 2：遍历文件夹
            foreach (DirectoryInfo curreentDir in dirArray)
            {
       
                DirectortOrFile(curreentDir);
            }
            //3、刷新编辑器
            AssetDatabase.Refresh();

        }

/// <summary>
/// 判断是否是目录或者文件
/// </summary>

        private static void DirectortOrFile(FileSystemInfo fileSystemInfo)
{
    //参数检查
    if (!fileSystemInfo.Exists)
    {
        Debug.LogError("目录不存在" + fileSystemInfo);
    }
    //得到下一级
    DirectoryInfo    dirinfoObj   = fileSystemInfo as DirectoryInfo;
    FileSystemInfo[] filesysArray = dirinfoObj.GetFileSystemInfos();
    foreach (FileSystemInfo fileinfo in filesysArray)
    {
        FileInfo fileinfoObj = fileinfo as FileInfo;
        //文件类型
        if (fileinfoObj != null)
        { 
            //加密
            if (fileinfoObj.Extension==".lua")
            {
                //获取文件的相对路径
                int tmpindex = fileinfoObj.FullName.IndexOf("Assets");
              string  assetFilePath = fileinfoObj.FullName.Substring(tmpindex);
        
              string aestext = Elvira.Helper.DES.AESEncrypt(File.ReadAllText(assetFilePath));
              string truePath = assetFilePath.Replace("Assets", "");
              string streamingAssetsPath = Application.streamingAssetsPath + truePath;
              Debug.Log("assetFilePath==="+assetFilePath);
              Debug.Log("truePath==="+truePath);
              Debug.Log("streamingAssetsPath==="+streamingAssetsPath);


             if (!Directory.Exists(truePath))
              {
                
                  Directory.CreateDirectory(  Path.GetDirectoryName(streamingAssetsPath));
              }

        
                     File.WriteAllText(streamingAssetsPath, aestext);
            }
        }
        //目录类型
        else
        {
            DirectortOrFile(fileinfo);
        }
    }


        }
        #endregion
    }
}