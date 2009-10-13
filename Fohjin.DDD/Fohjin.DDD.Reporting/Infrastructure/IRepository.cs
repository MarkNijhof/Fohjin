using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface IRepository 
    {
        IEnumerable<TDto> GetByExample<TDto>(object example) where TDto : class;
        void Save<TDto>(TDto dto) where TDto : class;
    }
}