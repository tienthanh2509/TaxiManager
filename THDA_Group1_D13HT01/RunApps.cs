using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace THDA_Group1_D13HT01
{
    class RunApps
    {
        private string apppath;
        public string Apppath
        {
            get { return apppath; }
            set { apppath = value; }
        }

        private string argument;
        public string Argument
        {
            get { return argument; }
            set { argument = value; }
        }

        public RunApps()
        {
            apppath = "";
            argument = "";
        }

        public RunApps(string apppath, string argument = "")
        {
            this.apppath = apppath;
            this.argument = argument;
        }

        public void Run()
        {
            Process run = new Process();
            run.StartInfo.FileName = Path.GetFullPath(this.apppath);
            run.StartInfo.Arguments = this.argument;
            run.Start();
        }

        public void Run_With_WordPad()
        {
            Process run = new Process();
            run.StartInfo.FileName = "wordpad.exe";
            run.StartInfo.Arguments = this.argument;
            run.Start();
        }

        public void Run_With_NotePad()
        {
            Process run = new Process();
            run.StartInfo.FileName = "notepad.exe";
            run.StartInfo.Arguments = this.argument;
            run.Start();
        }

        public void Run_With_NotePadPlusPlus()
        {
            Process run = new Process();
            run.StartInfo.FileName = Path.GetFullPath("notepad++/notepad++.exe");
            run.StartInfo.Arguments = this.argument;
            run.Start();
        }
    }
}
