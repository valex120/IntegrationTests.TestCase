namespace Marketplace.Client.Contracts;

/// <summary>
///     Запрос поиска товаров
/// </summary>
public class SearchProductsRequest
{
    /// <summary>
    ///     Вхождение в наименование товара
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Категория товара
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    ///     Начальная цена
    /// </summary>
    public int? StartPrice { get; set; }

    /// <summary>
    ///     Конечная цена
    /// </summary>
    public int? EndPrice { get; set; }
}