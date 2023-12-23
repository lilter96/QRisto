using QRisto.Persistence.Entity.Provider;

namespace QRisto.Persistence.Repositories.Comment;

public interface ICommentRepository : IGenericRepository<CommentEntity>
{
    Task<double> GetAverageRatingAsync(Guid serviceId);

    Task<List<CommentEntity>> GetUserCommentsWithPaginationAsync(Guid userId, int pageNumber, int pageSize);

    Task<List<CommentEntity>> GetServiceCommentsWithPaginationAsync(Guid serviceId, int pageNumber, int pageSize);
}