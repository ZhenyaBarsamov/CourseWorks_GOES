using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Msagl;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using MAGL_Test.GraphWrapper;

namespace MAGL_Test {
    public partial class MainForm : Form {
        Microsoft.Msagl.GraphViewerGdi.GViewer viewer;
        Microsoft.Msagl.Drawing.Graph graph;

        public MainForm() {
            // Из NuGet подтянуть:
            // Microsoft.Msagl
            // Microsoft.Msagl.GraphViewerGDI
            InitializeComponent();

            // ПРИМЕР 1
            //create a viewer object 
            viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            //viewer.ToolBarIsVisible = false;
            //create a graph object 
            graph = new Microsoft.Msagl.Drawing.Graph("graph");
            //create the graph content 
            graph.Directed = false;
            graph.AddEdge("A", "B").Attr.ArrowheadAtTarget = Microsoft.Msagl.Drawing.ArrowStyle.None;
            graph.AddEdge("B", "C").LabelText = "Я метка!";
            graph.AddEdge("A", "C").Attr.Color = Microsoft.Msagl.Drawing.Color.Green;
            graph.FindNode("A").Attr.FillColor = Microsoft.Msagl.Drawing.Color.Magenta;
            graph.FindNode("A").Attr.Color = Microsoft.Msagl.Drawing.Color.BlueViolet;
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
            tableLayoutPanel1.Controls.Add(viewer, 0, 0);
            this.ResumeLayout();

            viewer.Click += Viewer_Click;
            //viewer.ObjectUnderMouseCursorChanged += Viewer_Click;
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

        private void button1_Click(object sender, EventArgs e) {
            var node = graph.FindNode("A");
            graph.AddEdge("A", "E");
        }
    }
}
