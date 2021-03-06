
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//  CFile.cs
//  Author: Lu Zexi
//  2012-12-06


namespace Game.Utili
{
    /// <summary>
    /// 文件类
    /// </summary>
    public class CFile
    {

        /// <summary>
        /// 加载Unity资源文本数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> LoadUnityText(string path)
        {
          
            TextAsset file = (TextAsset)Resources.Load("Data/" + path, typeof(TextAsset));
           
           // TextAsset file = (TextAsset)GameGlobal.ASSETS_BUNDLE.Load(path, typeof(TextAsset));
            if (file == null)
            {
                Debug.Log("Can't find file:" + path);
                return null;
            }
            string[] splitStrings;
            string[] separator = new string[1];
            // 按换行符分段
            separator[0] = "\t";
            splitStrings = file.text.Split(separator, System.StringSplitOptions.None);
            List<string> infoList = new List<string>();
            foreach (string str in splitStrings)
            {
                string[] splitStrings2 = str.Split("\r\n".ToCharArray(), System.StringSplitOptions.None);
                foreach (string tmpstr in splitStrings2)
                {
                    //#开头为注释
                    if (tmpstr.Length == 0 || tmpstr[0] == '\r' || tmpstr.StartsWith("#"))
                    {
                        continue;
                    }
                    infoList.Add(tmpstr);
                }
            }
            return infoList;
        }

        /// <summary>
        /// 加载文本数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> LoadText(string path)
        {
#if UNITY_WEBPLAYER  
           return null; 
#else
            string text = System.IO.File.ReadAllText(path);
            string[] splitStrings;
            string[] separator = new string[1];
            // 按换行符分段
            separator[0] = "\t";
            splitStrings = text.Split(separator, System.StringSplitOptions.None);
            List<string> infoList = new List<string>();
            foreach (string str in splitStrings)
            {
                string[] splitStrings2 = str.Split("\r\n".ToCharArray(), System.StringSplitOptions.None);
                foreach (string tmpstr in splitStrings2)
                {
                    //#开头为注释
                    if (tmpstr.Length == 0 || tmpstr[0] == '\r' || tmpstr.StartsWith("#"))
                    {
                        continue;
                    }
                    infoList.Add(tmpstr);
                }
            }
            return infoList;
#endif
        }

        /// <summary>
        /// 加载2进制数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] Load(string path)
        {
            return null;
        }

        /// <summary>
        /// 写入文本数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="info"></param>
        public static void WriteText( string path , List<string> info )
        {
        }

        /// <summary>
        /// 写入2进制数据
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public static void Writ(string path, byte[] data)
        { 
        }

        /// <summary>
        /// 拷贝指定文件夹中所有内容到指定目录
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        public static void CopyDirectory(string sourceDirName, string destDirName)
        {

            if (!Directory.Exists(sourceDirName))
            {
                Debug.LogError("The src dir is not exist.");
                return;
            }

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
                File.SetAttributes(destDirName, File.GetAttributes(sourceDirName));
            }

            if (destDirName[destDirName.Length - 1] != Path.DirectorySeparatorChar)
                destDirName = destDirName + Path.DirectorySeparatorChar;

            string[] files = Directory.GetFiles(sourceDirName);
            foreach (string file in files)
            {
                File.Copy(file, destDirName + Path.GetFileName(file), true);
                File.SetAttributes(destDirName + Path.GetFileName(file), FileAttributes.Normal);
            }

            string[] dirs = Directory.GetDirectories(sourceDirName);
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, destDirName + Path.GetFileName(dir));
            }
        }

        /// <summary>
        /// 删除指定目录中所有内容
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Debug.LogError("The dir is not exist.");
                return;
            }

            DirectoryInfo info = new DirectoryInfo(path);

            var files = info.GetFiles();
            foreach (var fileInfo in files)
            {
                fileInfo.Delete();
            }

            var dirs = info.GetDirectories();
            foreach (var directoryInfo in dirs)
            {
                directoryInfo.Delete(true);
            }

            //Directory.Delete(path, true);
        }
   

		public static string GetUnusedFileName(string path, string name, string ext)
		{
			string fileName = path + name + ext;
			if (!File.Exists (fileName)) {
				return name;
			}

			int index = 1;

			do {
				fileName = path + name + "_"+ index.ToString()+ ext;
				if (!File.Exists (fileName)) {
					name += "_"+index.ToString();
					break;
				}

				index++;

			} while(true);

            return name;
		}
    }

}

