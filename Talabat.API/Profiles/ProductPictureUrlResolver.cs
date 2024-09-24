﻿using AutoMapper;
using Talabat.API.DTOS;
using Talabat.Core.Entities;

namespace Talabat.API.Profiles
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration) {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
