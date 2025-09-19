using Syncfusion.Maui.DataForm;

namespace ListViewMAUI;

public partial class RecipeDetailPage : ContentPage
{
	public RecipeDetailPage(RecipeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void OnGenerateDataFormItem(object sender, GenerateDataFormItemEventArgs e)
	{
		if (e.DataFormItem is DataFormMultilineItem)			
			(e.DataFormItem as DataFormMultilineItem).RowSpan = e.DataFormItem.FieldName == "Description" ? 2: 4;

        e.DataFormItem.LabelTextStyle = new DataFormTextStyle()
        {
            FontSize = 16,            
            FontAttributes = FontAttributes.Bold
        };
    }
}
