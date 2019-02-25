using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System.Windows.Threading;

namespace TimeTracker
{
    /// <summary>
    /// Contains all tasks for a given day
    /// </summary>
    public partial class TaskGrid : UserControl
    {

        public TaskGrid()
        {
            InitializeComponent();
        }

        private void addTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string taskName = AddTaskWindow.Prompt("Please name your new task:", "Add Task", "", AddTaskWindow.InputType.Text);

            if (taskName != null)
            {
                Task task = new Task(taskName);
                int index = TaskStack.Children.Count;

                TaskStack.Children.Insert(index, task);
            }
        }
    }
}
