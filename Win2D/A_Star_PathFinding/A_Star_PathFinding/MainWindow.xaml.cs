using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Linq;
using Windows.UI;

namespace A_Star_PathFinding
{
    public class Node
    {
        // Position on the grid (x, y)
        public Vector2 Position { get; set; }

        // Is this a wall?
        public bool Walkable { get; set; }

        // Costs
        public float G { get; set; } // Distance from start
        public float H { get; set; } // Distance to goal (Heuristic)
        public float F => G + H;     // Total cost

        // The Parent: The node we came from to get here.
        // This is the "breadcrumb trail" we follow back at the end.
        public Node Parent { get; set; }

        public Node(Vector2 pos, bool walkable)
        {
            Position = pos;
            Walkable = walkable;
        }
    }
    public sealed partial class MainWindow : Window
    {
        // --- A* Data Structures (Now Class Level) ---
        PriorityQueue<Node, float> _openSet = new PriorityQueue<Node, float>();
        HashSet<Node> _closedSet = new HashSet<Node>();
        List<Node> _path = new List<Node>(); // The final blue path

        Node[,] _grid; // Our map (2D array for easier access)
        Node _startNode;
        Node _targetNode;

        // Simulation State
        bool _isRunning = false;
        bool _isFinished = false;

        // Settings
        int _rows = 20;
        int _cols = 20;
        float _cellSize = 30;

        public MainWindow()
        {
            this.InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            _grid = new Node[_cols, _rows];
            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    // Create node (default walkable)
                    _grid[x, y] = new Node(new Vector2(x, y), true);
                }
            }

            // Set Start (Top Left) and End (Bottom Right)
            _startNode = _grid[0, 0];
            _targetNode = _grid[_cols - 1, _rows - 1];

            // Setup A* first step
            _startNode.G = 0;
            _startNode.H = GetDistance(_startNode, _targetNode);
            _openSet.Enqueue(_startNode, _startNode.F);

            _isRunning = true;
        }

        // --- The Update Loop (Runs 60 times/sec) ---
        private void MyCanvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            // Only run the algorithm if we are running and not finished
            if (_isRunning && !_isFinished && _openSet.Count > 0)
            {
                StepAStar(); // <--- We do just ONE step here!
            }
        }

        // --- The Logic: One Step of A* ---
        private void StepAStar()
        {
            // 1. Grab best node
            Node current = _openSet.Dequeue();

            // 2. Check Goal
            if (current == _targetNode)
            {
                _isFinished = true;
                _path = RetracePath(_startNode, _targetNode);
                return;
            }

            // 3. Move to Closed Set
            if (_closedSet.Contains(current)) return;
            _closedSet.Add(current);

            // 4. Check Neighbors (Simplified 4-direction logic for demo)
            foreach (var neighbor in GetNeighbors(current))
            {
                if (!neighbor.Walkable || _closedSet.Contains(neighbor)) continue;

                float newG = current.G + 1; // Assuming cost is always 1 for now

                // If neighbor is unvisited (G is 0) or we found a shorter path
                // Note: In real A*, init G to Infinity. Here checking if G==0 is a hack for empty nodes.
                // A better check is: Check if neighbor is NOT in OpenSet yet.
                bool inOpenSet = IsInOpenSet(neighbor);

                if (newG < neighbor.G || !inOpenSet)
                {
                    neighbor.G = newG;
                    neighbor.H = GetDistance(neighbor, _targetNode);
                    neighbor.Parent = current;

                    if (!inOpenSet)
                    {
                        _openSet.Enqueue(neighbor, neighbor.F);
                    }
                }
            }

            // If open set runs dry, no path exists
            if (_openSet.Count == 0) _isFinished = true;
        }

        // --- The Draw Loop (Runs 60 times/sec) ---
        private void MyCanvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            // Draw all nodes
            foreach (var node in _grid)
            {
                Color color = Colors.White; // Default empty

                // Color Logic
                if (_closedSet.Contains(node)) color = Colors.Red;   // Visited
                else if (IsInOpenSet(node)) color = Colors.Green; // Candidates
                if (node == _startNode) color = Colors.Orange;
                if (node == _targetNode) color = Colors.Purple;
                if (_path.Contains(node)) color = Colors.Blue;  // Final Path

                // Draw Rect
                args.DrawingSession.FillRectangle(
                    node.Position.X * _cellSize,
                    node.Position.Y * _cellSize,
                    _cellSize - 2, _cellSize - 2, color);
            }
        }

        // Helper to check if node is in PriorityQueue (PQ doesn't have .Contains natively!)
        // In a real app, you might use a secondary HashSet<Node> to track what is in the OpenSet for speed.
        private bool IsInOpenSet(Node n)
        {
            // This is slow (O(N)), but fine for a simple visualizer.
            // For production, use a HashSet alongside the PQ.
            foreach (var item in _openSet.UnorderedItems)
            {
                if (item.Element == n) return true;
            }
            return false;
        }

        // Include your GetDistance and RetracePath methods here...
        private float GetDistance(Node a, Node b)
        {
            return Vector2.Distance(a.Position, b.Position);
        }

        private List<Node> RetracePath(Node start, Node end)
        {
            var path = new List<Node>();
            var curr = end;
            while (curr != start) { path.Add(curr); curr = curr.Parent; }
            return path;
        }

        // Basic Neighbor logic (Up, Down, Left, Right)
        private List<Node> GetNeighbors(Node n)
        {
            List<Node> result = new List<Node>();
            int x = (int)n.Position.X;
            int y = (int)n.Position.Y;

            if (x > 0) result.Add(_grid[x - 1, y]);
            if (x < _cols - 1) result.Add(_grid[x + 1, y]);
            if (y > 0) result.Add(_grid[x, y - 1]);
            if (y < _rows - 1) result.Add(_grid[x, y + 1]);

            return result;
        }
    }
}