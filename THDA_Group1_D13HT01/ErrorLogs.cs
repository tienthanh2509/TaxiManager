using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace THDA_Group1_D13HT01
{
    class ErrorLogs
    {
        private string err;
        public string Err
        {
            get { return err; }
            set { err = value; }
        }

        public ErrorLogs()
        {
            err = "";
        }

        public ErrorLogs(string err)
        {
            this.err = err;
        }

        public void write(string e = "")
        {
            this.err = e != "" ? err + "\n" + e : this.err;

            StreamWriter MyStream = null;

            try
            {
                MyStream = File.AppendText(Properties.Settings.Default.FILE_LOGS);
                MyStream.Write(err);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (MyStream != null)
                    MyStream.Close();
            }
        }
    }
}
