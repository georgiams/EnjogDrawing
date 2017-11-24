using System;
using System.Windows;
using PerRecorder;
using System.Threading;
using CrashReporterDotNET;
using System.Reflection;
using log4net;
using System.IO;
using System.Globalization;
using Model.ModelController;
using Model.ModelEntity;

//注意下面的语句一定要加上，指定log4net使用.config文件来读取配置信息
//如果是WinForm（假定程序为MyDemo.exe，则需要一个MyDemo.exe.config文件）
//如果是WebForm，则从web.config中读取相关信息
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Model
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //For WPF
            Application.Current.DispatcherUnhandledException += (sender, args) =>
            {
                SendCrashReport((Exception)args.Exception);
                Environment.Exit(0);
            };

            //For Winform
            //Application.ThreadException += (sender, args) => SendCrashReport(args.Exception);

            //For Console
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                SendCrashReport((Exception)args.ExceptionObject);
                Environment.Exit(0);
            };

            InitializeComponent();

            //new Thread(ThrowException).Start();
            //new Thread(TestPerformanceRecord).Start();
            //new Thread(TestLog4net).Start(); 
        }
       
        void TestPerformanceRecord()
        {
            PerformanceRecorder.StartRecorder(RecordType.OpenEnjoyDrawing, "test");
            Thread.Sleep(3000);
            PerformanceRecorder.PauseRecorder(RecordType.OpenEnjoyDrawing);
            PerformanceRecorder.StartRecorder(RecordType.OpenFile);
            Thread.Sleep(3000);
            PerformanceRecorder.EndRecorder(RecordType.OpenFile);
            PerformanceRecorder.ResumeRecorder(RecordType.OpenEnjoyDrawing);
            Thread.Sleep(3000);
            PerformanceRecorder.EndRecorder(RecordType.OpenEnjoyDrawing);
        }

        private void ThrowException()
        {
            try
            {
                throw new ArgumentException();
            }
            catch (ArgumentException argumentException)
            {
                const string path = "test.txt";
                try
                {
                    if (!File.Exists(path))
                    {
                        throw new FileNotFoundException(
                            "File Not found when trying to write argument exception to the file", argumentException);
                    }
                }
                catch (Exception exception)
                {
                    SendCrashReport(exception, "Value of path variable is " + path);
                }
            }
        }

        void SendCrashReport(Exception exception, string developerMessage = "")
        {
            var reportCrash = new ReportCrash
            {
                CurrentCulture = new CultureInfo("en-US"),
                AnalyzeWithDoctorDump = true,
                DeveloperMessage = developerMessage,
                ToEmail = "xiong.ang@foxmail.com",
                DoctorDumpSettings = new DoctorDumpSettings
                {
                    ApplicationID = Guid.NewGuid(),
                    OpenReportInBrowser = true
                },
            };

            reportCrash.Send(exception);
        }

        void TestLog4net()
        {
            //Application.Run(new MainForm());
            //创建日志记录组件实例
            ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //记录错误日志
            log.Error("error", new Exception("发生了一个异常"));
            //记录严重错误
            log.Fatal("fatal", new Exception("发生了一个致命错误"));
            //记录一般信息
            log.Info("info");
            //记录调试信息
            log.Debug("debug");
            //记录警告信息
            log.Warn("warn");
        }

        private bool bBeginDraw=false;
        private ShapeType type=ShapeType.Rectangle;
        private Point startPoint;
        private Point endPoint;
        private ModelEntityController modelController = ModelEntityController.GetInstance();
        private void Canvas_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            bBeginDraw = true;
            startPoint = e.GetPosition(canvas);
        }  
        private void Canvas_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(bBeginDraw)
            {
                endPoint = e.GetPosition(canvas);
                modelController.Draw(canvas, type, startPoint, endPoint);
                bBeginDraw = false;
            }
        }
    }
}
