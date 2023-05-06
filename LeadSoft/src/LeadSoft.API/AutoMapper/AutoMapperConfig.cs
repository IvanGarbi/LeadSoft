using AutoMapper;
using LeadSoft.API.ViewModels;
using LeadSoft.Core.Models;

namespace LeadSoft.API.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<GetAuthorViewModel, Author>().ReverseMap();
        CreateMap<PostAuthorViewModel, Author>().ReverseMap();

        CreateMap<GetArticleViewModel, Article>().ReverseMap();
        CreateMap<PostArticleViewModel, Article>().ReverseMap();
    }
}