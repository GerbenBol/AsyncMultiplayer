[System.Serializable]
public struct RegisterRequest
{
    public string action;
    public string email;
    public string username;
    public string password;
}

[System.Serializable]
public struct LoginRequest
{
    public string action;
    public string username;
    public string password;
}

[System.Serializable]
public struct LoginResponse
{
    public bool status;
    public string message;
    public string token;
}

[System.Serializable]
public struct LogoutRequest
{
    public string action;
    public string token;
}

[System.Serializable]
public struct LogoutResponse
{
    public bool status;
    public string message;
}

[System.Serializable]
public struct CollectRequest
{
    public string action;
    public string token;
}

[System.Serializable]
public struct CollectResponse
{
    public bool status;
    public string message;
    public int newGold;
}

[System.Serializable]
public struct TownRequest
{
    public string action;
    public string upgrade;
    public string token;
}

[System.Serializable]
public struct TownResponse
{
    public bool status;
    public string message;
}
