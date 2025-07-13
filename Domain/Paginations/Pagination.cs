using Domain.ApiResponse;
using Microsoft.EntityFrameworkCore;


namespace Domain.Paginations;

public class Pagination<T>(IQueryable<T> queryable)
{
    public async Task<PagedResponse<List<T>>> GetPagedResponseAsync(int pageNumber, int pageSize)
    {
        try
        {
            var validFilter = new ValidFilter(pageNumber, pageSize);

            var query = queryable;

            var totalRecords = await query.CountAsync();

            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<List<T>>(data, totalRecords, pageNumber, pageSize);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
