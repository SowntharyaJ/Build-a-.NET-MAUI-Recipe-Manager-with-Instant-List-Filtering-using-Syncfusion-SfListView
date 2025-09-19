using Syncfusion.Maui.ListView;

namespace ListViewMAUI
{
    public partial class ListViewFilterBehavior : Behavior<ContentPage>
    {
        private SfListView? listView;
        private Entry? filterText;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            listView = bindable.FindByName<SfListView>("listView");
            filterText = bindable.FindByName<Entry>("filterText");
            if (filterText != null)
            {
                filterText.TextChanged += OnTextChanged;
            }
            base.OnAttachedTo(bindable);
        }

        private void OnTextChanged(object? sender, TextChangedEventArgs e)
        {
            if (listView != null && listView.DataSource != null)
            {
                listView.DataSource.Filter = FilterContacts;
                listView.DataSource.RefreshFilter();
            }
            listView.RefreshView();
        }
        private bool FilterContacts(object obj)
        {
            if (filterText == null || filterText.Text == null)
                return true;

            var recipe = obj as Recipe;
            if (recipe != null && (recipe.Name.ToLower().Contains(filterText.Text.ToLower())
                || recipe.Name.ToLower().Contains(filterText.Text.ToLower())))
            {
                return true;
            }
            return false;

        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            if (filterText != null)
            {
                filterText.TextChanged -= OnTextChanged;
            }
            listView = null;
            filterText = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
