using _5vRad.Commands.Base;
using _5vRad.Model;
using _5vRad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5vRad.Commands
{
    public class FieldClickCommand : Command
    {
        public override void Execute(object parameters)
        {
            object[] param = parameters as object[];

            FieldModel fm = (FieldModel)param[0];
            GameViewModel gvm = (GameViewModel)param[1];

            gvm.Click(fm);

        }
    }
}
