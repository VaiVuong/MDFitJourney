﻿using MDFitJourney.ViewModels;

namespace MDFitJourney;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainPageViewModel();
    }
}