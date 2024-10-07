using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class UserLogin : MonoBehaviour
{
    public static string MyToken = string.Empty;

    private UIDocument uiDoc;
    VisualElement root;
    private readonly string url = "http://127.0.0.1/edsa-webserver/account.php";

    private void Start()
    {
        uiDoc = GetComponent<UIDocument>();
        root = uiDoc.rootVisualElement;

        if (gameObject.name == "Login")
            LoginScreen();
        else if (gameObject.name == "Register")
            RegisterScreen();
        else
            LogoutScreen();
    }

    private void OnApplicationQuit()
    {
        StartCoroutine(nameof(LogoutUser));
    }

    private void RegisterScreen()
    {
        TextField username = new("Username");
        TextField email = new("Email");
        TextField password = new("Password");
        Button submit = new();

        password.isPasswordField = true;
        submit.text = "Register";
        email.RegisterCallback<ChangeEvent<string>>(evt =>
        {
            string filtered = FilterEmail(evt.newValue);

            if (filtered != evt.newValue)
                email.SetValueWithoutNotify(filtered);
        });
        submit.RegisterCallback<ClickEvent>(evt =>
        {
            bool validEmail;

            try
            {
                MailAddress mail = new(email.text);
                validEmail = true;
            }
            catch
            {
                Debug.Log("Email invalid!");
                validEmail = false;
            }

            if (validEmail)
            {
                string[] values = new string[3]
                { username.text, email.text, password.text };

                StartCoroutine(nameof(RegisterUser), values);
            }
        });

        root.Add(username);
        root.Add(email);
        root.Add(password);
        root.Add(submit);
    }

    private void LoginScreen()
    {
        TextField username = new("Username");
        TextField password = new("Password");
        Button submit = new();
        Button logout = new();

        password.isPasswordField = true;
        submit.text = "Login";
        submit.RegisterCallback<ClickEvent>(evt =>
        {
            string[] values = new string[2]
            { username.text, password.text };

            StartCoroutine(nameof(LoginUser), values);
        });
        logout.text = "Logout";
        logout.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(nameof(LogoutUser));
        });

        root.Add(username);
        root.Add(password);
        root.Add(submit);
        root.Add(logout);
    }

    private void LogoutScreen()
    {
        Button submit = new();
        submit.text = "Logout";
        submit.RegisterCallback<ClickEvent>(evt =>
        {
            StartCoroutine(nameof(LogoutUser));
        });
        root.Add(submit);
    }

    private string FilterEmail(string input)
    {
        Regex regex = new(@"^[a-zA-Z0-9@._\-]+$");

        string output = "";
        
        foreach (char c in input)
            if (regex.IsMatch(c.ToString()))
                output += c;

        return output;
    }

    private IEnumerator RegisterUser(string _username, string _email, string _password)
    {
        RegisterRequest req = new()
        {
            action = "register",
            username = _username,
            email = _email,
            password = _password
        };

        yield return StartCoroutine(Web.Request<RegisterRequest, LoginResponse>(req, url, response =>
        {
            if (response.status)
                MyToken = response.token;
            else
                Debug.Log("Register error. Message: " + response.message);
        }));
    }

    private IEnumerator LoginUser(string _username, string _password)
    {
        LoginRequest req = new()
        {
            action = "login",
            username = _username,
            password = _password
        };

        yield return StartCoroutine(Web.Request<LoginRequest, LoginResponse>(req, url, response =>
        {
            if (response != null)
            {
                if (response.status)
                    MyToken = response.token;
                else
                    Debug.Log("Login error. Message: " + response.message);
            }
        }));
    }

    private IEnumerator LogoutUser()
    {
        LogoutRequest req = new()
        {
            action = "logout",
            token = MyToken
        };

        yield return StartCoroutine(Web.Request<LogoutRequest, LogoutResponse>(req, url, response =>
        {
            if (response.status)
                MyToken = "";
            else
                Debug.Log("Logout error. Message: " + response.message);
        }));
    }
}
