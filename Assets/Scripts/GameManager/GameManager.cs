using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        public GameSettings gameSettings;
        public NodePriority nodePriority;
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
                nodePriority.Prioritize(activeNode);
                UpdateInputFieldText();
            }
        }
        public LinkSlot TargetSlot { get; set; }
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
