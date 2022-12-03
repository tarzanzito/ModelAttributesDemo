
using System;
using System.Reflection;

namespace ModelAttributesManager
{
    internal sealed class ModelHelperFieldInfo
    {
        public string Id { get; private set; }
        public int Index { get; private set; }
        public int Size { get; private set; }
        public MemberInfo MemberInfo { get; private set; }

        public ModelHelperFieldInfo(string id, int index, int size, MemberInfo memberInfo)
        {
            this.Id= id;
            this.Index = index;
            this.Size = size;
            this.MemberInfo = memberInfo;
        }
    }
}
