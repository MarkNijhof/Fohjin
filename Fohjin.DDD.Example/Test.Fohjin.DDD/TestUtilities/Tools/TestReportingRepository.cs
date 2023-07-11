using Fohjin.DDD.Reporting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.TestUtilities.Tools
{
    public class TestReportingRepository : IReportingRepository
    {
        private readonly TestContext _testContext;
        private readonly IServiceProvider _serviceProvider;

        public TestReportingRepository(
            TestContext testContext,
            IServiceProvider serviceProvider
            )
        {
            _testContext = testContext;
            _serviceProvider = serviceProvider;
        }


        public void Delete<TDto>(object example) where TDto : class
        {
            _testContext.AddResults(typeof(TDto).Name + "-delete", example);
        }

        public IEnumerable<TDto> GetByExample<TDto>(object? example) where TDto : class
        {
            if (example == null)
                yield break;

            _testContext.AddResults(typeof(TDto).Name + "-getby", example);
            yield return (TDto)typeof(TDto).BuildObject(_serviceProvider);
        }

        public void Save<TDto>(TDto dto) where TDto : class
        {
            _testContext.AddResults(typeof(TDto).Name, dto);
        }

        public void Update<TDto>(object update, object where) where TDto : class
        {
            _testContext.AddResults(typeof(TDto).Name + "-update", update);
            _testContext.AddResults(typeof(TDto).Name + "-where", where);
        }
    }
}