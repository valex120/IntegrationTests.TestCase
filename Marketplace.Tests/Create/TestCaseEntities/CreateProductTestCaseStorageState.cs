using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     ��������� ��������� ��� ���� ����� �� �������� ������
/// </summary>
public class CreateProductTestCaseStorageState
{
    /// <summary>
    ///     ������ �� �������� ������
    /// </summary>
    public CreateProductRequest? ExistingProduct { get; set; }
}