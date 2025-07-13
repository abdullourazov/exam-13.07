using AutoMapper;
using Domain.DTOs.Branch;
using Domain.DTOs.Car;
using Domain.DTOs.Customer;
using Domain.DTOs.Rental;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Car, CreateCarDto>().ReverseMap();
        CreateMap<Car, GetCarDto>().ReverseMap();
        CreateMap<Car, UpdateCarDto>().ReverseMap();

        CreateMap<Customer, CreateCustomerDto>().ReverseMap();
        CreateMap<Customer, GetCustomerDto>().ReverseMap();
        CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

        CreateMap<Branch, CreateBranchDto>().ReverseMap();
        CreateMap<Branch, GetBranchDto>().ReverseMap();
        CreateMap<Branch, UpdateBranchDto>().ReverseMap();

        CreateMap<Rental, CreateRentalDto>().ReverseMap();
        CreateMap<Rental, GetRentalDto>().ReverseMap();

    }
}
