using System.Windows.Forms;

namespace Fohjin.DDD.BankApplication.Views
{
    public partial class Popup : Form, IPopupView
    {
        public Popup()
        {
            InitializeComponent();
        }

        public string Exception
        {
            set { _exception.Text = value; }
        }

        public string Message
        {
            set { _message.Text = value; }
        }
    }
}
