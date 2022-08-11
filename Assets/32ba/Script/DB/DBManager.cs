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
    public LiteDatabase DB;
    private class DBConnectionSetting {
        public const string EncryptPass = "gRfdySsyF2CTfqRUc3QrFn2uFGRXKQjZ";
        public const string DbName = "data";
        public static string DbPass;
        public static string String;
    }

    private void Awake()
    {
        if(this != Instance){
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        DB = DBManager.Init(Application.persistentDataPath);
    }

    private void OnApplicationQuit()
    {
        DB.Dispose();
    }

    private static LiteDatabase Init(string dataPath)
    {
        DBConnectionSetting.DbPass = $"{dataPath}/{DBConnectionSetting.DbName}";
        return new LiteDatabase($"Filename={DBConnectionSetting.DbPass};Password={DBConnectionSetting.EncryptPass}");
    }
}