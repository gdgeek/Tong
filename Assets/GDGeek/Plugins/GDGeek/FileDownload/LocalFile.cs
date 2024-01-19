using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace GDGeek.FileDownload
{
    public class LocalFile : GDGeek.Singleton<LocalFile>
    {

        public string persistentDataPath { private set; get; } = null;

        void Awake()
        {
      
            persistentDataPath = Application.persistentDataPath;
        }

      
        public string getPathFile(string fileName)
        {
            string name = Path.GetFileName(fileName);
            string path = Path.Combine(persistentDataPath, "Cache", name);

            return path;
        }
      
     
        public bool exists(string fileName)
            => System.IO.File.Exists(getPathFile(fileName));

        public bool exists(string fileName, string md5)
        {
            string path = getPathFile(fileName);
        
            if (exists(path) && BuildFileMd5(path) == md5)
            {
                return true;
            }
            return false;
        }

        public static string FormatMD5(Byte[] data)
            => System.BitConverter.ToString(data).Replace("-", "").ToLower();//将byte[]装换成字符串

        public static String BuildFileMd5(String path)
        {
          
            try
            {
                using (var fileStream = System.IO.File.OpenRead(path))//!!!!!!!!!
                {
                    var md5 = MD5.Create();
                    var fileMD5Bytes = md5.ComputeHash(fileStream);//计算指定Stream 对象的哈希值                                     
                    String filemd5 = FormatMD5(fileMD5Bytes);
                    return filemd5;
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            return null;
        }

        public FileStream read(string fileName)
        {
            var stream = System.IO.File.OpenRead(getPathFile(fileName));
         
            return stream;
        }
        

        public void delete(string fileName) {

            if (exists(fileName)) {
                System.IO.File.Delete(getPathFile(fileName));
            }
        }
        public FileStream write(string fileName)
        {
            string pathFile = getPathFile(fileName);

            string path = Path.GetDirectoryName(pathFile);
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            return System.IO.File.OpenWrite(getPathFile(fileName));
        }
    }

}