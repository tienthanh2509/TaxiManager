using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Hỗ trợ hiển thị tiếng Việt
using System.Runtime.InteropServices;
// -/-Hỗ trợ hiển thị tiếng Việt

using System.IO;
using System.Diagnostics;

namespace THDA_Group1_D13HT01
{
    class Program
    {
        // Hỗ trợ hiển thị tiếng Việt
        [DllImport("kernel32.dll")]
        static extern bool SetConsoleOutputCP(uint wCodePageID);
        // -/-Hỗ trợ hiển thị tiếng Việt

        static void Main(string[] args)
        {
            // Kích thước của sổ Console
            Console.SetWindowSize(140, 30);
            // Tên cửa sổ
            Console.Title = "TAXI Manager";
            // Đặt màu
            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
            // Hỗ trợ hiển thị tiếng Việt
            SetConsoleOutputCP(65001);
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            // -/-Hỗ trợ hiển thị tiếng Việt

            // Khởi tạo Class
            DSLoaiXe loaixe = new DSLoaiXe();
            //loaixe.Nhap();
            //loaixe.Nhap_File();
            //loaixe.Xuat();
            //loaixe.Xuat_File();
            //Xe xe = new Xe();
            //xe.Nhap();
            //xe.Xuat();
            DSXe dsxe = new DSXe();
            //dsxe.Nhap();
            //dsxe.Nhap_File();
            //dsxe.Xuat();
            //dsxe.Xuat2(LoaiXe);
            //dsxe.Xuat_File();

            DSChuyenDi dscd = new DSChuyenDi();
            //dscd.Nhap_File();
            //dscd.Xuat();

            // Auto load \a ~ beep
            Console.WriteLine(">>>\aDữ liệu được nạp tự động để phục vụ cho quá trình kiểm thử!");
            loaixe.Nhap_File();
            dsxe.Nhap_File();
            dscd.Nhap_File();
            Console.Clear();

            //------------------------------------------------------------------------------------
            int chon = -1;
            do
            {
                //----------- Thông báo chào mừng
                Console.WriteLine("*******************************************************************");
                Console.WriteLine("*              Chào mừng đến với chương trình quản lý xe taxi     *");
                Console.WriteLine("*            được thực hiện bới nhóm 1, lớp D13HT01               *");
                Console.WriteLine("*******************************************************************");

                //----------- Menu các chức năng
                Console.WriteLine("1. Nhập vào danh sách các chiếc xe, danh sách các chuyến đi, phân loại xe.");
                Console.WriteLine("2. Tính Thành tiền cho mỗi chuyến đi.");
                Console.WriteLine("3. Tính Tổng số tiền trong các chuyến đi của tài xế có tên được nhập từ bàn phím.");
                Console.WriteLine("4. Xuất báo cáo tổng hợp ra màn hình/ file.");
                Console.WriteLine("5. Tìm xe có số xe X (X được nhập vào).");
                Console.WriteLine("6. Đếm số lượng xe theo từng loại.");
                Console.WriteLine("7. Cho biết xe nào có số km đi nhiều nhất.");
                Console.WriteLine("8. Cho biết tài xế nào lái xe có số tiền của các chuyến đi nhiều nhất.");
                Console.WriteLine("9. Cho biết thông tin chuyến đi có số km lớn nhất (số xe, loại xe, tên tài xế, số km, thành tiền).");
                Console.WriteLine("10. Sắp xếp danh sách tăng dần theo số xe.");
                Console.WriteLine("11. Với mỗi loại xe, cho biết xe nào được chạy nhiều nhất (số km nhiều nhất).");
                Console.WriteLine("------------------------------------------------------------");

                //----------- Các chức năng khác
                Console.WriteLine("30. Xuất dữ liệu ra màn hình/file (danh sách các chiếc xe, danh sách các chuyến đi, phân loại xe).");

                Console.WriteLine("\n>>> Thống kê sơ bộ [Đã bật nạp dữ liệu tự động từ file text]");
                Console.WriteLine("Tổng loại xe: {0}", loaixe.getN());
                Console.WriteLine("Tổng số xe: {0}", dsxe.getN());
                Console.WriteLine("Tổng số chuyến đi: {0}", dscd.getN());
                Console.WriteLine("------------------------------------------------------------");

                Console.WriteLine("0. Thoát chương trình.");

                //-----------
                Console.Write("Bạn chọn: ");
                try
                {
                    chon = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    /// Nhập sai
                    chon = -1;
                    ErrorLogs el = new ErrorLogs(ex.ToString());
                    el.write();
                }

                // Xử lý các chức năng

                // @TODO thiếu việc kiểm tra dữ liệu, phải có dữ liệu mới được tiến hành các chức năng nào đó
                // Chia menu ra 2 phần, 1 phần là khi chưa nạp dữ liệu, phần 2 là khi đã có dữ liệu mới cho phép thao tác

                /**
                 * Nhập liệu vào chương trình
                 */
                if (chon == 1)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 1. Nhập vào danh sách các chiếc xe, danh sách các chuyến đi, phân loại xe.");

                    string s = "";

                    do
                    {
                        Console.WriteLine("a. Nhập vào danh sách các chiếc xe.");
                        Console.WriteLine("b. Nhập vào danh sách các danh sách các chuyến đi.");
                        Console.WriteLine("c. Nhập vào danh sách phân loại xe.");

                        Console.WriteLine("---------\naf. Hàng loạt từ file.");
                        Console.WriteLine("t. Quay lại");
                        //-----------
                        Console.Write("Bạn chọn: ");
                        s = Console.ReadLine().ToLower();
                        //-----------
                        if (s == "a")
                        {
                            string ss = "";
                            do
                            {
                                Console.WriteLine(">>> a. Nhập vào danh sách các chiếc xe.");
                                Console.WriteLine("f. Nhập mới từ file");
                                Console.WriteLine("k. Nhập mới từ bàn phím.");
                                Console.WriteLine("---------");
                                Console.WriteLine("t. Quay lại");
                                //-----------
                                Console.Write("Bạn chọn: ");
                                ss = Console.ReadLine().ToLower();

                                if (ss == "f")
                                {
                                    dsxe.Nhap_File();
                                }
                                else if (ss == "k")
                                {
                                    dsxe.Nhap();
                                }
                            } while (ss != "t");
                            Console.WriteLine("---------");
                        }
                        else if (s == "b")
                        {
                            string ss = "";
                            do
                            {
                                Console.WriteLine(">>> b. Nhập vào danh sách các chuyến đi.");
                                Console.WriteLine("f. Nhập mới từ file");
                                Console.WriteLine("k. Nhập mới từ bàn phím.");
                                Console.WriteLine("---------");
                                Console.WriteLine("t. Quay lại");
                                //-----------
                                Console.Write("Bạn chọn: ");
                                ss = Console.ReadLine().ToLower();

                                if (ss == "f")
                                {
                                    dscd.Nhap_File();
                                }
                                else if (ss == "k")
                                {
                                    dscd.Nhap();
                                }
                            } while (ss != "t");
                            Console.WriteLine("---------");
                        }
                        else if (s == "c")
                        {
                            string ss = "";
                            do
                            {
                                Console.WriteLine(">>> b. Nhập vào danh sách phân loại xe.");
                                Console.WriteLine("f. Nhập mới từ file");
                                Console.WriteLine("k. Nhập mới từ bàn phím.");
                                Console.WriteLine("---------");
                                Console.WriteLine("t. Quay lại");
                                //-----------
                                Console.Write("Bạn chọn: ");
                                ss = Console.ReadLine().ToLower();

                                if (ss == "f")
                                {
                                    loaixe.Nhap_File();
                                }
                                else if (ss == "k")
                                {
                                    loaixe.Nhap();
                                }
                            } while (ss != "t");
                            Console.WriteLine("---------");
                        }
                        else if (s == "af")
                        {
                            loaixe.Nhap_File();
                            dsxe.Nhap_File();
                            dscd.Nhap_File();
                            Console.WriteLine("---------");
                        }
                    } while (s != "t");
                }
                /**
                 * Tính thành tiền cho mỗi chuyến đi
                 */
                else if (chon == 2)
                {
                    string s = "";

                    do
                    {
                        Console.Clear();
                        Console.WriteLine(">>> 2. Tính Thành tiền cho mỗi chuyến đi.");

                        //Console.WriteLine("f. Xuất ra file dữ liệu.");
                        Console.WriteLine("fr. Xuất ra file báo cáo.");
                        Console.WriteLine("m. Xuất ra màn hình.");
                        Console.WriteLine("---------");
                        Console.WriteLine("t. Quay lại");
                        //-----------
                        Console.Write("Bạn chọn: ");
                        s = Console.ReadLine().ToLower();

                        if (s == "fr")
                        {
                            dscd.Xuat_File_BaoCao();
                        }
                        else if (s == "m")
                        {
                            dscd.Xuat();
                        }
                    } while (s == "t");
                }
                /**
                 * Tính Tổng số tiền trong các chuyến đi của tài xế có tên được nhập từ bàn phím.
                 */
                else if (chon == 3)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 3. Tính Tổng số tiền trong các chuyến đi của tài xế có tên được nhập từ bàn phím.");
                    string s = "";
                    try
                    {
                        int i;
                        Console.Write("Nhập tên tài xế: ");
                        s = Console.ReadLine();
                        s.Trim(); // Cắt khoảng trắng thừa ở đầu và cuối chuỗi
                        // Cắt khoảng trắng thừa ở giữa chuỗi
                        string[] separators = { " " };
                        string[] temp1 = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        s = "";
                        for (i = 0; i < temp1.Length; i++)
                        {
                            if (i == temp1.Length - 1) s += temp1[i];
                            else s += temp1[i] + ' ';
                        }
                        // Chuyển đổi chuỗi về dạng chữ thường
                        string ss = s.ToLower();

                        // B1: Tìm mã của tài xế đó trước kể cả khi có nhiều tài xế với tên đó
                        int count = -1;
                        int[] list = new int[dsxe.getN()];

                        for (i = 0; i < dsxe.getN(); i++)
                            if (dsxe.getDSXe()[i].getTenTaiXe().ToLower() == ss || dsxe.getDSXe()[i].getTenTaiXe().ToLower().Contains(ss))
                                list[++count] = i;
                        // Nếu bộ đếm bằng 0 tức ta chả tìm được tài xế nào trùng
                        if (count < 0)
                            throw new System.ArgumentException("Không tìm thấy thông tin tài xế nào có tên, hoặc chứa ký tự '" + ss + "'");
                        // Xử lý tiếp nếu tìm thấy
                        Console.WriteLine("Tìm thấy {0} tài xế trùng với tên '{1}'", count + 1, s);
                        // Tính tổng số tiền trong các chuyến đi của từng tài xế
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("{0, 5}   {1, -25}    {2, -12}    {3, -20}   {4}", "#", "Họ && Tên", "Số xe", "Tổng $", "Tổng số chuyến đi");
                        Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                        for (i = 0; i <= count; i++)
                        {
                            double tongtien = 0;
                            int sochuyendi = 0;
                            for (int j = 0; j < dscd.getN(); j++) // Tìm mã tx trùng khớp với mã tx trong các chuyến đi
                                if (dsxe.getDSXe()[list[i]].getMaXe() == dscd.getDSChuyenDi()[j].getMaXe())
                                {
                                    tongtien += dscd.getDSChuyenDi()[j].getThanhTien();
                                    ++sochuyendi;
                                }

                            if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("{0, 5}   {1, -25}    {2, 12}    {3, 20}   {4, 17}", i, dsxe.getDSXe()[list[i]].getTenTaiXe(), dsxe.getDSXe()[list[i]].getSoXe(), string.Format("{0,0:0,0.#}", tongtien) + " VNĐ", sochuyendi);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                /**
                 * Xuất danh sách ra màn hình hoặc ra file
                 * STT      Mã tài xế   Tên tài xế      Số xe       Loại xe         Tổng quảng      Tổng tiền
                 *                                                                  đường đã đi
                */
                else if (chon == 4)
                {
                    Console.Clear();
                    StreamWriter file;
                    string s = "";
                    do
                    {

                        Console.WriteLine(">>> 4. Xuất báo cáo tổng hợp ra màn hình/ file.");
                        Console.WriteLine("m. Xuất danh sách ra màn hình");
                        Console.WriteLine("f. Xuất danh sách ra file");
                        Console.WriteLine("---------");
                        Console.WriteLine("t. Quay lại");
                        Console.Write("Bạn chọn: ");
                        s = Console.ReadLine().ToLower();

                        double[] tongtien = new double[dsxe.getN()];
                        double[] tong_qd = new double[dsxe.getN()];
                        for (int i = 0; i < dsxe.getN(); i++)
                        {
                            tongtien[i] = 0;
                            tong_qd[i] = 0;
                            for (int j = 0; j < dscd.getN(); j++)
                                if (dsxe.getDSXe()[i].getMaXe() == dscd.getDSChuyenDi()[j].getMaXe())
                                {
                                    tongtien[i] += dscd.getDSChuyenDi()[j].getThanhTien();
                                    tong_qd[i] += dscd.getDSChuyenDi()[j].getQuangDuong();
                                }

                        }

                        if (s == "m")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", "STT", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe", "Tổng QĐ", "Tổng tiền");
                            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                            //Console.WriteLine("________________________________________________________________________________________________________________");
                            for (int i = 0; i < dsxe.getN(); i++)
                            {
                                if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", i + 1, dsxe.getXEbyID(i).getMaXe(), dsxe.getXEbyID(i).getTenTaiXe(), dsxe.getXEbyID(i).getSoXe(), loaixe.getNameByID(dsxe.getXEbyID(i).getLoaiXe()), string.Format("{0:0,0}", tong_qd[i]), string.Format("{0:0,0}", tongtien[i]));
                                Console.BackgroundColor = ConsoleColor.Black;
                            }

                            Console.WriteLine("\nDanh sách này có {0} xe.\n", dsxe.getN());

                            s = "t";
                        }
                        else if (s == "f")
                        {

                            try
                            {
                                file = new StreamWriter(Properties.Settings.Default.FILE_BAOCAO);
                                file.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", "STT", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe", "Tổng QĐ", "Tổng tiền");
                                file.WriteLine("________________________________________________________________________________________________________________");
                                for (int i = 0; i < dsxe.getN(); i++)
                                    file.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", i + 1, dsxe.getXEbyID(i).getMaXe(), dsxe.getXEbyID(i).getTenTaiXe(), dsxe.getXEbyID(i).getSoXe(), loaixe.getNameByID(dsxe.getXEbyID(i).getLoaiXe()), string.Format("{0:0,0}", tong_qd[i]), string.Format("{0:0,0}", tongtien[i]));

                                file.WriteLine("\nDanh sách này có {0} xe.\n", dsxe.getN());

                                s = "t";
                                file.Close();
                                Console.WriteLine("Đã lưu vào {0}", Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
                            }
                        }

                    } while (s != "t");
                }
                /**
                 * Tìm xe có biển số X, X nhập từ bàn phím
                 */
                else if (chon == 5)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 5. Tìm xe có số xe X (X được nhập vào).");

                    string soxe = "";
                    Console.Write("Nhập số xe cần tìm: ");
                    soxe = Console.ReadLine().ToLower();

                    int found = 0;
                    for (int i = 0; i < dsxe.getN(); i++)
                    {
                        if (soxe == dsxe.getDSXe()[i].getSoXe())
                        {
                            Console.WriteLine("{0,7}  {1,20}  {2,12}  {3,5}", "Mã TX", "Tên tài xế", "Biển kiểm soát", "Loại xe");
                            dsxe.getDSXe()[i].Xuat2(loaixe);
                            found = 1;
                            break;
                        }
                    }

                    if (found == 0)
                        Console.WriteLine("Không tìm thấy xe có biển số '{0}' !!!", soxe);
                }
                /**
                 * Đếm số lượng xe của từng loại xe
                 */
                else if (chon == 6)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 6. Đếm số lượng xe theo từng loại.");

