using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Proyecto_WPF__II_
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Add = new RoutedUICommand
            ("Add", "Add", typeof(CustomCommands),
             null);

        public static readonly RoutedUICommand Edit = new RoutedUICommand
            ("Edit", "Edit", typeof(CustomCommands),
            new InputGestureCollection { new KeyGesture(Key.E, ModifierKeys.Control) });

        public static readonly RoutedUICommand Exit = new RoutedUICommand
            ("Exit", "Exit", typeof(CustomCommands),
            new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control) });

        public static readonly RoutedUICommand Config = new RoutedUICommand
            ("Config", "Config", typeof(CustomCommands),
            new InputGestureCollection() { new KeyGesture(Key.C, ModifierKeys.Control) });

        public static readonly RoutedUICommand Help = new RoutedUICommand
            ("Help", "Help", typeof(CustomCommands),
            new InputGestureCollection() { new KeyGesture(Key.H, ModifierKeys.Control) });
    }
}
