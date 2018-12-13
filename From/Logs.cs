using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace From {
    public partial class Logs : Form {
        public Logs() {
            InitializeComponent();
            logs = new Stack<Step>();
            redo = new Stack<Step>();
        }
        Stack<Step> logs;
        Stack<Step> redo;
        public void AddLog(Step Log) {
            logs.Push(Log);
            RefreshLog();
        }
        public void RefreshLog() {
            StringBuilder b = new StringBuilder();
            Step[] ll = logs.ToArray();
            for (int i = ll.Length-1; i >=0 ; i--) {
                b.Append(ll[i].function);
                foreach (string s in ll[i].parameters) {
                    b.Append(",");
                    b.Append(s);
                }
                b.Append("\n");
            }
            richTextBox1.Text = b.ToString();
        }
        public Step RemoveLog() {
            Step Log = logs.Pop();
            string s = richTextBox1.Text;
            s.Substring(0,s.LastIndexOf("\n"));
            return Log;
        }

        public Step[] GetLogs() {
            return logs.ToArray();
        }

        public void Undo() {
            if (logs.Count == 0) {
                return;
            }
            redo.Push(logs.Pop());
            RefreshLog();
        }
        public void Redo() {
            if (redo.Count == 0) {
                return;
            }
            logs.Push(redo.Pop());
            RefreshLog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
