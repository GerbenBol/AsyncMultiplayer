using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceStructure : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    private readonly string url = "http://127.0.0.1/edsa-webserver/town.php";
    private int level = 1;
    private static string currentOpen = "";

    private void OnMouseDown()
    {
        currentOpen = gameObject.name;
        ui.SetActive(true);
        TextMeshProUGUI tmp = ui.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        string[] splitText = tmp.text.Split("level");
        tmp.text = splitText[0] + "level " + level + ")";
    }

    public void GetResource(string resource)
    {
        StartCoroutine(CollectResources.Collect(resource));
        Deselect();
    }

    public void UpgradeSelected()
    {
        StartCoroutine(nameof(Upgrade));
        Deselect();
    }

    private void Deselect()
    {
        currentOpen = "";
        ui.SetActive(false);
    }

    private IEnumerator Upgrade()
    {
        Debug.Log(currentOpen);
        TownRequest req = new()
        {
            action = "upgrade",
            upgrade = currentOpen,
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
        TownResponse response = JsonUtility.FromJson<TownResponse>(json);

        if (response.status)
        {
            Debug.Log(response.message);
        }
        else
            Debug.Log($"Failed to upgrade {currentOpen}. Message: {response.message}");
    }
}
