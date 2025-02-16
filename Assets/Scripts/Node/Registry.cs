using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.Flowchart
{
    public class Registry : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private List<Node> nodeList;
        [SerializeField] private List<Link> linkList;
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
                        NodeData nodeData = new()
                        {
                            position = node.transform.position,
                            mode = (int)node.Mode,
                            description = node.Description
                        };
                        nodesData.Add(nodeData);
                    }
                    data.nodeData = nodesData;
                }
                {
                    List<LinkData> linksData = new();
                    foreach (var link in linkList)
                    {
                        LinkData linkData = new()
                        {
                            fromPoint = nodeList.FindIndex(node => node == link.fromPoint.master),
                            toPoint = nodeList.FindIndex(node => node == link.toPoint.master),
                            slotKey = link.fromPoint.key,
                        };
                        linksData.Add(linkData);
                    }
                    data.linkData = linksData;
                }
                return data;
            }
        }
        public void LoadData(FlowchartData newData)
        {
            if (newData.nodeData == null) return;
            Clear();
            foreach (var rawNode in newData.nodeData)
            {
                var node = NewNode(VectorTweaker.V2ToV3xy(rawNode.position));
                node.Mode = (NodeMode)rawNode.mode;
                node.Description = rawNode.description;
                nodeList.Add(node);
            }
            foreach (var rawLink in newData.linkData)
            {
                NewLink().Connect(nodeList[rawLink.fromPoint].outSlots[rawLink.slotKey], nodeList[rawLink.toPoint].inSlots[rawLink.slotKey]);
            }
        }
        public Node NewNode() => NewNode(Vector3.zero);
        public Node NewNode(Vector3 worldPosition)
            => Instantiate(GameManager.Instance.settings.nodePrefab, worldPosition, Quaternion.identity).GetComponent<Node>();
        public Link NewLink()
            => Instantiate(GameManager.Instance.settings.linkPrefab).GetComponent<Link>();
        public void Prioritize(Node node)
        {
            nodeList.Remove(node);
            nodeList.Add(node);

            UpdateLayer();
        }
        public void Prioritize(Link link)
        {
            linkList.Remove(link);
            linkList.Add(link);
        }
        public bool Remove(Node node) => nodeList.Remove(node);
        public bool Remove(Link link) => linkList.Remove(link);
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
}
