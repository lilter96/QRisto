using AutoMapper;
using QRisto.Application.Models.Request.Comment;
using QRisto.Application.Models.Response.Comment;
using QRisto.Persistence.Entity.Provider;

namespace QRisto.Application.Mappings;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<CreateCommentRequestModel, CommentEntity>();
        CreateMap<UpdateCommentRequestModel, CommentEntity>();
        CreateMap<CommentEntity, CommentResponseModel>();
    }
}