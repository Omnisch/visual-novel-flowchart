using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    public class NodeRegistry : MonoBehaviour
    {
        #region Fields
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
        #endregion

        #region Functions
        private void UpdateLayer()
        {
            for (int i = 0; i < nodeList.Count; i++)
                nodeList[i].transform.position = new Vector3(nodeList[i].transform.position.x, nodeList[i].transform.position.y, i * -0.1f);
        }
        #endregion

        #region Unity Methods
        private void Start()
        {
            Prioritize(GameManager.Instance.root);
        }
        #endregion
    }
}
