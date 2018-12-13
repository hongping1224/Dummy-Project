using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace From {
    public class Step {
        public string function;
        public string[] parameters;
        private Step() {
            function = "";
            parameters = new string[0];
        }
        public Step(string funcName, string[] parameters) {
            this.function = funcName;
            this.parameters = parameters;
        }

        public override string ToString() {
            StringBuilder b = new StringBuilder();
            b.Append(function);
            foreach (string s in parameters) {
                b.Append(",");
                b.Append(s);
            }
            return b.ToString();
        }
        public void Execute() {

        }

    }
}
