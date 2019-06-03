using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amls
{
    class CompletionListControl : ListBox
    {
        public CompletionListControl()
        {
            Visible = false;
        }

        public void Show(IEnumerable<string> words, Point location)
        {
            Items.Clear();
            Items.AddRange(words.ToArray());
            Location = location;
            if (Items.Count > 0)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void MoveUp()
        {
        }

        public void MoveDown()
        {
        }

    }
}
