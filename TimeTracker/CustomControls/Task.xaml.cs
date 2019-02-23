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
        private SolidColorBrush _startButtonColor;
        private SolidColorBrush _stopButtonColor;
        private SolidColorBrush _startButtonColorWithTime;
        private double _hourUnit;

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

            //set private members
            this._stopWatch = new Stopwatch();
            this._dispatcherTimer = new DispatcherTimer(DispatcherPriority.Send);
            this._dispatcherInterval = new TimeSpan(0, 0, 0, 0, 100);
            this._startTime = "00:00:00";
            this._taskName = taskName;
            this._startButtonColor = Brushes.LawnGreen;
            this._stopButtonColor = Brushes.OrangeRed;
            this._startButtonColorWithTime = Brushes.LightBlue;
            this._hourUnit = 0.5;

            //initialize properties
            this.TaskText.Text = _taskName;
            this.StartStopButton.Content = "Start";
            this.StartStopButton.Background = _startButtonColor;
            this.TimeText.Text = this._startTime;
        }

        #endregion

        #region Events

        private void StartStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this._dispatcherTimer.IsEnabled)
            {
                if(this.TimeText.Text != this._startTime)
                {
                    bool confirmReset = ConfirmTimerResetWindow.Prompt("Are you sure you wish to reset the timer?", "Alert!");
                    if (!confirmReset)
                        return;
                }
                this.TimeText.Text = _startTime;
                this._dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                this._dispatcherTimer.Interval = _dispatcherInterval;
                this._dispatcherTimer.Start();
                this._stopWatch.Start();
                this.StartStopButton.Content = "Stop";
                this.StartStopButton.Background = _stopButtonColor;
            }
            else
            {
                this._dispatcherTimer.Stop();
                this._stopWatch.Stop();
                this.ConvertElapsedTimeToHours();
                this._stopWatch.Reset();
                this.StartStopButton.Content = this.TimeText.Text != this._startTime ? "Time Recorded" : "Start";
                this.StartStopButton.Background = this.TimeText.Text != this._startTime ? _startButtonColorWithTime : _startButtonColor;
            }
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            this.TimeText.Text = this._stopWatch.Elapsed.ToString(@"hh\:mm\:ss");

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void ConvertElapsedTimeToHours()
        {
            if(this.TimeText.Text != this._startTime)
            {
                this.TimeText.Text = (Math.Ceiling(this._stopWatch.Elapsed.TotalHours / this._hourUnit) * this._hourUnit).ToString() + " hours";
            }
        }

        #endregion
    }
}
