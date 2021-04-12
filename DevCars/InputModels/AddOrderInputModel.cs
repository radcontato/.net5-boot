using System.Collections.Generic;

namespace DevCars.InputModels
{
    public class AddOrderInputModel
    {
        public int IdCar { get; set; }
        public int IdCustomer { get; set; }
        public List<ExtraItemInputMOdel> ExtraItems { get; set; }
    }

    public class ExtraItemInputMOdel
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

}
