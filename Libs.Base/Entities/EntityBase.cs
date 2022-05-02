using System;

namespace Libs.Base.Entities
{
    public class EntityBase
    {
        public virtual long Id { get; protected set; }
        public virtual void SetId(long key)
        {
            if (key.Equals(default)) throw new Exception("Index not found");
            Id = key;
        }
    }
}
