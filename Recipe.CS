using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
namespace RecipeApp
{
    public class Recipe : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public int TotalCalories { get; private set; }
        public Recipe(string name, List<Ingredient> ingredients, List<string> steps)
        {
            Name = name;
            Ingredients = new List<Ingredient>();
            foreach (var ingredient in ingredients)
            {
                Ingredients.Add(new Ingredient(ingredient.Name, ingredient.Quantity,
               ingredient.Unit, ingredient.Calories, ingredient.FoodGroup,
               ingredient.OriginalQuantity));
            }
            Steps = steps;
            CalculateTotalCalories();
        }
        public void CalculateTotalCalories()
        {
            TotalCalories = Ingredients.Sum(i => i.Calories);
        }
        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
            CalculateTotalCalories();
        }
        public void ResetRecipeQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
            CalculateTotalCalories();
        }
        public void ClearRecipe()
        {
            Ingredients.Clear();
            Steps.Clear();
            TotalCalories = 0;
        }
        public override string ToString()
        {
            return Name;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new
           PropertyChangedEventArgs(propertyName));
        }
    }
}