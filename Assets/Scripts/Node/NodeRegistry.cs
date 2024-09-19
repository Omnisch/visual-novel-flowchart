using System.Collections.Generic;
using System.Linq;
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
        public void LoadData(FlowchartData newData)
        {
            Clear();
            foreach (var rawNode in newData.nodeData)
            {
                var node = NewNode(VectorTweak.V2ToV3xy(rawNode.position));
                node.Mode = (NodeMode)rawNode.mode;
                nodeList.Add(node);
            }
            foreach (var rawLink in newData.linkData)
            {
                NewLink().Connect(nodeList[rawLink.fromPoint].outSlot, nodeList[rawLink.toPoint].inSlot);
            }
        }
        public Node NewNode() => NewNode(Vector3.zero);
        public Node NewNode(Vector3 worldPosition)
            => Instantiate(GameManager.Instance.settings.nodePrefab, worldPosition, Quaternion.identity).GetComponent<Node>();
        public NodeLink NewLink()
            => Instantiate(GameManager.Instance.settings.linkPrefab).GetComponent<NodeLink>();
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
        #endregion

        #region Functions
        private void UpdateLayer()
        {
            for (int i = 0; i < nodeList.Count; i++)
                nodeList[i].transform.position = new Vector3(nodeList[i].transform.position.x, nodeList[i].transform.position.y, i * -0.1f);
        }
        private void Clear()
        {
            while (nodeList.Count > 0)
                nodeList.First().RemoveSelf();
            while (linkList.Count > 0)
                linkList.First().Break();
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
        public Vector2 position;
        public int mode;
    }
    public struct LinkData
    {
        public int fromPoint;
        public int toPoint;
    }
}
