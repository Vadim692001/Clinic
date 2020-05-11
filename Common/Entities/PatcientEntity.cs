using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class PatcientEntity<T>
    {
        public static int counter = 0;

        private int id;

        public PatcientEntity()
        {
            id = ++counter;
        }

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                if (counter < value) counter = value;
            }
        }
    }
}
