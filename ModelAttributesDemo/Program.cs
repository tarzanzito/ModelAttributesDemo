using System;
using System.Collections.Generic;
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

            //2-Show 'ModelClassAttribute' informations
            var moldelHelperFieldInfoArray = modelHelper.ModelHelperFieldInfoArray;
            
            string modelId = modelHelper.ModelId;
            int modelVersion = modelHelper.ModelVersion;
            ModelClassAttributeType modelType = modelHelper.ModelType;
            string className = nameof(ModelExample);


            //3-Write To Console
            System.Console.WriteLine("className=" + className);
            System.Console.WriteLine("modelId=" + modelId);
            System.Console.WriteLine("modelVersion=" + modelVersion.ToString());
            System.Console.WriteLine("modelType=" + modelType);

      ////////////////////////      moldelHelperFieldInfoList.Add()
            //4-Show 'ModelFieldAttribute' information (list fields)
            foreach (ModelHelperFieldInfo item in moldelHelperFieldInfoArray)
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

            //5.1-static examples
            ModelExample newModel2 = ModelHelper<ModelExample>.GetNewModelInstanceS(); // REF002
            //5.2-static but stupid (passing Type)
            var newModel3 = ModelHelper<ModelExample>.GetNewModelInstanceS(typeof(ModelExample)); //REF003

            ////////////////////////////
            //6-Get Attributes Fields //
            ////////////////////////////
            ModelHelperFieldInfo modelHelperFieldInfo1 = modelHelper.GetFieldAttributeById("Field5");
            ModelHelperFieldInfo modelHelperFieldInfo2 = modelHelper.GetFieldAttributeByName("echo");
        }
    }
}
