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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using URDashboardLibrary;
using URRTDELibrary;
using URScritpsLibrary;
using URSecondaryLibrary;

namespace URScriptGeneratorDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IURDashboard dashboard { get; set; }
        public IURRTDE rtde { get; set; }
        public IURSecondary secondary { get; set; }
        public ReceiveData rd { get; set; }
               

        public URPose point1 { get; set; }
        public URPose point2 { get; set; }
        public URPose point3 { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            IPAddress.Text = "127.0.0.1";
            Point1.Fill = new SolidColorBrush(Colors.Red);
            Point2.Fill = new SolidColorBrush(Colors.Red);
            Point3.Fill = new SolidColorBrush(Colors.Red);
            //URScriptTextBox.Text = "def cscan():\n  movel(p[-0.044, -0.436, 0.22203002542838374, -0.001, 3.116, 0.039], a = 0.5, v = 0.3)\n  sleep(0.5)\n  force_mode(p[0.0, 0.0, 0.0, 0.0, 0.0, 0.0], [0, 0, 1, 0, 0, 0], [0.0, 0.0, -70.0, 0.0, 0.0, 0.0], 2, [0.1, 0.1, 0.15, 0.3490658503988659, 0.3490658503988659, 0.3490658503988659])\n  movel(p[-0.044, -0.436, 0.20203002542838375, -0.001, 3.116, 0.039], a = 0.5, v = 0.02)\n  sleep(1)\n  movel(p[-0.144, -0.436, 0.20203002542838416, -0.001, 3.116, 0.039], a = 0.5, v = 0.02)\n  end_force_mode()\n  sleep(0.5)\n  movel(p[-0.144, -0.436, 0.22203002542838415, -0.001, 3.116, 0.039], a = 0.1, v = 0.1)\nend";
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            dashboard = IURDashboardConnection.Create(IPAddress.Text);
            secondary = IURSecondaryConnection.Create(IPAddress.Text);

            // Connet UR RTDE port
            rtde = IURRTDEConnection.Create(IPAddress.Text);
            var np = new NegotiateProtocolVersion(rtde);
            ControllerVersion cv = new ControllerVersion(rtde);
            string vars = "timestamp,actual_TCP_pose,runtime_state";
            SetupOutput setupOut = new SetupOutput(rtde, 125, vars);
            var ss = new StartSending(rtde);
            rd = new ReceiveData(rtde, setupOut.VariableTypes);
            var taskRTDEReceiving = new Task(() => RTDEReceiving());
            taskRTDEReceiving.Start();
        }


        // Calibrate
        private void GetPoint1_Click(object sender, RoutedEventArgs e)
        {
            rd.Receive();
            point1 = new URPose(new Point3D(
                                    Convert.ToDouble(ActualTCPPoseX),
                                    Convert.ToDouble(ActualTCPPoseY),
                                    Convert.ToDouble(rd.ActualTCPPose.Z)),
                                new Point3D(
                                    Convert.ToDouble(ActualTCPPoseRX),
                                    Convert.ToDouble(ActualTCPPoseRY),
                                    Convert.ToDouble(ActualTCPPoseRZ)));
            Point1.Fill = new SolidColorBrush(Colors.Green);
        }

        private void GetPoint2_Click(object sender, RoutedEventArgs e)
        {
            rd.Receive();
            point2 = new URPose(new Point3D(
                                    Convert.ToDouble(ActualTCPPoseX),
                                    Convert.ToDouble(ActualTCPPoseY),
                                    Convert.ToDouble(rd.ActualTCPPose.Z)),
                                new Point3D(
                                    Convert.ToDouble(ActualTCPPoseRX),
                                    Convert.ToDouble(ActualTCPPoseRY),
                                    Convert.ToDouble(ActualTCPPoseRZ)));
            Point2.Fill = new SolidColorBrush(Colors.Green);
        }

        private void GetPoint3_Click(object sender, RoutedEventArgs e)
        {
            rd.Receive();
            point3 = new URPose(new Point3D(
                        Convert.ToDouble(ActualTCPPoseX),
                        Convert.ToDouble(ActualTCPPoseY),
                        Convert.ToDouble(rd.ActualTCPPose.Z)),
                    new Point3D(
                        Convert.ToDouble(ActualTCPPoseRX),
                        Convert.ToDouble(ActualTCPPoseRY),
                        Convert.ToDouble(ActualTCPPoseRZ)));
            Point3.Fill = new SolidColorBrush(Colors.Green);
        }

        // URscript control
        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            if ((point1 != null) && (point2 != null) && (point3 != null))
            {
                var scanmove = new URMovement(point1, point2);
                var boundary = new RectangularBoundary(scanmove, point3);
                double aperture = 0.06;
                double overlap = 0.1;
                var urMoves = PathPlanner.SStyle(boundary, aperture, overlap);
                var safeDist = new URVector(new Vector3D(0, 0, 0.02), new Vector3D(0, 0, 0));
                URScriptTextBox.Text = URScript.Generate("cscan", urMoves, safeDist);                
            }
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            secondary.SendReceive(URScriptTextBox.Text);
            URScriptTextBox.Text = "";
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            dashboard.Send(URDashboardLibrary.ICommand.StopProgram());
        }

        private void ResetPoints_Click(object sender, RoutedEventArgs e)
        {
            Point1.Fill = new SolidColorBrush(Colors.Red);
            Point2.Fill = new SolidColorBrush(Colors.Red);
            Point3.Fill = new SolidColorBrush(Colors.Red);
        }

        public void RTDEReceiving()
        {
            rd.Receive();

            while (true)
            {
                rd.Receive();
                ActualTCPPoseX = rd.ActualTCPPose.X.ToString("0.###");
                ActualTCPPoseY = rd.ActualTCPPose.Y.ToString("0.###");
                ActualTCPPoseZ = rd.ActualTCPPose.Z.ToString("0.###");
                ActualTCPPoseRX = rd.ActualTCPPose.RX.ToString("0.###");
                ActualTCPPoseRY = rd.ActualTCPPose.RY.ToString("0.###");
                ActualTCPPoseRZ = rd.ActualTCPPose.RZ.ToString("0.###");
            }
        }

        private string _actualTCPPoseX;

        public string ActualTCPPoseX
        {
            get { return _actualTCPPoseX; }
            set
            {
                _actualTCPPoseX = value;
            }
        }

        private string _actualTCPPoseY;

        public string ActualTCPPoseY
        {
            get { return _actualTCPPoseY; }
            set
            {
                _actualTCPPoseY = value;
            }
        }

        private string _actualTCPPoseZ;

        public string ActualTCPPoseZ
        {
            get { return _actualTCPPoseZ; }
            set
            {
                _actualTCPPoseZ = value;
            }
        }

        private string _actualTCPPoseRX;

        public string ActualTCPPoseRX
        {
            get { return _actualTCPPoseRX; }
            set
            {
                _actualTCPPoseRX = value;
            }
        }

        private string _actualTCPPoseRY;

        public string ActualTCPPoseRY
        {
            get { return _actualTCPPoseRY; }
            set
            {
                _actualTCPPoseRY = value;
            }
        }

        private string _actualTCPPoseRZ;

        public string ActualTCPPoseRZ
        {
            get { return _actualTCPPoseRZ; }
            set
            {
                _actualTCPPoseRZ = value;
            }
        }

        private void FreeDriveMode_Click(object sender, RoutedEventArgs e)
        {
            string cmd = "freedrive_mode()\n";
            secondary.SendReceive(cmd);
            URScriptTextBox.Text += $"{cmd}" + Environment.NewLine;
        }
    }
}
