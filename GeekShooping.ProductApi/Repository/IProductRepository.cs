using GeekShooping.ProductApi.Data.ValueObjects;

namespace GeekShooping.ProductApi.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProdutVO>> FindAll();
        Task<ProdutVO> FindById(long id);
        Task<ProdutVO> Create(ProdutVO vo);
        Task<ProdutVO> Update(ProdutVO vo);
        Task<bool> Delete(long id);
    }
}
