using UnityEngine;

namespace Omnis.Flowchart.Kinship
{
    public class NodeKin : Node
    {
        #region Serialized Fields
        [SerializeField] private TMPro.TMP_Text kinship;
        #endregion

        #region Fields
        private static readonly string[] unisexKin = { "×Ô¼º", "¸¸Ä¸", "¸ç¸ç½ã½ã", "µÜµÜÃÃÃÃ", "×ÓÅ®", "°®ÈË" };
        private static readonly string[] maleKin = { "×Ô¼º", "¸¸Ç×", "¸ç¸ç", "µÜµÜ", "¶ù×Ó", "ÕÉ·ò" };
        private static readonly string[] femaleKin = { "×Ô¼º", "Ä¸Ç×", "½ã½ã", "ÃÃÃÃ", "Å®¶ù", "ÆÞ×Ó" };
        private GenderEnum gender;
        private bool traversed;
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
        protected override void OnModeChanged(NodeMode value)
        {
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

            if (copy) (copy as NodeKin).RecursivelySetTraversed(traversed);
        }
        private void RecursivelySetKinship(int kinIndex = 0, int seniority = 0, string fromKinship = "", NodeKin fromNode = null)
        {
            if (traversed) return;
            traversed = true;

            string myKinship = "";

            if (fromKinship != "")
                myKinship = fromKinship + "µÄ";

            if (seniority > 0) myKinship += StringTweaker.NumberToHanCardinal(seniority.ToString());

            myKinship += gender switch
            {
                GenderEnum.Male => maleKin[kinIndex],
                GenderEnum.Female => femaleKin[kinIndex],
                _ => unisexKin[kinIndex]
            };
            kinship.text = KinshipHandler.Instance.TranslateKinship(myKinship);

            inSlots[0].inLinks.ForEach((link) => {
                link.fromPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(1, 0, myKinship, this);
            });
            if (fromNode)
            {
                bool younger = false;
                for (int i = 0; i < outSlots[0].outLinks.Count; i++)
                {
                    var child = outSlots[0].outLinks[i].toPoint.master.GetComponent<NodeKin>();
                    if (child == fromNode)
                    {
                        younger = true;
                        continue;
                    }
                    child.RecursivelySetKinship(younger ? 3 : 2, i + 1, fromKinship);
                }
            }
            else
            {
                outSlots[0].outLinks.ForEach((link) => {
                    link.toPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(4, 0, myKinship);
                });
            }
            inSlots[1].inLinks.ForEach((link) => {
                link.fromPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(5, 0, myKinship);
            });
            outSlots[1].outLinks.ForEach((link) => {
                link.toPoint.master.GetComponent<NodeKin>().RecursivelySetKinship(5, 0, myKinship);
            });

            if (copy) (copy as NodeKin).RecursivelySetKinship(kinIndex, seniority, fromKinship, fromNode);
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
