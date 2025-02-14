namespace Omnis.Flowchart.Kinship
{
    public class NodeKin : Node
    {
        #region Public functions
        public override NodeMode Mode
        {
            get => base.Mode;
            set
            {
                base.Mode = value;
                if (inSlots.Count > 1 && inSlots[1])
                    inSlots[1].gameObject.SetActive(value switch
                    {
                        NodeMode.LeftIn => true,
                        NodeMode.LeftRight => true,
                        _ => false
                    });
                if (outSlots.Count > 1 && outSlots[1])
                    outSlots[1].gameObject.SetActive(value switch
                    {
                        NodeMode.RightOut => true,
                        NodeMode.LeftRight => true,
                        _ => false
                    });
            }
        }

        public void LeftInMode()
        {
            if (outSlots.Count > 1 && outSlots[1]) outSlots[1].BreakAll();
            Mode = NodeMode.LeftIn;
        }
        public void RightOutMode()
        {
            if (inSlots.Count > 1 && inSlots[1]) inSlots[1].BreakAll();
            Mode = NodeMode.RightOut;
        }
        public void LeftRightMode()
        {
            Mode = NodeMode.LeftRight;
        }
        #endregion
    }
}
