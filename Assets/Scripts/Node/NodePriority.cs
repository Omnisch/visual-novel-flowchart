using System.Collections.Generic;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public class NodePriority : MonoBehaviour
    {
        #region Fields
        [SerializeField] private List<Node> queue;
        #endregion

        #region Interfaces
        public void Prioritize(Node node)
        {
            queue.Remove(node);
            queue.Add(node);

            RearrangePriority();
        }
        public bool Remove(Node node) => queue.Remove(node);
        #endregion

        #region Functions
        private void RearrangePriority()
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
