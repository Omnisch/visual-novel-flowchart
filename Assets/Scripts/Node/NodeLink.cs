using UnityEngine;

namespace Omnis.Flowchart
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
                VectorTweak.xyn(toPoint.transform.position, -100f)
            };
            lineRenderer.SetPositions(positions);
        }
        public void Connect(Linkable fromPoint, Linkable toPoint)
        {
            this.fromPoint = fromPoint;
            this.toPoint = toPoint;
            fromPoint.outLinks.Remove(this);
            fromPoint.outLinks.Add(this);
            toPoint.inLinks.Remove(this);
            toPoint.inLinks.Add(this);
            UpdatePositions();
        }
        public void Break()
        {
            doNotUpdatePositions = true;
            fromPoint.outLinks.Remove(this);
            toPoint.inLinks.Remove(this);
            GameManager.Instance.nodeRegistry.Remove(this);
            Destroy(gameObject);
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            doNotUpdatePositions = false;
            GameManager.Instance.nodeRegistry.Prioritize(this);
        }
        #endregion
    }
}
