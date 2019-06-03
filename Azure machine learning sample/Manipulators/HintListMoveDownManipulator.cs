using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amls;
using Amls.Manipulators;

namespace Alms.Manipulators
{
    class HintListMoveDownManipulator : IManipulator
    {
        public Keys TriggerKey => Keys.Down;
        public bool HandleKeyPress => true;

        public RichTextBox RichTextBox { get; set; }
        public CompletionListControl CompletionListControl { get; set; }

        public void Manipulate()
        {
            if (CompletionListControl.Items.Count > 0)
            {
                if (CompletionListControl.SelectedIndex >= (CompletionListControl.Items.Count - 1))
                {
                    CompletionListControl.SelectedIndex = 0;
                }
                else
                {
                    CompletionListControl.SelectedIndex++;
                }
            }
        }
    }
}
