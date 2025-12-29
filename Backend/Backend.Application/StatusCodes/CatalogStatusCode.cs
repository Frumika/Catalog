namespace Backend.Application.StatusCodes;


public enum CatalogStatusCode
{
    Success,
    ProductNotFound,
    MakerNotFound,
    IncorrectCategory,
    IncorrectMaker,
    BadRequest,
    UnknownError
}