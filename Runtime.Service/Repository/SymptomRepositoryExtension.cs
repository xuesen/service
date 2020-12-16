using System.Linq;
using IIMes.Infrastructure.Data.Interface;
using IIMes.Services.Runtime.Model.Quality;
using IIMes.Services.Runtime.Model.Resource;

namespace IIMes.Services.Runtime.Repository
{
    public static class SymptomRepositoryExtension
    {
        public static Symptom GetSymptom(this IRepository<Symptom> repository, string code)
        {
            return repository.Query().Where(p => p.Code == code).FirstOrDefault();
        }
    }
}
