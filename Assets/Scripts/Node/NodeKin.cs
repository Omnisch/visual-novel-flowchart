using UnityEngine;

namespace Omnis.Flowchart.Kinship
{
    public class NodeKin : Node
    {
        #region Serialized Fields
        [SerializeField] private TMPro.TMP_Text kinship;
        #endregion

        #region Fields
        private GenderEnum gender;
        private bool traversed;
        #endregion

        #region Properties
        public override NodeMode Mode
        {
            get => base.Mode;
            set
            {
                base.Mode = value;
                if (inSlots.Count > 1 && inSlots[1])
                {
                    if (value == NodeMode.RightOut)
                        inSlots[1].BreakAll();
                    inSlots[1].gameObject.SetActive(value switch
                    {
                        NodeMode.LeftIn => true,
                        NodeMode.LeftRight => true,
                        _ => false
                    });
                }
                if (outSlots.Count > 1 && outSlots[1])
                {
                    if (value == NodeMode.LeftIn)
                        outSlots[1].BreakAll();
                    outSlots[1].gameObject.SetActive(value switch
                    {
                        NodeMode.RightOut => true,
                        NodeMode.LeftRight => true,
                        _ => false
                    });
                }
                gender = value switch
                {
                    NodeMode.RightOut => GenderEnum.Male,
                    NodeMode.LeftIn => GenderEnum.Female,
                    _ => GenderEnum.Unisex
                };
            }
        }
        #endregion

        #region Public functions
        public void RightOutMode() => Mode = NodeMode.RightOut;
        public void LeftInMode() => Mode = NodeMode.LeftIn;
        public void LeftRightMode() => Mode = NodeMode.LeftRight;
        public void QueryKinship()
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
            if (fromKinship == "") myKinship = "�Լ�";
            else
            {
                myKinship = fromKinship + "��" + gender switch
                {
                    GenderEnum.Male => relativeKinship switch
                    {
                        "��ĸ" => "����",
                        "��Ů" => "����",
                        _ => "�ɷ�"
                    },
                    GenderEnum.Female => relativeKinship switch
                    {
                        "��ĸ" => "ĸ��",
                        "��Ů" => "Ů��",
                        _ => "����"
                    },
                    _ => relativeKinship
                };
            }
            kinship.text = KinshipHandler.Instance.TranslateKinship(myKinship);

            inSlots[0].inLinks.ForEach((link) => {
                link.fromPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "��ĸ");
            });
            outSlots[0].outLinks.ForEach((link) => {
                link.toPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "��Ů");
            });
            inSlots[1].inLinks.ForEach((link) => {
                link.fromPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "��ż");
            });
            outSlots[1].outLinks.ForEach((link) => {
                link.toPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(myKinship, "��ż");
            });
        }
        #endregion
    }

    public enum GenderEnum
    {
        Unisex,
        Male,
        Female,
    }
}
