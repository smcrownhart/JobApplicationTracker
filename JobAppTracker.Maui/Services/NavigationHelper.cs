using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace JobAppTracker.Maui.Services
{
    public class NavigationHelper : INavigationHelper
    {
        public async Task GoBackOrHomeAsync()
        {
            if (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.GoToAsync("//MainPage");
            }
        }

        public async Task GoToAsync(string route)
        {
            await Shell.Current.GoToAsync(route);
        }
    }
}
