using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    public enum NodeMode
    {
        Island,
        Root,
        Branch,
        Leaf,
        LeftIn,
        RightOut,
        LeftRight,
    }

    public struct FlowchartData
    {
        public List<NodeData> nodeData;
        public List<LinkData> linkData;
    }
    public struct NodeData
    {
        public Vector2 position;
        public int mode;
        public string description;
    }
    public struct LinkData
    {
        public int fromPoint;
        public int toPoint;
        public int slotKey;
    }
}
