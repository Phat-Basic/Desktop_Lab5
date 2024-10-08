using System;

namespace Lab5App
{
    public class SinhVien
    {
        public string MSSV { get; set; }
        public string HoTenLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
        public string CMND { get; set; }  // CMND và SDT nên là string do chứa số dài
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public bool Phai { get; set; }
        public string MonHocDangKy { get; set; }  // Danh sách môn học dưới dạng chuỗi

        public SinhVien() { }

        public SinhVien(string mssv, string hoTenLot, string ten, DateTime ngaySinh, string lop, string cmnd, string sdt, string diaChi, bool phai, string monHocDangKy)
        {
            MSSV = mssv;
            HoTenLot = hoTenLot;
            Ten = ten;
            NgaySinh = ngaySinh;
            Lop = lop;
            CMND = cmnd;
            SDT = sdt;
            DiaChi = diaChi;
            Phai = phai;
            MonHocDangKy = monHocDangKy;
        }
    }
}
