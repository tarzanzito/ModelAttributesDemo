using System;
using System.Reflection;

namespace ModelAttributesManager
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var instance = new ModelHelper<ModelExample>();
            instance.InspectModelFields();

            //var ins = new ModelHelper<ModelExample>().InspectModelFields();

            MoldelHelperProperties[] props = instance.propsFieldArray;
            string modelName = instance.ModelName;
            double modelVersion = instance.ModelVersion;
            
            string className = nameof(ModelExample);



            System.Console.WriteLine("className=" + className);
            System.Console.WriteLine("modelName=" + modelName);
            System.Console.WriteLine("modelVersion=" + modelVersion.ToString());



            foreach (MoldelHelperProperties item in props)
            {
                string name = item.Name;
                double version = item.Version;
                int size = item.Size;
                FieldInfo info = item.FieldInfo;


                System.Console.WriteLine("-------------------------");
                System.Console.WriteLine("Name=" + name);
                System.Console.WriteLine("Version=" + version.ToString());
                System.Console.WriteLine("Size=" + size.ToString());
                System.Console.WriteLine("Type=" + info.FieldType);
            }

            ////////////////////////////////////////////////////

            //static
            ModelHelper<ModelExample>.InspectModelFields2(); // REF002

            //static but stupid
            ModelHelper<ModelExample>.InspectModelFields3(typeof(ModelExample)); //REF003

        }
    }
}
