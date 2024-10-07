using System;
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
            StartCoroutine(LoginUser("notabot", "livingbeing"));
    }

    private IEnumerator LoginUser(string _username, string _password)
    {
        LoginRequest req = new()
        {
            action = "login",
            username = _username,
            password = _password
        };

        yield return StartCoroutine(Web.Request<LoginRequest, LoginResponse>(req, "http://127.0.0.1/edsa-webserver/account.php", response =>
        {
            if (response != null)
            {
                if (response.status)
                    UserLogin.MyToken = response.token;
                else
                    Debug.Log("Login error. Message: " + response.message);
            }
        }));
    }
}
