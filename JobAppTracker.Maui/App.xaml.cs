﻿using JobAppTracker.Maui.Views;

namespace JobAppTracker.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            


        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}