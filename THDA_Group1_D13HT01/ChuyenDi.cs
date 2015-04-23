using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace THDA_Group1_D13HT01
{
    class ChuyenDi
    {
        private int maxe;
        public int Maxe
        {
            get { return maxe; }
            set { maxe = value; }
        }

        private float quangduong;
        public float Quangduong
        {
            get { return quangduong; }
            set { quangduong = value; }
        }

        public ChuyenDi()
        {
            maxe = 0;
            quangduong = 0;
        }

        public ChuyenDi(int a, float b)
        {
            maxe = a;
            quangduong = b;
        }

        public static double ThanhTien(float km)
        {
            if (km > 0 && km <= 2)
                return 12000;
            else
                return (12000 + ((km - 2) * 15000));
        }

        public double getThanhTien()
        {
            return ChuyenDi.ThanhTien(this.quangduong);
        }

        public void Nhap()
        {
            try
            {
                Console.Write("Mã xe: ");
                maxe = Convert.ToInt32(Console.ReadLine());
                Console.Write("Quãng Đường: "); quangduong = float.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Bạn nhập không đúng định dạng!!!");
            }
        }

        public void Xuat()
        {
            double thanhtien = ChuyenDi.ThanhTien(quangduong);
            Console.WriteLine("{0,12} | {1,17} | {2,10}", maxe, quangduong, string.Format("{0:0,0.#}", thanhtien));
        }

        public string XuatS()
        {
            double thanhtien = ChuyenDi.ThanhTien(quangduong);
            return string.Format("{0,12} | {1,17} | {2,10}", maxe, quangduong, string.Format("{0:0,0.#}", thanhtien));
        }
    }
}
