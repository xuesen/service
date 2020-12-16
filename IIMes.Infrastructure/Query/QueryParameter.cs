using Microsoft.AspNetCore.Mvc;

namespace IIMes.Infrastructure.Query
{
    public class QueryParameter
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public bool Fuzzy { get; set; } = true;

        public string OrderBy { get; set; } = string.Empty;
    }
}