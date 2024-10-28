using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class GameManager : InstancedMonoBehaviour
    {
        #region Serialized Fields
        public GameSettings settings;
        public Registry registry;
        [SerializeField] private Node activeNode;
        #endregion

        #region Interfaces
        public static GameManager Instance => instance as GameManager;
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

        #region Functions
        protected override void OnAwake() {}
        #endregion

        #region Unity Methods
        private void Update()
        {
            if (ActiveNode) ActiveNode.OnUpdate();
        }
        #endregion
    }
}
