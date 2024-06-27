using System.Collections.Generic;
using System.Windows;
namespace RecipeApp
{
    public partial class ScaleRecipeWindow : Window
    {
        private Recipe recipe;
        public ScaleRecipeWindow(Recipe recipe)
        {
            InitializeComponent();
            this.recipe = recipe;
            ScaleFactorComboBox.ItemsSource = new List<double> { 0.5, 2, 3 };
            ScaleFactorComboBox.SelectedIndex = 0;
        }
        private void Scale_Click(object sender, RoutedEventArgs e)
        {
            if (ScaleFactorComboBox.SelectedItem is double factor)
            {
                recipe.ScaleRecipe(factor);
                Close();
            }
            else
            {
                MessageBox.Show("Please select a scaling factor.");
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
