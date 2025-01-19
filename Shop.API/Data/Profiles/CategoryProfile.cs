using System;
using AutoMapper;

namespace Shop.API.Data.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Entities.Category.Category, Models.Category.CategoryDTO>();
    }
}
