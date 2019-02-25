﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<Task> taskList;
        

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion


        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TaskStack.Children.Add(new TaskGrid());
        }

     
    }
}
