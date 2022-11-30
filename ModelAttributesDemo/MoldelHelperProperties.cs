
using System;
using System.Reflection;

namespace ModelAttributesManager
{
    internal sealed class MoldelHelperProperties
    {
        public string Name { get; private set; }
        public double Version { get; private set; }
        public int Size { get; private set; }

        public FieldInfo FieldInfo { get; private set; }
        public MoldelHelperProperties(string name, double Version, int size, FieldInfo fieldInfo)
        {
            this.Name = name;
            this.Version = Version;
            this.Size = size;
            this.FieldInfo = fieldInfo;
        }
    }
}
