using UnityEngine;

namespace Omnis.Flowchart.Kinship
{
    public class NodeKin : Node
    {
        #region Serialized Fields
        [SerializeField] private TMPro.TMP_Text kinship;
        #endregion

        #region Fields
        private bool traversed;
        #endregion

        #region Properties
        private GenderEnum gender;
        public GenderEnum Gender
        {
            get => gender;
            private set
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
        public void BeginKinship()
        {
            RecursivelySetTraversed(false);
            RecursivelySetKinship();
        }
        #endregion

        #region Functions
        private void RecursivelySetTraversed(bool traversed)
        {
            if (this.traversed == traversed) return;

            this.traversed = traversed;
            inSlots.ForEach((slot) => {
                slot.inLinks.ForEach((link) => {
                    link.fromPoint.master.GetComponent<NodeKin>().RecursivelySetTraversed(traversed);
                });
            });
            outSlots.ForEach((slot) => {
                slot.outLinks.ForEach((link) => {
                    link.toPoint.master.GetComponent<NodeKin>().RecursivelySetTraversed(traversed);
                });
            });
        }
        private void RecursivelySetKinship(string fromKinship = "", string relativeKinship = "")
        {
            if (traversed) return;
            traversed = true;

            string myKinship = "";
            if (fromKinship == "") myKinship = "Œ“";
            else
            {
                myKinship = fromKinship + "µƒ" + Gender switch
                {
                    GenderEnum.Male => relativeKinship switch
                    {
                        "∏∏ƒ∏" => "∏∏«◊",
                        "◊”≈Æ" => "∂˘◊”",
                        _ => "’…∑Ú"
                    },
                    GenderEnum.Female => relativeKinship switch
                    {
                        "∏∏ƒ∏" => "ƒ∏«◊",
                        "◊”≈Æ" => "≈Æ∂˘",
                        _ => "∆ﬁ◊”"
                    },
                    _ => relativeKinship
                };
            }
            kinship.text = KinshipHandler.Instance.TranslateKinship(myKinship);

            inSlots[0].inLinks.ForEach((link) => {
                link.fromPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "∏∏ƒ∏");
            });
            inSlots[1].inLinks.ForEach((link) => {
                link.fromPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "≈‰≈º");
            });
            outSlots[0].outLinks.ForEach((link) => {
                link.toPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "◊”≈Æ");
            });
            outSlots[1].outLinks.ForEach((link) => {
                link.toPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "≈‰≈º");
            });
        }
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
