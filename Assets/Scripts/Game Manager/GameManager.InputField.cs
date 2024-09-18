using TMPro;

namespace Omnis.Flowchart
{
    public partial class GameManager
    {
        #region Serialized Fields
        public TMP_InputField inputField;
        #endregion

        #region Fields
        #endregion

        #region Interfaces
        public void UpdateActiveNodeText() { if (ActiveNode) ActiveNode.Description = inputField.text; }
        #endregion

        #region Functions
        private void UpdateInputFieldText() { if (ActiveNode) inputField.text = ActiveNode.Description; }
        #endregion
    }
}
