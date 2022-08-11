//  AssetBundleMover.cs
//  http://kan-kikuchi.hatenablog.com/entry/AssetBundleMover
//
//  Created by kan.kikuchi on 2017.09.30.

using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Callbacks;
using System.IO;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ビルド時にプラットフォームに合わせてAssetBundleを移動するクラス
/// </summary>
[Obsolete("Obsolete")]
public class AssetBundleMover : IPreprocessBuild, IPostprocessBuild  {

    //移動元と先のパス
    private Dictionary<string, string> _pathDict = new Dictionary<string, string>();

    /// <summary>
    /// ビルドする前に実行される
    /// </summary>
    public void OnPreprocessBuild (BuildTarget target, string path){
        //StreamingAssetsディレクトリ作成
        Directory.CreateDirectory(Application.streamingAssetsPath);

        //対象のプラットフォームのディレクトリがあるか確認
        string directoryPath = "Assets/AssetBundles/" + target.ToString();
        if(!Directory.Exists(directoryPath)){
            Debug.LogError(directoryPath + "がありません！");
            return;
        }

        //ディレクトリの中身を移動
        string[] filePathArray = Directory.GetFiles (directoryPath, "*", SearchOption.AllDirectories);

        _pathDict.Clear();
        foreach (string filePath in filePathArray) {
            string toPath = Application.streamingAssetsPath + filePath.Substring(directoryPath.Length, filePath.Length - directoryPath.Length);
            File.Move(filePath, toPath);

            _pathDict[filePath] = toPath;
        }
    }

    /// <summary>
    /// ビルドした後に実行される
    /// </summary>
    public void OnPostprocessBuild (BuildTarget target, string path){
        //移動したファイルを戻す
        foreach (KeyValuePair<string, string> pair in _pathDict) {
            File.Move(pair.Value, pair.Key);
        }

        //StreamingAssetsディレクトリ削除
        Directory.Delete(Application.streamingAssetsPath);
    }

    /// <summary>
    /// 実行順を指定(0がデフォルト、低いほど先に実行される
    /// </summary>
    public int callbackOrder { get { return 0; } }

}