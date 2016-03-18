using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using SecureUWPChannel.IOTransport;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SecureUWPChannel
{

    public sealed partial class MainPage : Page
    {

        private static String TaskNames = "My Task";
        private IOSessionBuilder Initiate;
        private IBackgroundTaskRegistration multipletask = null;
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(480, 510);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(480, 600));
            Initiate = new IOSessionBuilder();
            CheckTaskRegistration();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {

            var access = await BackgroundExecutionManager.RequestAccessAsync();

            switch (access)
            {
                case BackgroundAccessStatus.Unspecified:
                    break;
                case BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity:
                    break;
                case BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity:
                    break;
                case BackgroundAccessStatus.Denied:
                    break;
                default:
                    break;
            }
            var task = new BackgroundTaskBuilder
            {
                Name = TaskNames,
                TaskEntryPoint = typeof(RuntimeComponent2.NotifyUser).ToString()
            };

            var trigger = new ApplicationTrigger();
            task.SetTrigger(trigger);

            var condition = new SystemCondition(SystemConditionType.InternetAvailable);
            task.Register();
            Register.IsEnabled = false;
            Unregister.IsEnabled = true;
            await Initiate.InitializeAsync();
            NotifyUser("Server Says: " + Initiate.ServerMessage, NotifyType.StatusMessage);
            await trigger.RequestAsync();

        }

        public void NotifyUser(string strMessage, NotifyType type)
        {
            switch (type)
            {
                case NotifyType.StatusMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.DarkSeaGreen);
                    break;
                case NotifyType.ErrorMessage:
                    StatusBorder.Background = new SolidColorBrush(Windows.UI.Colors.Coral);
                    break;
            }
            StatusBlock.Text = strMessage;
            StatusBorder.Visibility = (StatusBlock.Text != String.Empty) ? Visibility.Visible : Visibility.Collapsed;
            if (StatusBlock.Text != String.Empty)
            {
                StatusBorder.Visibility = Visibility.Visible;
                StatusPanel.Visibility = Visibility.Visible;
            }
            else
            {
                StatusBorder.Visibility = Visibility.Collapsed;
                StatusPanel.Visibility = Visibility.Collapsed;
            }
        }


        private void CheckTaskRegistration()
        {
            foreach (var current in BackgroundTaskRegistration.AllTasks)
            {
                if (current.Value.Name == TaskNames)
                {
                    multipletask = current.Value;
                    break;
                }
            }

            if (multipletask != null)
            {
                Register.IsEnabled = false;
                Unregister.IsEnabled = true;
                NotifyUser("You left Unregister task from previous Execution Unregister First", NotifyType.ErrorMessage);
            }

        }

        private void UnRegister_Click(object sender, RoutedEventArgs e)
        {

            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == TaskNames)
                {
                    cur.Value.Unregister(true);
                }
            }
            Unregister.IsEnabled = false;
            Register.IsEnabled = true;
           NotifyUser("You are ready to Register A backround Task ", NotifyType.StatusMessage);
        }

        public enum NotifyType
        {
            StatusMessage,
            ErrorMessage
        };


    }
}
