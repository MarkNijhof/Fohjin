using System;
using System.Collections.Generic;
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

        private void CreateNewClientButton_Click(object sender, EventArgs e)
        {
            _presenter.CreateNewClient();
        }

        private void _clients_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter.OpenSelectedClient();
        }
    }
}