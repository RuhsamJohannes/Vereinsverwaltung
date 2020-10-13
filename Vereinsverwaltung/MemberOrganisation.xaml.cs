using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vereinsverwaltung.Model;

namespace Vereinsverwaltung.Wpf
{
    /// <summary>
    /// Interaction logic for MemberOrganisation.xaml
    /// </summary>
    public partial class MemberOrganisation : Window
    {
        private Member SelectedMember { get; }

        public MemberOrganisation()
        {
            InitializeComponent();
            Loaded += AddMember_Loaded;
        }

        public MemberOrganisation(Member selectedMember)
        {
            InitializeComponent();
            SelectedMember = selectedMember;
            Loaded += EditMember_Loaded;
        }

        private void AddMember_Loaded(object sender, RoutedEventArgs e)
        {
            btnConfirm.Click += new RoutedEventHandler(BtnConfirm_Click);
            btnCancel.Click += new RoutedEventHandler(BtnCancel_Click);

            txtTitle.Text = "Neues Mitglied Hinzufügen";
            DataContext = new Member();
        }

        private void EditMember_Loaded(object sender, RoutedEventArgs e)
        {
            btnConfirm.Click += new RoutedEventHandler(BtnConfirm_Click);
            btnCancel.Click += new RoutedEventHandler(BtnCancel_Click);

            txtTitle.Text = $"Mitglied {SelectedMember.FullName} ändern";
            DataContext = new Member() { FirstName = SelectedMember.FirstName, LastName = SelectedMember.LastName, Birthday = SelectedMember.Birthday };
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMember != null)
            {
                Repository.GetInstance().RemoveMember(SelectedMember);
            }

            Repository.GetInstance().AddMember(DataContext as Member);
            Close();
        }
    }
}
