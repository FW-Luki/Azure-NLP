using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amls;
using Amls.Manipulators;

namespace ConsoleApp1.Manipulators
{
    class HintListMoveUpManipulator : IManipulator
    {
        public Keys TriggerKey => Keys.Up;
        public bool HandleKeyPress => true;

        public RichTextBox RichTextBox { get; set; }
        public CompletionListControl CompletionListControl { get; set; }

        public void Manipulate()
        {
            if (CompletionListControl.Items.Count > 0)
            {
                if (CompletionListControl.SelectedIndex <= 0)
                {
                    CompletionListControl.SelectedIndex = CompletionListControl.Items.Count - 1;
                }
                else
                {
                    CompletionListControl.SelectedIndex--;
                }
            }
        }
    }
}
