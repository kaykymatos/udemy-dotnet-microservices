using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GeekShooping.ProductApi.Model;
using GeekShooping.ProductApi.Data.ValueObjects;
using GeekShooping.ProductApi.Model.Context;
using GeekShooping.ProductApi.Repository;

namespace GeekShooping.CategoryApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MySqlContext _context;
        private IMapper _mapper;

        public CategoryRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryVO>> FindAll()
        {
            List<Category> Categorys = await _context.Category.ToListAsync();
            return _mapper.Map<List<CategoryVO>>(Categorys);
        }

        public async Task<CategoryVO> FindById(long id)
        {
            Category Category = await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync()??new Category();

            return _mapper.Map<CategoryVO>(Category);
        }
        public async Task<CategoryVO> Create(CategoryVO vo)
        {
            var Category = _mapper.Map<Category>(vo);
            _context.Category.Add(Category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryVO>(Category);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var category = await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync()?? new Category();
                if (category.Id<=0)
                    return false;
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public async Task<CategoryVO> Update(CategoryVO vo)
        {
            var Category = _mapper.Map<Category>(vo);
            _context.Category.Update(Category);
            await _context.SaveChangesAsync();
            return _mapper.Map<CategoryVO>(Category);
        }
    }
}
