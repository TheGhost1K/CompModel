using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompModel
{
    public abstract class Base
    {
        public int SerialNumber = 0;
        public double requireParamsNumber { protected set; get; }
        public string BaseName { protected set; get; }
        public abstract double f(double x, double y, params double[] parameters);
        public abstract double g(double x, double y, params double[] parameters);
    }
}
