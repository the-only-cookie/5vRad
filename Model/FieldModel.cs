using _5vRad.ViewModels.Base;
using _5vRad.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5vRad.Model
{
    internal class FieldModel : ViewModel
    {
        private int i;
        public int I { get => i; set => Set(ref i, value); }

        private int j;
        public int J { get => j; set => Set(ref j, value); }

        private string color = Colors.None;
        public string Color { get => color; set => Set(ref color, value); }

    }
}
