using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibRoadRunner;
using SBW;
using libstructural;

namespace LibBifurcationDiscovery
{
    public class OptimizationResult
    {
        public int Iterations { get; set; }
        public double Score { get; set; }
        public List<double> Values { get; set; }
        public List<double> RealEigenValues { get; set; }
        public List<double> ImagEigenValues { get; set; }
    }
}
