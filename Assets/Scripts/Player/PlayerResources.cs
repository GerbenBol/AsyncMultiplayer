using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    private static int currentGold = 0;
    private static int currentMetal = 0;
    private static int currentWood = 0;

    public static void Set(string resource, int newTotal)
    {
        switch (resource)
        {
            case "gold":
                {
                    currentGold = newTotal;
                    break;
                }
            case "metal":
                {
                    currentMetal = newTotal;
                    break;
                }
            case "wood":
                {
                    currentWood = newTotal;
                    break;
                }
            default: break;
        }
    }

    public static int Get(string resource)
    {
        return resource switch
        {
            "gold" => currentGold,
            "metal" => currentMetal,
            "wood" => currentWood,
            _ => 0,
        };
    }
}
