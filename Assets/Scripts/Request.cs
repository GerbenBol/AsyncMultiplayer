using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Request : MonoBehaviour
{
    private readonly string url = "http://127.0.0.1/edsa-webserver/api.php";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(nameof(RequestExample));
    }

    private IEnumerator RequestExample()
    {
        Highscore highscore = new()
        {
            name = "Ger",
            score = 238213
        };
        string json = JsonUtility.ToJson(highscore);
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection("json", json)
        };

        using UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
    }
}
