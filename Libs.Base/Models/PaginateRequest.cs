using Libs.Base.Entities;

namespace Libs.Base.Models
{
    public class PaginateRequest<T> : PaginateRequest
        where T : EntityBase
    {
        
    }
    public class PaginateRequest
    {
        public int Qt { get; set; }
        public int Pg { get; set; }
        public PaginateRequest()
        {
            Qt = 10;
            Pg = 0;
        }
    }
}
