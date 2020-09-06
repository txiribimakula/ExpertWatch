using System;
using System.Windows.Forms;

namespace Txiribimakula.ExpertWatch.Views
{
    public partial class MyUserControl : UserControl
    {
        public MyUserControl() {
            InitializeComponent();
            textBox1.LostFocus += textBox1_Leave;
        }

        internal OptionPageCustom optionsPage;

        public void Initialize() {
            textBox1.Text = optionsPage.OptionString;
        }

        private void textBox1_Leave(object sender, EventArgs e) {
            optionsPage.OptionString = textBox1.Text;
        }
    }
}
