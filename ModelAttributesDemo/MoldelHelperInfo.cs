
using System;
using System.Reflection;

namespace ModelAttributesManager
{
    internal sealed class MoldelHelperInfo
    {
        public string Id { get; private set; }
        public int Order { get; private set; }
        public int Size { get; private set; }
        public MemberInfo MemberInfo { get; private set; }

        public MoldelHelperInfo(string id, int order, int size, MemberInfo memberInfo)
        {
            this.Id= id;
            this.Order = order;
            this.Size = size;
            this.MemberInfo = memberInfo;
        }
    }
}
