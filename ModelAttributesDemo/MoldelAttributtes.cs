
namespace ModelAttributesManager
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class ModelClassAttribute : System.Attribute
    {
        public string Name { get; private set; }

        public double Version { get; set; } //ref011

        public ModelClassAttribute(string name)
        {
            this.Name = name;
            this.Version = 1.0;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Property)]
    public class ModelFieldAttribute : System.Attribute
    {
        public string Name { get; private set; }
        public double Version { get; set; }  //ref010
        
        public int Size { get; private set; }

        public ModelFieldAttribute(string name)
        {
            this.Name = name;
            this.Version = 1.0;
            this.Size = 0;
        }

        public ModelFieldAttribute(string name, int sise)
        {
            this.Name = name;
            this.Version = 1.0;
            this.Size = sise;
        }
    }
}
