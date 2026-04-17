# Build-a-.NET-MAUI-Recipe-Manager-with-Instant-List-Filtering-using-Syncfusion-SfListView

Build a .NET MAUI Recipe Manager with Instant List Filtering using Syncfusion ListView.

## Sample

```xaml
<syncfusion:SfListView x:Name="listView"
                                 Grid.Row="1"
                                 ItemSize="94"
                                 ItemsSource="{Binding Recipes}"
                                 TapCommand="{Binding RecipeTappedCommand}"
                                 ItemSpacing="1">
    <syncfusion:SfListView.ItemTemplate>
        <DataTemplate>
            <Grid>
                <Grid.RowDefinitions>
                    <RowDefinition Height="*"/>
                    <RowDefinition Height="1"/>
                </Grid.RowDefinitions>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="94"/>
                    <ColumnDefinition Width="*"/>
                </Grid.ColumnDefinitions>
                <Border HorizontalOptions="Center" Grid.Row="0" Grid.Column="0" HeightRequest="72" WidthRequest="72" Padding="0" Stroke="Transparent">
                    <Border.StrokeShape>
                        <RoundRectangle CornerRadius="4"/>
                    </Border.StrokeShape>
                    <Image Source="{Binding Image}" HorizontalOptions="Center" VerticalOptions="Fill" HeightRequest="72" WidthRequest="72" Aspect="AspectFill"/>
                </Border>
                <Grid Grid.Row="0" Grid.Column="1" VerticalOptions="Center">
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto"/>
                        <RowDefinition Height="Auto"/>
                    </Grid.RowDefinitions>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="*"/>
                        <ColumnDefinition Width="60"/>
                    </Grid.ColumnDefinitions>
                    <Label Grid.Row="0" Grid.Column="0"  Text="{Binding Name}" LineBreakMode="WordWrap" FontFamily="Roboto-Medium" FontSize="{OnPlatform Android={OnIdiom Phone=15, Tablet=22}, iOS=16, WinUI=14,MacCatalyst=16}" CharacterSpacing="0.1"/>
                    <Label Grid.Row="0" Grid.Column="1" Text="{Binding PreprationTime}" FontSize="{OnPlatform Default=10,WinUI=12,MacCatalyst=12}" FontFamily="Roboto-Regular" HorizontalOptions="End" CharacterSpacing="0.15" Margin="0,0,11,0" Opacity="0.8"/>
                    <Label Grid.Row="1" Grid.Column="0" LineBreakMode="WordWrap" LineHeight="{OnPlatform iOS={OnIdiom Tablet=1.2, Default=1.025}, Default=1.2}" Text="{Binding Description}" FontFamily="Roboto-Regular" FontSize="14" CharacterSpacing="0.1" Margin="0,4,0,0" Opacity="0.8" />
                </Grid>
                <BoxView Grid.Row="1" Grid.Column="1" Color="LightGrey" HeightRequest="1" VerticalOptions="End" />
            </Grid>
        </DataTemplate>
    </syncfusion:SfListView.ItemTemplate>
</syncfusion:SfListView>
```

```c#
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
}
```

## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

