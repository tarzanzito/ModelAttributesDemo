
namespace ModelAttributesManager
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class ModelClassAttribute : System.Attribute
    {
        public string Id { get; private set; }

        public int Version { get; set; } //REF011

        public ModelClassAttribute(string id)
        {
            this.Id = id;
            this.Version = 1;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class ModelFieldAttribute : System.Attribute
    {
        public string Id { get; private set; }
        public int Order { get; set; }  //REF010
        public int Size { get; private set; }

        public ModelFieldAttribute(string id)
        {
            this.Id = id;
            this.Order = 1;
            this.Size = 0;
        }

        public ModelFieldAttribute(string id, int order)
        {
            this.Id = id;
            this.Order = order;
            this.Size = 0;
        }

        public ModelFieldAttribute(string id, int order, int sise)
        {
            this.Id = id;
            this.Order = order;
            this.Size = sise;
        }
    }
}
