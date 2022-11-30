
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ModelAttributesManager
{
    internal sealed class ModelHelper<T> where T : class //REF020, new()
    {
        #region fields

        public MoldelHelperInfo[] MoldelHelperInfoArray { get; private set; }
        public string ModelId { get; private set; }
        public double ModelVersion { get; private set; }

        #endregion

        #region public

        public ModelHelper<T> InspectModelFields()
        {
            InspecAllModelFields();

            return this;
        }

        public T GetNewInstanceModel()
        {
            return (T)Activator.CreateInstance(typeof(T));
            //return = new T(); //REF020
        }

        public void SetValueByAttributeId(T model, string id, object value)
        {
            foreach (MoldelHelperInfo item in MoldelHelperInfoArray)
            {
                if (item.Id == id)
                {
                    SetValue(model, item.MemberInfo, value);
                    break;
                }
            }
        }

        public void SetValueByFieldName(T model, string name, object value)
        {
            foreach (MoldelHelperInfo item in MoldelHelperInfoArray)
            {
                if (item.MemberInfo.Name == name)
                {
                    SetValue(model, item.MemberInfo, value);
                    break;
                }
            }
        }

        #endregion

        #region demos statics 

        //static mode - REF002 (implies InspectModelFields() must be static
        public static void InspectModelFields2()
        {
            InspectAllModelFields99(typeof(T));
        }

        //Joke: only for demo, passing parameter  - REF003
        public static void InspectModelFields3(Type t)
        {
            InspectAllModelFields99(t);
        }

        private static void InspectAllModelFields99(Type t)
        {
            string x = t.Name;
        }

        public T GetNewInstanceModelWithParams(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        #endregion

        #region private

        private void InspecAllModelFields()
        {
            //Type t = typeof(T);

            //class

            ModelClassAttribute modelAttr =
                    (ModelClassAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ModelClassAttribute));

            if (modelAttr == null)
                throw new Exception($"Class '{nameof(T)}' does not contains '{nameof(ModelClassAttribute)}'.");

            ModelId = modelAttr.Id;
            ModelVersion = modelAttr.Version;

            //fields

            var propsFieldList = new List<MoldelHelperInfo>();

            List<MemberInfo> memberInfoList = typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.MemberType == MemberTypes.Field || p.MemberType == MemberTypes.Property).ToList(); ;

            foreach (MemberInfo item in memberInfoList)
            {
                MemberTypes memberType = item.MemberType;
                string name = item.Name;
                Console.WriteLine(name + "--" + memberType);

                ModelFieldAttribute fieldAttr = (ModelFieldAttribute)Attribute.GetCustomAttribute(item, typeof(ModelFieldAttribute));

                if (fieldAttr != null)
                {
                    string id = fieldAttr.Id;
                    int order = fieldAttr.Order;
                    int size = fieldAttr.Size;
                    //Type type = item.GetType();
                    var props = new MoldelHelperInfo(id, order, size, item);
                    propsFieldList.Add(props);
                }
                string nnam3e = item.Name;

            }

            if (MoldelHelperInfoArray.Length == 0)
                throw new Exception($"Class '{typeof(T).Name}' does not contains any field with'{nameof(ModelFieldAttribute)}'.");
        }

        private void SetValue(T model, MemberInfo memberInfo, object value)
        {
            //MemberInfo[] memberArray = model.GetType().GetMember(item.Name);
            //MemberInfo member1 = memberArray[0];

            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)memberInfo;
                    field.SetValue(model, value);
                    break;
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)memberInfo;
                    prop.SetValue(model, value);
                    break;
                default:
                    throw new Exception($"MemberInfo is not '{MemberTypes.Field}' or '{MemberTypes.Property}'.");
            }
        }

        #endregion

        #region OLDS tries

        private void InspecAllModelFieldsOLD()
        {
            //class

            ModelClassAttribute modelAttr =
                    (ModelClassAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ModelClassAttribute));

            ModelId = modelAttr.Id;
            ModelVersion = modelAttr.Version;

            //fields

            var propsFieldList = new List<MoldelHelperInfo>();

            PropertyInfo[] propertyArray = typeof(T).GetProperties(
              BindingFlags.Public | BindingFlags.Instance);

            FieldInfo[] fieldArray = typeof(T).GetFields(
                BindingFlags.Public | BindingFlags.Instance);

            //string[] nameArray = Array.ConvertAll<FieldInfo, string>(fieldArray,
            //          delegate (FieldInfo field) { return field.Name; });

            foreach (FieldInfo item in fieldArray)
            {
                ModelFieldAttribute fieldAttr =
                        (ModelFieldAttribute)Attribute.GetCustomAttribute(item, typeof(ModelFieldAttribute));

                if (fieldAttr != null)
                {
                    string id = fieldAttr.Id;
                    int order = fieldAttr.Order;
                    int size = fieldAttr.Size;
                    string name = item.Name;
                    //Type type = item.GetType();
                    //string fieldType = item.FieldType.Name;

                    var props = new MoldelHelperInfo(id, order, size, item);

                    propsFieldList.Add(props);
                }
            }

            foreach (PropertyInfo item in propertyArray)
            {
                ModelFieldAttribute fieldAttr =
                        (ModelFieldAttribute)Attribute.GetCustomAttribute(item, typeof(ModelFieldAttribute));

                if (fieldAttr != null)
                {
                    string id = fieldAttr.Id;
                    int order = fieldAttr.Order;
                    int size = fieldAttr.Size;
                    string name = item.Name;
                    //Type type = item.GetType();
                    //string fieldType = item.FieldType.Name;

                    var props = new MoldelHelperInfo(id, order, size, item);

                    propsFieldList.Add(props);
                }
            }

            MoldelHelperInfoArray = propsFieldList.ToArray();

            if (MoldelHelperInfoArray.Length == 0)
                throw new Exception($"Class '{typeof(T).Name}' does not contains any field with'{nameof(ModelFieldAttribute)}'.");
        }

        #endregion
    }
}



