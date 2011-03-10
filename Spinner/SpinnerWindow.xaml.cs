using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Spinner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String ViewModelProviderKey = "ViewModelProvider";

        private SpinnerViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Stream stream = File.Open("Background.jpg", FileMode.Open);
                BitmapImage imgsrc = new BitmapImage();
                imgsrc.BeginInit();
                imgsrc.StreamSource = stream;
                imgsrc.EndInit();

                imgBackground.Source = imgsrc;
            }
            catch (IOException) { }

            _viewModel = TryFindResource(ViewModelProviderKey) as SpinnerViewModel;
        }

        private double RadToDeg(double radians)
        {
            return radians * 180 / Math.PI;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed || e.MiddleButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)
            {
                Point mousePos = e.GetPosition((Canvas)sender);
                double opp = mousePos.X - _viewModel.SpinnerCentreX;
                double adj = _viewModel.SpinnerCentreY - mousePos.Y;

                double angle =0;
                if (adj > 0)
                {
                    double ratio = opp / adj;
                    angle = RadToDeg(Math.Atan(ratio));
                }
                else
                {
                    double ratio = opp / adj;
                    angle = RadToDeg(Math.Atan(ratio))+180;
                }
                _viewModel.StopSpin();
                _viewModel.AngularMomentum = angle - _viewModel.Angle;
                _viewModel.Angle = angle;
            }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            _viewModel.SpinnerHeight = Math.Min(e.NewSize.Width, e.NewSize.Height) / 500;
            _viewModel.SpinnerWidth = _viewModel.SpinnerHeight;
            _viewModel.SpinnerCentreX = e.NewSize.Width / 2;
            _viewModel.SpinnerCentreY = e.NewSize.Height / 2;
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _viewModel.Spin();
        }
    }

    
}
