namespace Marketplace.Domain.Errors;

/// <summary>
///     ������ �������� ������
/// </summary>
public enum DeleteProductError
{
    /// <summary>
    ///     ������ ���
    /// </summary>
    None = 0,

    /// <summary>
    ///     ����� �� ������
    /// </summary>
    NotFound
}