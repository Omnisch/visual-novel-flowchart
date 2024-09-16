using UnityEngine;

namespace Omnis.BranchTracker
{
    [RequireComponent(typeof(LineRenderer))]
    public class NodeLink : MonoBehaviour
    {
        #region Serialized Fields
        public BranchNode fromNode;
        public BranchNode toNode;
        #endregion

        #region Fields
        private LineRenderer lineRenderer;
        private bool doNotUpdatePositions;
        #endregion

        #region Interfaces
        public void UpdatePositions()
        {
            if (doNotUpdatePositions) return;

            Vector3[] positions = {
                VectorTweak.xyn(fromNode.transform.position + fromNode.outOffset, -100f),
                VectorTweak.xyn(toNode.transform.position + toNode.inOffset, -100f),
            };
            lineRenderer.SetPositions(positions);
        }
        public void Break()
        {
            doNotUpdatePositions = true;
            fromNode.childLinks.Remove(this);
            toNode.parentLinks.Remove(this);
            Destroy(gameObject);
        }
        #endregion

        #region Functions
        #endregion

        #region Unity Methods
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            doNotUpdatePositions = false;
        }
        #endregion
    }
}
