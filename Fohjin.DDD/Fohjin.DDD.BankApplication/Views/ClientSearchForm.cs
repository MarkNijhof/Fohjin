using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class ClientSearchForm : Form, IClientSearchFormView
    {
        public ClientSearchForm()
        {
            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            RegisterCLientEvents();
        }

        public event EventAction OnCreateNewClient;
        public event EventAction OnOpenSelectedClient;

        private void RegisterCLientEvents()
        {
            addANewClientToolStripMenuItem.Click += (s, e) => OnCreateNewClient();
            _clients.Click += (s, e) => OnOpenSelectedClient();
        }

        public IEnumerable<ClientReport> Clients
        {
            get { return (IEnumerable<ClientReport>)_clients.DataSource; }
            set { _clients.DataSource = value; }
        }

        public ClientReport GetSelectedClient()
        {
            return (ClientReport)_clients.SelectedItem;
        }
    }
}