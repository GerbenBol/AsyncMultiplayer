using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web
{
    private static readonly Web _instance = new();

    private Web() { }

    public static Web Instance
    {
        get
        {
            return _instance;
        }
    }

    public static IEnumerator Request<TRequest, TResponse>(TRequest req, string url, Action<TResponse> onComplete)
        where TRequest : AbstractRequest
        where TResponse : AbstractResponse
    {
        string json = JsonUtility.ToJson(req);
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection("json", json)
        };

        using UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        webRequest.timeout = 10;
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
            webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log($"Request failure. Message: {webRequest.error}");
            onComplete?.Invoke(null);
        }
        else
        {
            try
            {
                TResponse response = JsonUtility.FromJson<TResponse>(webRequest.downloadHandler.text);
                onComplete?.Invoke(response);
            }
            catch (Exception e)
            {
                Debug.Log($"Parse failure. Message: {e.Message} Input string: {webRequest.downloadHandler.text}");
                onComplete?.Invoke(null);
            }
        }
    }
}
