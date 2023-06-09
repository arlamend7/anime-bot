using System;

namespace Libs.Base.Entities
{
    public class EntityBase
    {
        public virtual object Id { get; protected set; }
        public virtual void SetId(object key)
        {
            if (key.Equals(default)) throw new Exception("Index not found");
            Id = key;
        }
    }
}
