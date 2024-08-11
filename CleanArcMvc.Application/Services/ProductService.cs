using AutoMapper;
using CleanArcMvc.Application.DTOs;
using CleanArcMvc.Application.Interfaces;
using CleanArcMvc.Application.Products.Commands;
using CleanArcMvc.Application.Products.Queries;
using CleanArcMvc.Domain.Entities;
using CleanArcMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArcMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            //var productsEntity = await _productRepository.GetProducts();
        //    return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);

            var productsQuery = new GetProductsQuery();
            if (productsQuery == null)            
                throw new Exception($"Entity could not be loaded.");

            var result = await _mediator.Send(productsQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }


        public async Task<ProductDTO> GetById(int? id)
        {
            var productByIdQuery = new GetProductByIdQuery(id.Value);
            if (productByIdQuery == null)
                throw new Exception($"Entity could not be loaded.");

            var result = await _mediator.Send(productByIdQuery);
            return _mapper.Map<ProductDTO>(result);
        }

        //public async Task<ProductDTO> GetProductCategory(int? id)
        //{
        //    var productByIdQuery = new GetProductByIdQuery(id.Value);
        //    if (productByIdQuery == null)
        //        throw new Exception($"Entity could not be loaded.");
        //    var result = await _mediator.Send(productByIdQuery);
        //    return _mapper.Map<ProductDTO>(result);
        //}

        public async Task Add(ProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }

        public async Task Remove(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if (productRemoveCommand == null)
                throw new Exception($"Entity could not be loaded.");

            await _mediator.Send(productRemoveCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }
    }
}
