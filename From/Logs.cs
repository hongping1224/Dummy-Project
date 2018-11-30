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
        }
        Stack<string> logs;

        public void AddLog(string Log) {
            logs.Push(Log);
        }
        public string RemoveLog() {
            return logs.Pop();
        }
        public string[] GetLogs() {
            return logs.ToArray();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) {

        }
    }
}
