using Microsoft.EntityFrameworkCore;
using SquadAsService.Domain.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquadAsService.Application.Extensions
{
    public static class QuerableExentsions
    {
        public static async Task<PaginatedResponse<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken) where T : class
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.CountAsync();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            return PaginatedResponse<T>.Create(items, count, pageNumber, pageSize);
        }

        public static string SearchingFormat(this string val)
        {
            return val.ToLower().Replace(" ", "");
        }
    }
}
