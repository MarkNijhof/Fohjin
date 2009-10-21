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

        public event Action OnCreateNewClient;
        public event Action OnOpenSelectedClient;
        public event Action OnRefresh;

        private void RegisterCLientEvents()
        {
            addANewClientToolStripMenuItem.Click += (e, s) => OnCreateNewClient();
            _refreshButton.Click += (e, s) => OnRefresh();
            _clients.Click += (e, s) => OnOpenSelectedClient();
        }

        public IEnumerable<Client> Clients
        {
            get { return (IEnumerable<Client>)_clients.DataSource; }
            set { _clients.DataSource = value; }
        }

        public Client GetSelectedClient()
        {
            return (Client)_clients.SelectedItem;
        }
    }
}