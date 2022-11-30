
namespace ModelAttributesManager
{
    [ModelClassAttribute("teste1", Version = 2)]  //REF011
    internal class ModelExample
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

        [ModelFieldAttribute("Field6", Order = 5)]   //REF010
        public string foxtrot;

        [ModelFieldAttribute("Field7", 6, 20)]  
        public string Golf { get; set; }

        private string _juliette;

        [ModelFieldAttribute("Field8", Order = 7)]
        public string Juliette { get { return _juliette; } set { _juliette = value; } }
    }
}
