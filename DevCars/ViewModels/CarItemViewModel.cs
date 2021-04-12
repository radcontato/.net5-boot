namespace DevCars.ViewModels
{
    public class CarItemViewModel
    {
        public CarItemViewModel(int id, string brand, string model, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }

        public int Id { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; private set; }
    }
}
