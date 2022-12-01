
namespace ModelAttributesManager
{
    internal enum ModelClassAttributeType
    {
        Walk,
        Run,
        Swim,
        FreeFallin
    }

    [System.AttributeUsage(System.AttributeTargets.Class)] // | System.AttributeTargets.Struct)]
    internal sealed class ModelClassAttribute : System.Attribute
    {
        public string Id { get; private set; }
        public int Version { get; set; } //REF011
        public ModelClassAttributeType Type { get; private set; }

        public ModelClassAttribute(string id)
        {
            this.Id = id;
            this.Type = ModelClassAttributeType.Walk;
            this.Version = 1;
        }

        public ModelClassAttribute(string id, ModelClassAttributeType type)
        {
            this.Id = id;
            Type = type;
            this.Version = 1;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    internal class ModelFieldAttribute : System.Attribute
    {
        public string Id { get; private set; }
        public int Index { get; set; }  //REF010
        public int Size { get; private set; }

        public ModelFieldAttribute(string id)
        {
            this.Id = id;
            this.Index = 1;
            this.Size = 0;
        }

        public ModelFieldAttribute(string id, int order)
        {
            this.Id = id;
            this.Index = order;
            this.Size = 0;
        }

        public ModelFieldAttribute(string id, int order, int sise)
        {
            this.Id = id;
            this.Index = order;
            this.Size = sise;
        }
    }
}
