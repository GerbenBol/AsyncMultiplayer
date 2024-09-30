using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        rb.AddForce(new(hor * moveSpeed, 0, ver * moveSpeed));

        // !! For development only !!
        if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(new UserLogin().LoginUser("notabot", "livingbeing"));
    }
}
