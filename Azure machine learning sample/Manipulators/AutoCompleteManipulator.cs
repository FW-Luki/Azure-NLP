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
    class AutoCompleteManipulator : IManipulator
    {
        public Keys TriggerKey => Keys.Enter;
        public bool HandleKeyPress => true;

        public RichTextBox RichTextBox { get; set; }
        public CompletionListControl CompletionListControl { get; set; }

        public virtual void Manipulate()
        {
            if (CompletionListControl.Items.Count > 0)
            {
                if (CompletionListControl.SelectedItem != null)
                {
                    var cursorPosition = RichTextBox.SelectionStart;
                    var firstPart = RichTextBox.Text.Substring(0, RichTextBox.SelectionStart).Split();
                    var secondPart = RichTextBox.Text.Substring(RichTextBox.SelectionStart, RichTextBox.TextLength - RichTextBox.SelectionStart).TrimStart();
                    var autoCompleted = CompletionListControl.SelectedItem.ToString();
                    RichTextBox.Text = string.Join(" ", firstPart.Take(firstPart.Length - 1).Concat(new[] { autoCompleted, secondPart }.Where(str => !string.IsNullOrEmpty(str))));
                    RichTextBox.SelectionStart = cursorPosition + autoCompleted.Length - firstPart.Last().Length;
                }
            }
        }
    }
}
