using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Diagnostics;

/**
 * Class Quản lý Các Loại Xe
 * 
 * @author: Phạm Tiến Thành
 * 
 */

namespace THDA_Group1_D13HT01
{
    class DSLoaiXe
    {
        private string[] dsLoai; // Mảng lưu tên loại xe, chỉ số mảng sẽ là mã loại xe
        private int n; // Tổng loại xe
        private int maxsize = Properties.Settings.Default.MAX_SL_LOAIXE; // Giới hạn lưu trữ tối đa
        private string datafile = Properties.Settings.Default.FILE_DANHSACHLOAIXE; // Địa chỉ file lưu dữ liệu

        public DSLoaiXe()
        {
            dsLoai = new String[maxsize];
        }

        public int getN()
        {
            return n;
        }

        public string[] getDSLoai()
        {
            return dsLoai;
        }

        // Nhập các loại xe từ bàn phím
        public void Nhap()
        {
            Console.Write("Nhập số lượng loại xe: ");
            try
            {
                do
                {
                    n = int.Parse(Console.ReadLine());

                    if (n < 0 || n > maxsize)
                        System.Console.WriteLine("N quá lớn hoặc quá nhỏ, 0 <= N <= {0}.\nNhập lại n:", maxsize);

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
                Console.Write("Nhập tên loại xe thứ {0}: ", i + 1);
                string s;
                s = Console.ReadLine();
                dsLoai[i] = s;
            }

            Console.WriteLine("Đã nhập {0} loại xe", n);
        }

        // Nhập hàng loạt các loại xe từ file đã lưu
        public void Nhap_File()
        {
            try
            {
                StreamReader myfile = File.OpenText(datafile);
                n = int.Parse(myfile.ReadLine());
                Console.WriteLine("Số lượng loại xe: " + n);

                if (n < 1)
                    return;

                for (int i = 0; i < n; i++)
                {
                    string[] separators = { "\t" };
                    string value = myfile.ReadLine();
                    string[] words = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    // Ví dụ: 0 Thuong, mỗi thuộc tính cách nhau bởi phím TAB
                    dsLoai[int.Parse(words[0])] = words[1]; // Chú ý, việc nhập xuất file nên làm tự động, tránh can thiệp vào file dữ liệu dễ dẫn tới hỏng
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

        // Xuất các loại xe hiện có
        public void Xuat()
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện có không có loại xe nào trong danh sách!");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(String.Format("{0,-15} | {1,-20} ", "Mã loại xe", "Tên loại xe"));
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;

            for (int i = 0; i < n; i++)
            {
                if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(String.Format("{0,-15} | {1,-20} ", i, dsLoai[i]));
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Tổng: {0}.", n);
        }

        // Xuất các loại xe hiện có
        public string XuatS()
        {
            if (n == 0)
                return ("Hiện có không có loại xe nào trong danh sách!");

            string s = "";
            s = String.Format("{0,-15} | {1,-20} \n", "Mã loại xe", "Tên loại xe");

            for (int i = 0; i < n; i++)
                s += String.Format(String.Format("{0,-15} | {1,-20} \n", i, dsLoai[i]));

            s += "---------------------------\n";
            s += String.Format("Tổng: {0}.\n", n);

            return s;
        }

        // Xuất các loại xe hiện có vào file
        public void Xuat_File()
        {
            if (n == 0)
            {
                Console.WriteLine("Hiện không có loại xe nào trong danh sách!");
                return;
            }

            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(datafile);
                file.WriteLine("{0}", n);
                for (int i = 0; i < n; i++)
                {
                    file.WriteLine(String.Format("{0}\t{1}", i, dsLoai[i]));
                }
                file.Close();

                Console.WriteLine("Đã lưu vào: {0}", Path.GetFullPath(datafile));
                Process notePad = new Process();
                //notePad.StartInfo.FileName = "notepad.exe";
                notePad.StartInfo.FileName = Path.GetFullPath(datafile);
                //notePad.StartInfo.Arguments = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                notePad.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
            }
        }

        /*
         * Trả về tên loại ứng với mã loại
         * 
	     * @param	int	c	Mã loại
	     * @return	string
         */
        public string getNameByID(int c)
        {
            string rt = "";

            try
            {
                rt = dsLoai[c];
            }
            catch (Exception ex)
            {
                rt = "[N/a]";
                ErrorLogs el = new ErrorLogs(ex.ToString());
                el.write();
            }

            return rt;
        }

        /*
         * Trả về mã loại ứng với tên
         * 
         * @param	string	c	Tên
         * @return	int
         */
        public int getIDByName(string c)
        {
            int rt = -1;

            for (int i = 0; i < n; i++)
                if (dsLoai[i] == c)
                {
                    // C1: return i;
                    // C2:
                    rt = i;
                    break;
                }

            return rt;
        }

        /*
         * Kiểm tra mã loại xe có hợp lệ hay không
         * 
         * @param	int	c	Mã loại
         * @return	bool
         */
        public bool is_valid(int c)
        {
            // Mã hợp lệ là mã lớn hơn bằng 0 và nhỏ hơn n, [0, n)
            if (c >= 0 && c < n)
                return true;
            else
                return false;
        }
    }
}
