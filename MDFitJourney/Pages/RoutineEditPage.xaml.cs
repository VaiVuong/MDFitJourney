using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MDFitJourney.Pages;

[QueryProperty(nameof(Day), "day")]
public partial class RoutineEditPage : ContentPage, INotifyPropertyChanged
{
    private string day;
    public string Day
    {
        get => day;
        set
        {
            day = value;
            OnPropertyChanged();
        }
    }

    public RoutineEditPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
