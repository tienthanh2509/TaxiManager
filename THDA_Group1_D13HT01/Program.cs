using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Hỗ trợ hiển thị tiếng Việt
using System.Runtime.InteropServices;
// -/-Hỗ trợ hiển thị tiếng Việt

using System.IO;

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
            DSXe dsxe = new DSXe();
            DSChuyenDi dscd = new DSChuyenDi();

            // Auto load \a ~ beep
            if (Properties.Settings.Default.AUTO_LOAD_DATA)
            {
                //Console.WriteLine(">>>\aDữ liệu được nạp tự động để phục vụ cho quá trình kiểm thử!");
                loaixe.Nhap_File();
                dsxe.Nhap_File();
                dscd.Nhap_File();
                Console.Clear();
            }

            int chon = -1;
            do
            {
                Console.Clear();
                //----------- Thông báo chào mừng
                Console.BackgroundColor = ConsoleColor.DarkYellow;

                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write("*");
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write(" ");
                for (int i = 0; i < Console.WindowWidth; i++)
                    Console.Write("*");
                Console.WriteLine();

                Console.SetCursorPosition(Convert.ToInt32(Console.WindowWidth * 0.34), 1);
                Console.Write("Chào mừng đến với chương trình quản lý xe taxi");

                Console.SetCursorPosition(Convert.ToInt32(Console.WindowWidth * 0.36), 2);
                Console.Write("Thiết kế & lập trình bới nhóm 1 lớp D13HT01");

                Console.SetCursorPosition(Convert.ToInt32(Console.WindowWidth * 0.85), 2);
                Console.Write("Nhóm 1 - D13HT01");

                Console.SetCursorPosition(Convert.ToInt32(Console.WindowWidth * 0.85), 3);
                Console.Write("Phiên bản: 1.1.0.17");

                Console.SetCursorPosition(Convert.ToInt32(Console.WindowWidth * 0.85), 4);
                Console.Write(DateTime.Now);

                Console.SetCursorPosition(0, 6);

                Console.BackgroundColor = ConsoleColor.Black;

                //----------- Menu các chức năng
                Console.WriteLine("1. Nhập/xuất danh sách các chiếc xe, danh sách các chuyến đi, phân loại xe.");
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

                for (int i = 0; i < Console.WindowWidth / 2; i++)
                    Console.Write("-");
                Console.WriteLine();

                //----------- Các chức năng khác
                Console.WriteLine("999. Tự động nạp & lưu dữ liệu mỗi khi khởi động và thoát chương trình. [{0}]", Properties.Settings.Default.AUTO_LOAD_DATA ? "Đã bật" : "Đã tắt");
                Console.WriteLine("\n>>> Thống kê sơ bộ: ");
                Console.WriteLine("\tTổng loại xe: {0}", loaixe.N);
                Console.WriteLine("\tTổng số xe: {0}", dsxe.N);
                Console.WriteLine("\tTổng số chuyến đi: {0}", dscd.N);

                for (int i = 0; i < Console.WindowWidth / 2; i++)
                    Console.Write("-");
                Console.WriteLine();

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

                /**
                 * Nhập liệu vào chương trình
                 */
                if (chon == 1)
                {
                    string s = "";
                    int mode = 0;

                    do
                    {
                        Console.Clear();
                        Console.WriteLine(">>> 1. Nhập/xuất danh sách các chiếc xe, danh sách các chuyến đi, phân loại xe.");
                        Console.WriteLine("a. Nhập mới danh sách các chiếc xe.");
                        Console.WriteLine("b. Nhập mới danh sách các danh sách các chuyến đi.");
                        Console.WriteLine("c. Nhập mới danh sách phân loại xe.");

                        Console.WriteLine("");

                        Console.WriteLine("o. Xuất danh sách các chiếc xe.");
                        Console.WriteLine("m. Xuất danh sách các danh sách các chuyến đi.");
                        Console.WriteLine("n. Xuất danh sách phân loại xe.");
                        Console.WriteLine("p. Xuất toàn bộ.");

                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(">> Chế độ xuất dữ liệu hiện tại: {0}", mode == 0 ? "Màn hình" : "Tệp");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine("t. Thay đổi chế độ xuất dữ liệu.");

                        Console.WriteLine("---------");

                        Console.WriteLine("---------\naf. Hàng loạt từ file.");
                        Console.WriteLine("bb. Quay lại");
                        Console.Write("Bạn chọn: ");
                        s = Console.ReadLine().ToLower();

                        if (s == "a")
                        {
                            string ss = "";
                            do
                            {
                                Console.WriteLine(">>> a. Nhập mới danh sách các chiếc xe.");
                                Console.WriteLine("f. Nhập mới từ file");
                                Console.WriteLine("k. Nhập mới từ bàn phím.");
                                Console.WriteLine("bb. Quay lại");
                                Console.Write("Bạn chọn: ");
                                ss = Console.ReadLine().ToLower();

                                if (ss == "f")
                                    dsxe.Nhap_File();
                                else if (ss == "k")
                                    dsxe.Nhap();
                            } while (ss != "bb");
                            Console.WriteLine("---------");
                        }
                        else if (s == "b")
                        {
                            string ss = "";
                            do
                            {
                                Console.WriteLine(">>> b. Nhập mới danh sách các chuyến đi.");
                                Console.WriteLine("f. Nhập mới từ file");
                                Console.WriteLine("k. Nhập mới từ bàn phím.");
                                Console.WriteLine("---------");
                                Console.WriteLine("bb. Quay lại");
                                Console.Write("Bạn chọn: ");
                                ss = Console.ReadLine().ToLower();

                                if (ss == "f")
                                    dscd.Nhap_File();
                                else if (ss == "k")
                                    dscd.Nhap();
                            } while (ss != "bb");
                            Console.WriteLine("---------");
                        }
                        else if (s == "c")
                        {
                            string ss = "";
                            do
                            {
                                Console.WriteLine(">>> c. Nhập mới danh sách phân loại xe.");
                                Console.WriteLine("f. Nhập mới từ file");
                                Console.WriteLine("k. Nhập mới từ bàn phím.");
                                Console.WriteLine("---------");
                                Console.WriteLine("bb. Quay lại");
                                Console.Write("Bạn chọn: ");
                                ss = Console.ReadLine().ToLower();

                                if (ss == "f")
                                    loaixe.Nhap_File();
                                else if (ss == "k")
                                    loaixe.Nhap();
                            } while (ss != "bb");
                            Console.WriteLine("---------");
                        }
                        else if (s == "af")
                        {
                            loaixe.Nhap_File();
                            dsxe.Nhap_File();
                            dscd.Nhap_File();
                            Console.WriteLine("---------");
                        }
                        else if (s == "t")
                        {
                            mode = mode == 0 ? 1 : 0;
                            Console.Clear();
                        }
                        else
                        {
                            if (mode == 0)
                            {
                                if (s == "p")
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
                                else if (s == "o")
                                    loaixe.Xuat();
                                else if (s == "m")
                                    dsxe.Xuat();
                                else if (s == "n")
                                    dscd.Xuat();
                            }
                            else
                            {
                                try
                                {
                                    StreamWriter file = new StreamWriter(Properties.Settings.Default.FILE_BAOCAO);

                                    if (s == "p")
                                    {
                                        Console.WriteLine("> Danh sách loại xe...");
                                        file.WriteLine(loaixe.XuatS());

                                        Console.WriteLine("> Danh sách xe...");
                                        file.WriteLine(dsxe.Xuat2S(loaixe));

                                        Console.WriteLine("> Danh sách chuyến đi...");
                                        file.WriteLine(dscd.XuatS());
                                    }
                                    else if (s == "o")
                                        file.WriteLine(loaixe.XuatS());
                                    else if (s == "m")
                                        file.WriteLine(dsxe.Xuat2S(loaixe));
                                    else if (s == "n")
                                        file.WriteLine(dscd.XuatS());

                                    file.Close();
                                    Console.WriteLine("Đã lưu vào: {0}", Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO));

                                    // Mở tệp với NotePad++
                                    RunApps run = new RunApps();
                                    //run.Apppath = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                    run.Argument = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                    run.Run_With_NotePadPlusPlus();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.ToString());
                                }
                            }

                            if (s != "bb")
                            {
                                Console.Write("Nhấn phím bất kỳ để trở lại menu...");
                                Console.ReadKey();
                                Console.Clear();
                            }
                        }
                    } while (s != "bb");
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

                        try
                        {
                            if (dsxe.N < 1)
                                throw new System.ArgumentException("Chưa có dữ liệu các xe, không thể tiếp tục!");
                            else if (dscd.N < 1)
                                throw new System.ArgumentException("Chưa có dữ liệu các chuyến đi, không thể tiếp tục!");
                            else if (loaixe.N < 1)
                                throw new System.ArgumentException("Chưa có dữ liệu các loại xe, không thể tiếp tục!");

                            Console.WriteLine("fr. Xuất ra file báo cáo.");
                            Console.WriteLine("m. Xuất ra màn hình.");
                            Console.WriteLine("---------");
                            Console.WriteLine("bb. Quay lại");
                            Console.Write("Bạn chọn: ");
                            s = Console.ReadLine().ToLower();

                            if (s == "fr")
                            {
                                dscd.Xuat_File_BaoCao();

                                // Auto load notepad++
                                RunApps run = new RunApps();
                                //run.Apppath = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                run.Argument = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                run.Run_With_NotePadPlusPlus();
                            }
                            else if (s == "m")
                                dscd.Xuat();
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine(ex.Message);
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
                        if (dsxe.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các xe, không thể tiếp tục!");
                        else if (dscd.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các chuyến đi, không thể tiếp tục!");
                        else if (loaixe.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các loại xe, không thể tiếp tục!");

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
                        int[] list = new int[dsxe.N];

                        for (i = 0; i < dsxe.N; i++)
                            if (dsxe.DsXE[i].Tentaixe.ToLower() == ss || dsxe.DsXE[i].Tentaixe.ToLower().Contains(ss))
                                list[++count] = i;
                        // Nếu bộ đếm bằng 0 tức ta chả tìm được tài xế nào trùng
                        if (count < 0)
                            throw new System.ArgumentException("Không tìm thấy thông tin tài xế nào có tên, hoặc chứa ký tự '" + ss + "'");
                        // Xử lý tiếp nếu tìm thấy
                        Console.WriteLine("Tìm thấy {0} tài xế trùng với từ khóa '{1}'.", count + 1, s);
                        // Tính tổng số tiền trong các chuyến đi của từng tài xế
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("{0, 5} | {1, -25}    {2, 15}    {3, 25}   {4, 17}", "#", "Họ && Tên", "Số xe", "Tổng $", "Tổng số chuyến đi");
                        Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                        for (i = 0; i <= count; i++)
                        {
                            double tongtien = 0;
                            int sochuyendi = 0;
                            for (int j = 0; j < dscd.N; j++) // Tìm mã tx trùng khớp với mã tx trong các chuyến đi
                                if (dsxe.DsXE[list[i]].Maxe == dscd.DsCD[j].Maxe)
                                {
                                    tongtien += dscd.DsCD[j].getThanhTien();
                                    ++sochuyendi;
                                }

                            if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("{0, 5} | {1, -25}    {2, 15}    {3, 25}   {4, 17}", i, dsxe.DsXE[list[i]].Tentaixe, dsxe.DsXE[list[i]].Soxe, string.Format("{0,0:0,0}", tongtien) + " VNĐ", sochuyendi);
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
                    StreamWriter file;
                    string s = "";
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(">>> 4. Xuất báo cáo tổng hợp ra màn hình/ file.");
                        Console.WriteLine("m. Xuất danh sách ra màn hình");
                        Console.WriteLine("f. Xuất danh sách ra file");
                        Console.WriteLine("---------");
                        Console.WriteLine("bb. Quay lại");
                        Console.Write("Bạn chọn: ");
                        s = Console.ReadLine().ToLower();

                        double[] tongtien = new double[dsxe.N];
                        double[] tong_qd = new double[dsxe.N];
                        for (int i = 0; i < dsxe.N; i++)
                        {
                            tongtien[i] = 0;
                            tong_qd[i] = 0;
                            for (int j = 0; j < dscd.N; j++)
                                if (dsxe.DsXE[i].Maxe == dscd.DsCD[j].Maxe)
                                {
                                    tongtien[i] += dscd.DsCD[j].getThanhTien();
                                    tong_qd[i] += dscd.DsCD[j].Quangduong;
                                }

                        }

                        if (s == "m")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", "STT", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe", "Tổng QĐ", "Tổng tiền");
                            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                            //Console.WriteLine("________________________________________________________________________________________________________________");
                            for (int i = 0; i < dsxe.N; i++)
                            {
                                if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", i + 1, dsxe.getXEbyID(i).Maxe, dsxe.getXEbyID(i).Tentaixe, dsxe.getXEbyID(i).Soxe, loaixe.getNameByID(dsxe.getXEbyID(i).Loaixe), string.Format("{0:0,0}", tong_qd[i]), string.Format("{0:0,0}", tongtien[i]));
                                Console.BackgroundColor = ConsoleColor.Black;
                            }

                            Console.WriteLine("\nDanh sách này có {0} xe.\n", dsxe.N);

                            s = "bb";
                        }
                        else if (s == "f")
                        {

                            try
                            {
                                file = new StreamWriter(Properties.Settings.Default.FILE_BAOCAO);
                                file.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", "STT", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe", "Tổng QĐ", "Tổng tiền");
                                file.WriteLine("________________________________________________________________________________________________________________");
                                for (int i = 0; i < dsxe.N; i++)
                                    file.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", i + 1, dsxe.getXEbyID(i).Maxe, dsxe.getXEbyID(i).Tentaixe, dsxe.getXEbyID(i).Soxe, loaixe.getNameByID(dsxe.getXEbyID(i).Loaixe), string.Format("{0:0,0}", tong_qd[i]), string.Format("{0:0,0}", tongtien[i]));

                                file.WriteLine("\nDanh sách này có {0} xe.\n", dsxe.N);

                                s = "bb";
                                file.Close();
                                Console.WriteLine("Đã lưu vào {0}", Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO));
                                // Mở tệp với NotePad++
                                RunApps run = new RunApps();
                                //run.Apppath = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                run.Argument = Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO);
                                run.Run_With_NotePadPlusPlus();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
                            }
                        }

                    } while (s != "bb");
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
                    int[] list = new int[dsxe.N];
                    for (int i = 0; i < dsxe.N; i++)
                        if (soxe == dsxe.DsXE[i].Soxe.ToLower() || dsxe.DsXE[i].Soxe.ToLower().Contains(soxe))
                            list[found++] = i;

                    if (found == 0)
                        Console.WriteLine("Không tìm thấy xe có biển số '{0}' !!!", soxe);
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("{4, 5} | {0,-7} | {1,-15} | {2,-32} | {3,-14}", "Mã TX", "Tên tài xế", "Biển kiểm soát", "Loại xe", "#");
                        Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                        for (int i = 0; i < dsxe.N; i++)
                        {
                            if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write("{0, 5} | {1}", i, dsxe.DsXE[i].Xuat2S(loaixe));
                            Console.BackgroundColor = ConsoleColor.Black;
                        }

                        Console.WriteLine("\n---------------\nĐã tìm thấy {0} xe trùng khớp với từ khóa {1}.", found, soxe);
                    }
                }
                /**
                 * Đếm số lượng xe của từng loại xe
                 */
                else if (chon == 6)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 6. Đếm số lượng xe theo từng loại.");

                    int[] soluong = new int[100];
                    int nloai = loaixe.N;
                    int unknown = 0; // Số lượng xe không rõ loại

                    for (int i = 0; i < nloai; i++)
                    {
                        soluong[i] = 0;
                    }

                    for (int i = 0; i < dsxe.N; i++)
                    {
                        if (loaixe.is_valid(dsxe.DsXE[i].Loaixe))
                        {
                            ++soluong[dsxe.DsXE[i].Loaixe];
                        }
                        else
                        {
                            ++unknown;
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("{0, 12}  {1, 12}", "Mã", "Loại");
                    Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("{0, 12}  {1, 12}", "???", unknown);
                    Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                    for (int i = 0; i < nloai; i++)
                    {
                        if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("{0, 12}  {1, 12}", loaixe.getNameByID(i), soluong[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
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
                        if (dsxe.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các xe, không thể tiếp tục!");
                        else if (dscd.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các chuyến đi, không thể tiếp tục!");

                        float[] tkm = new float[dsxe.N];
                        string[] bks = new string[dsxe.N];
                        for (int i = 0; i < dsxe.N; i++)
                        { tkm[i] = 0; bks[i] = dscd.DsCD[i].Maxe.ToString(); }

                        for (int i = 0; i < dsxe.N; i++)
                            for (int j = 0; j < dscd.N; j++)
                                if (bks[i] == dscd.DsCD[j].Maxe.ToString())
                                    tkm[i] += dscd.DsCD[j].Quangduong;

                        int max = 0;
                        for (int i = 1; i < dsxe.N; i++)
                            if (tkm[i] > tkm[max])
                                max = i;

                        Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("{0,-7} | {1,-15} | {2,-32} | {3,-14} | {4}", "Mã TX", "Tên tài xế", "Biển kiểm soát", "Loại xe", "Tổng QD");
                        Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;

                        int[] list = new int[dsxe.N];
                        int count = 0;

                        for (int i = 0; i < count; i++)
                            if (tkm[i] == tkm[max])
                                list[count++] = i;

                        for (int i = 0; i <= count; i++)
                            Console.WriteLine("{0}  {1, 13}", dsxe.getXEbyID(max).Xuat2S(loaixe).Trim(), String.Format("{0:0,0}", tkm[max]));

                        //for (int i = 0; i <= dsxe.N; i++)
                        //    Console.WriteLine("{0}  {1, 13}", dsxe.getXEbyID(i).Xuat2S(loaixe).Trim(), String.Format("{0:0,0}", tkm[i]));
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                /**
                * Cho biết tài xế nào lái xe có số tiền của các chuyến đi nhiều nhất.");
                * STT      Biển số xe      Tên tài xế      Tổng tiền các chuyến đi
                */
                else if (chon == 8)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 8. Cho biết tài xế nào lái xe có số tiền của các chuyến đi nhiều nhất.");
                    try
                    {
                        if (dsxe.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các xe, không thể tiếp tục!");
                        else if (dscd.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các chuyến đi, không thể tiếp tục!");
                        else if (loaixe.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các loại xe, không thể tiếp tục!");

                        double max = 0;
                        double[] tongtien = new double[dsxe.N];

                        // Tìm tổng tiền của từng tài xế và lấy giá trị lớn nhất
                        int i;
                        for (i = 0; i < dsxe.N; i++)
                        {
                            tongtien[i] = 0;
                            for (int j = 0; j < dscd.N; j++)
                                if (dsxe.DsXE[i].Maxe == dscd.DsCD[j].Maxe)
                                    tongtien[i] += dscd.DsCD[j].getThanhTien();
                            if (max < tongtien[i]) max = tongtien[i];
                        }

                        // Xuất thông tin
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("{0,6}|{1,-9}|{2,-15}|{3,-25}|{4,-14}|{5,15}", "STT", "Mã xe", "Biển số xe", "Tên tài xế", "Loại xe", "Tổng tiền");

                        Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                        for (i = 0; i < dsxe.N; i++)
                            if (max == tongtien[i])
                                Console.WriteLine("{0,6}|{1,-9}|{2,-15}|{3,-25}|{4,-14}|{5,15}", i++, dsxe.DsXE[i].Maxe, dsxe.DsXE[i].Soxe, dsxe.DsXE[i].Tentaixe, loaixe.getNameByID(dsxe.DsXE[i].Loaixe), String.Format("{0:0,#}", tongtien[i]));

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

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
                        if (dsxe.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các xe, không thể tiếp tục!");
                        else if (dscd.N < 1)
                            throw new System.ArgumentException("Chưa có dữ liệu các chuyến đi, không thể tiếp tục!");

                        int max = 0;
                        float maxkm = 0;
                        int count = -1;
                        int[] list = new int[dsxe.N];

                        // B1: Tìm chuyến đi có quãng đường dài nhất
                        for (int i = 1; i < dsxe.N; i++)
                            for (int j = 0; j < dscd.N; j++)
                                if (dscd.getCDbyID(i).Quangduong > dscd.getCDbyID(max).Quangduong)
                                    max = i;
                        maxkm = dscd.getCDbyID(max).Quangduong;

                        // B2: Tìm các chuyến đi có cùng chỉ số
                        for (int i = 1; i < dscd.N; i++)
                            if (dscd.getCDbyID(i).Quangduong == dscd.getCDbyID(max).Quangduong)
                                list[++count] = dscd.getCDbyID(i).Maxe;

                        // B3: Hiển thị dữ liệu
                        Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", "#", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe", "Quãng đường", "Thành tiền");
                        Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;
                        double thanhtien = ChuyenDi.ThanhTien(maxkm);
                        for (int i = 0; i <= count; i++)
                        {
                            Xe tam = dsxe.getXEbyMX(list[i]);
                            if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}  {5,10}  {6,16}", i + 1, tam.Maxe, tam.Tentaixe, tam.Soxe, loaixe.getNameByID(tam.Loaixe), maxkm, string.Format("{0:0,0}", thanhtien));
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        Console.WriteLine("\nDanh sách này có {0} xe.\n", count + 1);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                // Sắp xếp danh sách xe tăng dần theo biển số xe
                else if (chon == 10)
                {
                    Console.Clear();

                    StreamWriter file;
                    string s = "";
                    dsxe.interchangesort();
                    do
                    {
                        Console.Clear();
                        Console.WriteLine(">>> 10. Sắp xếp danh sách tăng dần theo số xe.");
                        Console.WriteLine("m. Xuất danh sách ra màn hình");
                        Console.WriteLine("f. Xuất danh sách ra file");
                        Console.WriteLine("---------");
                        Console.WriteLine("bb. Quay lại");
                        Console.Write("Bạn chọn: ");
                        s = Console.ReadLine().ToLower();


                        if (s == "m")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow; Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}", "STT", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe");
                            Console.ForegroundColor = ConsoleColor.White; Console.BackgroundColor = ConsoleColor.Black;

                            for (int i = 0; i < dsxe.N; i++)
                            {
                                if (i % 2 != 0) Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}", i + 1, dsxe.getXEbyID(i).Maxe, dsxe.getXEbyID(i).Tentaixe, dsxe.getXEbyID(i).Soxe, loaixe.getNameByID(dsxe.getXEbyID(i).Loaixe));
                                Console.BackgroundColor = ConsoleColor.Black;
                            }

                            Console.WriteLine("\nDanh sách này có {0} xe.\n", dsxe.N);

                            s = "bb";
                        }
                        else if (s == "f")
                        {

                            try
                            {
                                file = new StreamWriter(Properties.Settings.Default.FILE_BAOCAO);
                                file.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}", "STT", "Mã Xe", "Tên tài xế", "Biển kiểm soát", "Loại xe");
                                file.WriteLine("________________________________________________________________________________________________________________");
                                for (int i = 0; i < dsxe.N; i++)
                                    file.WriteLine("{0,-4} | {1,-7}  {2,-32}  {3,-15}  {4,-15}", i + 1, dsxe.getXEbyID(i).Maxe, dsxe.getXEbyID(i).Tentaixe, dsxe.getXEbyID(i).Soxe, loaixe.getNameByID(dsxe.getXEbyID(i).Loaixe));

                                file.WriteLine("\nDanh sách này có {0} xe.\n", dsxe.N);

                                s = "t";
                                file.Close();
                                Console.WriteLine("Đã lưu vào {0}", Path.GetFullPath(Properties.Settings.Default.FILE_BAOCAO));

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Có lỗi không rõ đã xảy ra, chi tiết: " + ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vui lòng nhập đúng!");
                            Console.WriteLine("--------------");
                            Console.ReadKey();
                            //s = "t";
                        }

                    } while (s != "bb");
                }
                /**
                 * Với mỗi loại xe, cho biết xe nào được chạy nhiều nhất (số km nhiều nhất
                 */
                else if (chon == 11)
                {
                    Console.Clear();
                    Console.WriteLine(">>> 11. Với mỗi loại xe, cho biết xe nào được chạy nhiều nhất (số km nhiều nhất).");

                    Xe[,] mang_luu_temp = new Xe[loaixe.N, dsxe.N];

                    int i, j;
                    //B1: Tìm tất cả các xe cho từng loại 
                    int q = 0, w = 0, e = 0;
                    for (j = 0; j < dsxe.N; j++)
                    {

                        if (dsxe.DsXE[j].Loaixe == 0)
                        {
                            mang_luu_temp[0, q] = new Xe();

                            mang_luu_temp[0, q] = dsxe.DsXE[j];

                            q++;
                        }
                        else if (dsxe.DsXE[j].Loaixe == 1)
                        {
                            mang_luu_temp[1, w] = new Xe();
                            mang_luu_temp[1, w] = dsxe.DsXE[j];
                            w++;
                        }
                        else if (dsxe.DsXE[j].Loaixe == 2)
                        {
                            mang_luu_temp[2, e] = new Xe();
                            mang_luu_temp[2, e] = dsxe.DsXE[j];
                            e++;
                        }
                    }
                    // B2: Tính tổng quảng đường cho tất cả các xe và tìm giá trị lớn nhất
                    double[,] tongkm = new double[loaixe.N, dsxe.N];
                    double maxt = 0, maxv = 0, maxhd = 0;
                    for (i = 0; i < q; i++)
                    {
                        tongkm[0, i] = 0;
                        for (j = 0; j < dscd.N; j++)
                        {
                            if (dscd.DsCD[j].Maxe == mang_luu_temp[0, i].Maxe)
                                tongkm[0, i] += dscd.DsCD[j].Quangduong;

                        }
                        if (maxt < tongkm[0, i]) maxt = tongkm[0, i];
                    }
                    for (i = 0; i < w; i++)
                    {
                        tongkm[1, i] = 0;
                        for (j = 0; j < dscd.N; j++)
                            if (dscd.DsCD[j].Maxe == mang_luu_temp[1, i].Maxe)
                                tongkm[1, i] += dscd.DsCD[j].Quangduong;
                        if (maxv < tongkm[0, i]) maxv = tongkm[1, i];
                    }

                    for (i = 0; i < e; i++)
                    {
                        tongkm[2, i] = 0;
                        for (j = 0; j < dscd.N; j++)
                            if (dscd.DsCD[j].Maxe == mang_luu_temp[2, i].Maxe)
                                tongkm[2, i] += dscd.DsCD[j].Quangduong;
                        if (maxhd < tongkm[0, i]) maxhd = tongkm[2, i];
                    }
                    // B3: Xuất danh sách
                    Console.WriteLine("{0, 12}  {1, 12} {2,12}", "Loại", "Biển số xe", "Tổng số km");

                    for (i = 0; i < q; i++)
                        if (maxt == tongkm[0, i])
                            Console.WriteLine("{0,12}   {1,-12}  {2,12}", "Thường", mang_luu_temp[0, i].Soxe, String.Format("{0:0,#}", tongkm[0, i]));

                    for (i = 0; i < w; i++)
                        if (maxv == tongkm[1, i])
                            Console.WriteLine("{0,12}   {1,-12}  {2,12}", "Vip", mang_luu_temp[1, i].Soxe, String.Format("{0:0,#}", tongkm[1, i]));

                    for (i = 0; i < e; i++)
                        if (maxhd == tongkm[2, i])
                            Console.WriteLine("{0,12}   {1,-12}  {2,12}", "Hợp Đồng", mang_luu_temp[2, i].Soxe, String.Format("{0:0,#}", tongkm[2, i]));
                }
                /**
                 * Tùy chọn tự động nạp dữ liệu mỗi khi mở và tắt chương trình
                 */
                else if (chon == 999)
                {
                    Properties.Settings.Default.AUTO_LOAD_DATA = !Properties.Settings.Default.AUTO_LOAD_DATA;
                    Properties.Settings.Default.Save();
                    Console.WriteLine("Tự động nạp dữ liệu đã {0}", Properties.Settings.Default.AUTO_LOAD_DATA ? "Bật" : "Tắt");
                }

                if (chon != 0)
                {
                    Console.Write("Nhấn phím bất kỳ để tiếp tục...");
                    Console.ReadKey();
                }
            } while (chon != 0);

            if (Properties.Settings.Default.AUTO_LOAD_DATA)
            {
                Console.Write("Đang lưu dữ liệu...");
                loaixe.Xuat_File(true);
                dsxe.Xuat_File(true);
                dscd.Xuat_File(true);
            }
            //------------------------------------------------------------------------------------
        }
    }
}
