using System;
using System.Collections.Generic;
using System.Text;
using DAL.UnitOfWork;
using DAL.Models;
using BLL.Mappers;
using System.Linq;
using BLL.DTO;
using System.Linq.Expressions;

namespace BLL
{
    public class TasksService
    {
        private UnitOfWork _unitOfWork = new UnitOfWork();
        private ProductMapper _productMapper = new ProductMapper();
        private ProviderMapper _providerMapper = new ProviderMapper();
        private CategoryMapper _categoryMapper = new CategoryMapper();
        public IEnumerable<ProductDTO> GetProductsByCategory(CategoryDTO category)
        {
            if (category == null)
            {
                return null;
            }
            int categoryId;
            if (_unitOfWork.CategoryRepository.GetById(category.Id) == null)
            {
                if (_unitOfWork.CategoryRepository.Get((c => c.Name == category.Name)) == null)
                {
                    return null;
                }
                else
                {
                    categoryId = _unitOfWork.CategoryRepository.Get((c => c.Name == category.Name)).FirstOrDefault().Id;
                }
            }
            else
            {
                categoryId = category.Id ?? throw new ArgumentNullException(nameof(category.Id));
            }
            var productCollection = _unitOfWork.ProductRepository.Get((p => p.CategoryId == categoryId));
            return _productMapper.Map(productCollection);
        }
        public IEnumerable<ProviderDTO> GetProvidersByCategory(CategoryDTO category)
        {
            if (GetProductsByCategory(category) == null)
            {
                return null;
            }
            else
            {
                var products = GetProductsByCategory(category);
                List<Provider> providers = new List<Provider>();
                foreach (ProductDTO product in products)
                {
                    var provider = _unitOfWork.ProviderRepository.Get((pr => pr.Name == product.ProviderName)).FirstOrDefault();
                    if (provider != null && !providers.Contains(provider))
                    {
                        providers.Add(provider);
                    }
                }
                return _providerMapper.Map(providers);
            }
        }
        public IEnumerable<ProductDTO> GetProductsByProvider(ProviderDTO provider)
        {
            if (provider == null)
            {
                return null;
            }
            int providerId;
            if (_unitOfWork.ProviderRepository.GetById(provider.Id) == null)
            {
                if(_unitOfWork.ProviderRepository.Get((p => p.Name == provider.Name)) == null)
                {
                    return null;
                }
                else
                {
                    providerId = _unitOfWork.ProviderRepository.Get((p => p.Name == provider.Name)).First().Id;
                }
            }
            else
            {
                providerId = provider.Id ?? throw new ArgumentNullException(nameof(provider.Id));
            }
            var productCollection = _unitOfWork.ProductRepository.Get((p => p.ProviderId == providerId));
            return _productMapper.Map(productCollection);
        }
        public IEnumerable<ProductDTO> GetProductByPredicate(Expression<Func<Product, bool>> filter = null)
        {
            return _productMapper.Map(_unitOfWork.ProductRepository.Get(filter));
        }
        public IEnumerable<CategoryDTO> GetCategoriesByPredicate(Expression<Func<Category, bool>> filter = null)
        {
            return _categoryMapper.Map(_unitOfWork.CategoryRepository.Get(filter));
        }
        public IEnumerable<ProviderDTO> GetProvidersByPredicate(Expression<Func<Provider, bool>> filter = null)
        {
            return _providerMapper.Map(_unitOfWork.ProviderRepository.Get(filter));
        }
    }
}
