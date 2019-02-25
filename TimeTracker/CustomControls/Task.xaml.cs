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
    /// Individual task object that contains task name, task time, and start/stop button
    /// </summary>
    public partial class Task : UserControl
    {
        #region Members and Properties
        private Stopwatch stopWatch;
        private DispatcherTimer dispatcherTimer;
        private TimeSpan dispatcherInterval;
        private string startTime;
        private string taskName;
        private SolidColorBrush startButtonColor;
        private SolidColorBrush stopButtonColor;
        private SolidColorBrush startButtonColorWithTime;
        private double hourUnit;

        public bool IsCustom;


        public string TaskName
        {
            get { return taskName; }
        }
        public string ElapsedTimeString
        {
            get { return TimeText.Text; }
        }
        public bool IsRunning
        {
            get { return this.TimeText.Text != this.startTime ? true : false; }
        }


        #endregion

        #region Constructors
        public Task(string taskName, bool isCustom)
        {
            InitializeComponent();

            //set private members
            this.stopWatch = new Stopwatch();
            this.dispatcherTimer = new DispatcherTimer(DispatcherPriority.Send);
            this.dispatcherInterval = new TimeSpan(0, 0, 0, 0, 100);
            this.startTime = "00:00:00";
            this.taskName = taskName;
            this.startButtonColor = Brushes.LawnGreen;
            this.stopButtonColor = Brushes.OrangeRed;
            this.startButtonColorWithTime = Brushes.LightBlue;
            this.hourUnit = 0.5;

            //initialize properties
            this.TaskText.Text = taskName;
            this.StartStopButton.Content = "Start";
            this.StartStopButton.Background = startButtonColor;
            this.TimeText.Text = this.startTime;
            this.IsCustom = isCustom;
        }

        #endregion

        #region Events

        private void startStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.dispatcherTimer.IsEnabled)
            {
                if(this.IsRunning)
                {
                    bool confirmReset = ConfirmTimerResetWindow.Prompt("Are you sure you wish to reset the timer?", "Alert!");
                    if (!confirmReset)
                        return;
                }
                this.TimeText.Text = startTime;
                this.dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                this.dispatcherTimer.Interval = dispatcherInterval;
                this.dispatcherTimer.Start();
                this.stopWatch.Start();
                this.StartStopButton.Content = "Stop";
                this.StartStopButton.Background = stopButtonColor;
            }
            else
            {
                this.dispatcherTimer.Stop();
                this.stopWatch.Stop();
                this.convertElapsedTimeToHours();
                this.stopWatch.Reset();
                this.StartStopButton.Content = this.TimeText.Text != this.startTime ? "Time Recorded" : "Start";
                this.StartStopButton.Background = this.TimeText.Text != this.startTime ? startButtonColorWithTime : startButtonColor;
            }
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            this.TimeText.Text = this.stopWatch.Elapsed.ToString(@"hh\:mm\:ss");

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }

        private void convertElapsedTimeToHours()
        {
            if(this.TimeText.Text != this.startTime)
            {
                this.TimeText.Text = (Math.Ceiling(this.stopWatch.Elapsed.TotalHours / this.hourUnit) * this.hourUnit).ToString() + " hours";
            }
        }

        #endregion
    }
}
