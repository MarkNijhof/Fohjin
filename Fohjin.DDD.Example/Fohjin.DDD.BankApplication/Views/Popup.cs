namespace Fohjin.DDD.BankApplication.Views
{
    public partial class Popup : ViewFormBase, IPopupView
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
