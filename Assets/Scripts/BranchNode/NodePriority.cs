using System.Collections.Generic;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public class NodePriority : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private List<BranchNode> queue;
        #endregion

        #region Interfaces
        public void Prioritize(BranchNode node)
        {
            queue.Remove(node);
            queue.Add(node);

            RearrangePriority();
        }
        public bool Remove(BranchNode node) => queue.Remove(node);
        #endregion

        #region Functions
        private void RearrangePriority()
        {
            for (int i = 0; i < queue.Count; i++)
                queue[i].transform.position = new Vector3(queue[i].transform.position.x, queue[i].transform.position.y, i * -0.1f);
        }
        #endregion
    }
}
