using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class InputExample : MonoBehaviour
{
    private UIDocument uiDoc;
    private readonly string url = "http://127.0.0.1/edsa-webserver/connect.php";

    private void Start()
    {
        uiDoc = GetComponent<UIDocument>();
        VisualElement root = uiDoc.rootVisualElement;
        TextField fruitName = new("Fruit Name");
        TextField fruitColor = new("Fruit Color");
        TextField fruitQuantity = new("Fruit Quantity");
        Button submit = new();
        submit.text = "Submit";
        submit.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(InsertIntoDatabase(fruitName.text, fruitColor.text, Convert.ToInt32(fruitQuantity.text)));
        });
        root.Add(fruitName);
        root.Add(fruitColor);
        root.Add(fruitQuantity);
        root.Add(submit);

        /*TextField textField = new("password");
        root.Add(textField);
        Button button = new();
        button.text = "Submit";
        button.RegisterCallback<ClickEvent>(evt =>
        {
            Debug.Log(textField.text);
        });
        root.Add(button);
        Button thatSameButton = root.Q<Button>("Button");*/

        /*for (int i = 0; i < 10; i++)
        {
            int id = i;
            Button button = new();
            button.text = "Button: " + i;
            button.RegisterCallback<ClickEvent>(evt =>
            {
                Debug.Log("button " + id + " click");
            });
            root.Add(button);
        }*/
    }

    private IEnumerator InsertIntoDatabase(string name, string color, int quantity)
    {
        Fruit fruit = new()
        {
            Name = name,
            Color = color,
            Quantity = quantity
        };
        string json = JsonUtility.ToJson(fruit);
        List<IMultipartFormSection> form = new()
        {
            new MultipartFormDataSection("json", json)
        };

        using UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);
    }
}

public struct Fruit
{
    public string Name, Color;
    public int Quantity;
}
