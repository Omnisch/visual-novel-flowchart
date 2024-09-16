using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        public GameSettings gameSettings;
        public NodePriority nodePriority;
        public BranchNode root;
        #endregion

        #region Fields
        private BranchNode activeNode;
        #endregion

        #region Interfaces
        public BranchNode ActiveNode
        {
            get => activeNode;
            set
            {
                activeNode = value;
                if (activeNode == null) return;
                nodePriority.Prioritize(activeNode);
                UpdateInputFieldText();
            }
        }
        #endregion

        #region Functions
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
