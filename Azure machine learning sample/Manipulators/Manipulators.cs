using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amls;
using Amls.Manipulators;

namespace Alms.Manipulators
{
    class Manipulators : IManipulatingControls
    {
        private readonly ReadOnlyCollection<IManipulator> manipulators;

        public Manipulators()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IManipulator).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
            manipulators = new ReadOnlyCollection<IManipulator>(types.Select(t =>
            {
                try
                {
                    var manipulator = Activator.CreateInstance(t) as IManipulator;
                    return manipulator;
                }
                catch
                {
                    return null;
                }
            }).Where(m => m != null).ToList());
        }

        public RichTextBox RichTextBox { get; set; }
        public CompletionListControl CompletionListControl { get; set; }

        public bool Manipulate(Keys trigger)
        {
            var manipulated = false;
            foreach (var manipulator in manipulators)
            {
                if (manipulator.TriggerKey == trigger)
                {
                    manipulator.RichTextBox = RichTextBox;
                    manipulator.CompletionListControl = CompletionListControl;

                    manipulator.Manipulate();
                    manipulated = true;
                }
            }
            return manipulated;
        }
    }
}
