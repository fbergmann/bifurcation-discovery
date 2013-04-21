using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBW;

namespace LibBifurcationDiscovery
{
    public class Generation
    {
        public int numOfMembers;
        public double genFitScore;
        public Member[] memArray;

        public Generation()
        {

        }

        public void allocMemberArray(int numMembers)
        {

            this.numOfMembers = numMembers;
            memArray = new Member[numMembers];
        }

        public void sortMemberArray()
        {
            Array.Sort<Member>(memArray, (Member a, Member b) => a.memFitScore.CompareTo(b.memFitScore));
        }

        public void setFitScore()
        {
            //std::stable_sort (this->memArray.begin(), this->memArray.end()/*begin() + (numOfMembers-1)*/);
            genFitScore = memArray[0].memFitScore;
        }

        public void setMember(int index, Member m)
        {
            memArray[index] = m;
        }

        public Member getMember(int index)
        {
            return memArray[index];
        }
    }
}
