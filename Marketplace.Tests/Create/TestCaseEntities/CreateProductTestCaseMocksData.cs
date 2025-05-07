namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     Данные для настройки моков для тест кейса на создание товара
/// </summary>
public class CreateProductTestCaseMocksData
{
    /// <summary>
    ///     Зафиксированная дата-время для мока <see cref="TimeProvider"/>
    /// </summary>
    public DateTimeOffset UtcNow { get; set; }
}