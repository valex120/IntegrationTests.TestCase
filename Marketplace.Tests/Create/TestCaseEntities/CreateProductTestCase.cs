namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     Nест-кейс на создание товара
/// </summary>
public class CreateProductTestCase
{
    /// <summary>
    ///     Идентификатор тест кейса
    /// </summary>
    public string TestId { get; set; } = default!;

    /// <summary>
    ///     Описание тест кейса
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    ///     Состояние хранилища
    /// </summary>
    public CreateProductTestCaseStorageState StorageState { get; set; } = default!;

    /// <summary>
    ///     Данные для настройки моков
    /// </summary>
    public CreateProductTestCaseMocksData MocksData { get; set; } = default!;

    /// <summary>
    ///     Входные параметры
    /// </summary>
    public CreateProductTestCaseParameters Parameters { get; set; } = default!;

    /// <summary>
    ///     Ожидаемые значения
    /// </summary>
    public CreateProductTestCaseExpectations Expectations { get; set; } = default!;
}