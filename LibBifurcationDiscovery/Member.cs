using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBW;
using libstructural;

namespace LibBifurcationDiscovery
{
    public class Member
    {
        
        public double memFitScore;
        public int numOfParameters;
        public List<double> pValuesArray;
        public int eigenValueArrayDim;
        public List<double> realEigenValueArray;
        public List<double> complexEigenValueArray;

        public Member()
        {
            numOfParameters = 0;
            memFitScore = 0;
            eigenValueArrayDim = 0;
            pValuesArray = new List<double>();
            realEigenValueArray = new List<double>();
            complexEigenValueArray = new List<double>();
        }

        public Member(Member mem) : this()
        {
            memFitScore = mem.memFitScore;
            numOfParameters = mem.numOfParameters;

            pValuesArray.Clear();
            pValuesArray.AddRange(mem.pValuesArray);

            eigenValueArrayDim = mem.eigenValueArrayDim;
            realEigenValueArray.Clear();
            realEigenValueArray.AddRange(mem.realEigenValueArray);
            complexEigenValueArray.Clear();
            complexEigenValueArray.AddRange(mem.complexEigenValueArray);
        }

        public void initParamArray(List<double> pValues, List<double> minBounds, List<double> maxBounds)
        {
            double randNumb = 0;
            numOfParameters = pValues.Count;
            pValuesArray.Clear();
            for (int i = 0; i < numOfParameters; i++)
            {
                randNumb = Optimizer.rnd.NextDouble();
                pValuesArray.Add(pValues[i] * (0.5 + randNumb));
                if (pValuesArray[i] < minBounds[i])
                {
                    pValuesArray[i] = minBounds[i];
                }
                if (pValuesArray[i] > maxBounds[i])
                {
                    pValuesArray[i] = maxBounds[i];
                }
            }
        }

        public void checkMinMaxBounds(int index, List<double> minBoundsArray, List<double> maxBoundsArray)
        {
            if (pValuesArray[index] < minBoundsArray[index])
            {
                pValuesArray[index] = minBoundsArray[index];
            }
            if (pValuesArray[index] > maxBoundsArray[index])
            {
                pValuesArray[index] = maxBoundsArray[index];
            }
        }


        public static bool operator <(Member a, Member b)
        {
            return a.memFitScore < b.memFitScore;
        }

        public static bool operator >(Member a, Member b)
        {
            return a.memFitScore > b.memFitScore;
        }

        public void SetEigenValues(Complex[] eigen)
        {
            eigenValueArrayDim = eigen.Length;
            realEigenValueArray.Clear();
            complexEigenValueArray.Clear();
            foreach (Complex complex in eigen)
            {
                realEigenValueArray.Add(complex.Real);
                complexEigenValueArray.Add(complex.Imag);
            }
        }

        public void SetEigenValues(SBWComplex[] eigen)
        {
            eigenValueArrayDim = eigen.Length;
            realEigenValueArray.Clear();
            complexEigenValueArray.Clear();
            foreach (SBWComplex complex in eigen)
            {
                realEigenValueArray.Add(complex.Real);
                complexEigenValueArray.Add(complex.Imag);
            }
        }
        public void SetEigenValues(List<double> wr, List<double> wi)
        {
            eigenValueArrayDim = wr.Count;
            realEigenValueArray.Clear();
            complexEigenValueArray.Clear();
            for (int i = 0; i < wr.Count; i++)
            {
                realEigenValueArray.Add(wr[i]);
                complexEigenValueArray.Add(wi[i]);
            }
        }
        public override string ToString()
        {
            return string.Format("score: {0}, numParam: {1}, real: {2}, complex {3}", memFitScore, numOfParameters, realEigenValueArray.Count, complexEigenValueArray.Count);
        }

    }
}
