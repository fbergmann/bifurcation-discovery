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
    public struct Arguments
    {
        private int _NumOfGenerations;
        private int _NumOfMembers;
        private int _NumOfMemSelect;
        private double _Pc;
        private double _Pm;
        private double _Tolerance;
        private int _RandomSeed;
        private double _SimulationTime;
        private int _NUpdate;
        /// <summary>
        /// Summary for Arguments
        /// </summary>
        public Arguments(int numOfGenerations, int numOfMembers, int numOfMemSelect, double Pc, double Pm, double Tolerance, int randomSeed, double simulationTime, int nUpdate)
        {
            _NumOfGenerations = numOfGenerations;
            _NumOfMembers = numOfMembers;
            _NumOfMemSelect = numOfMemSelect;
            _Pc = Pc;
            _Pm = Pm;
            _Tolerance = Tolerance;
            _RandomSeed = randomSeed;
            _SimulationTime = simulationTime;
            _NUpdate = nUpdate;
        }
        public int NumOfGenerations
        {
            get
            {
                return _NumOfGenerations;
            }
        }
        public int NumOfMembers
        {
            get
            {
                return _NumOfMembers;
            }
        }
        public int NumOfMemSelect
        {
            get
            {
                return _NumOfMemSelect;
            }
        }
        public double Pc
        {
            get
            {
                return _Pc;
            }
        }
        public double Pm
        {
            get
            {
                return _Pm;
            }
        }
        public double Tolerance
        {
            get
            {
                return _Tolerance;
            }
        }
        public int RandomSeed
        {
            get
            {
                return _RandomSeed;
            }
        }
        public double SimulationTime
        {
            get
            {
                return _SimulationTime;
            }
        }
        public int NUpdate
        {
            get
            {
                return _NUpdate;
            }
        }
    }
}
