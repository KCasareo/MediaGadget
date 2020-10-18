using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace MediaGadget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /*
        * https://stackoverflow.com/questions/7417739/make-wpf-window-draggable-no-matter-what-element-is-clicked
        * @Rachel
        * 
        * 
        * 
        * 
        *   
        */
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }


        // Move button functions to code-behind for resources
        private void Button_OnMouseEnter(object sender, MouseEventArgs e)
        {
            Button source = e.Source as Button;
            // Fade to another colour
            if (e.RoutedEvent.Equals(MouseEnterEvent) )
            {

            }
        }

        private void Button_OnMouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Button_Play_OnClick(object sender, MouseButtonEventArgs e)
        {

        }
    }

}
