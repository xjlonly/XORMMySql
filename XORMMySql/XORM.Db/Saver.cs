using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace XORM.Db
{
    internal class Saver
    {
        /// <summary>
        /// 日志队列
        /// </summary>
        private Queue<Saver.LogInfo> _Ilog = new Queue<Saver.LogInfo>();

        /// <summary>
        /// 定时器：保存数据
        /// </summary>
        private Timer SaveTimer = new Timer(60000.0);

        /// <summary>
        /// 文件对象
        /// </summary>
        private FileInfo LogFile = (FileInfo)null;

        /// <summary>
        /// 日志路径
        /// </summary>
        private string LogFilePath = "";

        public Saver(string _LogFilePath)
        {
            this.LogFilePath = _LogFilePath;
            this.CheckDir(this.LogFilePath);
            this.SaveTimer.Elapsed += new ElapsedEventHandler(this.SaveTimer_Elapsed);
            this.SaveTimer.AutoReset = true;
            this.SaveTimer.Start();
        }

        /// <summary>
        /// 定时保存日志数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                this.CheckFile();
                using (StreamWriter streamWriter = this.LogFile.AppendText())
                {
                    streamWriter.AutoFlush = true;
                    for (int count = this._Ilog.Count; count > 0; --count)
                    {
                        Saver.LogInfo logInfo = this._Ilog.Dequeue();
                        streamWriter.WriteLine("[@Log]:" + logInfo.Time.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + logInfo.Content);
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        private void CheckFile()
        {
            //object[] objArray = new object[7];
            //int index1 = 0;
            //string str1 = this.LogFilePath;
            //objArray[index1] = (object)str1;
            //int index2 = 1;
            //string str2 = "\\";
            //objArray[index2] = (object)str2;
            //int index3 = 2;
            //// ISSUE: variable of a boxed type
            //__Boxed<int> local1 = (ValueType)DateTime.Now.Year;
            //objArray[index3] = (object)local1;
            //int index4 = 3;
            //string str3 = "-";
            //objArray[index4] = (object)str3;
            //int index5 = 4;
            //// ISSUE: variable of a boxed type
            //__Boxed<int> local2 = (ValueType)DateTime.Now.Month;
            //objArray[index5] = (object)local2;
            //int index6 = 5;
            //string str4 = "\\";
            //objArray[index6] = (object)str4;
            //int index7 = 6;
            //string str5 = DateTime.Now.Day.ToString().PadLeft(2, '0');
            //objArray[index7] = (object)str5;
            //string DirName = string.Concat(objArray);
            //this.CheckDir(DirName);
            //string fileName = DirName + "\\" + DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(":", "-") + ".log";
            //if (this.LogFile != null && !(this.LogFile.FullName != fileName))
            //    return;
            //this.LogFile = new FileInfo(fileName);


            string FileDir = LogFilePath + "\\" + DateTime.Now.Year + "-" + DateTime.Now.Month + "\\" + DateTime.Now.Day.ToString().PadLeft(2, '0');
            this.CheckDir(FileDir);
            string FileLocation = FileDir + "\\" + DateTime.Now.ToString("yyyy-MM-dd HH:mm").Replace(":", "-") + ".log";
            if (LogFile == null || LogFile.FullName != FileLocation)
            {
                LogFile = new FileInfo(FileLocation);
            }
        }

        private void CheckDir(string DirName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(DirName);
            if (directoryInfo.Exists)
                return;
            if (directoryInfo.Parent != null)
                this.CheckDir(directoryInfo.Parent.FullName);
            if (DirName.Contains(":") || directoryInfo.Exists)
                return;
            directoryInfo.Create();
        }

        public void Info(string message)
        {
            this._Ilog.Enqueue(new Saver.LogInfo()
            {
                Content = message,
                Time = DateTime.Now
            });
        }

        public void FinalSave()
        {
            try
            {
                this.CheckFile();
                using (StreamWriter streamWriter = this.LogFile.AppendText())
                {
                    streamWriter.AutoFlush = true;
                    for (int count = this._Ilog.Count; count > 0; --count)
                    {
                        Saver.LogInfo logInfo = this._Ilog.Dequeue();
                        streamWriter.WriteLine("[@Log]:" + logInfo.Time.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + logInfo.Content);
                    }
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
            catch
            {
            }
        }

        public class LogInfo
        {
            public DateTime Time { get; set; }

            public string Content { get; set; }

            public string BlockCode { get; set; }
        }
    }
}
