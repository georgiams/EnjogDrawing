using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Model.ViewModel
{
    class NodeEntry : NotificationObject
    {
        # region properity
        int id;
        public int ID
        {
            get { return id; }
            set { id = value; RaisePropertyChanged("ID"); }
        }

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged("Name"); }
        }

        public int ParentID { get; set; }

        bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; RaisePropertyChanged("IsChecked"); }
        }

        ObservableCollection<NodeEntry> nodeEntrys;
        public ObservableCollection<NodeEntry> NodeEntrys
        {
            get { return nodeEntrys; }
            set
            {
                nodeEntrys = value;
                RaisePropertyChanged("NodeEntrys");
            }
        }
        # endregion properity

        # region constructor
        public NodeEntry()
        {
            this.NodeEntrys = new ObservableCollection<NodeEntry>();
            this.ParentID = -1;
            this.IsChecked = true;
        }
        # endregion constructor

        # region override method
        public override string ToString()
        {
            return Name;
        }
        # endregion override method

        # region static method
        public static ObservableCollection<NodeEntry> Bind(ObservableCollection<NodeEntry> nodes)
        {
            ObservableCollection<NodeEntry> outputList = new ObservableCollection<NodeEntry>();
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].IsChecked = false;
                if (nodes[i].ParentID == -1) outputList.Add(nodes[i]);
                else FindDownward(nodes, nodes[i].ParentID).NodeEntrys.Add(nodes[i]);
            }
            return outputList;
        }
        private static NodeEntry FindDownward(ObservableCollection<NodeEntry> nodes, int id)
        {
            if (nodes == null) return null;
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].ID == id)
                {
                    return nodes[i];
                }
                NodeEntry node = FindDownward(nodes[i].NodeEntrys, id);
                if (node != null) return node;
            }
            return null;
        }
        # endregion static method
    }
}