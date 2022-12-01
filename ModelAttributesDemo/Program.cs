using System;
using System.Reflection;

namespace ModelAttributesManager
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //////////////
            //1-Inspect //
            //////////////
            
            //1.1-instance
            var modelHelper = new ModelHelper<ModelExample>();
            modelHelper.InspectModel();
            //or
            //var instance = new ModelHelper<ModelExample>().InspectModelFields();

            //1.2-static
            ModelHelper<ModelExample>.InspectModel2(); // REF002
            //1.3-static but stupid (demo passing parameter)
            ModelHelper<ModelExample>.InspectModel3(typeof(ModelExample)); //REF003


            //2-Show 'ModelClassAttribute' informations
            MoldelHelperFieldInfo[] moldelHelperFieldInfoArray = modelHelper.MoldelHelperFieldInfoArray;
            string modelId = modelHelper.ModelId;
            int modelVersion = modelHelper.ModelVersion;
            ModelClassAttributeType modelType = modelHelper.ModelType;
            string className = nameof(ModelExample);


            //3-Write To Console
            System.Console.WriteLine("className=" + className);
            System.Console.WriteLine("modelId=" + modelId);
            System.Console.WriteLine("modelVersion=" + modelVersion.ToString());
            System.Console.WriteLine("modelType=" + modelType);


            //4-Show 'ModelFieldAttribute' information (array fields)
            foreach (MoldelHelperFieldInfo item in moldelHelperFieldInfoArray)
            {
                //41.1-Show 'ModelFieldAttribute' informations
                string id = item.Id;
                int index = item.Index;
                int size = item.Size;
                MemberInfo info = item.MemberInfo;

                //4.2-Write To Console
                System.Console.WriteLine("-------------------------");
                System.Console.WriteLine("Id=" + id);
                System.Console.WriteLine("Index=" + index.ToString());
                System.Console.WriteLine("Size=" + size.ToString());
                System.Console.WriteLine("Name=" + info.Name);
                System.Console.WriteLine("Type=" + info.MemberType);
            }

            ///////////////////////////
            //5-Set Values In Fields //
            ///////////////////////////
            var newModel = modelHelper.GetNewModelInstance();

            modelHelper.SetModelValueByAttributeId(newModel, "Field1", "abcd");
            modelHelper.SetModelValueByFieldName(newModel, "Golf", "zulus");

            modelHelper.SetModelValueByAttributeId(newModel, "Field5", 30);
            modelHelper.SetModelValueByFieldName(newModel, "echo", 25);
        }
    }
}
