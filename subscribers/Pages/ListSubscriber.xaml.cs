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

namespace subscribers.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListSubscriesr.xaml
    /// </summary>
    public partial class ListSubscriesr : Page
    {
        bool b;
        public ListSubscriesr()
        {
            InitializeComponent();
            dgSubscriers.ItemsSource = Base.EM.Subscriber.ToList();
            cbActive.IsChecked = true;
            List<Raoins> raions = Base.EM.Raoins.ToList(); 
            cbFilterRaion.Items.Add("Все районы");
            foreach (Raoins raion in raions)
            {
                cbFilterRaion.Items.Add(raion.raion_name);
            }
            cbFilterRaion.SelectedIndex = 0;
            cbFilterStreet.IsEnabled = false;
            cbFiltNomerHouse.IsEnabled = false;
        }

        private void Sort()
        {
            List<Subscriber> subscribers = new List<Subscriber>();
            if ((bool)!cbActive.IsChecked && (bool)!cbNotActive.IsChecked) 
            {
                subscribers = Base.EM.Subscriber.ToList();
            }
            else if ((bool)cbActive.IsChecked && (bool)!cbNotActive.IsChecked)
            {
                subscribers = Base.EM.Subscriber.Where(x => x.date_terminate == null).ToList();
            }
            else if ((bool)!cbActive.IsChecked && (bool)cbNotActive.IsChecked)
            {
                subscribers = Base.EM.Subscriber.Where(x => x.date_terminate != null).ToList();
            }
            else
            {
                subscribers = new List<Subscriber>();
            }
            if (tbSearchSurname.Text.Replace(" ", "").Length > 0) 
            {
                subscribers = subscribers.Where(x => x.lastname.ToLower().Contains(tbSearchSurname.Text.ToLower())).ToList();
            }
            if (cbFilterRaion.SelectedIndex > 0) 
            {
                Raoins raion = Base.EM.Raoins.FirstOrDefault(x => x.raion_name == cbFilterRaion.SelectedValue); 
                subscribers = subscribers.Where(x => x.address_subscriber.id_raion == raion.id_raion).ToList();
            }
            if (cbFilterStreet.SelectedIndex > 0) 
            {
                Streets street = Base.EM.Streets.FirstOrDefault(x => x.street == cbFilterStreet.SelectedValue);
                subscribers = subscribers.Where(x => x.address_subscriber.id_street == street.id_street).ToList();
            }
            if (cbFiltNomerHouse.SelectedIndex > 0) 
            {
                subscribers = subscribers.Where(x => Convert.ToString(x.address_subscriber.house) == (string)cbFiltNomerHouse.SelectedValue).ToList();
            }
            if (tbSearchPersonalAccount.Text.Replace(" ", "").Length > 0) 
            {
                subscribers = subscribers.Where(x => x.personal_account.ToString().ToLower().Contains(tbSearchPersonalAccount.Text.Replace(" ", "").ToLower())).ToList();
            }
            dgSubscriers.ItemsSource = subscribers;
            if (subscribers.Count == 0)
            {
                MessageBox.Show("Данные отсутсвуют");
            }
        }
        private void tbSearchSurname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            b = true;
            Sort();
           
        }

        private void cbFilterRaion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
            if (cbFilterRaion.SelectedIndex > 0)
            {
                b = false;
                cbFilterStreet.Items.Clear();
                cbFilterStreet.IsEnabled = true;
                List<address_subscriber> residentialAddresses = Base.EM.address_subscriber.Where(x => x.id_raion == cbFilterRaion.SelectedIndex).ToList();
                List<string> streets = new List<string>();
                cbFilterStreet.Items.Add("Все улицы");
                foreach (address_subscriber res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.id_street != null)
                    {
                        streets.Add(res.Streets.street);
                    }
                }
                streets = streets.Distinct().ToList();
                foreach (string street in streets)
                {
                    cbFilterStreet.Items.Add(street);
                }
                cbFilterStreet.SelectedIndex = 0;
            }
            else
            {
                b = true;
                cbFilterStreet.IsEnabled = false;
                cbFilterStreet.Items.Clear();
            }
          
        }

        private void cbFilterStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Sort();
            if (cbFilterStreet.SelectedIndex > 0)
            {
                cbFiltNomerHouse.Items.Clear();
                cbFiltNomerHouse.IsEnabled = true;
                List<address_subscriber> residentialAddresses = Base.EM.address_subscriber.Where(x => x.id_raion == cbFilterRaion.SelectedIndex && x.id_street == cbFilterStreet.SelectedIndex).ToList();
                List<string> houses = new List<string>();
                cbFiltNomerHouse.Items.Add("Все дома");
                foreach (address_subscriber res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.house != null)
                    {
                        houses.Add(Convert.ToString(res.house));
                    }
                }
                houses = houses.Distinct().ToList();
                foreach (string house in houses)
                {
                    cbFiltNomerHouse.Items.Add(house);
                }
                cbFiltNomerHouse.SelectedIndex = 0;
            }
            else
            {
                cbFiltNomerHouse.IsEnabled = false;
                cbFiltNomerHouse.Items.Clear();
            }
        }

        private void cbFiltNomerHouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Sort();

        }

        private void tbSearchPersonalAccount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
                if (!(Char.IsDigit(e.Text, 0)))
                {
                    e.Handled = true;
                }
            
        }

        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            b = true;
            Sort();
        }
    }
}
