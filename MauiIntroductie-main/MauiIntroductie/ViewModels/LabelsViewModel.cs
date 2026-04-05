using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MauiIntroductie.ViewModels
{
    public partial class LabelsViewModel : ObservableObject
    {
        [ObservableProperty]
        string text, textColor;

        [ObservableProperty]
        FontAttributes fontAttributes;

        [ObservableProperty]
        int fontSize, padding;

        public LabelsViewModel()
        {
            FontSize = 40;
            Text = "Dit is binding";
            FontAttributes = FontAttributes.Bold;
            TextColor = "#AABBCC";
            Padding = 40;
        }
    }
}
