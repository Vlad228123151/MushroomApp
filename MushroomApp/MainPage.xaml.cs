using MushroomApp.Models;
using MushroomApp.Services;
using MushroomApp.Views;

namespace MushroomApp;

public partial class MainPage : ContentPage
{
    private MushroomService _service = new();
    private List<Mushroom> _mushrooms = new();

    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMushrooms();
    }

    private async Task LoadMushrooms()
    {
        _mushrooms = await _service.LoadAsync();
        MushroomList.ItemsSource = null; // сброс старого источника
        MushroomList.ItemsSource = _mushrooms;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddMushroomPage(_service));
    }

    private async void OnSelected(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Mushroom;
        if (selected == null)
            return;

        await Navigation.PushAsync(new EditMushroomPage(_service, selected));
        MushroomList.SelectedItem = null; // сброс выделения
    }
}
