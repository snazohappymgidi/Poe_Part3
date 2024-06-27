using System.Windows;
namespace RecipeApp
{
    public partial class InputBox : Window
    {
        public string InputText { get; private set; }
        public InputBox(string prompt, string title = "Input")
        {
            InitializeComponent();
            this.Title = title;
            PromptTextBlock.Text = prompt;
        }
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            this.InputText = InputTextBox.Text;
            this.DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
