using System.Collections.Generic;

namespace Libs.Base.Models
{
    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Registros { get; set; }
        public long Total { get; set; }
    }
}
