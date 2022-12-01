
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ModelAttributesManager
{
    internal sealed class ModelHelper<T> where T : class //, new() //REF020
    {
        #region fields

        public string ModelId { get; private set; }
        public int ModelVersion { get; private set; }
        public ModelClassAttributeType ModelType { get; private set; }
        public MoldelHelperFieldInfo[] MoldelHelperFieldInfoArray { get; private set; }

        #endregion

        #region public

        public ModelHelper<T> InspectModel()
        {
            InspecAllModel();

            return this;
        }

        public T GetNewModelInstance()
        {
            return (T)Activator.CreateInstance(typeof(T));
            //return = new T(); //REF020
        }

        public void SetModelValueByAttributeId(T model, string id, object value)
        {
            foreach (MoldelHelperFieldInfo item in MoldelHelperFieldInfoArray)
            {
                if (item.Id == id)
                {
                    SetValue(model, item.MemberInfo, value);
                    break;
                }
            }
        }

        public void SetModelValueByFieldName(T model, string name, object value)
        {
            foreach (MoldelHelperFieldInfo item in MoldelHelperFieldInfoArray)
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

        //static mode - REF002 (implies method 'InspectModel'() must be static
        public static void InspectModel2()
        {
            InspectAllModel99(typeof(T));
        }

        //Joke: only for demo, passing parameter  - REF003
        public static void InspectModel3(Type t)
        {
            InspectAllModel99(t);
        }

        private static void InspectAllModel99(Type t)
        {
            string x = t.Name;
        }

        public T GetNewInstanceModelWithParams(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        #endregion

        #region private

        private void InspecAllModel()
        {
            GetModelClassInfo();
            GetAllModelFieldInfo();
        }

        private void GetModelClassInfo()
        {
            //Type type = typeof(T);

            ModelClassAttribute modelAttr =
                    (ModelClassAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ModelClassAttribute));

            if (modelAttr == null)
                throw new Exception($"Class '{nameof(T)}' does not contains '{nameof(ModelClassAttribute)}'.");

            this.ModelId = modelAttr.Id;
            this.ModelVersion = modelAttr.Version;
            this.ModelType = modelAttr.Type;
        }

        private void GetAllModelFieldInfo()
        {
            List<MemberInfo> memberInfoList = typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.MemberType == MemberTypes.Field || p.MemberType == MemberTypes.Property).ToList(); ;

            var propsFieldList = new List<MoldelHelperFieldInfo>();

            foreach (MemberInfo item in memberInfoList)
            {
                MoldelHelperFieldInfo fieldInfo = GetMoldelFieldInfo(item);
                if (fieldInfo != null)
                    propsFieldList.Add(fieldInfo);
            }

            if (MoldelHelperFieldInfoArray.Length == 0)
                throw new Exception($"Class '{typeof(T).Name}' does not contains any field with'{nameof(ModelFieldAttribute)}'.");
        }

        private MoldelHelperFieldInfo GetMoldelFieldInfo(MemberInfo memberInfo)
        {
            ModelFieldAttribute fieldAttr = (ModelFieldAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ModelFieldAttribute));

            if (fieldAttr == null)
                return null;

            //MemberTypes memberType = memberInfo.MemberType;
            //string name = memberInfo.Name;
            //Type type = memberInfo.GetType();
            //string fieldType = memberInfo.FieldType.Name;

            //string id = fieldAttr.Id;
            //int index = fieldAttr.Index;
            //int size = fieldAttr.Size;
#if DEBUG
            Console.WriteLine(memberInfo.Name + "--" + memberInfo.MemberType);
#endif
            return new MoldelHelperFieldInfo(fieldAttr.Id, fieldAttr.Index, fieldAttr.Size, memberInfo);
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

        #region OLDS

        private void GetAllModelFieldInfoOLD() //Get Properties and GetFields separated
        {
            //Get Properties
            PropertyInfo[] propertyArray = typeof(T).GetProperties(
              BindingFlags.Public | BindingFlags.Instance);

            //Get Fields
            FieldInfo[] fieldArray = typeof(T).GetFields(
                BindingFlags.Public | BindingFlags.Instance);

            //string[] nameArray = Array.ConvertAll<FieldInfo, string>(fieldArray,
            //          delegate (FieldInfo field) { return field.Name; });

            var propsFieldList = new List<MoldelHelperFieldInfo>();

            foreach (FieldInfo item in fieldArray)
            {
                MoldelHelperFieldInfo fieldInfo = GetMoldelFieldInfo(item);
                if (fieldInfo != null)
                    propsFieldList.Add(fieldInfo);
            }

            foreach (PropertyInfo item in propertyArray)
            {
                MoldelHelperFieldInfo fieldInfo = GetMoldelFieldInfo(item);
                if (fieldInfo != null)
                    propsFieldList.Add(fieldInfo);
            }

            MoldelHelperFieldInfoArray = propsFieldList.ToArray();

            if (MoldelHelperFieldInfoArray.Length == 0)
                throw new Exception($"Class '{typeof(T).Name}' does not contains any field with'{nameof(ModelFieldAttribute)}'.");
        }

        #endregion
    }
}



