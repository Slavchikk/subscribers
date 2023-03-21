using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
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

namespace subscribers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Base.EM = new Entities1();
            FrameClass.MainFrame = fMain;
            FrameClass.MainFrame.Navigate(new Pages.ListSubscriesr());
            tbHead.Text = "Абоненты ТНС";
            cbFIO.ItemsSource = Base.EM.USERS.ToList(); // Заполнение списка сотрудников
            cbFIO.SelectedValuePath = "EmployeesID";
            cbFIO.DisplayMemberPath = "FIO";
            cbFIO.SelectedIndex = 0;
        }



        private void cbFIO_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            USERS user = Base.EM.USERS.FirstOrDefault(x => x.ID_USER == cbFIO.SelectedIndex + 2);
            imgUser.ImageSource = new BitmapImage(new Uri("" + user.images, UriKind.Relative));
            List<active_module> activs = Base.EM.active_module.Where(x => x.id_role == user.ID_ROLE).ToList(); // Изменение доступных модулей
            imgSubs.Visibility = Visibility.Collapsed; // Скрытие всех элементов
            imgObor.Visibility = Visibility.Collapsed;
            imgActive.Visibility = Visibility.Collapsed;
            imgBillg.Visibility = Visibility.Collapsed;
            imgSup.Visibility = Visibility.Collapsed;
            imgCRM.Visibility = Visibility.Collapsed;
            lbSubs.Visibility = Visibility.Collapsed;
            lbObor.Visibility = Visibility.Collapsed;
            lbAcrive.Visibility = Visibility.Collapsed;
            lbBilg.Visibility = Visibility.Collapsed;
            lbSup.Visibility = Visibility.Collapsed;
            lbCRM.Visibility = Visibility.Collapsed;
            foreach (active_module module in activs) 
            {
                switch (module.Moduls.name_module)
                {
                    case "Абоненты":
                        imgSubs.Visibility = Visibility.Visible;
                        lbSubs.Visibility = Visibility.Visible;
                        break;
                    case "CRM":
                        imgObor.Visibility = Visibility.Visible;
                        lbObor.Visibility = Visibility.Visible;
                        break;
                    case "Биллинг":
                        imgActive.Visibility = Visibility.Visible;
                        lbAcrive.Visibility = Visibility.Visible;
                        break;
                    case "Поддержка пользователей":
                        imgBillg.Visibility = Visibility.Visible;
                        lbBilg.Visibility = Visibility.Visible;
                        break;
                    case "Управление оборудованием":
                        imgSup.Visibility = Visibility.Visible;
                        lbSup.Visibility = Visibility.Visible;
                        break;
                    case "Активы":
                        imgCRM.Visibility = Visibility.Visible;
                        lbCRM.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            gridMain.Width = 200;
            spOpen.Visibility = Visibility.Visible;
            
            spClose.Visibility = Visibility.Collapsed;
            Grid.SetZIndex(spOpen, 1);
            Grid.SetZIndex(spClose, 2);
           
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            gridMain.Width = 100;
            spOpen.Visibility = Visibility.Collapsed;
            spClose.Visibility = Visibility.Visible;
            Grid.SetZIndex(spOpen, 1);
            Grid.SetZIndex(spClose, 2);
        }

      






    
    }
}
