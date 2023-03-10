using AutoMapper;
using BookProject.Application.Models;
using BookProject.Data.Entities;
using BookProject.Data.Models;

namespace BookProject.Application.Mapper
{
    public class BookProjectMappingProfile:Profile
    {
        public BookProjectMappingProfile()
        {
            CreateMap<Article, ArticleModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Magazine, MagazineModel>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();
            CreateMap<Article, ArticleResponse>().ReverseMap();
            CreateMap<Article, ArticleResponse>().ReverseMap();
            CreateMap<ArticleResponse, ArticleJoinModel>().ReverseMap();
            CreateMap<ArticleResonseNames, ArticleJoinModel>().ReverseMap();
            CreateMap<Magazine, MagazineResponse>().ReverseMap();
            
        }

    }
}
