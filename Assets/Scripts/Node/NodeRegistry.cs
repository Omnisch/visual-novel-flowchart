using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    public class NodeRegistry : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private List<Node> nodeList;
        [SerializeField] private List<NodeLink> linkList;
        #endregion

        #region Interfaces
        public Node NewNode() => NewNode(Vector3.zero);
        public Node NewNode(Vector3 worldPosition)
        {
            var newNode = Instantiate(GameManager.Instance.gameSettings.nodePrefab, worldPosition, Quaternion.identity).GetComponent<Node>();
            return newNode;
        }
        public void Prioritize(Node node)
        {
            nodeList.Remove(node);
            nodeList.Add(node);

            UpdateLayer();
        }
        public void Prioritize(NodeLink link)
        {
            linkList.Remove(link);
            linkList.Add(link);
        }
        public bool Remove(Node node) => nodeList.Remove(node);
        public bool Remove(NodeLink link) => linkList.Remove(link);
        public FlowchartData Data
        {
            get
            {
                FlowchartData data = new();
                {
                    List<NodeData> nodesData = new();
                    foreach (var node in nodeList)
                    {
                        NodeData nodeData = new();
                        nodeData.position = node.transform.position;
                        nodeData.mode = (int)node.Mode;
                        nodesData.Add(nodeData);
                    }
                    data.nodeData = nodesData;
                }
                {
                    List<LinkData> linksData = new();
                    foreach (var link in linkList)
                    {
                        LinkData linkData = new();
                        linkData.fromPoint = nodeList.FindIndex(node => node == link.fromPoint.master);
                        linkData.toPoint = nodeList.FindIndex(node => node == link.toPoint.master);
                        linksData.Add(linkData);
                    }
                    data.linkData = linksData;
                }
                return data;
            }
        }
        #endregion

        #region Functions
        private void UpdateLayer()
        {
            for (int i = 0; i < nodeList.Count; i++)
                nodeList[i].transform.position = new Vector3(nodeList[i].transform.position.x, nodeList[i].transform.position.y, i * -0.1f);
        }
        #endregion
    }

    public struct FlowchartData
    {
        public List<NodeData> nodeData;
        public List<LinkData> linkData;
    }
    public struct NodeData
    {
        public Vector3 position;
        public int mode;
    }
    public struct LinkData
    {
        public int fromPoint;
        public int toPoint;
    }
}
