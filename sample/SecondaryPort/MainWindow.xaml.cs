using System;
using System.Windows;
using URSecondaryLibrary;

namespace SecondaryPort
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IURSecondary urSec { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            urSec = IURSecondaryConnection.Create(IpAddress.Text);
            var resp = urSec.Receive();
            Logging.Text += resp + Environment.NewLine;
        }

        private void RunScript_Click(object sender, RoutedEventArgs e)
        {
            var cmds = URScript.Text.Split('\n');
            for (int i=0; i<cmds.Length; i++)
            {
                urSec.Send(cmds[i]+'\n');
            }

            URScript.Text = "";

            var resp = urSec.Receive();
            Logging.Text += resp + Environment.NewLine;

            LogScrollViewer.ScrollToBottom();
        }
    }
}
