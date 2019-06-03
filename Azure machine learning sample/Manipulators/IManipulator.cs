using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alms.Manipulators;

namespace Amls.Manipulators
{
    interface IManipulator: IManipulatingControls
    {
        Keys TriggerKey { get; }

        bool HandleKeyPress { get; }

        void Manipulate();
    }
}
