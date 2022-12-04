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
    public class Vertex
    {
        public String name;
        public int status;
        public int predecessor;
        public int pathLength;
        public Vertex(String name)
        {
            this.name = name;
        }
    }

    class DirectedWeightedGraph
    {
        public readonly int MAX_VERTICES = 30;

        public int n =0;
        int e;
        public int[,] adj;
        public Vertex[] vertexList;
        private readonly int INFINITY = 9999999;
        private readonly int PERMANENT = 2;
        private readonly int TEMPORARY = 1;
        private readonly int NIL = -1;
        public List<Point> listPoint = new List<Point>();
        public List<Point> pathIndex = new List<Point>();

        public DirectedWeightedGraph()
        {
            adj = new int[MAX_VERTICES, MAX_VERTICES];
            vertexList = new Vertex[MAX_VERTICES];
        }

        private void Dijkstra(int s)
        {
            int v, c;
            for (v = 0; v < n; v++)
            {
                vertexList[v].status = TEMPORARY;
                vertexList[v].pathLength = INFINITY;
                vertexList[v].predecessor = NIL;
            }
            vertexList[s].pathLength = 0;
            while (true)
            {
                c = TempVertexWithMinPL();
                if (c == NIL)
                    return;
                vertexList[c].status = PERMANENT;
                for (v = 0; v < n; v++)
                {
                    if (IsAdjacent(c, v) && vertexList[v].status == TEMPORARY)
                    {
                        if (vertexList[c].pathLength + adj[c, v] < vertexList[v].pathLength)
                        {
                            vertexList[v].predecessor = c;
                            vertexList[v].pathLength = vertexList[c].pathLength + adj[c, v];
                        }
                    }
                }
            }

        }

        public void FindPaths(string source, string last, TextBox tbCost, TextBox tbPath)
        {
            int s = GetIndex(source);
            Dijkstra(s);

            int v = Convert.ToInt32(last);
            {
                if (v != s)
                {
                    if (vertexList[v].pathLength == INFINITY)
                    {
                        tbPath.Text += "\tNo path \n";
                    }
                    else
                    {
                        FindPath(s, v, tbCost, tbPath);
                    }
                }
            }
        }

        public void FindPath(int s, int v, TextBox tbCost, TextBox tbPath)
        {
            int i, u;
            int[] path = new int[n];
            int sd = 0;
            int count = 0;
            while (v != s)
            {
                count++;
                path[count] = v;
                u = vertexList[v].predecessor;
                sd += adj[u, v];
                v = u;
            }

            count++;
            if (count >= n)
            {
                MessageBox.Show("Error!", "Notify!");
                
            }
            path[count] = s;
            for (i = count; i >= 1; i--)
            {
                pathIndex.Add(listPoint[path[i]]);
                if (tbPath.Text == "")
                {
                    tbPath.Text += vertexList[path[i]].name;
                }
                else
                {
                    tbPath.Text += " -> " + vertexList[path[i]].name;
                }
            }
            tbCost.Text = $"{sd}";
        }

        public int GetIndex(string s)
        {
            for (int i = 0; i < n; i++)
            {
                if (s.Equals(vertexList[i].name))
                    return i;
            }
            throw new System.InvalidOperationException("Invalid Vertex");
        }

        public void InsertVertex(string name)
        {
            vertexList[n++] = new Vertex(name);
        }
        private bool IsAdjacent(int u, int v)
        {
            return adj[u, v] != 0;
        }

        private int TempVertexWithMinPL()
        {
            int min = INFINITY;
            int x = NIL;
            for (int v = 0; v < n; v++)
            {
                if (vertexList[v].status == TEMPORARY && vertexList[v].pathLength < min)
                {
                    min = vertexList[v].pathLength;
                    x = v;
                }
            }
            return x;
        }

        public void InsertEdge(string v1, string v2, int v3)
        {
            int i = GetIndex(v1);
            int j = GetIndex(v2);
            adj[i, j] = v3;
            adj[j, i] = v3;
        }

    }
}
