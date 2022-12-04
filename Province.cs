using Dijkstra_Vietnam;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dijkstra_Vietnam
{
    public class Province
    {
        private string nameProvince { get; set; }
        private string pointName { get; set; }
        private Point provinceLocation { get; set; }

        public Province(string name, string symbol, int x, int y)
        {
            nameProvince = name;
            pointName = symbol;
            Point p = new Point(x, y);
            provinceLocation = p;
        }
        public string getName()
        {
            return nameProvince;
        }
        public string getPointName()
        {
            return pointName;
        }
        public Point getPoint()
        {
            return provinceLocation;
        }
    }
}
