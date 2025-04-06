using MushroomApp.Models;
using MushroomApp.Services;

namespace MushroomApp.Views;

public partial class AddMushroomPage : ContentPage
{
    private MushroomService _service;

    public AddMushroomPage(MushroomService service)
    {
        InitializeComponent();
        _service = service;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var mushroom = new Mushroom
        {
            Name = NameEntry.Text,
            LatinName = LatinEntry.Text,
            Description = DescEditor.Text,
            ImagePath = ImagePathEntry.Text
        };

        var list = await _service.LoadAsync();
        list.Add(mushroom);
        await _service.SaveAsync(list);

        await Navigation.PopAsync();
    }
}