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
    public class Province//Tạo đối tượng Province
    {
        private string nameProvince { get; set; } //Tên tỉnh
        private string pointName { get; set; } //Kí hiệu trên Map
        private Point provinceLocation { get; set; } //Vị trí trên Map

        public Province(string name, string symbol, int x, int y) //Constructor
        {
            nameProvince = name;
            pointName = symbol;
            Point p = new Point(x, y);
            provinceLocation = p;
        }
        //Getter
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
