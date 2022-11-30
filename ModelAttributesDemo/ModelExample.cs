
namespace ModelAttributesManager
{
    [ModelClassAttribute("teste1", Version = 2.1)]  //ref011
    internal class ModelExample
    {
        [ModelFieldAttribute("Field1", 20)]
        string alfa;

        [ModelFieldAttribute("Field2", 50)]
        string bravo;

        [ModelFieldAttribute("Field3", 120)]
        string charley;

        string delta;

        [ModelFieldAttribute("Field5"]
        int echo;

        [ModelFieldAttribute("Field6", Version = 2.5)]   //ref010
        string foxtrot;
    }
}
