
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace ModelAttributesManager
{
    internal sealed class ModelHelper<T> where T : class //, new() //REF020
    //internal sealed class ModelHelper<T> where T : ModelBase     //REF030 - all Model are childdren of ModelBase
    {
        #region fields

        public string ModelId { get; private set; }
        public int ModelVersion { get; private set; }
        public ModelClassAttributeType ModelType { get; private set; }

        //in theory should not be public
        public IEnumerable<ModelHelperFieldInfo> ModelHelperFieldInfoArray { get; private set; }
        //private IEnumerable<ModelHelperFieldInfo> ModelHelperFieldInfoArray;

        #endregion

        #region Constructors
        public ModelHelper()
        {
            InspecAllModel(); //REF40 - final remove - public ModelHelper<T> InspectModel()
        }

        #endregion

        #region public

        public T GetNewModelInstance()
        {
            return (T)Activator.CreateInstance(typeof(T));
            //return = new T(); //REF020 
        }

        public void SetModelValueByAttributeId(T model, string id, object value)
        {
            foreach (ModelHelperFieldInfo item in this.ModelHelperFieldInfoArray)
            {        
                if (item.Id == id)
                {
                    SetFieldValue(model, item.MemberInfo, value);
                    return;
                }
            }

            throw new Exception($"Field attribute Id:'{id}' not found in ' {model.GetType().FullName}'.");
        }

        public void SetModelValueByFieldName(T model, string name, object value)
        {
            foreach (ModelHelperFieldInfo item in this.ModelHelperFieldInfoArray)
            {
                if (item.MemberInfo.Name == name)
                {
                    SetFieldValue(model, item.MemberInfo, value);
                    return;
                }
            }

            throw new Exception($"Field Name:'{name}' not found in ' {model.GetType().FullName}'.");
        }

        public ModelHelperFieldInfo GetFieldAttributeByName(string name)
        {
            foreach (ModelHelperFieldInfo item in this.ModelHelperFieldInfoArray)
            {
                if (item.MemberInfo.Name == name)
                    return item;
            }

            throw new Exception($"Field Name:'{name}' not found.");
        }

        public ModelHelperFieldInfo GetFieldAttributeById(string id)
        {
            foreach (ModelHelperFieldInfo item in this.ModelHelperFieldInfoArray)
            {
                if (item.Id == id)
                    return item;
            }

            throw new Exception($"Field Attribute Id:'{id}' not found.");
        }
        #endregion

        #region demos

        public ModelHelper<T> InspectModel() //REF40 - this method has already been executed by the constructor
        {
            InspecAllModel();

            return this;
        }

        //Static - REF002
        public static T GetNewModelInstanceS()
        {
            return GetNewModelInstanceS(typeof(T));
        }

        //Static - REF003 - passing Type - Joke: (only for demo because class know <T>
        public static T GetNewModelInstanceS(Type t)
        {
            return (T) Activator.CreateInstance(t);
        }

        //passing parameters for constructor 
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
            var memberInfoList = typeof(T).GetMembers(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.MemberType == MemberTypes.Field || p.MemberType == MemberTypes.Property).ToList(); ;

            var modelHelperFieldInfoList = new List<ModelHelperFieldInfo>();

            foreach (MemberInfo item in memberInfoList)
            {
                ModelHelperFieldInfo fieldInfo = GetMoldelFieldInfo(item);
                if (fieldInfo != null)
                    modelHelperFieldInfoList.Add(fieldInfo);
            }

            if (modelHelperFieldInfoList.Count == 0)
                throw new Exception($"Class '{typeof(T).Name}' does not contains any field with'{nameof(ModelFieldAttribute)}'.");

            this.ModelHelperFieldInfoArray = modelHelperFieldInfoList.ToArray();
        }

        private ModelHelperFieldInfo GetMoldelFieldInfo(MemberInfo memberInfo)
        {
            ModelFieldAttribute fieldAttr = (ModelFieldAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ModelFieldAttribute));

            if (fieldAttr == null)
                return null;
#if DEBUG
            Console.WriteLine(memberInfo.Name + "--" + memberInfo.MemberType);
#endif
            return new ModelHelperFieldInfo(fieldAttr.Id,
                                            memberInfo.Name,
                                            fieldAttr.Index,
                                            fieldAttr.Size,
                                            memberInfo);
        }

        private void SetFieldValue(T model, MemberInfo memberInfo, object value)
        {
            //-Get member by Name
            //MemberInfo[] memberArray = model.GetType().GetMember(item.Name);
            //MemberInfo member1 = memberArray[0];
            //-

            switch (memberInfo.MemberType)
            {
                case MemberTypes.Field:
                    FieldInfo field = (FieldInfo)memberInfo;
                    if (field.FieldType != value.GetType())
                        throw new Exception($"Error: Field Type is '{field.FieldType.FullName}' and value is '{value.GetType().FullName}'.");
                    
                    field.SetValue(model, value);
                    break;
                case MemberTypes.Property:
                    PropertyInfo prop = (PropertyInfo)memberInfo;
                    if (prop.PropertyType != value.GetType())
                        throw new Exception($"Error: Property Type is '{prop.PropertyType.FullName}' and value is '{value.GetType().FullName}'.");

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

            var modelHelperFieldInfoList = new List<ModelHelperFieldInfo>();

            foreach (FieldInfo item in fieldArray)
            {
                ModelHelperFieldInfo fieldInfo = GetMoldelFieldInfo(item);
                if (fieldInfo != null)
                    modelHelperFieldInfoList.Add(fieldInfo);
            }

            foreach (PropertyInfo item in propertyArray)
            {
                ModelHelperFieldInfo fieldInfo = GetMoldelFieldInfo(item);
                if (fieldInfo != null)
                    modelHelperFieldInfoList.Add(fieldInfo);
            }
           

            if (modelHelperFieldInfoList.Count == 0)
                throw new Exception($"Class '{typeof(T).Name}' does not contains any field with'{nameof(ModelFieldAttribute)}'.");

            this.ModelHelperFieldInfoArray = modelHelperFieldInfoList.ToArray();
        }

        #endregion
    }
}



