using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Diagnostics;
using System.Windows.Threading;
using System.Reflection.Emit;
using System.Windows.Media.Animation;

namespace TrafficLightsCrossRoad
{
    public partial class ctrlTrafficLight : UserControl
    {

        public enum enLight { Red, Orange, Green }

        private enLight _currentLight;
        DispatcherTimer _dispatcherTimer ;
        private int _CurrentCountDownValue;
        public ctrlTrafficLight()
        {
            InitializeComponent();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        [Category("TrafficLight"), Description("TrafficLight Light")]
        public enLight CurrentLight
        {
            get
            {
                return _currentLight;
            }

            set
            {
                _currentLight = value;
                switch (_currentLight)
                {
                    case enLight.Red:
                        Imgtraffic.Source = new BitmapImage(new Uri("pack://application:,,,/TrafficLightsCrossRoad;component/Images/Red.png"));
                        break;
                    case enLight.Orange:
                        Imgtraffic.Source = new BitmapImage(new Uri("pack://application:,,,/TrafficLightsCrossRoad;component/Images/Orange.png"));
                        break;
                    case enLight.Green:
                        Imgtraffic.Source = new BitmapImage(new Uri("pack://application:,,,/TrafficLightsCrossRoad;component/Images/Green.png"));
                        break;
                }
            }
        }

        [Category("TrafficLightTime"), Description("Red Light Duration")]
        public int RedDuration { get; set; } = 3;

        [Category("TrafficLightTime"), Description("Orange Light Duration")]
        public int OrangeDuration { get; set; } = 1;

        [Category("TrafficLightTime"), Description("Green Light Duration")]
        public int GreenDuration { get; set; } = 3;


       

        public class TrafficLightEventArgs : EventArgs
        {
            public enLight CurrentLight { get; }
            public int LightDuration { get; }

            public TrafficLightEventArgs(enLight CurrentLight, int LightDuration)
            {
                this.CurrentLight = CurrentLight;
                this.LightDuration = LightDuration;
            }
        }

        // RED

        public event EventHandler<TrafficLightEventArgs> OnRedLightOff;
        public void RaiseRedLightOff()
        {
            RaiseRedLightOff(new TrafficLightEventArgs(CurrentLight, RedDuration));
        }
        protected virtual void RaiseRedLightOff(TrafficLightEventArgs e)
        {
            OnRedLightOff?.Invoke(this, e);
        }

        public event EventHandler<TrafficLightEventArgs> OnRedLightOn;

        public void RaiseRedLightOn()
        {
            RaiseRedLightOn(new TrafficLightEventArgs(CurrentLight, RedDuration));
        }
        protected virtual void RaiseRedLightOn(TrafficLightEventArgs e)
        {
            OnRedLightOn?.Invoke(this, e);
        }


        // Green

        public event EventHandler<TrafficLightEventArgs> OnOrangeLightOff;
        public void RaiseOrangeLightOff()
        {
            RaiseOrangeLightOff(new TrafficLightEventArgs(CurrentLight, OrangeDuration));
        }
        protected virtual void RaiseOrangeLightOff(TrafficLightEventArgs e)
        {
            OnOrangeLightOff?.Invoke(this, e);
        }

        public event EventHandler<TrafficLightEventArgs> OnOrangeLightOn;

        public void RaiseOrangeLightOn()
        {
            RaiseOrangeLightOn(new TrafficLightEventArgs(CurrentLight, OrangeDuration));
        }
        protected virtual void RaiseOrangeLightOn(TrafficLightEventArgs e)
        {
            OnOrangeLightOn?.Invoke(this, e);
        }

        // Orange


        public event EventHandler<TrafficLightEventArgs> OnGreenLightOff;
        public void RaiseGreenLightOff()
        {
            RaiseGreenLightOff(new TrafficLightEventArgs(CurrentLight, GreenDuration));
        }
        protected virtual void RaiseGreenLightOff(TrafficLightEventArgs e)
        {
            OnGreenLightOff?.Invoke(this, e);
        }

        public event EventHandler<TrafficLightEventArgs> OnGreenLightOn;

        public void RaiseGreenLightOn()
        {
            RaiseGreenLightOn(new TrafficLightEventArgs(CurrentLight, GreenDuration));
        }
        protected virtual void RaiseGreenLightOn(TrafficLightEventArgs e)
        {
            OnGreenLightOn?.Invoke(this, e);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (_CurrentCountDownValue == 0)
            {
                _ChangeLight();

            }
            else
            {
                --_CurrentCountDownValue;
            }

        }


        private enLight _LightAfterOrangeGreenOrRed;

        private void _ChangeLight()
        {

            switch (CurrentLight)
            {
                case enLight.Red:
                    _LightAfterOrangeGreenOrRed = enLight.Green;
                    CurrentLight = enLight.Orange;
                    _CurrentCountDownValue = OrangeDuration;
                    RaiseRedLightOff();
                    RaiseOrangeLightOn();
                    break;
                case enLight.Orange:
                    if (_LightAfterOrangeGreenOrRed == enLight.Green)
                    {
                        CurrentLight = enLight.Green;
                        _CurrentCountDownValue = GreenDuration;
                        RaiseGreenLightOff();
                        RaiseGreenLightOn();
                    }
                    else
                    {
                        CurrentLight = enLight.Red;
                        _CurrentCountDownValue = RedDuration;
                        RaiseRedLightOff();
                        RaiseRedLightOn();
                    }
                    break;
                case enLight.Green:
                    _LightAfterOrangeGreenOrRed = enLight.Red;
                    CurrentLight = enLight.Orange;
                    _CurrentCountDownValue = OrangeDuration;

                    RaiseOrangeLightOn();
                    RaiseGreenLightOff();
                    break;
            }


        }

        public void Start()
        {
            
            switch (CurrentLight)
            {
                case enLight.Red:
                    _CurrentCountDownValue = RedDuration;
                    break;
                case enLight.Orange:
                    _CurrentCountDownValue = OrangeDuration;
                    break;
                case enLight.Green:
                    _CurrentCountDownValue = GreenDuration;
                    break;
            }
            _dispatcherTimer.Start();
        }
        public void Stop()
        {
            _dispatcherTimer.Stop();
        }
    }
}
