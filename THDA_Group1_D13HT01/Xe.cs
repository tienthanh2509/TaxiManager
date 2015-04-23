using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THDA_Group1_D13HT01
{
    class Xe
    {
        private int maxe;
        public int Maxe
        {
            get { return maxe; }
            set { maxe = value; }
        }

        private string soxe;
        public string Soxe
        {
            get { return soxe; }
            set { soxe = value; }
        }

        private string tentaixe;
        public string Tentaixe
        {
            get { return tentaixe; }
            set { tentaixe = value; }
        }

        private int loaixe;
        public int Loaixe
        {
            get { return loaixe; }
            set { loaixe = value; }
        }
        public Xe(int a = 0, string b = "", string c = "", int d = 0)
        {
            maxe = a;
            soxe = b;
            tentaixe = c;
            loaixe = d;
        }

        public void Nhap()
        {
            try
            {

                Console.Write("Mã tài xế: "); maxe = Convert.ToInt32(Console.ReadLine());
                Console.Write("Tên tài xế: "); tentaixe = Console.ReadLine();
                Console.Write("Loại xe: "); loaixe = Convert.ToInt32(Console.ReadLine());
                Console.Write("Số xe: "); soxe = Console.ReadLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Bạn nhập không đúng định dạng!!!");
            }
        }

        public void Xuat()
        {
            Console.WriteLine("{0,-7} | {1,-15} | {2,-32} | {3,-7}", maxe, soxe, tentaixe, loaixe);
        }

        public void Xuat2(DSLoaiXe ds)
        {
            Console.WriteLine("{0,-7} | {1,-15} | {2,-32} | {3,-14}", maxe, soxe, tentaixe, ds.getNameByID(loaixe));
        }

        public string XuatS()
        {
            return String.Format("{0,-7} | {1,-15} | {2,-32} | {3,-7}\n", maxe, soxe, tentaixe, loaixe);
        }

        public string Xuat2S(DSLoaiXe ds)
        {
            return String.Format("{0,-7} | {1,-15} | {2,-32} | {3,-14}\n", maxe, soxe, tentaixe, ds.getNameByID(loaixe));
        }
    }
}
