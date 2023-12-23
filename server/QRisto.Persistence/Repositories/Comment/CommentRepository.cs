using Microsoft.EntityFrameworkCore;
using QRisto.Persistence.Entity.Provider;
using QRisto.Persistence.Repositories.Implementations;

namespace QRisto.Persistence.Repositories.Comment;

public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<double> GetAverageRatingAsync(Guid serviceId)
    {
        var averageRatingForService = await DbSet
            .Where(c => c.ServiceId == serviceId)
            .AverageAsync(c => (double)c.Rating);

        return averageRatingForService;
    }
    
    public async Task<List<CommentEntity>> GetUserCommentsWithPaginationAsync(Guid userId, int pageNumber, int pageSize)
    {
        var comments = await DbSet
            .Where(c => c.UserId == userId && c.DeletedDate == null)
            .OrderByDescending(c => c.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return comments;
    }
    
    public async Task<List<CommentEntity>> GetServiceCommentsWithPaginationAsync(Guid serviceId, int pageNumber, int pageSize)
    {
        var comments = await DbSet
            .Where(c => c.ServiceId == serviceId && c.DeletedDate == null)
            .OrderByDescending(c => c.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return comments;
    }
}