using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ResourceStructure : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    private readonly string upgradeURL = "http://127.0.0.1/edsa-webserver/town.php";
    private readonly string collectURL = "http://127.0.0.1/edsa-webserver/resources.php";
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
        StartCoroutine(nameof(Collect), resource);
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
        TownRequest req = new()
        {
            action = "upgrade",
            upgrade = currentOpen,
            token = UserLogin.MyToken
        };

        yield return StartCoroutine(Web.Request<TownRequest, TownResponse>(req, upgradeURL, response =>
        {
            if (response != null)
            {
                if (response.status)
                    level = Convert.ToInt32(response.message);
                else
                    Debug.Log($"Failed to upgrade {currentOpen}. Message: {response.message}");
            }
        }));
    }

    private IEnumerator Collect(string resource)
    {
        CollectRequest req = new()
        {
            action = "collect-" + resource,
            token = UserLogin.MyToken
        };

        yield return StartCoroutine(Web.Request<CollectRequest, CollectResponse>(req, collectURL, response =>
        {
            if (response != null)
            {
                if (response.status)
                    PlayerResources.Set(resource, response.newValue);
                else
                    Debug.Log($"Failed to collect {resource}. Message: {response.message}");
            }
        }));
    }
}
