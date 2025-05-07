using Marketplace.Client.Contracts;

namespace Marketplace.Tests.Create.TestCaseEntities;

/// <summary>
///     —осто€ние хранилища дл€ тест кейса на создание товара
/// </summary>
public class CreateProductTestCaseStorageState
{
    /// <summary>
    ///     «апрос на создание товара
    /// </summary>
    public CreateProductRequest? ExistingProduct { get; set; }
}