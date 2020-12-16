using System;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Model;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class ResourceDTO
    {
        public virtual int Id { get; set; }
        public virtual int ParentId { get; set; }
        public virtual string Application { get; set; }
        public virtual string SubApplication { get; set; }
        public virtual string Code { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? IsLeaf { get; set; }
        public virtual string Url { get; set; }
        public virtual string Icon { get; set; }
        public virtual int? Sequence { get; set; }
        public virtual string UpdateEditor { get; set; }
    }
}