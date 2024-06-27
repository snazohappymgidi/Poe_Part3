using System.Collections.Generic;
using System.Windows;
namespace RecipeApp
{
    public partial class AddIngredientWindow : Window
    {
        private List<Ingredient> ingredients;
        public AddIngredientWindow(List<Ingredient> ingredients)
        {
            InitializeComponent();
            this.ingredients = ingredients;
            FoodGroupComboBox.ItemsSource = new List<string> { "Protein",
"Vegetable", "Fruit", "Grain", "Dairy" };
            FoodGroupComboBox.SelectedIndex = 0;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var name = IngredientNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Please enter an ingredient name.");
                return;
            }
            if (!double.TryParse(QuantityTextBox.Text, out double quantity) ||
           quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }
            var unit = UnitTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(unit))
            {
                MessageBox.Show("Please enter a unit of measurement.");
                return;
            }
            if (!int.TryParse(CaloriesTextBox.Text, out int calories) || calories <=
           0)
            {
                MessageBox.Show("Please enter valid calories.");
                return;
            }
            var foodGroup = FoodGroupComboBox.SelectedItem.ToString();
            var ingredient = new Ingredient(name, quantity, unit, calories,
           foodGroup);
            ingredients.Add(ingredient);
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
