using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5App
{
    public class SinhVien
    {
        public string MSSV { get; set; }
        public string HoTenLot { get; set; }
        public string Ten { get; set; }
        public DateTime NgaySinh { get; set; }
        public string Lop { get; set; }
        public int CMND { get; set; }
        public int SDT { get; set; }
        public string DiaChi { get; set; }
        public bool Phai { get; set; }
        public string MonHocDangKy { get; set; } // Thêm thuộc tính Môn học

        public SinhVien()
        {

        }

        public SinhVien(string mSSV, string hoTenLot, string ten, DateTime ngaySinh, string lop, int cMND, int sDT, string diaChi, bool phai, string monHocDangKy)
        {
            MSSV = mSSV;
            HoTenLot = hoTenLot;
            Ten = ten;
            NgaySinh = ngaySinh;
            Lop = lop;
            CMND = cMND;
            SDT = sDT;
            DiaChi = diaChi;
            Phai = phai;
            MonHocDangKy = monHocDangKy;
        }
    }

}
