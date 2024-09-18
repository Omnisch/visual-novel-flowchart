using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    public class NodeRegistry : MonoBehaviour
    {
        #region Fields
        [SerializeField] private List<Node> queue;
        #endregion

        #region Interfaces
        public Node NewNode() => NewNode(Vector3.zero);
        public Node NewNode(Vector3 worldPosition)
        {
            var newNode = Instantiate(GameManager.Instance.gameSettings.nodePrefab, worldPosition, Quaternion.identity).GetComponent<Node>();
            Prioritize(newNode);
            return newNode;
        }
        public void Prioritize(Node node)
        {
            queue.Remove(node);
            queue.Add(node);

            UpdateLayer();
        }
        public bool Remove(Node node) => queue.Remove(node);
        #endregion

        #region Functions
        private void UpdateLayer()
        {
            for (int i = 0; i < queue.Count; i++)
                queue[i].transform.position = new Vector3(queue[i].transform.position.x, queue[i].transform.position.y, i * -0.1f);
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
