
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ListViewMAUI
{
    public class Recipe : INotifyPropertyChanged
    {
        private string name;
        private string? ingredients;
        private string? instructions;
        private string? image = "emptyimage.png";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name should not be empty")]
        [StringLength(15, ErrorMessage = "Name should not exceed 15 characters")]
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Ingredients should not be empty")]        
        [DataType(DataType.MultilineText)]
        public string Ingredients
        {
            get => ingredients;
            set
            {
                if (ingredients != value)
                {
                    ingredients = value;
                    OnPropertyChanged();
                }
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Instructions should not be empty")]
        [DataType(DataType.MultilineText)]
        public string Instructions
        {
            get => instructions;
            set
            {
                if (instructions != value)
                {
                    instructions = value;
                    OnPropertyChanged();
                }
            }
        }

        [Display(AutoGenerateField = false)]
        public string Image
        {
            get => image;
            set
            {
                if (image != value)
                {
                    image = value;
                    OnPropertyChanged();
                }
            }
        }

        private string description;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description should not be empty")]
        [DataType(DataType.MultilineText)]
        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string preprationTime;

        public string PreprationTime
        {
            get => preprationTime;
            set
            {
                if (preprationTime != value)
                {
                    preprationTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
