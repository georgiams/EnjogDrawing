using Microsoft.Win32;
using Model.EDAction;
using Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace Model.ViewModel
{
    //View Model for Drawing
    partial class ViewModel:NotificationObject
    {
        # region binding properity
        private bool bDrawLine;
        public bool BDrawLine
        {
            get { return bDrawLine; }
            set 
            {
                if (bDrawLine != value)
                {
                    bDrawLine = value;
                    RaisePropertyChanged("BDrawLine");
                }
            }
        }

        private bool bDrawRectangle;
        public bool BDrawRectangle
        {
            get { return bDrawRectangle; }
            set
            {
                if (bDrawRectangle != value)
                {
                    bDrawRectangle = value;
                    RaisePropertyChanged("BDrawRectangle");
                }
            }
        }

        private bool bDrawCircle;
        public bool BDrawCircle
        {
            get { return bDrawCircle; }
            set
            {
                if (bDrawCircle != value)
                {
                    bDrawCircle = value;
                    RaisePropertyChanged("BDrawCircle");
                }
            }
        }

        private string command="Input command to draw~";
        public string Command
        {
            get { return command; }
            set 
            {
                if (command != value)
                {
                    command = value;
                    RaisePropertyChanged("Command");
                }
            }
        }
        
        # endregion binding properity

        # region binding command
        public DelegateCommand NewCommand { get; set; }
        public DelegateCommand OpenCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand OpenShellCommand { get; set; }
        public DelegateCommand UndoCommand { get; set; }
        public DelegateCommand RodoCommand { get; set; }
        public DelegateCommand ShellInputCommand { get; set; }
        public DelegateCommand StartDrawCommand { get; set; }
        public DelegateCommand EndDrawCommand { get; set; }
        # endregion binding command

        # region private properity
        private ModelProcessor actionProcessor = ModelProcessor.GetInstance();
        private string projectXML = null;
/*@"
            <?xml version='1.0' encoding='utf-8' ?>
            <Project>
                <Pages/>
            </Project>";*/
        private Window uiWindow = null;
        # endregion private properity

        # region private method
        private void ExecuteActions(string actionsXML)
        {
            Thread t = new Thread(
                () =>
                {
                    actionProcessor.ExecuteActions(actionsXML, ref projectXML);
                    UpdateViewModel(projectXML);
                });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void OpenProject(object parameter)
        {
            Stream myStream;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "EnjoyDrawing File (*.enj)|*.enj|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Handle IOException
                try
                {
                    myStream = openFileDialog.OpenFile();
                }
                catch (IOException e)
                {
                    return;
                }

                if (myStream != null)
                {
                    StreamReader sw = new StreamReader(myStream);

                    // Read file
                    projectXML=sw.ReadToEnd();
                    sw.Close();
                }
            }
            UpdateViewModel(projectXML);
        }

        private void SaveProject(object parameter)
        {
            Stream myStream;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "EnjoyDrawing File (*.enj)|*.enj|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                //Handle IOException
                try
                {
                    myStream = saveFileDialog.OpenFile();
                }
                catch (IOException e)
                {
                    return;
                }

                if (myStream != null)
                {
                    StreamWriter sw = new StreamWriter(myStream);
                    // Write file
                    sw.WriteLine(projectXML);
                    sw.Close();
                }
            }
        }
        private void CloseProject(object parameter)
        {
            //exit(0);
        }

        private void OpenShell(object parameter)
        {
            Thread t = new Thread(
                () =>
                {
                    ConsoleManager.Show();
                    Console.WriteLine("Input Command to Draw~");
                    Console.WriteLine("-----------------------------------------------------------------");
                    while(true)
                    {
                        string command=Console.ReadLine();
                        if (string.Equals("exit", command))
                            break;
                        //Create and Exexute Action XML
                        string output = ParseAndExecuteShellCommand(command);

                        Console.WriteLine(output);
                    }
                    ConsoleManager.Hide();
                });
            t.Start();
        }

        private string ParseAndExecuteShellCommand(string shellCommand)
        {
            string actionXML = string.Empty;
            string output = string.Empty;
            if (string.IsNullOrWhiteSpace(shellCommand)) return null;

            XmlDocument xDoc = new XmlDocument();
            if (!string.IsNullOrWhiteSpace(projectXML))
                xDoc.InnerXml=projectXML;

            string[] command = shellCommand.Split(' ');
            if (string.Equals(command[0],"add")&&string.Equals(command[1],"p")&&!string.IsNullOrWhiteSpace(command[2]))
            {
                actionXML = @"<Action type='do' name='addpage'>
                            <Page name='page_add'/>
                            </Action>";
            }
            else if (string.Equals(command[0], "del") && string.Equals(command[1], "p") && !string.IsNullOrWhiteSpace(command[2]))
            {

            }
            else if (string.Equals(command[0], "tab") && string.Equals(command[1], "p"))
            {

            }
            else if (string.Equals(command[0], "draw") && string.Equals(command[1], "l"))
            {

            }
            else if (string.Equals(command[0], "draw") && string.Equals(command[1], "r"))
            {

            }
            else if (string.Equals(command[0], "draw") && string.Equals(command[1], "c"))
            {

            }
            else if (string.Equals(command[0], "del") && string.Equals(command[1], "l") && !string.IsNullOrWhiteSpace(command[2]))
            {

            }
            else if (string.Equals(command[0], "del") && string.Equals(command[1], "r") && !string.IsNullOrWhiteSpace(command[2]))
            {

            }
            else if (string.Equals(command[0], "del") && string.Equals(command[1], "c") && !string.IsNullOrWhiteSpace(command[2]))
            {

            }else if (string.Equals(command[0], "show") && command.Length == 1)
            {
                XmlNodeList pageNodeList= xDoc.SelectNodes("/Project/Pages/Page");
                if (pageNodeList != null)
                {
                    foreach (XmlNode page in pageNodeList)
                    {
                        string Name = page.Attributes["name"].Value;
                        output += Name + "\n";
                        XmlNodeList shapeNodeList = page.ChildNodes;
                        if (null != shapeNodeList)
                        {
                            foreach (XmlNode shape in shapeNodeList)
                            {
                                string shapeName = shape.Attributes["name"].Value;
                                string shapeType = shape.Attributes["type"].Value;
                                output += "     [" + shapeType + "]" + shapeName + "\n";
                            }
                        }
                        
                    }
                }
                
            }
            else if (string.Equals(command[0], "show") && string.Equals(command[1], "p"))
            {
                XmlNodeList pageNodeList = xDoc.SelectNodes("/Project/Pages/Page");
                if (pageNodeList != null)
                {
                    foreach (XmlNode page in pageNodeList)
                    {
                        string Name = page.Attributes["name"].Value;
                        output += Name + "\n";
                    }
                }
            }
            else if (string.Equals(command[0], "show") && string.Equals(command[1], "s"))
            {
                XmlNodeList pageNodeList = xDoc.SelectNodes("/Project/Pages/Page");
                if (pageNodeList != null&& pageNodeList[selectedPage]!=null)
                {
                    XmlNode page = pageNodeList[selectedPage];
                    string Name = page.Attributes["name"].Value;
                    output += Name + "\n";
                    XmlNodeList shapeNodeList = page.ChildNodes;
                    if (null != shapeNodeList)
                    {
                        foreach (XmlNode shape in shapeNodeList)
                        {
                            string shapeName = shape.Attributes["name"].Value;
                            string shapeType = shape.Attributes["type"].Value;
                            output += "     [" + shapeType + "] " + shapeName + "\n";
                        }
                    }
                }
            }
            
            else if (string.Equals(command[0], "save") && command.Length == 1)
            {
                Thread t = new Thread(
                () =>
                    {
                         SaveProject(null);
                    });
                t.Start();
            }
            else if (string.Equals(command[0], "close") && command.Length == 1)
            {
                CloseProject(null);
            }
            else if (string.Equals(command[0], "undo") && command.Length == 1)
            {
                actionXML = @"<Action type='undo'/>";
            }
            else if (string.Equals(command[0], "redo") && command.Length == 1)
            {
               actionXML= @"<Action type='redo'/>";
            }

            //Execute Action
            if (!string.IsNullOrWhiteSpace(actionXML))
                ExecuteActions(actionXML);

            return output;
        }
        # endregion private method

        # region constructor
        public ViewModel()
        {
            UpdateViewModel(null);

            //Command response
            NewCommand = new DelegateCommand();
            OpenCommand = new DelegateCommand();
            SaveCommand = new DelegateCommand();
            CloseCommand = new DelegateCommand();
            OpenShellCommand = new DelegateCommand();
            UndoCommand = new DelegateCommand();
            RodoCommand = new DelegateCommand();
            ShellInputCommand = new DelegateCommand();
            StartDrawCommand = new DelegateCommand();
            EndDrawCommand = new DelegateCommand();

            OpenCommand.ExecuteAction += OpenProject;
            SaveCommand.ExecuteAction += SaveProject;
            OpenShellCommand.ExecuteAction+=OpenShell;
        }
        public ViewModel(MainWindow m):this()
        {
            uiWindow = m;
        }
        # endregion constructor
    }
}
