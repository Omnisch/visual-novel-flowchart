using UnityEngine;

namespace Omnis.BranchTracker
{
    [RequireComponent(typeof(LineRenderer))]
    public class NodeLink : MonoBehaviour
    {
        #region Serialized Fields
        public Linkable fromPoint;
        public Linkable toPoint;
        #endregion

        #region Fields
        private LineRenderer lineRenderer;
        private bool doNotUpdatePositions;
        #endregion

        #region Interfaces
        public void UpdatePositions()
        {
            if (doNotUpdatePositions) return;
            if (!fromPoint || !toPoint) return;

            Vector3[] positions = {
                VectorTweak.xyn(fromPoint.transform.position, -100f),
                VectorTweak.xyn(toPoint.transform.position, -100f),
            };
            lineRenderer.SetPositions(positions);
        }
        public void Break()
        {
            doNotUpdatePositions = true;
            fromPoint.outLinks.Remove(this);
            toPoint.inLinks.Remove(this);
            Destroy(gameObject);
        }
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