                    // Nếu chưa phân loại xe thì báo số xe chưa phân loại
                    //if (loaixe.getN() == 0)
                    //{
                    //    Console.WriteLine("Hiện chưa có phân loại xe rõ ràng!!!\nSố xe chưa phân loại là: ", dsxe.getN());
                    //}

                    int[] soluong = new int[100];
                    int nloai = loaixe.getN();
                    int unknown = 0; // Số lượng xe không rõ loại

                    for (int i = 0; i < nloai; i++)
                    {
                        soluong[i] = 0;
                    }

                    for (int i = 0; i < dsxe.getN(); i++)
                    {
                        if (loaixe.is_valid(dsxe.getDSXe()[i].getLoaiXe()))
                        {
                            ++soluong[dsxe.getDSXe()[i].getLoaiXe()];
                        }
                        else
                        {
                            ++unknown;
                        }
                    }

                    Console.WriteLine("{0, 12}  {1, 12}", "Mã", "Loại");
                    Console.WriteLine("{0, 12}  {1, 12}", "???", unknown);
                    for (int i = 0; i < nloai; i++)
                    {
                        Console.WriteLine("{0, 12}  {1, 12}", loaixe.getNameByID(i), soluong[i]);
                    }
                }
                /**
                 * Cho biết xe nào có số km đi nhiều nhất
                 */
                else if (chon == 7)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 7. Cho biết xe nào có số km đi nhiều nhất.");

