using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonExample : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            MakeJson();
    }

    private void MakeJson()
    {
        Highscore highscore = new()
        {
            name = "Ger",
            score = 238213
        };
        string json = JsonUtility.ToJson(highscore);
        Debug.Log(json);
    }
}

[System.Serializable]
public struct Highscore
{
    public string name;
    public int score;
}
