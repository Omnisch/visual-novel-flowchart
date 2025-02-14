namespace Omnis.Flowchart.Kinship
{
    public class NodeKin : Node
    {
        #region Properties
        private GenderEnum gender;
        public GenderEnum Gender
        {
            get => gender;
            set
            {
                gender = value;
                switch (value)
                {
                    case GenderEnum.Female:
                        if (outSlots.Count > 1 && outSlots[1]) outSlots[1].BreakAll();
                        Mode = NodeMode.LeftIn;
                        break;
                    case GenderEnum.Male:
                        if (inSlots.Count > 1 && inSlots[1]) inSlots[1].BreakAll();
                        Mode = NodeMode.RightOut;
                        break;
                    case GenderEnum.Unisex:
                        Mode = NodeMode.LeftRight;
                        break;
                }
            }
        }

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

        #endregion

        #region Public functions
        public void SetMale() => Gender = GenderEnum.Male;
        public void SetFemale() => Gender = GenderEnum.Female;
        public void SetUnisex() => Gender = GenderEnum.Unisex;
        #endregion

        #region Unity Methods
        protected override void Start()
        {
            base.Start();
            SetUnisex();
        }
        #endregion
    }

    public enum GenderEnum
    {
        Female,
        Male,
        Unisex,
    }
}
