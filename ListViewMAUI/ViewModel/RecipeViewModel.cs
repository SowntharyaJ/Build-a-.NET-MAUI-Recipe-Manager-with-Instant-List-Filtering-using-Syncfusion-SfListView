using Syncfusion.Maui.DataForm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ListViewMAUI
{
    public class RecipeViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Recipe> recipes;
        private Recipe selectedRecipe;
        private bool isReadOnly;        

        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                isReadOnly = value;

                OnPropertyChanged(nameof(IsReadOnly));
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsVisible
        {
            get
            {
                return !IsReadOnly;
            }
        }

        public ObservableCollection<Recipe> Recipes
        {
            get => recipes;
            set
            {
                if (recipes != value)
                {
                    recipes = value;
                    OnPropertyChanged();
                }
            }
        }

        public Recipe SelectedRecipe
        {
            get => selectedRecipe;
            set
            {
                if (selectedRecipe != value)
                {
                    selectedRecipe = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddRecipeCommand { get; }
        public ICommand EditRecipeCommand { get; }
        public ICommand DeleteRecipeCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand RecipeTappedCommand { get; }
        public RecipeViewModel()
        {
            Recipes = new ObservableCollection<Recipe>();
            PopulateRecipes();
            RecipeTappedCommand = new Command<object>(async (recipe) => await OnRecipeTapped(recipe));
            AddRecipeCommand = new Command(OnAddRecipe);
            EditRecipeCommand = new Command<Recipe>(OnEditRecipe);
            DeleteRecipeCommand = new Command<Recipe>(OnDeleteRecipe);
            SaveCommand = new Command<object>(OnSave);
            CancelCommand = new Command(async () => await OnCancel());
        }

        private async void OnAddRecipe()
        {
            SelectedRecipe = new Recipe();
            IsReadOnly = false;
            await App.Current.MainPage.Navigation.PushAsync(new RecipeDetailPage(this));
        }

        private async void OnEditRecipe(Recipe recipe)
        {
            IsReadOnly = false;            
        }

        private async void OnSave(object obj)
        {
            var dataForm = obj as SfDataForm;       
            if (dataForm.Validate())
            {

                if (!Recipes.Contains(SelectedRecipe))
                {
                    Recipes.Add(SelectedRecipe);
                }
                dataForm.Commit();
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async Task OnCancel()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnDeleteRecipe(Recipe recipe)
        {
            Recipes.Remove(SelectedRecipe);
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async Task OnRecipeTapped(object eventArgs)
        {
            SelectedRecipe = (eventArgs as Syncfusion.Maui.ListView.ItemTappedEventArgs).DataItem as Recipe;
            if (SelectedRecipe != null)
            {
                IsReadOnly = true;
                await Application.Current.MainPage.Navigation.PushAsync(new RecipeDetailPage(this));
            }
        }

        private void PopulateRecipes()
        {
            // Add sample data
            Recipes.Add(new Recipe { Name = "Pancakes", Description = "Fluffy and light, these classic American-style pancakes are a breakfast favorite, perfect for a sweet start to your day.", PreprationTime="15 min", Ingredients = "1 cup all-purpose flour\n2 tbsp sugar\n1 tsp baking powder\n1/2 tsp salt\n1 egg\n1 cup milk\n2 tbsp melted butter", Instructions = "1. In a large bowl, whisk together flour, sugar, baking powder, and salt.\n2. In another bowl, beat the egg and then whisk in milk and melted butter.\n3. Pour the wet ingredients into the dry ingredients and stir until just combined.\n4. Heat a lightly oiled griddle or frying pan over medium-high heat. Pour or scoop the batter onto the griddle, using approximately 1/4 cup for each pancake. Cook until bubbles appear on the surface, then flip and cook until golden brown.", Image = "pancakes.jpg" });
            Recipes.Add(new Recipe { Name = "Spaghetti", Description = "Classic Italian pasta dish with rich meat sauce.", PreprationTime = "30 min", Ingredients = "1 lb spaghetti\n2 tbsp olive oil\n1 lb ground beef\n1 jar of marinara sauce\n1/2 cup grated Parmesan cheese", Instructions = "1. Cook the spaghetti according to package directions.\n2. While the pasta is cooking, heat the olive oil in a large skillet over medium heat. Add the ground beef and cook until browned.\n3. Drain any excess fat, then stir in the marinara sauce and simmer for 10 minutes.\n4. Serve the sauce over the cooked spaghetti and top with Parmesan cheese.", Image = "spaghetti.jpg" });
            Recipes.Add(new Recipe { Name = "Tacos", Description = "Delicious Mexican street food with various fillings.", PreprationTime = "20 min", Ingredients = "1 lb ground beef\n1 packet taco seasoning\n12 taco shells\n1 cup shredded lettuce\n1 cup diced tomatoes\n1 cup shredded cheddar cheese", Instructions = "1. In a skillet, cook the ground beef over medium heat until browned. Drain the fat.\n2. Stir in the taco seasoning and 1/4 cup of water. Simmer for 5 minutes.\n3. Warm the taco shells according to package directions.\n4. Assemble the tacos with the beef, lettuce, tomatoes, and cheese.", Image = "tacos.jpg" });
            Recipes.Add(new Recipe { Name = "Salad", Description = "Healthy and refreshing mix of fresh vegetables.", PreprationTime = "15 min", Ingredients = "1 head of romaine lettuce\n2 large tomatoes\n1 cucumber\n1/2 red onion\n1/4 cup olive oil\n2 tbsp red wine vinegar\n1 tsp Dijon mustard\nsalt and pepper to taste", Instructions = "1. Chop the lettuce, tomatoes, cucumber, and onion and place them in a large bowl.\n2. In a small bowl, whisk together the olive oil, red wine vinegar, Dijon mustard, salt, and pepper.\n3. Pour the dressing over the vegetables and toss to combine.", Image = "salad.jpg" });
            Recipes.Add(new Recipe { Name = "Pizza", Description = "Classic Italian dish with various toppings.", PreprationTime = "25 min", Ingredients = "1 pre-made pizza dough\n1/2 cup pizza sauce\n1 cup shredded mozzarella cheese\nyour favorite toppings (pepperoni, mushrooms, onions, etc.)", Instructions = "1. Preheat your oven to 425°F (220°C).\n2. Roll out the pizza dough on a lightly floured surface.\n3. Spread the pizza sauce evenly over the dough, leaving a small border for the crust.\n4. Sprinkle the mozzarella cheese over the sauce and add your favorite toppings.\n5. Bake for 12-15 minutes, or until the crust is golden and the cheese is bubbly.", Image = "pizza.jpg" });
            Recipes.Add(new Recipe { Name = "Cheese burger", Description = "Classic American burger with melted cheese.", PreprationTime = "20 min", Ingredients = "1 lb ground beef\n4 hamburger buns\n4 slices of cheddar cheese\nlettuce\ntomato\nonion\npickles\nketchup\nmustard", Instructions = "1. Divide the ground beef into 4 equal patties.\n2. Grill or pan-fry the patties to your desired doneness.\n3. Place a slice of cheese on each patty during the last minute of cooking.\n4. Toast the buns and assemble the burgers with your favorite toppings.", Image = "cheese_burger.jpg" });
            Recipes.Add(new Recipe { Name = "French Toast", Description = "Sweet and savory breakfast or brunch item.", PreprationTime = "15 min", Ingredients = "4 slices of bread\n2 eggs\n1/4 cup milk\n1 tsp cinnamon\n1 tbsp butter", Instructions = "1. In a shallow dish, whisk together the eggs, milk, and cinnamon.\n2. Dip each slice of bread into the egg mixture, coating both sides.\n3. Melt the butter in a skillet over medium heat and cook the bread until golden brown on both sides.", Image = "french_toast.jpg" });
            Recipes.Add(new Recipe { Name = "Chicken Soup", Description = "Comforting and nutritious soup with chicken and vegetables.", PreprationTime = "40 min", Ingredients = "1 lb boneless, skinless chicken breasts\n8 cups chicken broth\n1 cup chopped carrots\n1 cup chopped celery\n1 onion, chopped\n1 cup egg noodles", Instructions = "1. In a large pot, combine the chicken, broth, carrots, celery, and onion.\n2. Bring to a boil, then reduce heat and simmer for 20 minutes, or until the chicken is cooked through.\n3. Remove the chicken from the pot and shred it. Return the shredded chicken to the pot.\n4. Stir in the egg noodles and cook for another 5-7 minutes, until the noodles are tender.", Image = "chicken_soup.jpg" });
            Recipes.Add(new Recipe { Name = "Grilled Cheese", Description = "Simple and delicious classic comfort food.", PreprationTime = "10 min", Ingredients = "2 slices of bread\n2 slices of your favorite cheese\n1 tbsp butter", Instructions = "1. Butter one side of each slice of bread.\n2. Place one slice of bread, butter-side down, in a skillet over medium heat. Top with the cheese and the other slice of bread, butter-side up.\n3. Grill for 3-4 minutes per side, until the bread is golden brown and the cheese is melted.", Image = "grilled_cheese.jpg" });
            Recipes.Add(new Recipe { Name = "Omelette", Description = "Versatile egg dish with various fillings.", PreprationTime = "10 min", Ingredients = "2 eggs\n1/4 cup milk\n1/4 cup shredded cheese\nyour favorite fillings (diced ham, bell peppers, onions, etc.)\n1 tbsp butter", Instructions = "1. In a small bowl, whisk together the eggs and milk.\n2. Melt the butter in a skillet over medium heat. Pour in the egg mixture and cook until the edges begin to set.\n3. Add the cheese and fillings to one side of the omelette and fold the other side over.\n4. Cook for another minute, until the cheese is melted.", Image = "omelette.jpg" });
            Recipes.Add(new Recipe { Name = "Sushi", Description = "Traditional Japanese dish with rice, seafood, and vegetables.", PreprationTime = "45 min", Ingredients = "2 cups sushi rice\n1/4 cup rice vinegar\n4 sheets of nori (seaweed)\n1/2 lb sushi-grade fish, sliced\n1 avocado, sliced", Instructions = "1. Cook the sushi rice according to package directions.\n2. While the rice is still warm, stir in the rice vinegar.\n3. Lay a sheet of nori on a bamboo rolling mat. Spread a thin layer of rice over the nori, leaving a small border at the top.\n4. Arrange the fish and avocado in a line across the center of the rice.\n5. Roll the sushi tightly and slice into bite-sized pieces.", Image = "sushi.jpg" });
            Recipes.Add(new Recipe { Name = "Fried Rice", Description = "Quick and easy stir-fried rice dish.", PreprationTime = "25 min", Ingredients = "3 cups cooked rice\n2 tbsp soy sauce\n1 tbsp sesame oil\n1 cup frozen peas and carrots\n1 onion, diced\n2 eggs, beaten", Instructions = "1. Heat the sesame oil in a large skillet or wok over medium-high heat.\n2. Add the onion and cook until softened.\n3. Stir in the peas and carrots and cook until heated through.\n4. Push the vegetables to one side of the skillet and pour the beaten eggs on the other side. Scramble the eggs and then mix them with the vegetables.\n5. Stir in the rice and soy sauce and cook until heated through.", Image = "fried_rice.jpg" });
            Recipes.Add(new Recipe { Name = "Ramen", Description = "Japanese noodle soup with rich broth and toppings.", PreprationTime = "20 min", Ingredients = "1 package of ramen noodles\n2 cups of chicken or vegetable broth\n1 soft-boiled egg\nsliced green onions\na few slices of cooked pork or chicken", Instructions = "1. Cook the ramen noodles according to package directions.\n2. While the noodles are cooking, heat the broth in a small pot.\n3. Drain the noodles and place them in a bowl. Pour the hot broth over the noodles.\n4. Top with the soft-boiled egg, green onions, and cooked pork or chicken.", Image = "ramen.jpg" });
            Recipes.Add(new Recipe { Name = "Mashed Potatoes", Description = "Creamy and fluffy side dish.", PreprationTime = "20 min", Ingredients = "2 lbs potatoes, peeled and cubed\n1/2 cup milk\n1/4 cup butter\nsalt and pepper to taste", Instructions = "1. Place the potatoes in a large pot and cover with water. Bring to a boil and cook for 15-20 minutes, or until tender.\n2. Drain the potatoes and return them to the pot. Mash the potatoes with a potato masher or electric mixer.\n3. Stir in the milk, butter, salt, and pepper until smooth and creamy.", Image = "mashed_potatoes.jpg" });
            Recipes.Add(new Recipe { Name = "Steak", Description = "Juicy and tender grilled steak.", PreprationTime = "20 min", Ingredients = "1 lb beef steak (ribeye, sirloin, etc.)\n1 tbsp olive oil\nsalt and pepper to taste", Instructions = "1. Pat the steak dry with a paper towel and season generously with salt and pepper.\n2. Heat the olive oil in a skillet over high heat.\n3. Carefully place the steak in the skillet and cook for 3-5 minutes per side for medium-rare, or to your desired doneness.\n4. Let the steak rest for a few minutes before slicing and serving.", Image = "steak.jpg" });
            Recipes.Add(new Recipe { Name = "Brownies", Description = "Rich chocolatey dessert.", PreprationTime = "40 min", Ingredients = "1 cup all-purpose flour\n1 cup sugar\n1/2 cup cocoa powder\n1/2 tsp baking powder\n1/2 cup melted butter\n2 eggs", Instructions = "1. Preheat your oven to 350°F (175°C).\n2. In a large bowl, whisk together the flour, sugar, cocoa powder, and baking powder.\n3. In another bowl, whisk together the melted butter and eggs.\n4. Pour the wet ingredients into the dry ingredients and stir until just combined.\n5. Pour the batter into a greased 8x8 inch baking pan and bake for 20-25 minutes.", Image = "brownies.jpg" });
            Recipes.Add(new Recipe { Name = "Ice Cream", Description = "Classic frozen dessert.", PreprationTime = "30 min", Ingredients = "2 cups heavy cream\n1 cup whole milk\n3/4 cup sugar\n1 tsp vanilla extract", Instructions = "1. In a medium bowl, whisk together the heavy cream, milk, sugar, and vanilla extract until the sugar is dissolved.\n2. Pour the mixture into an ice cream maker and churn according to the manufacturer's instructions.", Image = "ice_cream.jpg" });
            Recipes.Add(new Recipe { Name = "Mac and Cheese", Description = "Creamy and cheesy pasta dish.", PreprationTime = "30 min", Ingredients = "1 lb elbow macaroni\n1/4 cup butter\n1/4 cup all-purpose flour\n2 cups milk\n2 cups shredded cheddar cheese", Instructions = "1. Cook the macaroni according to package directions.\n2. While the macaroni is cooking, melt the butter in a large saucepan over medium heat. Whisk in the flour and cook for 1 minute.\n3. Gradually whisk in the milk until the sauce is smooth and bubbly.\n4. Stir in the cheese until melted.\n5. Drain the macaroni and add it to the cheese sauce. Stir to combine.", Image = "mac_and_cheese.jpg" });
            Recipes.Add(new Recipe { Name = "Waffles", Description = "Crispy and fluffy breakfast favorite.", PreprationTime = "20 min", Ingredients = "2 cups all-purpose flour\n2 tbsp sugar\n1 tsp baking powder\n1/2 tsp salt\n2 eggs\n1 3/4 cups milk\n1/2 cup melted butter", Instructions = "1. In a large bowl, whisk together the flour, sugar, baking powder, and salt.\n2. In another bowl, beat the eggs and then whisk in the milk and melted butter.\n3. Pour the wet ingredients into the dry ingredients and stir until just combined.\n4. Cook the batter in a preheated waffle iron according to the manufacturer's directions.", Image = "waffles.jpg" });
            Recipes.Add(new Recipe { Name = "Cupcakes", Description = "Sweet and delicious individual cakes.", PreprationTime = "35 min", Ingredients = "1 1/2 cups all-purpose flour\n1 cup sugar\n1 tsp baking powder\n1/2 cup butter, softened\n2 eggs\n1/2 cup milk\n1 tsp vanilla extract", Instructions = "1. Preheat your oven to 350°F (175°C) and line a muffin tin with paper liners.\n2. In a large bowl, cream together the butter and sugar until light and fluffy.\n3. Beat in the eggs, one at a time, then stir in the vanilla.\n4. In another bowl, whisk together the flour and baking powder. Gradually add the dry ingredients to the wet ingredients, alternating with the milk, and mix until just combined.\n5. Fill the muffin cups about 2/3 full and bake for 18-20 minutes.", Image = "cup_cakes.jpg" });
            Recipes.Add(new Recipe { Name = "Smoothie", Description = "Healthy and refreshing blended drink.", PreprationTime = "5 min", Ingredients = "1 banana\n1 cup frozen berries\n1/2 cup yogurt\n1/2 cup milk\n1 tbsp honey", Instructions = "Combine all ingredients in a blender and blend until smooth.", Image = "smoothie.jpg" });
            Recipes.Add(new Recipe { Name = "Muffins", Description = "Moist and flavorful breakfast or snack.", PreprationTime = "30 min", Ingredients = "2 cups all-purpose flour\n3/4 cup sugar\n1 tsp baking powder\n1/2 tsp salt\n1 egg\n1 cup milk\n1/4 cup melted butter\n1 cup blueberries", Instructions = "1. Preheat your oven to 400°F (200°C) and grease a muffin tin.\n2. In a large bowl, whisk together the flour, sugar, baking powder, and salt.\n3. In another bowl, beat the egg and then whisk in the milk and melted butter.\n4. Pour the wet ingredients into the dry ingredients and stir until just combined. Gently fold in the blueberries.\n5. Fill the muffin cups about 2/3 full and bake for 20-25 minutes.", Image = "muffins.jpg" });
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
