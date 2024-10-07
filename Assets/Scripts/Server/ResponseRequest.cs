using System;

[Serializable]
public abstract class AbstractRequest
{
    public string action;
}

[Serializable]
public abstract class AbstractResponse
{
    public bool status;
    public string message;
}

[Serializable]
public class RegisterRequest : AbstractRequest
{
    public string email;
    public string username;
    public string password;
}

[Serializable]
public class LoginRequest : AbstractRequest
{
    public string username;
    public string password;
}

[Serializable]
public class LoginResponse : AbstractResponse
{
    public string token;
}

[Serializable]
public class LogoutRequest : AbstractRequest
{
    public string token;
}

[Serializable]
public class LogoutResponse : AbstractResponse
{ }

[Serializable]
public class CollectRequest : AbstractRequest
{
    public string token;
}

[Serializable]
public class CollectResponse : AbstractResponse
{
    public int newValue;
}

[Serializable]
public class TownRequest : AbstractRequest
{
    public string upgrade;
    public string token;
}

[Serializable]
public class TownResponse : AbstractResponse
{ }
