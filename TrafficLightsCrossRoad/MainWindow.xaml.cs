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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrafficLightsCrossRoad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            Tl1.Stop();
            Tl3.Stop();
           
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Tl1.Start();
            Tl3.Start();
            
        }

        private void Tl3_OnRedLightOn(object sender, ctrlTrafficLight.TrafficLightEventArgs e)
        {
            Tl2.CurrentLight = ctrlTrafficLight.enLight.Green;
            Tl4.CurrentLight = ctrlTrafficLight.enLight.Green;
        }

        private void Tl3_OnRedLightOff(object sender, ctrlTrafficLight.TrafficLightEventArgs e)
        {
            
        }

        private void Tl3_OnOrangeLightOn(object sender, ctrlTrafficLight.TrafficLightEventArgs e)
        {

            Tl2.CurrentLight = ctrlTrafficLight.enLight.Orange;
            Tl4.CurrentLight = ctrlTrafficLight.enLight.Orange;
        }

        private void Tl3_OnOrangeLightOff(object sender, ctrlTrafficLight.TrafficLightEventArgs e)
        {
            
        }

        private void Tl3_OnGreenLightOn(object sender, ctrlTrafficLight.TrafficLightEventArgs e)
        {
            
            Tl2.CurrentLight = ctrlTrafficLight.enLight.Red;
            Tl4.CurrentLight = ctrlTrafficLight.enLight.Red;
           
        }

        private void Tl3_OnGreenLightOff(object sender, ctrlTrafficLight.TrafficLightEventArgs e)
        {
            
        }
    }
}
