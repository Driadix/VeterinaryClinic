using System.Windows;
using PetModel = Core.Models.Pet;

namespace Client
{
    public partial class PetEditorWindow : Window
    {
        public PetModel Pet { get; private set; }

        public PetEditorWindow(int ownerClientId, PetModel? pet = null)
        {
            InitializeComponent();

            if (pet != null)
            {
                Pet = pet;
                Title = "Редактирование питомца";
                NameTextBox.Text = pet.Name;
                SpeciesTextBox.Text = pet.Species;
                BreedTextBox.Text = pet.Breed;
            }
            else
            {
                Pet = new PetModel { ClientId = ownerClientId };
                Title = "Добавление нового питомца";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Pet.Name = NameTextBox.Text;
            Pet.Species = SpeciesTextBox.Text;
            Pet.Breed = BreedTextBox.Text;

            DialogResult = true;
        }
    }
}