using System;
using System.Windows.Forms;

namespace Lab5App
{
    public partial class frmTimKiem : Form
    {
        public string MSSV { get; private set; }
        public string Ten { get; private set; }
        public string Lop { get; private set; }

        public frmTimKiem()
        {
            InitializeComponent();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Lấy các giá trị người dùng đã nhập
            MSSV = textBoxMSSV.Text.Trim();
            Ten = textBoxTen.Text.Trim();
            Lop = textBoxLop.Text.Trim();

            // Đóng form và trả kết quả cho form chính
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Đóng form mà không làm gì
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
