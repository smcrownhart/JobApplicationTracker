﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace JobAppTracker.Maui.Services
{
    public interface INavigationHelper
    {
        Task GoBackOrHomeAsync();
        Task GoToAsync(string route);
    }
}
