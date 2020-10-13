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
using Vereinsverwaltung.Model;
using Vereinsverwaltung.Wpf;

namespace Vereinsverwaltung
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Member> _members { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Repository repository = Repository.GetInstance();
            _members = repository.GetAllMembers();
            lbxMembers.ItemsSource = _members;

            btnNew.Click += BtnNew_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Member selectedMember = lbxMembers.SelectedItem as Member;
            if (selectedMember == null)
            {
                MessageBox.Show("Sie müssen ein Mitglied auswählen");
            }
            else
            {
                Repository.GetInstance().RemoveMember(selectedMember);
                _members = Repository.GetInstance().GetAllMembers();
                lbxMembers.ItemsSource = _members;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Member selectedMember = lbxMembers.SelectedItem as Member;
            if (selectedMember == null)
            {
                MessageBox.Show("Sie müssen ein Mitglied auswählen");
            }
            else
            {
                MemberOrganisation memberOrganisation = new MemberOrganisation(selectedMember);
                memberOrganisation.ShowDialog();

                _members = Repository.GetInstance().GetAllMembers();
                lbxMembers.ItemsSource = _members;
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            MemberOrganisation memberOrganisation = new MemberOrganisation();
            memberOrganisation.ShowDialog();

            _members = Repository.GetInstance().GetAllMembers();
            lbxMembers.ItemsSource = _members;
        }
    }
}
