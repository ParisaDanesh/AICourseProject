using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace localSearch
{
    class person
    {
        public int x { get; set; }
        public int y { get; set; }

        public int type { get; set; }

        public int number { get; set; }

        public person(int x,int y,int type,int number)
        {
            this.x = x;
            this.y = y;
            this.type = type;
            this.number = number;
        }

        public bool Equal(person obj)
        {
            if (this.x == obj.x && this.y == obj.y)
            {
                return true;
            }
            else
                return false;
        }
    }
}
