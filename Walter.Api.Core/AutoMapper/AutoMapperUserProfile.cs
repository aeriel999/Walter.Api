using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Dtos;
using Walter.Api.Core.Entities;

namespace Walter.Api.Core.AutoMapper;
public class AutoMapperUserProfile : Profile
{
    public AutoMapperUserProfile()
    {
		CreateMap<AddUserDto, ApiUser>().ReverseMap();
	}
}
