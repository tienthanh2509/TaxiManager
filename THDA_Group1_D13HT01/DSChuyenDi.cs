using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace THDA_Group1_D13HT01
{
    class DSChuyenDi
    {
        private ChuyenDi[] dsCD; // Mảng quản lý đối tượng chuyến đi
        private int n; // Tổng loại chuyến đi
        private int maxsize = Properties.Settings.Default.MAX_SL_CHUYENDI; // Giới hạn lưu trữ tối đa
        private string datafile = Properties.Settings.Default.FILE_DANHSACHCHUYENDI; // Địa chỉ file lưu dữ liệu
        private const string FILE_REPORT = "BaoCaoTaiChinh.txt";

        public DSChuyenDi()
        {
            dsCD = new ChuyenDi[maxsize];
        }

        public int getN()
        {
            return n;
        }

        public ChuyenDi getCDbyID(int id)
        {
            return dsCD[id];
        }

        public ChuyenDi[] getDSChuyenDi()
        {
            return dsCD;
        }

        public void Nhap()
        {
            Console.Write("Nhập số lượng chuyến đi: ");
            try
            {
                do
                {
                    n = int.Parse(Console.ReadLine());

                    if (n < 0 || n > maxsize)
                        System.Console.WriteLine("Số lượng chuyến đi (N)  quá lớn hoặc quá nhỏ, 0 <= N <= {0}.\nNhập lại N:", maxsize);

                } while (n < 0 || n > maxsize);
            }
            catch (Exception ex)
            {
                n = 0;
                ErrorLogs el = new ErrorLogs(ex.ToString());
                el.write();
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Nhập tên thông tin chuyến đi thứ {0}: ", i + 1);
                dsCD[i] = new ChuyenDi();
                dsCD[i].Nhap();
            }

            Console.WriteLine("Đã nhập {0} chuyến đi", n);
        }

        public void Nhap_File()
        {
            try
            {
                StreamReader myfile = File.OpenText(datafile);
                n = int.Parse(myfile.ReadLine());

                Console.WriteLine("Số lượng chuyến đi: " + n);

                if (n < 0 || n > maxsize)
                    throw new System.AggregateException("Số lượng chuyến đi (N)  quá lớn hoặc quá nhỏ, 0 <= N <= {0}.");

                for (int i = 0; i < n; i++)
                {
                    string[] separators = { "\t" };
                    string value = myfile.ReadLine();
                    string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    // Ví dụ: 58-A6.7993	88.4, mỗi thuộc tính cách nhau bởi phím TAB
                    dsCD[i] = new ChuyenDi(int.Parse(words[0]), float.Parse(words[1]));
                }

                myfile.Close();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("\n{0} không tồn tại", datafile);
            }
            catch (IOException)
            {
                Console.WriteLine("\n{0} đã tồn tại", datafile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi không rõ đã xảy ra");
                ErrorLogs el = new ErrorLogs(ex.ToString());
                el.write();
            }
        }

        public void Xuat()
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện có không chuyến đi nào!");
                return;
            }

            Console.WriteLine("{0,12}  {1,12}  {2,17}  {3,10}", "STT", "Số xe", "Quãng đường (KM)", "Thành Tiền (VNĐ)");
            for (int i = 0; i < n; i++)
                Console.WriteLine("{0,12}  {1}", i + 1, dsCD[i].XuatS());
            Console.WriteLine("\n---------------------------\nDanh sách này có {0} chuyến đi.", n);
        }

        public string XuatS()
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện có không chuyến đi nào!");
                return "";
            }

            string s = "";
            s += String.Format("{0,12}  {1,12}  {2,17}  {3,10}\n", "STT", "Số xe", "Quãng đường (KM)", "Thành Tiền (VNĐ)");
            for (int i = 0; i < n; i++)
                s += String.Format("{0,12}  {1}\n", i + 1, dsCD[i].XuatS());
            s += String.Format("\n---------------------------\nDanh sách này có {0} chuyến đi.\n", n);

            return s;
        }

        public void Xuat_File_BaoCao()
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện có không chuyến đi nào!");
                return;
            }

            System.IO.StreamWriter file = new System.IO.StreamWriter(FILE_REPORT);
            file.WriteLine("{0,12}  {1,12}  {2,17}  {3,10}", "STT", "Số xe", "Quãng đường (KM)", "Thành Tiền (VNĐ)");
            for (int i = 0; i < n; i++)
                file.WriteLine("{0,12}  {1}", i + 1, dsCD[i].XuatS());
            file.WriteLine("\n---------------------------\nDanh sách này có {0} chuyến đi.", n);
            file.Close();
            Console.WriteLine("Đã lưu vào: {0}", Path.GetFullPath(FILE_REPORT));
        }

        public void Xuat_File()
        {
            if (n <= 0)
            {
                Console.WriteLine("Hiện có không có loại xe nào trong danh sách!");
                return;
            }

            try
            {
                StreamWriter file = new StreamWriter(datafile);
                file.WriteLine("{0}", n);
                for (int i = 0; i < n; i++)
                {
                    file.WriteLine(String.Format("{0}\t{1}", dsCD[i].getMaXe(), dsCD[i].getQuangDuong()));
                }
                file.Close();

                Console.WriteLine("Đã lưu vào: {0}", Path.GetFullPath(datafile));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
            }
        }
    }
}
