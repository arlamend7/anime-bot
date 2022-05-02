using Libs.Base.Entities;

namespace Libs.Base.Models.Responses
{
    public class Response<T>
        where T : EntityBase
    {
        public long Id { get; set; }
    }
}
