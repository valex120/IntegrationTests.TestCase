using System.Net;
using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     ��������� �������� ��� ���� ����� �� �������� ������
/// </summary>
public class CreateProductTestCaseExpectations
{
    /// <summary>
    ///     ��� ������ �������
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    /// <summary>
    ///     ����� ������
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    ///     �������� ������ Location � ������ �������
    /// </summary>
    public string? LocationHeader { get; set; }

    /// <summary>
    ///     ��������� �����
    /// </summary>
    public ProductDto? Product { get; set; }
}