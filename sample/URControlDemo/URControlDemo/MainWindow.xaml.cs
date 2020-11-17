using System;
using System.Threading.Tasks;
using System.Windows;
using URRTDELibrary;
using URSecondaryLibrary;

namespace URControlDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IURSecondary urSec { get; set; }
        public IURRTDE rtde { get; set; }
        public string variables { get; set; }
        public ReceiveData rd { get; set; }
        public bool setMode { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            IPAddress.Text = "192.168.0.10";
            SetReadOnly(true);
            SetURPosition.IsEnabled = true;
            Run.IsEnabled = false;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            setMode = true;
            if ((urSec != null) && (urSec.IsConnected == true))
            {
                urSec.Close();
            }
            if (rtde != null)
            {
                rtde.Close();
            }
            base.OnClosing(e);
        }

        public void SetReadOnly(bool flag)
        {
            URX.IsReadOnly = flag;
            URY.IsReadOnly = flag;
            URZ.IsReadOnly = flag;
            URRX.IsReadOnly = flag;
            URRY.IsReadOnly = flag;
            URRZ.IsReadOnly = flag;
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            // Connet UR Secondary Port
            urSec = IURSecondaryConnection.Create(IPAddress.Text);
            var resp = urSec.Receive();

            // Connet UR RTDE port
            rtde = IURRTDEConnection.Create(IPAddress.Text);
            var np = new NegotiateProtocolVersion(rtde);
            Logging.Text += $"NegotiateProtocolVersion Accepted: { np.Accepted }";

            ControllerVersion cv = new ControllerVersion(rtde);
            Logging.Text += $"Controller Version: major { cv.Major }" +
                $" minor { cv.Minor } bugfix { cv.Bugfix } build { cv.Build }";

            string vars = "timestamp,actual_TCP_pose,actual_q,runtime_state";
            SetupOutput setupOut = new SetupOutput(rtde, 125, vars);
            Logging.Text += $"Setup Output: output recipe id { setupOut.OutputRecipeId }" +
                $" variable types { setupOut.VariableTypes }";

            var ss = new StartSending(rtde);
            rd = new ReceiveData(rtde, setupOut.VariableTypes);

            var task = new Task(() => ReceiveRTDE());
            task.Start();
        }

        public void ReceiveRTDE()
        {
            while (setMode == false)
            {
                rd.Receive();
                var position = rd.ActualTCPPose;
                var jointPos = rd.ActualJointPose;
                Dispatcher.Invoke(() =>
                {
                    //TCP
                    URX.Text = (position.X * 1000).ToString("0.#");
                    URY.Text = (position.Y * 1000).ToString("0.#");
                    URZ.Text = (position.Z * 1000).ToString("0.#");
                    URRX.Text = position.RX.ToString("0.###");
                    URRY.Text = position.RY.ToString("0.###");
                    URRZ.Text = position.RZ.ToString("0.###");

                    //Joint
                    Base.Text = jointPos.Base.ToString("0.###");
                    Shoulder.Text = jointPos.Shoulder.ToString("0.###");
                    Elbow.Text = jointPos.Elbow.ToString("0.###");
                    Wrist1.Text = jointPos.Wrist1.ToString("0.###");
                    Wrist2.Text = jointPos.Wrist2.ToString("0.###");
                    Wrist3.Text = jointPos.Wrist3.ToString("0.###");
                });
            }
        }

        private void SetURPosition_Click(object sender, RoutedEventArgs e)
        {
            setMode = true;
            SetReadOnly(false);
            SetURPosition.IsEnabled = false;
            Run.IsEnabled = true;
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            SetURPosition.IsEnabled = true;
            Run.IsEnabled = false;
            string cmd = MoveL(
                (Convert.ToDouble(URX.Text) / 1000).ToString(),
                (Convert.ToDouble(URY.Text) / 1000).ToString(),
                (Convert.ToDouble(URZ.Text) / 1000).ToString(),
                URRX.Text,
                URRY.Text,
                URRZ.Text);
            Logging.Text += cmd;
            urSec.Send(cmd);
            setMode = false;
            SetReadOnly(true);
            var task = new Task(() => ReceiveRTDE());
            task.Start();
        }

        public static string MoveL(string x, string y, string z, string rx, string ry, string rz, double a = 0.5, double v = 0.1)
        {
            return $"movel(p[{ x }, { y }, { z }, { rx }, { ry }, { rz }], a={ a }, v={ v })\n";
        }

        private void SendURScript_Click(object sender, RoutedEventArgs e)
        {
            var cmds = URScript.Text.Split('\n');
            for (int i = 0; i < cmds.Length; i++)
            {
                var cmd = cmds[i].Trim('\r') + '\n';
                urSec.Send(cmd);
            }

            URScript.Text = "";

            var resp = urSec.Receive();
            Logging.Text += resp + Environment.NewLine;

            LogScrollViewer.ScrollToBottom();
        }
    }
}
