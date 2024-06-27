using RecipeApp;
using System.Collections.Generic;
using System.Windows;
namespace RecipeApp
{
    public partial class NewRecipeWindow : Window
    {
        private List<Recipe> recipes;
        private List<Ingredient> ingredients;
        private List<string> steps;
        private int numIngredients;
        private int numSteps;
        public NewRecipeWindow(List<Recipe> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;
            ingredients = new List<Ingredient>();
            steps = new List<string>();
            IngredientsListBox.ItemsSource = ingredients;
            StepsListBox.ItemsSource = steps;
        }
        private void SetIngredients_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumIngredientsTextBox.Text, out numIngredients) &&
           numIngredients > 0)
            {
                AddIngredientButton.IsEnabled = true;
                ingredients.Clear(); // Clear the underlying collection
                IngredientsListBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of ingredients.");
            }
        }
        private void SetSteps_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumStepsTextBox.Text, out numSteps) && numSteps > 0)
            {
                AddStepButton.IsEnabled = true;
                steps.Clear(); // Clear the underlying collection
                StepsListBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of steps.");
            }
        }
        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            if (ingredients.Count < numIngredients)
            {
                var addIngredientWindow = new AddIngredientWindow(ingredients);
                addIngredientWindow.ShowDialog();
                IngredientsListBox.Items.Refresh();
            }
            else
            {
                MessageBox.Show("You have already added the specified number of ingredients.");
            }
        }
        private void AddStep_Click(object sender, RoutedEventArgs e)
        {
            if (steps.Count < numSteps)
            {
                var inputBox = new InputBox("Enter step description:", "Add Step");
                if (inputBox.ShowDialog() == true)
                {
                    var step = inputBox.InputText;
                    if (!string.IsNullOrWhiteSpace(step))
                    {
                        steps.Add(step);
                        StepsListBox.Items.Refresh();
                    }
                }
            }
            else
            {
                MessageBox.Show("You have already added the specified number of steps.");
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var recipeName = RecipeNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.");
                return;
            }
            if (ingredients.Count < numIngredients)
            {
                MessageBox.Show($"Please add {numIngredients - ingredients.Count} more ingredient(s).");
            return;
            }
            if (steps.Count < numSteps)
            {
                MessageBox.Show($"Please add {numSteps - steps.Count} more step(s).");
            return;
            }
            var recipe = new Recipe(recipeName, new List<Ingredient>(ingredients),
           new List<string>(steps));
            recipes.Add(recipe);
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
