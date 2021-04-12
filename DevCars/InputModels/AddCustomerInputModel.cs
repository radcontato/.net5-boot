using System;

namespace DevCars.InputModels
{
    public class AddCustomerInputModel
    {
        public string FullName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
