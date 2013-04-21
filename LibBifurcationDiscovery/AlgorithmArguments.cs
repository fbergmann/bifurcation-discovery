using System;
using System.Collections;
using System.Collections.Generic;
using LibRoadRunner;
using SBW;
using Troschuetz.Random;
using libstructural;

namespace LibBifurcationDiscovery
{
    public struct AlgorithmArguments
    {
        /// <summary>
        /// Summary for AlgorithmArguments
        /// </summary>
        public AlgorithmArguments(int evalOption, List<Tuple<String, double, double, double>> nameValueBounds, Arguments args)
            : this()
        {
            EvalOption = evalOption;
            NameValueBounds = nameValueBounds;
            Args = args;
        }

        public int EvalOption { get; set; }

        public List<Tuple<string, double, double, double>> NameValueBounds { get; set; }

        public Arguments Args { get; set; }
    }
}
