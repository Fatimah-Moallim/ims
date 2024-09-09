namespace IMS.UseCases.Products.Interfaces
{
    public interface IDeleteProductUseCase
    {
        Task ExcuteAsync(int productId);
    }
}