namespace SIS.HTTP.Enumerators
{
    public enum HttpResponseCode
    {
        OK = 200,
        MOVEDPERMANENTLY = 301,
        FOUND = 302,
        TEMPORARYREDIRECT = 307,
        UNAUTHORIZED = 401,
        FORBIDDEN = 403,
        NOTFOUND = 404,
        INTERNALSERVERERROR = 500,
    }
}
