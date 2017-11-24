using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PerRecorder
{
    public enum RecordType
    {
        [Description("Open EnjoyDrawing")]
        OpenEnjoyDrawing,
        [Description("Open File")]
        OpenFile
    }

    internal class RecorderData
    {
        public string param;
        public RecordType type;
        public DateTime startTm;
        public DateTime stopTm;

        public List<DateTime> pauseResumeTm = new List<DateTime>();

        public RecorderData(RecordType type, string param = "")
        {
            this.param = param;
            this.type = type;
            this.startTm = DateTime.Now;
        }

        public void RecorderPauseTime()
        {
            if (this.pauseResumeTm.Count % 2 != 0) // Avoid multiple pause 
                return;
            this.pauseResumeTm.Add(DateTime.Now);
        }

        public void RecorderResumeTime()
        {
            if (this.pauseResumeTm.Count % 2 == 0) // Avoid multiple resume 
                return;
            this.pauseResumeTm.Add(DateTime.Now);
        }

        public double GetTimeSpan()
        {
            if (this.stopTm > this.startTm)
            {
                double invalidTime = 0;
                int lastValidIndex = pauseResumeTm.Count - 1;
                if (lastValidIndex % 2 == 0)
                    lastValidIndex--;
                for (int index = 0; index < lastValidIndex; index += 2)
                {
                    invalidTime += ((TimeSpan)(pauseResumeTm[index + 1] - pauseResumeTm[index])).TotalMilliseconds;
                }
                return (((TimeSpan)(this.stopTm - this.startTm)).TotalMilliseconds - invalidTime);
            }
            return 0;
        }
    }

    public class PerformanceRecorder
    {
        #region static properties
        private static PerformanceRecorder Recorder;
        #endregion static properties

        #region private properties
        private string FileName;
        private Dictionary<RecordType, RecorderData> RecordList;
        #endregion private properties

        #region private method
        private PerformanceRecorder(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    throw null;
            }
            catch (Exception)
            {
            }
            this.FileName = fileName;
            RecordList = new Dictionary<RecordType, RecorderData>();
        }

        private void InnerStartRecorder(RecordType type, string param = "")
        {
            if (RecordList == null)
                return;
            try
            {
                if (RecordList.ContainsKey(type))
                    throw null;
                RecorderData data = new RecorderData(type, param);

                RecordList.Add(type, data);
                WriterStartInformation(type);
            }
            catch (Exception)
            {
            }
        }

        private void InnerPauseRecorder(RecordType type)
        {
            if (this.RecordList == null)
                return;
            try
            {
                if (!RecordList.ContainsKey(type))
                {
                    return;
                }
                RecordList[type].RecorderPauseTime();
            }
            catch (Exception)
            {
            }
        }

        private void InnerResumeRecorder(RecordType type)
        {
            if (this.RecordList == null)
                return;
            try
            {
                if (!RecordList.ContainsKey(type))
                {
                    return;
                }
                RecordList[type].RecorderResumeTime();
            }
            catch (Exception)
            {
            }
        }

        private void InnerEndRecorder(RecordType type)
        {
            if (this.RecordList == null)
                return;
            try
            {
                if (!RecordList.ContainsKey(type))
                {
                    return;
                }
                RecordList[type].stopTm = DateTime.Now;
                WriterEndInformation(type);
                RecordList.Remove(type);
            }
            catch (Exception)
            {
            }
        }

        private void WriterStartInformation(RecordType type)
        {
            try
            {
                using (FileStream stream = new FileStream(this.FileName, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine(@"[{0}],[{1}], Start at, {2}", GetTypeString(type), this.RecordList[type].param, GetDateTimeString(this.RecordList[type].startTm));
                    writer.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        private void WriterEndInformation(RecordType type)
        {
            try
            {
                using (FileStream stream = new FileStream(this.FileName, FileMode.Append, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine(@"[{0}],[{1}], Stop at, {2}, Time span is, {3}", GetTypeString(type), this.RecordList[type].param, GetDateTimeString(this.RecordList[type].stopTm), this.RecordList[type].GetTimeSpan());
                    writer.Close();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion private method

        #region static memeber
        static PerformanceRecorder()
        {
            Init("D://perRecord.log");
        }
        public static void Init(string fileName)
        {
            if (Recorder == null)
                Recorder = new PerformanceRecorder(fileName);
        }

        public static void StartRecorder(RecordType type, string param = "")
        {
            if (Recorder != null)
                Recorder.InnerStartRecorder(type, param);
        }

        public static void PauseRecorder(RecordType type)
        {
            if (Recorder != null)
                Recorder.InnerPauseRecorder(type);
        }

        public static void ResumeRecorder(RecordType type)
        {
            if (Recorder != null)
                Recorder.InnerResumeRecorder(type);
        }

        public static void EndRecorder(RecordType type)
        {
            if (Recorder != null)
                Recorder.InnerEndRecorder(type);
        }

        public static string GetDateTimeString(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss fff");
        }

        public static string GetTypeString(RecordType type)
        {
            Type t = type.GetType();
            FieldInfo info = t.GetField(Enum.GetName(t, type));
            DescriptionAttribute description = (DescriptionAttribute)Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute));
            return description.Description;
        }
        #endregion static member

    }
}
