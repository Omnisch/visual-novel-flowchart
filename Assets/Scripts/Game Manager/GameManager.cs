using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        public GameSettings settings;
        public Registry registry;
        public ViewportInputHandler viewport;
        [SerializeField] private Node activeNode;
        #endregion

        #region Properties
        public Node ActiveNode
        {
            get => activeNode;
            set
            {
                activeNode = value;
                UpdateInputFieldText();
            }
        }
        #endregion

        #region Public Functions
        public Node CreateNode(Vector3 worldPosition) => registry.NewNode(worldPosition);
        public void FollowActiveNode() => FollowNode(ActiveNode);
        public void FollowNode(Node node)
        {
            if (node == null || viewport == null) return;

            viewport.ForceMoveTo(VectorTweaker.xy(node.transform.position));
        }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            if (!EnsureSingleton())
                return;
        }

        private void Update()
        {
            if (ActiveNode) ActiveNode.OnUpdate();
        }
        #endregion
    }
}
