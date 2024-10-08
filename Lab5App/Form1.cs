﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Lab5App
{
    public partial class frmSinhVien : Form
    {
        private List<SinhVien> sinhVienList = new List<SinhVien>();
        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            // Gọi hàm đọc dữ liệu từ file khi form được tải
            DocTuFile();
        }



        private void rNu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tbTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mtbSoDT_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void tbDiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void mtbCMND_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void tbHoTenLot_TextChanged(object sender, EventArgs e)
        {

        }

        private void mtbtMSSV_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (IsInputFieldsValid())
            {
                AddStudentToListView();
                SaveListViewToFile();
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (IsInputFieldsValid())
            {
                UpdateStudentInListView();
                SaveListViewToFile();
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // Kiểm tra nếu không có sinh viên nào được chọn
            if (listView1.SelectedItems.Count == 0)
            {
                e.Cancel = true; // Hủy mở menu nếu không có mục nào được chọn
                return;
            }

            // Nếu có sinh viên được chọn, thêm item "Xóa" vào ContextMenuStrip
            contextMenuStrip1.Items.Clear(); // Xóa tất cả các mục cũ trong menu

            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Xóa sinh viên đã chọn");
            deleteMenuItem.Click += DeleteSelectedStudents; // Gắn sự kiện cho mục "Xóa"

            contextMenuStrip1.Items.Add(deleteMenuItem); // Thêm mục "Xóa" vào menu
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận thoát
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát chương trình không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng chọn Yes, thoát chương trình
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát chương trình
            }
            // Nếu người dùng chọn No, chương trình tiếp tục chạy và không làm gì
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có ít nhất một item được chọn
            if (listView1.SelectedItems.Count > 0)
            {
                // Chỉ gọi LoadSelectedStudentToInputFields() nếu có mục được chọn
                LoadSelectedStudentToInputFields();
            }
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Hiển thị form tìm kiếm
            frmTimKiem timKiemForm = new frmTimKiem();

            if (timKiemForm.ShowDialog() == DialogResult.OK)
            {
                // Lấy giá trị tìm kiếm từ form TimKiem
                string searchMSSV = timKiemForm.MSSV;
                string searchTen = timKiemForm.Ten;
                string searchLop = timKiemForm.Lop;

                // Gọi hàm để tìm kiếm và hiển thị kết quả
                TimKiemSinhVien(searchMSSV, searchTen, searchLop);
            }
        }


        private void TimKiemSinhVien(string mssv, string ten, string lop)
        {
            // Duyệt qua tất cả các item trong ListView
            foreach (ListViewItem item in listView1.Items)
            {
                bool mssvMatch = string.IsNullOrEmpty(mssv) || item.SubItems[0].Text.Contains(mssv);
                bool tenMatch = string.IsNullOrEmpty(ten) || item.SubItems[2].Text.Contains(ten);
                bool lopMatch = string.IsNullOrEmpty(lop) || item.SubItems[4].Text.Contains(lop);

                // Hiển thị item nếu nó khớp với điều kiện tìm kiếm
                item.BackColor = (mssvMatch && tenMatch && lopMatch) ? Color.Yellow : Color.White;
                item.ForeColor = (mssvMatch && tenMatch && lopMatch) ? Color.Red : Color.Black;
            }

            // Nếu không tìm thấy sinh viên nào, thông báo cho người dùng
            if (listView1.Items.Cast<ListViewItem>().All(i => i.BackColor == Color.White))
            {
                MessageBox.Show("Không tìm thấy sinh viên nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DocTuFileTxt()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(projectDirectory, @"..\..\Sinhvien.txt");
            filePath = System.IO.Path.GetFullPath(filePath);

            if (System.IO.File.Exists(filePath))
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);

                if (lines.Length > 0)
                {
                    foreach (string line in lines)
                    {
                        string[] fields = line.Split('\t');

                        if (fields.Length == 10) // Đảm bảo có đủ trường dữ liệu bao gồm cả môn học
                        {
                            ListViewItem item = new ListViewItem(fields[0]); // MSSV
                            item.SubItems.Add(fields[1]); // Họ và tên lót
                            item.SubItems.Add(fields[2]); // Tên
                            item.SubItems.Add(fields[3]); // Ngày sinh
                            item.SubItems.Add(fields[4]); // Lớp
                            item.SubItems.Add(fields[5]); // Số CMND
                            item.SubItems.Add(fields[6]); // Số điện thoại
                            item.SubItems.Add(fields[7]); // Địa chỉ
                            item.SubItems.Add(fields[8]); // Giới tính
                            item.SubItems.Add(fields[9]); // Môn học đã đăng ký

                            listView1.Items.Add(item);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("File Sinhvien.txt trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("File Sinhvien.txt không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DocTuFile()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(projectDirectory, @"..\..\Sinhvien.json");
            filePath = Path.GetFullPath(filePath);

            if (File.Exists(filePath))
            {
                try
                {
                    string jsonString = File.ReadAllText(filePath);
                    List<SinhVien> sinhVienList = JsonConvert.DeserializeObject<List<SinhVien>>(jsonString);

                    if (sinhVienList != null && sinhVienList.Count > 0)
                    {
                        listView1.Items.Clear(); // Xóa danh sách hiện tại
                        foreach (SinhVien sv in sinhVienList)
                        {
                            ListViewItem item = new ListViewItem(sv.MSSV);
                            item.SubItems.Add(sv.HoTenLot);
                            item.SubItems.Add(sv.Ten);
                            item.SubItems.Add(sv.NgaySinh.ToString("dd/MM/yyyy"));
                            item.SubItems.Add(sv.Lop);
                            item.SubItems.Add(sv.CMND);
                            item.SubItems.Add(sv.SDT);
                            item.SubItems.Add(sv.DiaChi);
                            item.SubItems.Add(sv.Phai ? "Nam" : "Nữ");
                            item.SubItems.Add(sv.MonHocDangKy);

                            listView1.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File Sinhvien.json không chứa dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi đọc file JSON: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("File Sinhvien.json không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //Chức năng phụ cho việc nhập xuất
        private bool IsInputFieldsValid()
        {
            if (string.IsNullOrWhiteSpace(mtbtMSSV.Text) || mtbtMSSV.Text.Length != 7 ||
                string.IsNullOrWhiteSpace(mtbCMND.Text) || mtbCMND.Text.Length != 9 ||
                string.IsNullOrWhiteSpace(mtbSoDT.Text) || mtbSoDT.Text.Length != 10 ||
                string.IsNullOrWhiteSpace(tbHoTenLot.Text) || string.IsNullOrWhiteSpace(tbTen.Text) ||
                string.IsNullOrWhiteSpace(cbLop.Text) || string.IsNullOrWhiteSpace(tbDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin với đúng định dạng:\n" +
                                "- MSSV: 7 chữ số\n- CMND: 9 chữ số\n- Số ĐT: 10 chữ số",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một môn học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }


        private bool IsAtLeastOneFieldFilled()
        {
            // Kiểm tra nếu có ít nhất một trường nhập liệu có dữ liệu
            return !string.IsNullOrWhiteSpace(mtbtMSSV.Text) ||
                   !string.IsNullOrWhiteSpace(tbHoTenLot.Text) ||
                   !string.IsNullOrWhiteSpace(tbTen.Text) ||
                   !string.IsNullOrWhiteSpace(cbLop.Text) ||
                   !string.IsNullOrWhiteSpace(mtbCMND.Text) ||
                   !string.IsNullOrWhiteSpace(mtbSoDT.Text) ||
                   !string.IsNullOrWhiteSpace(tbDiaChi.Text);
        }

        private void ClearInputFields()
        {
            mtbtMSSV.Clear();
            tbHoTenLot.Clear();
            tbTen.Clear();
            mtbCMND.Clear();
            mtbSoDT.Clear();
            tbDiaChi.Clear();
            cbLop.SelectedIndex = -1;
            rNam.Checked = true; // Mặc định chọn Nam
            dtpNgaySinh.Value = DateTime.Now; // Đặt lại giá trị mặc định cho DateTimePicker
        }

        // ok

        private void AddStudentToListView()
        {
            // Tạo một item mới trong ListView từ các trường nhập liệu
            ListViewItem item = new ListViewItem(mtbtMSSV.Text);
            item.SubItems.Add(tbHoTenLot.Text);
            item.SubItems.Add(tbTen.Text);
            item.SubItems.Add(dtpNgaySinh.Value.ToString("dd/MM/yyyy"));
            item.SubItems.Add(cbLop.Text);
            item.SubItems.Add(mtbCMND.Text);
            item.SubItems.Add(mtbSoDT.Text);
            item.SubItems.Add(tbDiaChi.Text);
            item.SubItems.Add(rNam.Checked ? "Nam" : "Nữ");

            // Thêm các môn học đã đăng ký vào cột Môn Học
            string monHocDangKy = string.Join(",", checkedListBox1.CheckedItems.OfType<string>());
            item.SubItems.Add(monHocDangKy);

            // Thêm item vào ListView
            listView1.Items.Add(item);

            // Xóa trường nhập liệu sau khi thêm
            ClearInputFields();
        }


        private void SetupContextMenuForListView()
        {
            contextMenuStrip1 = new ContextMenuStrip();

            ToolStripMenuItem removeItem = new ToolStripMenuItem("Xóa sinh viên đã chọn");
            removeItem.Click += RemoveSelectedStudents; // Event handler

            contextMenuStrip1.Items.Add(removeItem);

            // Assign the context menu to the ListView
            listView1.ContextMenuStrip = contextMenuStrip1;
        }
        private void RemoveSelectedStudents(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận xóa
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa các sinh viên đã chọn?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                // Tạo danh sách các chỉ số cần xóa
                List<int> indexesToRemove = new List<int>();
                foreach (ListViewItem item in listView1.CheckedItems)
                {
                    indexesToRemove.Add(item.Index);
                }

                // Xóa từ chỉ số cao nhất để tránh lỗi khi xóa
                indexesToRemove.Sort();
                indexesToRemove.Reverse();

                foreach (int index in indexesToRemove)
                {
                    listView1.Items.RemoveAt(index);
                    sinhVienList.RemoveAt(index);
                }

                // Lưu thay đổi vào file JSON
                SaveListViewToFile();

                MessageBox.Show("Đã xóa các sinh viên đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        private void RemoveSubject()
        {
            if (checkedListBox1.SelectedIndex >= 0)
            {
                checkedListBox1.Items.RemoveAt(checkedListBox1.SelectedIndex);
            }
        }


        private void UpdateStudentInListView()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Lấy dòng được chọn trong ListView
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Cập nhật thông tin từ các trường nhập liệu
                selectedItem.SubItems[0].Text = mtbtMSSV.Text;
                selectedItem.SubItems[1].Text = tbHoTenLot.Text;
                selectedItem.SubItems[2].Text = tbTen.Text;
                selectedItem.SubItems[3].Text = dtpNgaySinh.Value.ToString("dd/MM/yyyy");
                selectedItem.SubItems[4].Text = cbLop.Text;
                selectedItem.SubItems[5].Text = mtbCMND.Text;
                selectedItem.SubItems[6].Text = mtbSoDT.Text;
                selectedItem.SubItems[7].Text = tbDiaChi.Text;
                selectedItem.SubItems[8].Text = rNam.Checked ? "Nam" : "Nữ";

                // Thông báo thành công
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa trường nhập liệu sau khi cập nhật
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sinh viên để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSelectedStudentToInputFields()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Lấy dòng được chọn trong ListView
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Đưa dữ liệu lên các trường nhập liệu
                mtbtMSSV.Text = selectedItem.SubItems[0].Text;
                tbHoTenLot.Text = selectedItem.SubItems[1].Text;
                tbTen.Text = selectedItem.SubItems[2].Text;
                dtpNgaySinh.Value = DateTime.ParseExact(selectedItem.SubItems[3].Text, "dd/MM/yyyy", null);
                cbLop.Text = selectedItem.SubItems[4].Text;
                mtbCMND.Text = selectedItem.SubItems[5].Text;
                mtbSoDT.Text = selectedItem.SubItems[6].Text;
                tbDiaChi.Text = selectedItem.SubItems[7].Text;

                // Giới tính
                if (selectedItem.SubItems[8].Text == "Nam")
                {
                    rNam.Checked = true;
                }
                else
                {
                    rNu.Checked = true;
                }

                // Môn học (tích vào các môn học đã đăng ký)
                string[] registeredSubjects = selectedItem.SubItems[9].Text.Split(','); // Giả sử môn học ở cột 9

                // Bỏ tích toàn bộ môn học trước khi cập nhật
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }

                // Duyệt qua các môn học đã đăng ký và tích vào CheckedListBox
                foreach (string subject in registeredSubjects)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.Items[i].ToString().Trim() == subject.Trim())
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }





        private void SaveListViewToFileTxt()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(projectDirectory, @"..\..\Sinhvien.txt");
            filePath = System.IO.Path.GetFullPath(filePath);

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath))
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    // Lấy danh sách môn học đã tích trong CheckedListBox
                    List<string> selectedSubjects = new List<string>();
                    for (int i = 0; i < checkedListBox1.CheckedItems.Count; i++)
                    {
                        selectedSubjects.Add(checkedListBox1.CheckedItems[i].ToString());
                    }

                    string subjects = string.Join(", ", selectedSubjects);

                    string line = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
                        item.SubItems[0].Text,  // MSSV
                        item.SubItems[1].Text,  // Họ và tên lót
                        item.SubItems[2].Text,  // Tên
                        item.SubItems[3].Text,  // Ngày sinh
                        item.SubItems[4].Text,  // Lớp
                        item.SubItems[5].Text,  // Số CMND
                        item.SubItems[6].Text,  // Số điện thoại
                        item.SubItems[7].Text,  // Địa chỉ liên lạc
                        item.SubItems[8].Text,  // Giới tính (Nam/Nữ)
                        subjects                 // Môn học đăng ký
                    );
                    writer.WriteLine(line);
                }
            }
        }


        private void SaveListViewToFile()
        {
            string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(projectDirectory, @"..\..\Sinhvien.json");
            filePath = Path.GetFullPath(filePath);

            List<SinhVien> sinhVienList = new List<SinhVien>();

            foreach (ListViewItem item in listView1.Items)
            {
                // Lấy các giá trị SubItems ra trước để dễ đọc và gán
                var mssv = item.SubItems[0].Text;
                var hoTenLot = item.SubItems[1].Text;
                var ten = item.SubItems[2].Text;
                var ngaySinhText = item.SubItems[3].Text;
                var lop = item.SubItems[4].Text;
                var cmnd = item.SubItems[5].Text;
                var sdt = item.SubItems[6].Text;
                var diaChi = item.SubItems[7].Text;
                var phaiText = item.SubItems[8].Text;
                var monHoc = item.SubItems[9].Text;

                // Parse ngày sinh
                DateTime ngaySinh;
                if (!DateTime.TryParseExact(ngaySinhText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out ngaySinh))
                {
                    MessageBox.Show("Định dạng ngày sinh không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                // Tạo đối tượng SinhVien và thêm vào danh sách
                SinhVien sv = new SinhVien
                {
                    MSSV = mssv,
                    HoTenLot = hoTenLot,
                    Ten = ten,
                    NgaySinh = ngaySinh,
                    Lop = lop,
                    CMND = cmnd,
                    SDT = sdt,
                    DiaChi = diaChi,
                    Phai = phaiText == "Nam",  // Nếu là "Nam", giá trị sẽ là true, nếu không là false
                    MonHocDangKy = monHoc
                };
                sinhVienList.Add(sv);
            }

            try
            {
                // Chuyển danh sách sang định dạng JSON và ghi vào file
                string jsonString = JsonConvert.SerializeObject(sinhVienList, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu file JSON: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        //Delete multi selected SinhVien
        private void xoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Duyệt qua các item trong ListView
            for (int i = listView1.CheckedItems.Count - 1; i >= 0; i--)
            {
                // Xóa item nếu được chọn (checkbox được đánh dấu)
                listView1.Items.Remove(listView1.CheckedItems[i]);
            }

            // Sau khi xóa, lưu lại danh sách vào file
            SaveListViewToFile();
        }

        private void DeleteSelectedStudents(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sinh viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận việc xóa
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa các sinh viên đã chọn?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                // Thu thập các chỉ mục của sinh viên được chọn để xóa
                List<int> indexesToRemove = new List<int>();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    indexesToRemove.Add(item.Index);
                }

                // Xóa sinh viên từ chỉ mục cao nhất để tránh lỗi khi cập nhật danh sách
                indexesToRemove.Sort();
                indexesToRemove.Reverse();

                foreach (int index in indexesToRemove)
                {
                    listView1.Items.RemoveAt(index);
                    sinhVienList.RemoveAt(index); // Xóa khỏi danh sách sinh viên
                }

                // Lưu lại dữ liệu sau khi xóa
                SaveListViewToFile();

                MessageBox.Show("Đã xóa các sinh viên đã chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
