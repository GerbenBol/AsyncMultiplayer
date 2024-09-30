using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWeapon : MonoBehaviour
{
    private PlayerWeapons player;

    private void Start()
    {
        player = GameObject.Find("Starter").GetComponent<PlayerWeapons>();
    }

    public void ChooseWeapon(string weaponName)
    {
        if (weaponName == "axe" || weaponName == "pick")
            player.SetRightWeapon(weaponName);

        gameObject.SetActive(false);
    }
}
