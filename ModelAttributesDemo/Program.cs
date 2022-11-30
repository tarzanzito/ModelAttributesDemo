using System;
using System.Reflection;

namespace ModelAttributesManager
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //1-Inspect

            //1.1-instance
            var instance = new ModelHelper<ModelExample>();
            instance.InspectModelFields();
            //or
            //var ins = new ModelHelper<ModelExample>().InspectModelFields();

            //1.2-static
            ModelHelper<ModelExample>.InspectModelFields2(); // REF002
            //1.3-static but stupid
            ModelHelper<ModelExample>.InspectModelFields3(typeof(ModelExample)); //REF003

            //2-Show 'ModelClassAttribute' informations
            MoldelHelperInfo[] infoArray = instance.MoldelHelperInfoArray;
            string modelId = instance.ModelId;
            double modelVersion = instance.ModelVersion;
            string className = nameof(ModelExample);

            //To Console
            System.Console.WriteLine("className=" + className);
            System.Console.WriteLine("modelId=" + modelId);
            System.Console.WriteLine("modelVersion=" + modelVersion.ToString());

            //3-Show 'ModelFieldAttribute' information (array fields)
            foreach (MoldelHelperInfo item in infoArray)
            {
                string id = item.Id;
                int order = item.Order;
                int size = item.Size;
                MemberInfo info = item.MemberInfo;

                //To Console
                System.Console.WriteLine("-------------------------");
                System.Console.WriteLine("Id=" + id);
                System.Console.WriteLine("Order=" + order.ToString());
                System.Console.WriteLine("Size=" + size.ToString());
                System.Console.WriteLine("Name=" + info.Name);
                System.Console.WriteLine("Type=" + info.MemberType);
            }

            //4-Set value fields
            var model = instance.GetNewInstanceModel();
            instance.SetValueByAttributeId(model, "Field1", "abcd");
            instance.SetValueByFieldName(model, "Golf", "zulus");

            instance.SetValueByAttributeId(model, "Field5", 30);
            instance.SetValueByFieldName(model, "echo", 25);
        }
    }
}
