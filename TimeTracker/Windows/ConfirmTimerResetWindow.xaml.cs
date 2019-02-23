using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeTracker
{
    /// <summary>
    /// Interaction logic for AddTaskWindow.xaml
    /// </summary>
    public partial class ConfirmTimerResetWindow : Window
    {

        public static bool ConfirmReset;

        public ConfirmTimerResetWindow(string question, string title)
        {
            InitializeComponent();
            txtQuestion.Text = question;
            Title = title;
        }

        public static bool Prompt(string question, string title)
        {
            ConfirmTimerResetWindow inst = new ConfirmTimerResetWindow(question, title);
            inst.ShowDialog();

            return ConfirmReset;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            ConfirmReset = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ConfirmReset = false;
            Close();
        }
    }
}
