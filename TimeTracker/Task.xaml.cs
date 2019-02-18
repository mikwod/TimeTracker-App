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
    public partial class Task : UserControl
    {
        #region Members and Properties
        private Stopwatch _stopWatch;
        private DispatcherTimer _dispatcherTimer;
        private TimeSpan _dispatcherInterval;
        private string _startTime;
        private string _taskName;

        public string TaskName
        {
            get { return _taskName; }
        }
        public string ElapsedTimeString
        {
            get { return TimeText.Text; }
        }


        #endregion

        #region Constructors
        public Task(string taskName)
        {
            InitializeComponent();

            this._stopWatch = new Stopwatch();
            this._dispatcherTimer = new DispatcherTimer(DispatcherPriority.Send);
            this._dispatcherInterval = new TimeSpan(0, 0, 0, 0, 100);
            this._startTime = "00:00:00";
            this._taskName = taskName;
            this.TaskText.Text = _taskName;
        }

        #endregion

        #region Events

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            this.TimeText.Text = _startTime;

            this._dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            this._dispatcherTimer.Interval = _dispatcherInterval;
            this._dispatcherTimer.Start();
            this._stopWatch.Start();
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            this._stopWatch.Stop();
            this._dispatcherTimer.Stop();
            this._stopWatch.Reset();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            this.TimeText.Text = _stopWatch.Elapsed.ToString(@"hh\:mm\:ss");

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion
    }
}