                    try
                    {
                        //
                        if (dsxe.getN() < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các xe, không thể tiếp tục!");
                        else if (dscd.getN() < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các chuyến đi, không thể tiếp tục!");

                        float[] tkm = new float[dsxe.getN()];
                        string[] bks = new string[dsxe.getN()];
                        for (int i = 0; i < dsxe.getN(); i++)
                        { tkm[i] = 0; bks[i] = dscd.getDSChuyenDi()[i].getMaXe().ToString(); }

                        for (int i = 0; i < dsxe.getN(); i++)
                        {
                            for (int j = 0; j < dscd.getN(); j++)
                            {
                                if (bks[i] == dscd.getDSChuyenDi()[j].getMaXe().ToString())
                                    tkm[i] += dscd.getDSChuyenDi()[j].getQuangDuong();
                            }


                        }

                        int max = 0;
                        for (int i = 1; i < dsxe.getN(); i++)
                            if (tkm[i] > tkm[max])
                                max = i;

                        //Console.WriteLine("{0,12}   {1,12}", bks[max], tkm[max]);
                        Console.WriteLine("{0,7}  {1,20}  {2,12}  {3,5}", "Mã TX", "Tên tài xế", "Biển kiểm soát", "Loại xe");
                        dsxe.getXEbyID(max).Xuat2(loaixe);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                /**
                 * Cho biết thông tin chuyến đi có số km lớn nhất (số xe, loại xe, tên tài xế, số km, thành tiền).
                 */
                else if (chon == 9)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 9. Cho biết thông tin chuyến đi có số km lớn nhất (số xe, loại xe, tên tài xế, số km, thành tiền).");

                    try
                    {
                        //
                        if (dsxe.getN() < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các xe, không thể tiếp tục!");
                        else if (dscd.getN() < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các chuyến đi, không thể tiếp tục!");

                        int max = 0;
                        float maxkm = 0;
                        int count = -1;
                        int[] list = new int[dsxe.getN()];

                        // B1: Tìm chuyến đi có quãng đường dài nhất
                        for (int i = 1; i < dsxe.getN(); i++)
                            for (int j = 0; j < dscd.getN(); j++)
                                if (dscd.getCDbyID(i).getQuangDuong() > dscd.getCDbyID(max).getQuangDuong())
                                    max = i;
                        maxkm = dscd.getCDbyID(max).getQuangDuong();

                        // B2: Tìm các chuyến đi có cùng chỉ số
                        for (int i = 1; i < dscd.getN(); i++)
                            if (dscd.getCDbyID(i).getQuangDuong() == dscd.getCDbyID(max).getQuangDuong())
                                list[++count] = dscd.getCDbyID(i).getMaXe();

                        // B3: Hiển thị dữ liệu
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", "#", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe", "Quãng đường", "Thành tiền");
                        Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                        double thanhtien = ChuyenDi.ThanhTien(maxkm);
                        for (int i = 0; i <= count; i++)
                        {
                            Xe tam = dsxe.getXEbyMX(list[i]);
                            if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", i + 1, tam.getMaXe(), tam.getTenTaiXe(), tam.getSoXe(), loaixe.getNameByID(tam.getLoaiXe()), maxkm, string.Format("{0:0,0}", thanhtien));
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        Console.WriteLine("\nDanh sách này có {0} xe.\n", count + 1);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                else if (chon == 10)
                { 
                    
                }
                /**
                 * Xuất dữ liệu ra màn hình (danh sách các chiếc xe, danh sách các chuyến đi, phân loại xe).
                 */
                else if (chon == 30)
                {
                    Console.Clear();
                    string s = "";
                    int mode = 0;
                    do
                    {
                        Console.WriteLine(">>> 30. Xuất dữ liệu ra màn hình/file (danh sách các chiếc xe, danh sách các chuyến đi, phân loại xe).");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(">> Chế độ xuất dữ liệu hiện tại: {0}", mode == 0 ? "Màn hình" : "Tệp");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("---------");

                        Console.WriteLine("a. Danh sách loại xe.");
                        Console.WriteLine("b. Danh sách xe.");
                        Console.WriteLine("c. Danh sách chuyến đi.");
                        Console.WriteLine("d. Toàn bộ.");
                        Console.WriteLine("t. Thay đổi chế độ xuất dữ liệu.");
                        Console.WriteLine("---------");
                        Console.WriteLine("e. Quay lại");

                        Console.Write("Bạn chọn: ");
                        s = Console.ReadLine().ToLower();

                        if (s == "t")
                        {
                            mode = mode == 0 ? 1 : 0;
                            Console.Clear();
                        }
                        else
                        {
                            if (mode == 0)
                            {
                                if (s == "d")
                                {
                                    Console.WriteLine("> Danh sách loại xe:");
                                    loaixe.Xuat();
                                    Console.Write("Nhấn phím bất kỳ để tiếp tục...");
                                    Console.ReadKey();

                                    Console.WriteLine("> Danh sách xe:");
                                    dsxe.Xuat2(loaixe);
                                    Console.Write("Nhấn phím bất kỳ để tiếp tục...");
                                    Console.ReadKey();

                                    Console.WriteLine("> Danh sách chuyến đi:");
                                    dscd.Xuat();
                                }
                                else if (s == "a")
                                    loaixe.Xuat();
                                else if (s == "b")
                                    dsxe.Xuat();
                                else if (s == "c")
                                    dscd.Xuat();
                            }
                            else
                            {
                                try
                                {
                                    StreamWriter file = new StreamWriter(Properties.Settings.Default.FILE_BAOCAO);

                                    if (s == "d")
                                    {
                                        Console.WriteLine("> Danh sách loại xe...");
                                        file.WriteLine(loaixe.XuatS());

                                        Console.WriteLine("> Danh sách xe...");
                                        file.WriteLine(dsxe.Xuat2S(loaixe));

                                        Console.WriteLine("> Danh sách chuyến đi...");
                                        file.WriteLine(dscd.XuatS());
                                    }
                                    else if (s == "a")
                                        file.WriteLine(loaixe.XuatS());
                                    else if (s == "b")
                                        file.WriteLine(dsxe.Xuat2S(loaixe));
                                    else if (s == "c")
                                        file.WriteLine(dscd.XuatS());

                                    file.Close();
                                    Console.WriteLine("Đã lưu vào: {0}", Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO));
                                    Process notePad = new Process();
                                    //notePad.StartInfo.FileName = "notepad.exe";
                                    notePad.StartInfo.FileName = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                    //notePad.StartInfo.Arguments = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                    notePad.Start();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
                                }
                            }

                            Console.Write("Nhấn phím bất kỳ để trở lại menu...");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    } while (s != "e");
                }

                Console.Write("Nhấn phím bất kỳ để tiếp tục...");
                Console.ReadKey();
            } while (chon != 0);
            //------------------------------------------------------------------------------------
        }
    }
}