using System;
using System.Collections.Generic;
using IIMes.Infra.Privilege.Model;
using ProtoBuf;

namespace IIMes.Infra.Privilege.Interface
{
    [ProtoContract]
    public class TreeNode
    {
        [ProtoMember(1, DataFormat = DataFormat.WellKnown)]
        public virtual Resource resources { get; set; }

        [ProtoMember(2, DataFormat = DataFormat.WellKnown)]
        public virtual IList<TreeNode> Nodes { get; set; }
    }
}