using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class ClientSearchForm : Form, IClientSearchFormView
    {
        private IClientSearchFormPresenter _presenter;

        public ClientSearchForm()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
        }

        public IEnumerable<Client> Clients
        {
            get { return (IEnumerable<Client>)_clients.DataSource; }
            set { _clients.DataSource = value; }
        }

        public void SetPresenter(IClientSearchFormPresenter clientSearchFormPresenter)
        {
            _presenter = clientSearchFormPresenter;
        }

        public Client GetSelectedClient()
        {
            return (Client)_clients.SelectedItem;
        }

        private void _clients_DoubleClick(object sender, EventArgs e)
        {
            _presenter.OpenSelectedClient();
        }

        private void addANewClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.CreateNewClient();
        }

        private void refreshExistingClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _presenter.Refresh();
        }
    }
}