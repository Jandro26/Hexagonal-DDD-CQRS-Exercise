using AutoMapper;
using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.entry_point.catalog.v1.model;

namespace Hexagonal_Exercise.entry_point.catalog.v1
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<FindProductQueryResult, GetProductByIdResultModel>();
        }
        
    }
}
