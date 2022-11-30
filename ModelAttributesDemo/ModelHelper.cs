
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ModelAttributesManager
{
    internal sealed class ModelHelper<T> where T : class
    {
        public MoldelHelperProperties[] propsFieldArray { get; private set; }
        public string ModelName { get; private set; }
        public double ModelVersion { get; private set; }


        public ModelHelper<T> InspectModelFields()
        {
            InspecAllModelFields();

            return this;
        }

        //static mode - REF002 (implies InspectModelFields() must be static
        public static void InspectModelFields2()
        {
            InspectAllModelFields99(typeof(T));
        }

        //Joke: only for demo passing parameter  - REF003
        public static void InspectModelFields3(Type t)
        {
            InspectAllModelFields99(t);
        }

        ////////////////////

        private static void InspectAllModelFields99(Type t)
        {
            string x = t.Name;
        }

        private void InspecAllModelFields()
        {
            //Type t = typeof(T);

            ModelClassAttribute modelAttr =
                    (ModelClassAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ModelClassAttribute));

            if (modelAttr == null)
            {
                throw new Exception($"Class '{nameof(T)}' does not contains '{nameof(ModelClassAttribute)}'.");
            }
            else
            {
                ModelName = modelAttr.Name;
                ModelVersion = modelAttr.Version;

                var propsFieldList = new List<MoldelHelperProperties>();

                FieldInfo[] fieldArray = typeof(T).GetFields(
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                //string[] nameArray = Array.ConvertAll<FieldInfo, string>(fieldArray, delegate (FieldInfo field) { return field.Name; });

                foreach (FieldInfo item in fieldArray)
                {
                    ModelFieldAttribute fieldAttr =
                            (ModelFieldAttribute)Attribute.GetCustomAttribute(item, typeof(ModelFieldAttribute));

                    if (fieldAttr != null)
                    {
                        string name = fieldAttr.Name;
                        double version = fieldAttr.Version;
                        int size = fieldAttr.Size;
                        //Type type = item.GetType();

                        var props = new MoldelHelperProperties(name, version, size, item);

                        propsFieldList.Add(props);
                    }
                }
                propsFieldArray = propsFieldList.ToArray();

            }

            if (propsFieldArray.Length == 0)
                throw new Exception($"Class '{typeof(T).Name}' does not contains any field with'{nameof(ModelFieldAttribute)}'.");
        }

    }

}
