using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace Spinner
{
    public class SpinnerViewModel : DependencyObject
    {
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register("Angle", typeof(double), typeof(SpinnerViewModel), new UIPropertyMetadata(0d));
        public static readonly DependencyProperty SpinnerCentreXProperty = DependencyProperty.Register("SpinnerCentreX", typeof(double), typeof(SpinnerViewModel), new UIPropertyMetadata(250d));
        public static readonly DependencyProperty SpinnerCentreYProperty = DependencyProperty.Register("SpinnerCentreY", typeof(double), typeof(SpinnerViewModel), new UIPropertyMetadata(250d));
        public static readonly DependencyProperty SpinnerWidthProperty = DependencyProperty.Register("SpinnerWidth", typeof(double), typeof(SpinnerViewModel), new UIPropertyMetadata(1d));
        public static readonly DependencyProperty SpinnerHeightProperty = DependencyProperty.Register("SpinnerHeight", typeof(double), typeof(SpinnerViewModel), new UIPropertyMetadata(1d));

        public static readonly DependencyProperty AngularMomentumProperty = DependencyProperty.Register("AngularMomentum", typeof(double), typeof(SpinnerViewModel), new UIPropertyMetadata(0d));

        /// <summary>
        /// Gets or sets the AngularMomentum.
        /// </summary>
        public double AngularMomentum
        {
            get { return (double)GetValue(AngularMomentumProperty); }
            set { SetValue(AngularMomentumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Angle.
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Angle.
        /// </summary>
        public double SpinnerCentreX
        {
            get { return (double)GetValue(SpinnerCentreXProperty); }
            set { SetValue(SpinnerCentreXProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Angle.
        /// </summary>
        public double SpinnerCentreY
        {
            get { return (double)GetValue(SpinnerCentreYProperty); }
            set { SetValue(SpinnerCentreYProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Angle.
        /// </summary>
        public double SpinnerWidth
        {
            get { return (double)GetValue(SpinnerWidthProperty); }
            set { SetValue(SpinnerWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Angle.
        /// </summary>
        public double SpinnerHeight
        {
            get { return (double)GetValue(SpinnerHeightProperty); }
            set { SetValue(SpinnerHeightProperty, value); }
        }

        private Task _currentSpinTask;
        private bool _spinning;
        internal void Spin()
        {
            _currentSpinTask = Task.Factory.StartNew(() =>
            {
                _spinning = true;
                while (_spinning)
                {
                    Thread.Sleep(10);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.Angle += this.AngularMomentum;
                        if (Math.Abs(this.AngularMomentum) > 20)
                        {
                            this.AngularMomentum = 20;
                        }
                        else if (Math.Abs(this.AngularMomentum) > 0.01)
                        {
                            if (this.AngularMomentum > 0)
                            {
                                this.AngularMomentum *= 0.99;// -= 0.1;
                            }
                            else
                            {
                                this.AngularMomentum *= 0.99; //+= 0.1;
                            }
                        }
                        else
                        {
                            this.AngularMomentum = 0;
                            _spinning = false;
                        }
                    }));
                }
            });
        }

        internal void StopSpin()
        {
            _spinning = false;
        }
    }
}
