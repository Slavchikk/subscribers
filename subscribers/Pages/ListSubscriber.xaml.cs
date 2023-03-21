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
            cbRaion.Items.Add("Все районы");
            foreach (Raoins raion in raions)
            {
                cbRaion.Items.Add(raion.raion_name);
            }
            cbRaion.SelectedIndex = 0;
           
            cbStreet.IsEnabled = false;
            cbHomes.IsEnabled = false;
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
            if (tbLastname.Text.Replace(" ", "").Length > 0) 
            {
                subscribers = subscribers.Where(x => x.lastname.ToLower().Contains(tbLastname.Text.ToLower())).ToList();
            }
            if (cbRaion.SelectedIndex > 0) 
            {
                Raoins raion = Base.EM.Raoins.FirstOrDefault(x => x.raion_name == cbRaion.SelectedValue); 
                subscribers = subscribers.Where(x => x.address_subscriber.id_raion == raion.id_raion).ToList();
            }
            if (cbStreet.SelectedIndex > 0) 
            {
                Streets street = Base.EM.Streets.FirstOrDefault(x => x.street == cbStreet.SelectedValue);
                subscribers = subscribers.Where(x => x.address_subscriber.id_street == street.id_street).ToList();
            }
            if (cbHomes.SelectedIndex > 0) 
            {
                subscribers = subscribers.Where(x => Convert.ToString(x.address_subscriber.house) == (string)cbHomes.SelectedValue).ToList();
            }
            if (tbParsAc.Text.Replace(" ", "").Length > 0) 
            {
                subscribers = subscribers.Where(x => x.personal_account.ToString().ToLower().Contains(tbParsAc.Text.Replace(" ", "").ToLower())).ToList();
            }
            dgSubscriers.ItemsSource = subscribers;
            if (subscribers.Count == 0)
            {
                MessageBox.Show("Данные отсутсвуют");
            }
        }
        private void tbLastname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            b = true;
            Sort();
           
        }

        private void cbRaion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
            if (cbRaion.SelectedIndex > 0)
            {
                b = false;
                cbStreet.Items.Clear();
                cbStreet.IsEnabled = true;
                List<address_subscriber> residentialAddresses = Base.EM.address_subscriber.Where(x => x.id_raion == cbRaion.SelectedIndex).ToList();
                List<string> streets = new List<string>();
                cbStreet.Items.Add("Все улицы");
                foreach (address_subscriber res in residentialAddresses)
                {
                    if (res.id_street != null)
                    {
                        streets.Add(res.Streets.street);
                    }
                }
                streets = streets.Distinct().ToList();
                foreach (string street in streets)
                {
                    cbStreet.Items.Add(street);
                }
                cbStreet.SelectedIndex = 0;
            }
            else
            {
                b = true;
                cbStreet.IsEnabled = false;
                cbStreet.Items.Clear();
            }
          
        }

        private void cbStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Sort();
            if (cbStreet.SelectedIndex > 0)
            {
                cbHomes.Items.Clear();
                cbHomes.IsEnabled = true;
                List<address_subscriber> residentialAddresses = Base.EM.address_subscriber.Where(x => x.id_raion == cbRaion.SelectedIndex && x.id_street == cbStreet.SelectedIndex).ToList();
                List<string> houses = new List<string>();
                cbHomes.Items.Add("Все дома");
                foreach (address_subscriber res in residentialAddresses) 
                {
                    if (res.house != null)
                    {
                        houses.Add(Convert.ToString(res.house));
                    }
                }
                houses = houses.Distinct().ToList();
                foreach (string house in houses)
                {
                    cbHomes.Items.Add(house);
                }
                cbHomes.SelectedIndex = 0;
            }
            else
            {
                cbHomes.IsEnabled = false;
                cbHomes.Items.Clear();
            }
        }

        private void cbHomes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Sort();

        }

          
        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            b = true;
            Sort();
        }

        private void tbParsAc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
    }
}
