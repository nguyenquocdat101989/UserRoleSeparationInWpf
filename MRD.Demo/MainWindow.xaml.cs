using MRD.UserRoleSeparationHelper;
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

namespace MRD.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private UserType userPermission;

        public UserType LoggedInUserType
        {
            get { return userPermission; }
            set
            {
                userPermission = value;
                OnPropertyChanged(nameof(LoggedInUserType));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUserType = UserType.Guest;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoginAsAdmin(object sender, RoutedEventArgs e)
        {
            LoggedInUserType = UserType.Admin;
        }

        private void LoggOut(object sender, RoutedEventArgs e)
        {
            LoggedInUserType = UserType.Guest;
        }
    }
}
