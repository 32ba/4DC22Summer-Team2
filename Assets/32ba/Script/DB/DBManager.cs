using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using _32ba.Script;
using UnityEngine;
using LiteDB;
using UnityEngine.Networking;

public class DBManager: SingletonMonoBehaviour<DBManager>
{ 
    private class DBConnectionSetting {
        public const string EncryptPass = "gRfdySsyF2CTfqRUc3QrFn2uFGRXKQjZ";
        public const string DbName = "data";
        public static string DbPass;
        public static string String;
    }

    public static LiteDatabase Init(string dataPath)
    {
        DBConnectionSetting.DbPass = $"{dataPath}/{DBConnectionSetting.DbName}";
        return new LiteDatabase($"Filename={DBConnectionSetting.DbPass};Password={DBConnectionSetting.EncryptPass}");
    }
}