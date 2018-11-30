using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace From {
    class Step {
        public enum Function {

        }

        public Function func;
        public string[] parameters;

        public override string ToString() {
            StringBuilder b = new StringBuilder();
            b.Append(func.ToString());
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
