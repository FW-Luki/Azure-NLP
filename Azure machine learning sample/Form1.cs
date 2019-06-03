using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Amls
{
    /// <summary>
    /// Main window.
    /// </summary>
    public partial class Form1 : Form
    {
        private readonly Alms.Manipulators.Manipulators manipulators = new Alms.Manipulators.Manipulators();

        /// <summary>
        /// Gets or sets the <see cref="NGramCollection"/> used by this window.
        /// </summary>
        public NGramCollection NGramCollection { get; set; }
        

        /// <summary>
        /// Creates new instance of this window.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            manipulators.RichTextBox = richTextBox1;
            manipulators.CompletionListControl = completionListControl1;
        }


        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            var manipulated = manipulators.Manipulate(e.KeyCode);
            e.Handled = manipulated;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            var hints = NGramCollection.GetHints(richTextBox1.Text.Substring(0, richTextBox1.SelectionStart).Split());

            var pos = richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
            pos.Offset(-Math.Max(pos.X + completionListControl1.Width - richTextBox1.Right, 0), TextRenderer.MeasureText("I", richTextBox1.Font).Height);

            completionListControl1.Show(hints, pos);
        }

    }
}
