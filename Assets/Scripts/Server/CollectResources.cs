using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CollectResources : MonoBehaviour
{
    private static readonly string url = "http://127.0.0.1/edsa-webserver/resources.php";

    public static IEnumerator Collect(string resource)
    {
        CollectRequest req = new()
        {
            action = "collect-" + resource,
            token = UserLogin.MyToken
        };

        string json = JsonUtility.ToJson(req);
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection("json", json)
        };

        using UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        webRequest.timeout = 10;
        yield return webRequest.SendWebRequest();
        CollectResponse response = JsonUtility.FromJson<CollectResponse>(webRequest.downloadHandler.text);

        if (response.status)
        {
            PlayerResources.Set(resource, response.newGold);
            Debug.Log($"{resource} added! new {resource}: {response.newGold}");
        }
        else
            Debug.Log($"Failed to collect {resource}. Message: " + response.message);
    }
}
