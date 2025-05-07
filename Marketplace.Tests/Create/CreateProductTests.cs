using System.Net.Http.Json;
using FluentAssertions;
using Marketplace.Api;
using Marketplace.Client;
using Marketplace.Client.Contracts;
using Marketplace.Tests.Create.TestCaseEntities;
using Marketplace.Tests.Create.TestCases;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace Marketplace.Tests.Create;

public class CreateProductTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly ITestOutputHelper _outputHelper;
    private readonly WebApplicationFactory<Program> _applicationFactory;
    private readonly Mock<TimeProvider> _timeProviderMock;

    public CreateProductTests(ITestOutputHelper outputHelper, WebApplicationFactory<Program> applicationFactory)
    {
        _outputHelper = outputHelper;
        _timeProviderMock = new Mock<TimeProvider>();
        _applicationFactory = applicationFactory;
    }

    /// <summary>
    ///     Позитивные тест-кейсы
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<object[]> PositiveTesCases()
    {
        yield return new object[] { Create_Product_Returns201Created_AddsProductToDb.Get() };
    }

    [Theory]
    [MemberData(nameof(PositiveTesCases))]
    public async Task Create_ValidProduct_Returns201Created(CreateProductTestCase testCase)
    {
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        ArrangeMocks(testCase.MocksData);

        var httpClient = CreateClient();
        var productsApiClient = new ProductsApiClient(httpClient);

        await ArrangeStorageState(testCase.StorageState, productsApiClient);


        // ACT
        var response = await httpClient.PostAsJsonAsync("api/v1/Products", testCase.Parameters.NewProduct, CancellationToken.None);
        var product = await response.Content.ReadFromJsonAsync<ProductDto>(CancellationToken.None);
        var actualProduct = await productsApiClient.GetAsync(product!.Id, CancellationToken.None);

        // ASSERT
        response.StatusCode.Should().Be(testCase.Expectations.HttpStatusCode);
        response.Headers.Location.Should().Be(string.Format(testCase.Expectations.LocationHeader!, arg0: product.Id));
        actualProduct.Should().BeEquivalentTo(testCase.Expectations.Product, options => options.Excluding(dto => dto.Id));
    }

    [Theory]
    [ClassData(typeof(CreateProductNegativeCases))]
    public async Task Create_InvalidProduct_ReturnsUnsuccessfulCode(CreateProductTestCase testCase)
    {
        _outputHelper.WriteLine(testCase.TestId);
        _outputHelper.WriteLine(testCase.Description);

        // ARRANGE 
        ArrangeMocks(testCase.MocksData);

        var httpClient = CreateClient();
        var productsApiClient = new ProductsApiClient(httpClient);

        await ArrangeStorageState(testCase.StorageState, productsApiClient);


        // ACT
        var response = await httpClient.PostAsJsonAsync("api/v1/Products", testCase.Parameters.NewProduct, CancellationToken.None);
        var error = await response.Content.ReadAsStringAsync(CancellationToken.None);

        // ASSERT
        response.StatusCode.Should().Be(testCase.Expectations.HttpStatusCode);
        response.Headers.Location.Should().BeNull();
        error.Should().Be(testCase.Expectations.Error);
    }

    /// <summary>
    ///     Настраивает состояние БД перед прогоном тестов
    /// </summary>
    private async Task ArrangeStorageState(CreateProductTestCaseStorageState storageState, ProductsApiClient productsApiClient)
    {
        if (storageState.ExistingProduct is not null)
            await productsApiClient.CreateAsync(storageState.ExistingProduct, CancellationToken.None);
    }

    /// <summary>
    ///     Поднимает контект приложения, возвращает сконфигурированный клиент
    /// </summary>
    /// <returns></returns>
    private HttpClient CreateClient()
    {
        return _applicationFactory.WithWebHostBuilder(b => b.ConfigureTestServices(s => s.AddSingleton(_timeProviderMock.Object)))
                                  .CreateClient();
    }

    /// <summary>
    ///     Настраивает моки
    /// </summary>
    /// <param name="mocksData"></param>
    private void ArrangeMocks(CreateProductTestCaseMocksData mocksData)
    {
        _timeProviderMock.Setup(provider => provider.GetUtcNow()).Returns(mocksData.UtcNow);
    }
}