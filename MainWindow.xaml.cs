using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media; 

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private List<Recipe> recipes;
        private List<Recipe> displayedRecipes; // Separate list for displayed recipes
        private readonly string filterByIngredientPlaceholder = "Filter by ingredient";
        private readonly string maxCaloriesPlaceholder = "Max calories";

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            recipes = new List<Recipe>();
            displayedRecipes = new List<Recipe>();

            // Add initial recipes or load from file/database

            FilterByFoodGroupComboBox.ItemsSource = new List<string> { "All", "Protein", "Vegetable", "Fruit", "Grain", "Dairy" , "Water" };
            FilterByFoodGroupComboBox.SelectedIndex = 0;

            SetPlaceholderText(FilterByNameTextBox, filterByIngredientPlaceholder);
            SetPlaceholderText(MaxCaloriesTextBox, maxCaloriesPlaceholder);

            RecipeListBox.ItemsSource = displayedRecipes;
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ApplyFilters_Click(object sender, RoutedEventArgs e)
        {
            string ingredientFilter = FilterByNameTextBox.Text == filterByIngredientPlaceholder ? string.Empty : FilterByNameTextBox.Text.ToLower();
            string foodGroupFilter = FilterByFoodGroupComboBox.SelectedItem.ToString();
            int maxCalories = MaxCaloriesTextBox.Text == maxCaloriesPlaceholder ? int.MaxValue : int.TryParse(MaxCaloriesTextBox.Text, out int result) ? result : int.MaxValue;

            displayedRecipes = recipes.Where(recipe =>
                (string.IsNullOrWhiteSpace(ingredientFilter) || recipe.Ingredients.Any(i => i.Name.ToLower().Contains(ingredientFilter))) &&
                (foodGroupFilter == "All" || recipe.Ingredients.Any(i => i.FoodGroup == foodGroupFilter)) &&
                recipe.TotalCalories <= maxCalories
            ).OrderBy(recipe => recipe.Name).ToList();

            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = displayedRecipes;
        }

        private void RecipeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                RecipeNameTextBlock.Text = selectedRecipe.Name;
                IngredientsListBox.ItemsSource = selectedRecipe.Ingredients.Select(i => i.ToString()).ToList();
                StepsListBox.ItemsSource = selectedRecipe.Steps.Select((step, index) => $"{index + 1}. {step}").ToList();
                TotalCaloriesTextBlock.Text = $"Total Calories: {selectedRecipe.TotalCalories}";

                if (selectedRecipe.TotalCalories > 300) // Example threshold for warning
                {
                    MessageBox.Show("Warning: This recipe exceeds 300 calories!");
                }
            }
        }

        private void DisplayRecipes_Click(object sender, RoutedEventArgs e)
        {
            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = recipes;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (textBox.Text == filterByIngredientPlaceholder || textBox.Text == maxCaloriesPlaceholder)
                {
                    textBox.Text = string.Empty;
                    textBox.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    SetPlaceholderText(textBox, textBox.Name == "FilterByNameTextBox" ? filterByIngredientPlaceholder : maxCaloriesPlaceholder);
                }
            }
        }

        private void SetPlaceholderText(TextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void AddNewRecipe_Click(object sender, RoutedEventArgs e)
        {
            var newRecipeWindow = new NewRecipeWindow(recipes);
            newRecipeWindow.ShowDialog();
            displayedRecipes = new List<Recipe>(recipes); // Update displayedRecipes after adding a new recipe

            RecipeListBox.ItemsSource = null;
            RecipeListBox.ItemsSource = displayedRecipes;

            ApplyFilters_Click(this, new RoutedEventArgs());
        }

        private void ScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                var scaleRecipeWindow = new ScaleRecipeWindow(selectedRecipe);
                scaleRecipeWindow.ShowDialog();
                RecipeListBox_SelectionChanged(this, null);
            }
            else
            {
                MessageBox.Show("Please select a recipe to scale.");
            }
        }

        private void ResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                selectedRecipe.ResetRecipeQuantities();
                RecipeListBox_SelectionChanged(this, null);
            }
            else
            {
                MessageBox.Show("Please select a recipe to reset quantities.");
            }
        }

        private void ClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                selectedRecipe.ClearRecipe();
                RecipeListBox_SelectionChanged(this, null);
            }
            else
            {
                MessageBox.Show("Please select a recipe to clear.");
            }
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeListBox.SelectedItem is Recipe selectedRecipe)
            {
                recipes.Remove(selectedRecipe);
                displayedRecipes = new List<Recipe>(recipes); // Update displayedRecipes after deletion

                RecipeListBox.ItemsSource = null;
                RecipeListBox.ItemsSource = displayedRecipes;

                RecipeNameTextBlock.Text = string.Empty;
                IngredientsListBox.ItemsSource = null;
                StepsListBox.ItemsSource = null;
                TotalCaloriesTextBlock.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Please select a recipe to delete.");
            }
        }
    }
}
