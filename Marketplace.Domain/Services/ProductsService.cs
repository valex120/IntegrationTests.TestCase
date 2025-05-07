using System.Text.RegularExpressions;
using Marketplace.Domain.Entities;
using Marketplace.Domain.Errors;

namespace Marketplace.Domain.Services;

/// <summary>
///     Служба товаров
/// </summary>
public sealed class ProductsService
{
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    /// <summary>
    ///     Возвращает товар
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetAsync(id, cancellationToken);
        return product;
    }

    /// <summary>
    ///     Возвращает страницу поиска товаров
    /// </summary>
    /// <param name="name">Вхождение в наименование товара</param>
    /// <param name="startPrice">Начальная цена товара</param>
    /// <param name="endPrice">Конечная цена товара</param>
    /// <param name="productCategory">Категория товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>
    ///     Возвращает ошибку, если переданы некорректные параметры поиска
    /// </returns>
    public async Task<(Product[]?, SearchParametersError)> SearchAsync(
        string? name,
        int? startPrice,
        int? endPrice,
        ProductCategory? productCategory,
        CancellationToken cancellationToken)
    {
        if (productCategory is ProductCategory.Unknown)
            return (null, SearchParametersError.UnknownCategory);

        if (startPrice == 0)
            return (null, SearchParametersError.ZeroStartPrice);

        if (startPrice < 0)
            return (null, SearchParametersError.NegativeStartPrice);

        if (endPrice == 0)
            return (null, SearchParametersError.ZeroEndPrice);

        if (endPrice < 0)
            return (null, SearchParametersError.NegativeEndPrice);

        var products = await _productsRepository.SearchAsync(name, startPrice, endPrice, productCategory, cancellationToken);
        return (products, SearchParametersError.None);
    }

    /// <summary>
    ///     Добавляет новый товар
    /// </summary>
    /// <param name="product">Данные товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>
    ///     Возвращает ошибку, если переданы некорректные данные товара
    /// </returns>
    public async Task<CreateProductError> CreateAsync(Product product, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(product.Name))
            return CreateProductError.EmptyName;

        if (product.Name.Length > 1000)
            return CreateProductError.NameTooLong;

        if (product.Article.Length > 255)
            return CreateProductError.ArticleTooLong;

        if (Regex.IsMatch(product.Article, "^[A-Za-z0-9-]{10,40}$") is false)
            return CreateProductError.IncorrectArticle;

        if (product.Category is ProductCategory.Unknown)
            return CreateProductError.UnknownCategory;

        if (product.Price == 0)
            return CreateProductError.ZeroPrice;

        if (product.Price < 0)
            return CreateProductError.NegativePrice;

        if (await _productsRepository.ExistsAsync(product.Article, cancellationToken))
            return CreateProductError.AlreadyExists;

        await _productsRepository.CreateAsync(product, cancellationToken);
        return CreateProductError.None;
    }

    /// <summary>
    ///     Изменяет данные товара
    /// </summary>
    /// <param name="product">Данные товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>
    ///     Возвращает ошибку, если переданы некорректные данные товара
    /// </returns>
    public async Task<UpdateProductError> UpdateAsync(Product product, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(product.Name))
            return UpdateProductError.EmptyName;

        if (product.Name.Length > 1000)
            return UpdateProductError.NameTooLong;

        if (product.Price == 0)
            return UpdateProductError.ZeroPrice;

        if (product.Price < 0)
            return UpdateProductError.NegativePrice;

        if (product.Category is ProductCategory.Unknown)
            return UpdateProductError.UnknownCategory;

        if (await _productsRepository.GetAsync(product.Id, cancellationToken) is not null)
            return UpdateProductError.NotFound;

        await _productsRepository.UpdateAsync(product, cancellationToken);
        return UpdateProductError.None;
    }

    /// <summary>
    ///     Удаляет товар
    /// </summary>
    /// <param name="id">Идентификатор товара</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>
    ///     Возвращает ошибку, если товар не был найден
    /// </returns>
    public async Task<DeleteProductError> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        if (await _productsRepository.GetAsync(id, cancellationToken) is not null)
            return DeleteProductError.NotFound;

        await _productsRepository.DeleteAsync(id, cancellationToken);
        return DeleteProductError.None;
    }
}