using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     ������� ��������� ��� ���� ����� �� �������� ������
/// </summary>
public class CreateProductTestCaseParameters
{
    /// <summary>
    ///     ������ �� �������� ������
    /// </summary>
    public CreateProductRequest NewProduct { get; set; } = default!;
}