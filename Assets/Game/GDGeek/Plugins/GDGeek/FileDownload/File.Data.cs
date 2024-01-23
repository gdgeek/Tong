using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDGeek.FileDownload
{
    [Serializable]
    public class FileData
    {

        public string md5;
        public string type;
        public string url;
        public string key;
        public Uri uri => new Uri(this.url);


    }



}