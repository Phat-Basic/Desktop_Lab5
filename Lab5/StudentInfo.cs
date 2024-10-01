using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class StudentInfo
    {
        public string MSSV {  get; set; }
        public string HoTen { get; set; }
        public int Tuoi {  get; set; }
        public double Diem {  get; set; }
        public bool TonGiao {  get; set; }
        
        public StudentInfo(string mssv, string HoTen, int Tuoi, double Diem, bool TonGiao) 
        { 
            this.MSSV = mssv;
            this.HoTen = HoTen; 
            this.Tuoi = Tuoi;
            this.Diem = Diem;
            this.TonGiao = TonGiao;    
        }

      
    }
}
