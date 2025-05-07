using System.Net;

namespace Marketplace.Client.Exceptions;

/// <summary>
///     ������ ������� � API
/// </summary>
public sealed class ApiException : Exception
{
    /// <summary>
    ///     ��� ������
    /// </summary>
    public HttpStatusCode Code { get; }

    public ApiException(HttpStatusCode code, string message) : base(message)
    {
        Code = code;
    }
}