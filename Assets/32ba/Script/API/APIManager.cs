using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using _32ba.Script;
using LiteDB;
using UnityEngine;
using UnityEngine.Networking;

internal static class APIEndpoints
{
    private const string BaseURL = "https://0cf2.api.32ba.net/api/v1";
    public const string UserSignup = BaseURL + "/user/signup";
    public static string UserUpdate = BaseURL + "/user/update";
    public const string TokenRefresh = BaseURL + "/token/refresh";
    public const string Ranking = BaseURL + "/ranking";
}

class UserInfo
{
    public int Id { get; set; }
    public string Guid { get; set; }
    public string Name { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

[Serializable]
public class SignupRequest
{
    [SerializeField] public string guid;

    public static string ToJson(SignupRequest model)
    {
        return JsonUtility.ToJson(model);
    }
}

[Serializable]
public class SignupResponse
{
    [SerializeField] public string access_token;
    [SerializeField] public string refresh_token;

    public static SignupResponse FromJson(string json)
    {
        return JsonUtility.FromJson<SignupResponse>(json);
    }
}

[Serializable]
public class SendScoreRequest
{
    [SerializeField] public string song_uuid = "";
    [SerializeField] public long score = 0;

    public static string ToJson(SendScoreRequest model)
    {
        return JsonUtility.ToJson(model);
    }
}

public class GetRankingRequest
{
    public string SongUuid { get; set; }
    public string RankingType { get; set; }
}

[Serializable]
public class GetRankingResponse
{
    [Serializable]
    public class Ranking
    {
        [SerializeField] public int rank = 0;
        [SerializeField] public long score = 0;
        [SerializeField] public string name = "";
    }
    [SerializeField] public string song_uuid = "";
    [SerializeField] public string ranking_type = "";
    [SerializeField] public List<Ranking> ranking;
    
    public static GetRankingResponse FromJson(string json)
    {
        return JsonUtility.FromJson<GetRankingResponse>(json);
    }
} 

[Serializable]
public class TokenRefreshRequest
{
    [SerializeField] public string refresh_token;
    public static string ToJson(TokenRefreshRequest model)
    {
        return JsonUtility.ToJson(model);
    }
}
[Serializable]
public class TokenRefreshResponse
{
    [SerializeField] public string access_token;
    public static TokenRefreshResponse FromJson(string json)
    {
        return JsonUtility.FromJson<TokenRefreshResponse>(json);
    }
}

public class APIManager : SingletonMonoBehaviour<APIManager>
{
    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public async void Start()
    {
        if (GetAccessToken(DBManager.Instance.DB) == "")
        {
            await Signup();
        }
        else
        {
            TokenRefresh();
        }
    }

    public static async Task<bool> Signup()
    {
        var json = new SignupRequest();
        {
            json.guid = GetGuid(DBManager.Instance.DB);
        }
        var reqJson = SignupRequest.ToJson(json);
        var req = await _postJsonRequest(APIEndpoints.UserSignup, reqJson);
        if (req.result != UnityWebRequest.Result.Success || req.responseCode != 200)
        {
            req.Dispose();
            return false;
        }
        
        var res = SignupResponse.FromJson(req.downloadHandler.text);
        var userInfo = new UserInfo
            {
                Id = 1,
                Guid = json.guid,
                AccessToken = res.access_token,
                RefreshToken = res.refresh_token
            };
        var dbUserInfo = DBManager.Instance.DB.GetCollection<UserInfo>("UserInfo");
        dbUserInfo.Update(userInfo);
        req.Dispose();
        return true;
    }

    public static async Task<bool> SendScore(SendScoreRequest request)
    {
        var accesstoken = GetAccessToken(DBManager.Instance.DB);
        var req = await _postJsonRequest(APIEndpoints.Ranking, SendScoreRequest.ToJson(request), accesstoken);
        if (req.result == UnityWebRequest.Result.Success && req.responseCode == 200)
        {
            req.Dispose();
            return true;
        }
        req.Dispose();
        return false;
    }

    public static async Task<GetRankingResponse> GetRanking(GetRankingRequest request)
    {
        var queryParams = $"?song_uuid={request.SongUuid}&ranking_type={request.RankingType}";
        var req = await _getJsonRequest(APIEndpoints.Ranking, queryParams);
        var res = GetRankingResponse.FromJson(req.downloadHandler.text);
        req.Dispose();
        return res;
    }

    private static async void TokenRefresh()
    {
        var refreshToken = GetRefreshToken(DBManager.Instance.DB);
        var request = new TokenRefreshRequest()
        {
            refresh_token = refreshToken
        };
        var req = await _postJsonRequest(APIEndpoints.TokenRefresh, TokenRefreshRequest.ToJson(request));
        Debug.Log(req.downloadHandler.text);
        var res = TokenRefreshResponse.FromJson(req.downloadHandler.text);
        SetAccessToken(DBManager.Instance.DB, res.access_token);
        req.Dispose();
    }

    private static async Task<UnityWebRequest> _getJsonRequest(string url, string queryParams = null, string auth = null)
    {
        var getUrl = url;
        if (queryParams != null) getUrl += queryParams;
        var req = new UnityWebRequest(getUrl, "GET");
        if(auth != null)req.SetRequestHeader("Authorization", auth);
        req.downloadHandler = new DownloadHandlerBuffer();
        await req.SendWebRequest();
        return req;
    }
    
    private static async Task<UnityWebRequest> _postJsonRequest(string url, string json, string auth = null)
    {
        var postData = System.Text.Encoding.UTF8.GetBytes(json);
        var req = new UnityWebRequest(url, "POST");
        if(auth != null)req.SetRequestHeader("Authorization", auth); 
        req.uploadHandler = new UploadHandlerRaw(postData);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        await req.SendWebRequest();
        return req;
    }

    private static string GetGuid(LiteDatabase db)
    {
        var dbUserInfo = db.GetCollection<UserInfo>("UserInfo");
        var userInfo = dbUserInfo.FindOne(x => x.Id.Equals(1));
        if (userInfo != null) return userInfo.Guid;
        userInfo = new UserInfo
        {
            Id = 1,
            Guid = Guid.NewGuid().ToString(),
            AccessToken = "",
            RefreshToken = ""
        };
        dbUserInfo.Insert(userInfo);
        Debug.Log(userInfo.Guid);
        return userInfo.Guid;
    }

    private static string GetAccessToken(ILiteDatabase db)
    {
        var dbUserInfo = db.GetCollection<UserInfo>("UserInfo");
        var userInfo = dbUserInfo.FindOne(x => x.Id.Equals(1));
        return userInfo != null ? userInfo.AccessToken : "";
    }
    
    private static void SetAccessToken(ILiteDatabase db, string accessToken)
    {
        var dbUserInfo = db.GetCollection<UserInfo>("UserInfo");
        var userInfo = dbUserInfo.FindOne(x => x.Id.Equals(1));
        userInfo.AccessToken = accessToken;
        dbUserInfo.Update(userInfo);
    }
    
    private static string GetRefreshToken(ILiteDatabase db)
    {
        var dbUserInfo = db.GetCollection<UserInfo>("UserInfo");
        var userInfo = dbUserInfo.FindOne(x => x.Id.Equals(1));
        return userInfo != null ? userInfo.RefreshToken : "";
    }
    
    
}