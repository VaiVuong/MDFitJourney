using System.ComponentModel;
using System.Runtime.CompilerServices;

public class ExerciseModule : INotifyPropertyChanged
{
    private string exerciseName;
    private string sets;
    private string reps;
    private string weight;
    private bool isEditing;

    public string ExerciseName
    {
        get => exerciseName;
        set { exerciseName = value; OnPropertyChanged(); }
    }
    public string Sets
    {
        get => sets;
        set { sets = value; OnPropertyChanged(); }
    }
    public string Reps
    {
        get => reps;
        set { reps = value; OnPropertyChanged(); }
    }
    public string Weight
    {
        get => weight;
        set { weight = value; OnPropertyChanged(); }
    }
    public bool IsEditing
    {
        get => isEditing;
        set { isEditing = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
}