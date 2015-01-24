/// Author: Daniel Anderson  Jan.23,2015
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public partial class member
    {
        public override string ToString()
        {
            return string.Format(" {0,-10}{1,-15}  Address: {2,-18}  Age: {3,-4}  Intrests: {4,-20}", name, sirname, address, age, intrets);
        }
    }
}
