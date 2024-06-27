using System.ComponentModel;
namespace RecipeApp
{
    public class Ingredient : INotifyPropertyChanged
    {
        private double _quantity;
        public string Name { get; set; }
        public double Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public string Unit { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }
        public double OriginalQuantity { get; private set; }
        public Ingredient(string name, double quantity, string unit, int calories,
       string foodGroup)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
            OriginalQuantity = quantity;
        }
        // Additional constructor to set OriginalQuantity explicitly
        public Ingredient(string name, double quantity, string unit, int calories,
       string foodGroup, double originalQuantity)
        {
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Calories = calories;
            FoodGroup = foodGroup;
            OriginalQuantity = originalQuantity;
        }
        public override string ToString()
        {
            return $"{Quantity} {Unit} of {Name} ({Calories} calories, { FoodGroup})";
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new
           PropertyChangedEventArgs(propertyName));
        }
    }
}