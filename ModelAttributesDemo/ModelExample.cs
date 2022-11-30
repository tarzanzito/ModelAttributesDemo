
namespace ModelAttributesManager
{
    [ModelClassAttribute("teste1", Version = 2.1)]
    internal class ModelExample
    {
        [ModelFieldAttribute("Field1", 20)]
        string alfa;

        [ModelFieldAttribute("Field2", 50)]
        string bravo;

        [ModelFieldAttribute("Field3", 120)]
        string charley;

        string delta;


        [ModelFieldAttribute("Field3", 0)]
        int echo;
    }
}
