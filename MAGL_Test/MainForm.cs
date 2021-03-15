using System;
using System.Windows.Forms;
using Microsoft.Msagl;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;

namespace MAGL_Test {
    public partial class MainForm : Form {
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer;

        public MainForm() {
            // Из NuGet подтянуть:
            // Microsoft.Msagl
            // Microsoft.Msagl.GraphViewerGDI
            InitializeComponent();

            // ПРИМЕР 1

            //create a viewer object 
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //create a graph object 
            Microsoft.Msagl.Drawing.Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            graph.AddEdge("A", "B");
            graph.AddEdge("B", "C");
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("B").Attr.FillColor = Microsoft.Msagl.Drawing.Color.MistyRose;
            Microsoft.Msagl.Drawing.Node c = graph.FindNode("C");
            c.Attr.FillColor = Microsoft.Msagl.Drawing.Color.PaleGreen;
            //c.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Diamond;
            foreach (var node in graph.Nodes) {
                node.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Circle;
            }
            //bind the graph to the viewer 
            viewer.Graph = graph;
            //associate the viewer with the form 
            this.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(viewer);
            this.ResumeLayout();

            viewer.Click += Viewer_Click;

        }

        private void Viewer_Click(object sender, EventArgs e) {
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = sender as Microsoft.Msagl.GraphViewerGdi.GViewer;
            if (viewer.SelectedObject is Microsoft.Msagl.Drawing.Node) {
                Microsoft.Msagl.Drawing.Node node = viewer.SelectedObject as Microsoft.Msagl.Drawing.Node;
                MessageBox.Show($"Это вершина {node.Attr.Id}");
            }
            else if (viewer.SelectedObject is Microsoft.Msagl.Drawing.Edge) {
                Microsoft.Msagl.Drawing.Edge edge = viewer.SelectedObject as Microsoft.Msagl.Drawing.Edge;
                MessageBox.Show($"Это дуга {edge.Source}-{edge.Target}");
            }
        }
    }
}
