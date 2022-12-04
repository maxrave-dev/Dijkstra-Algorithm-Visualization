using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dijkstra_Vietnam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public List<Province> provinces = new List<Province>();
        DirectedWeightedGraph g = new DirectedWeightedGraph();
        private Graphics graph; 
        private void Form1_Load(object sender, EventArgs e)
        {
            Province haNoi = new Province("Hà Nội", "A", 388, 320);
            Province ninhBinh = new Province("Ninh Bình", "B", 411, 391);
            Province quangNinh = new Province("Quảng Ninh", "C", 506, 294);
            Province langSon = new Province("Lạng Sơn", "D", 438, 212);
            Province caoBang = new Province("Cao Bằng", "E", 401, 124);
            Province phuTho = new Province("Phú Thọ", "F", 328, 253);
            Province haGiang = new Province("Hà Giang", "G", 288, 148);
            Province laoCai = new Province("Lào Cai", "H", 226, 183);
            Province laiChau = new Province("Lai Châu", "I", 169, 179);
            Province dienBien = new Province("Điện Biên", "K", 125, 242);
            provinces.Add(haNoi);
            provinces.Add(ninhBinh);
            provinces.Add(quangNinh);
            provinces.Add(langSon);
            provinces.Add(caoBang);
            provinces.Add(phuTho);
            provinces.Add(haGiang);
            provinces.Add(laoCai);
            provinces.Add(laiChau);
            provinces.Add(dienBien);
            Graphics graph = pnMap.CreateGraphics();
            for (int i = 0; i < provinces.Count; i++)
            {
                lvListProvinces.Items.Add(provinces[i].getPointName());
                lvListProvinces.Items[i].SubItems.Add(provinces[i].getName());
                g.listPoint.Add(provinces[i].getPoint());
                g.InsertVertex(provinces[i].getName());
            }
            g.InsertEdge("Điện Biên", "Lai Châu", 141);
            g.InsertEdge("Lai Châu", "Lào Cai", 101);
            g.InsertEdge("Lào Cai", "Hà Giang", 206);
            g.InsertEdge("Hà Giang", "Cao Bằng", 249);
            g.InsertEdge("Cao Bằng", "Lạng Sơn", 125);
            g.InsertEdge("Lạng Sơn", "Quảng Ninh", 145);
            g.InsertEdge("Quảng Ninh", "Ninh Bình", 209);
            g.InsertEdge("Ninh Bình", "Hà Nội", 95);
            g.InsertEdge("Quảng Ninh", "Hà Nội", 220);
            g.InsertEdge("Lạng Sơn", "Hà Nội", 160);
            g.InsertEdge("Hà Giang", "Hà Nội", 284);
            g.InsertEdge("Lào Cai", "Phú Thọ", 191);
            g.InsertEdge("Phú Thọ", "Hà Nội", 98);
            g.InsertEdge("Hà Giang", "Phú Thọ", 226);
            g.InsertEdge("Điện Biên", "Hà Nội", 490);
            cbSource.Items.Add("Điện Biên");
            cbSource.Items.Add("Hà Nội");
            cbSource.Items.Add("Ninh Bình");
            cbSource.Items.Add("Quảng Ninh");
            cbSource.Items.Add("Lạng Sơn");
            cbSource.Items.Add("Cao Bằng");
            cbSource.Items.Add("Phú Thọ");
            cbSource.Items.Add("Hà Giang");
            cbSource.Items.Add("Lào Cai");
            cbSource.Items.Add("Lai Châu");
            cbSource.Items.Add("Điện Biên");
            cbDestination.Items.Add("Hà Nội");
            cbDestination.Items.Add("Ninh Bình");
            cbDestination.Items.Add("Quảng Ninh");
            cbDestination.Items.Add("Lạng Sơn");
            cbDestination.Items.Add("Cao Bằng");
            cbDestination.Items.Add("Phú Thọ");
            cbDestination.Items.Add("Hà Giang");
            cbDestination.Items.Add("Lào Cai");
            cbDestination.Items.Add("Lai Châu");
            cbDestination.Items.Add("Điện Biên");
        }
        //Vẽ bản đồ ra Panel
        private void pnMap_Paint(object sender, PaintEventArgs e)
        {
            Graphics graph = pnMap.CreateGraphics();
            for (int i = 0; i < provinces.Count; i++)
            {
                SolidBrush brush = new SolidBrush(Color.Purple);
                Brush pointName = new SolidBrush(Color.White);
                graph.FillEllipse(brush, provinces[i].getPoint().X - 5, provinces[i].getPoint().Y - 5, 20, 20);
                graph.DrawString(provinces[i].getPointName(), new Font("Arial", 10), pointName, provinces[i].getPoint().X , provinces[i].getPoint().Y );
            }
            DrawLine();
        }

        private void DrawLine()
        {
                DrawLine("Điện Biên", "Lai Châu");
                DrawLine("Lai Châu", "Lào Cai");
                DrawLine("Lào Cai", "Hà Giang");
                DrawLine("Hà Giang", "Cao Bằng");
                DrawLine("Cao Bằng", "Lạng Sơn");
                DrawLine("Lạng Sơn", "Quảng Ninh");
                DrawLine("Quảng Ninh", "Ninh Bình");
                DrawLine("Ninh Bình", "Hà Nội");
                DrawLine("Quảng Ninh", "Hà Nội");
                DrawLine("Lạng Sơn", "Hà Nội");
                DrawLine("Hà Giang", "Hà Nội");
                DrawLine("Lào Cai", "Phú Thọ");
                DrawLine("Phú Thọ", "Hà Nội");
                DrawLine("Hà Giang", "Phú Thọ");
                DrawLine("Điện Biên", "Hà Nội");
        }
        private void DrawLine(string a, string b)
        {
            Graphics graph = pnMap.CreateGraphics();
            int x = g.GetIndex(a);
            int y = g.GetIndex(b);
            Pen p = new Pen(Color.Black, 2);
            Point point1 = new Point(g.listPoint[x].X , g.listPoint[x].Y );
            Point point2 = new Point(g.listPoint[y].X , g.listPoint[y].Y );
            graph.DrawLine(p, point1, point2);
            graph.DrawString($"{g.adj[x, y]}", new Font("Fira Code", 10), Brushes.Black, new Point((point1.X + point2.X) / 2 - 8, (point1.Y + point2.Y) / 2 + 8));
        }

        private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSource.SelectedIndex != -1 && cbDestination.SelectedIndex != -1)
            {
                pnMap.Controls.Clear();
                pnMap.Refresh();
                DrawLine();
                g.pathIndex.Clear();
                tbCost.Clear();
                tbPath.Clear();
                g.FindPaths(cbSource.SelectedItem.ToString(), cbDestination.SelectedIndex.ToString(), tbCost, tbPath);
                for (int i = 0; i < g.pathIndex.Count - 1; i++)
                {
                    DrawPathLine(i);
                }
            }
        }

        private void cbDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSource.SelectedIndex != -1 && cbDestination.SelectedIndex != -1)
            {
                pnMap.Controls.Clear();
                pnMap.Refresh();
                DrawLine();
                g.pathIndex.Clear();
                tbCost.Clear();
                tbPath.Clear();
                g.FindPaths(cbSource.SelectedItem.ToString(), cbDestination.SelectedIndex.ToString(), tbCost, tbPath);
                for (int i = 0; i < g.pathIndex.Count - 1; i++)
                {
                    DrawPathLine(i);
                }
            }
        }
        //Vẽ lại đường đi ngắn nhất
        private void DrawPathLine(int i)
        {
            Graphics graph = pnMap.CreateGraphics();
            Pen p = new Pen(Color.Aqua, 2);
            Point point1 = new Point(g.pathIndex[i].X , g.pathIndex[i].Y );
            Point point2 = new Point(g.pathIndex[i + 1].X , g.pathIndex[i + 1].Y );
            graph.DrawLine(p, point1, point2);
        }
    }
}
