

namespace ModelAttributesManager
{         
    [ModelClassAttribute("teste1", Version = 2)]  //REF011
    internal class ModelExample //: ModelBase
    {
        [ModelFieldAttribute("Field1", 1, 20)]
        public string alfa;

        [ModelFieldAttribute("Field2", 2, 50)]
        public string bravo;

        [ModelFieldAttribute("Field3", 3)]
        public string charley;

        public string delta;

        [ModelFieldAttribute("Field5", 4)]
        public int echo;

        [ModelFieldAttribute("Field6", Index = 5)]  //REF010
        public string foxtrot;

        [ModelFieldAttribute("Field7", 6, 20)]  
        public string Golf { get; set; }

        private string _juliette; //private - it doesn't make sense, VS should alert

        [ModelFieldAttribute("Field8", Index = 7)]
        public string Juliette { get { return _juliette; } set { _juliette = value; } }
    }
}
