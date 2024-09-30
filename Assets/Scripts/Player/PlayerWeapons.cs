using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    private string leftWeapon;
    private string rightWeapon;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            UseLeftAttack();
        if (Input.GetMouseButtonUp(1))
            UseRightAttack();
    }

    public void SetLeftWeapon(string weaponName)
    {
        leftWeapon = weaponName;
        Transform hand = leftHand.transform;

        for (int i = 0; i < hand.childCount; i++)
            if (hand.GetChild(i).name == weaponName)
            {
                hand.GetChild(i).gameObject.SetActive(true);
                break;
            }
    }

    public void SetRightWeapon(string weaponName)
    {
        rightWeapon = weaponName;
        Transform hand = rightHand.transform;

        for (int i = 0; i < hand.childCount; i++)
            if (hand.GetChild(i).name == weaponName)
            {
                hand.GetChild(i).gameObject.SetActive(true);
                break;
            }
    }

    private void UseLeftAttack()
    {

    }

    private void UseRightAttack()
    {

    }
}
