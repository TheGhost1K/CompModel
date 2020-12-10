using System;

namespace CompModel
{
    public class Task : Base
    {
        public Task()
        {
            requireParamsNumber = 1;
        }
        public override double f(double x, double y, params double[] parameters)
        {
            if (parameters.Length < requireParamsNumber)
                throw new ArgumentNullException("Недостаточное количество параметров");
            double k = parameters[0];
            return x - x * y - k * (x * x);
        }

        public override double g(double x, double y, params double[] paremeters)
        {
            return -y + x * y;
        }
    }
}
