using System;
using System.Collections;
using System.Collections.Generic;
using LibRoadRunner;
using SBW;
using Troschuetz.Random;
using libstructural;

namespace LibBifurcationDiscovery
{
    public class Optimizer
    {
        public const int OSCILLATOR = 1;
        public const int TURNINGPOINT = 2;
        public const int SWITCH = 3;
        public const int HOPFBIFURCATION = 4;
        internal int numPoints = 100;

        /// <summary>
        /// Initializes a new instance of the Optimizer class.
        /// </summary>
        public Optimizer()
        {
            maxBoundsArray = new List<double>();
            minBoundsArray = new List<double>();
            pValues = new List<double>();
            pNames = new List<string>();
            rr = new RoadRunner();
        }

        internal static MT19937Generator rnd = new MT19937Generator();
        private double Pc;
        private double Pm;
        private double Tolerance;
        private int evalOption;
        private bool hasUpdateStatusMethod;
        private Member initMember;
        public static bool isProcessActive;
        private List<double> maxBoundsArray;
        private List<double> minBoundsArray;
        private int nUpdate;
        private int numOfGenerations;
        private int numOfMemSelect;
        private int numOfMembers;
        private int numOfParameters;
        private List<string> pNames;
        private List<double> pValues;
        private int randomSeed;

        private RoadRunner rr;
        private double simulationTime;

        public Dictionary<string, bool> CheckForHopfBifurcation(string sbmlInput, List<double> values,
            List<string> pNamesVector)
        {
            var result = new Dictionary<string, bool>();
            rr.loadSBML(sbmlInput);
            pValues = values;

            for (int i = 0; i < pNamesVector.Count; i++)
            {
                bool isHopfBifurcationFlag = checkHopfBifurcation(pValues, pNamesVector, i);
                result[pNamesVector[i]] = isHopfBifurcationFlag;
            }
            return result;
        }

        public Dictionary<string, bool> CheckForSwitch(string sbmlInput, List<double> values, List<string> pNamesVector,
            int speciesIndex)
        {
            var result = new Dictionary<string, bool>();
            rr.loadSBML(sbmlInput);
            pValues = values;

            for (int i = 0; i < pNamesVector.Count; i++)
            {
                bool isSwitchFlag = false;
                bool isBiStableFlag = checkBiStability(pValues, pNamesVector, i, speciesIndex);
                /*Now check if it is really a switch or a point-care bifurcation*/
                if (isBiStableFlag)
                {
                    isSwitchFlag = checkToConfrimSwitch(pValues, pNamesVector, i, speciesIndex);
                }
                result[pNamesVector[i]] = isSwitchFlag;
            }
            return result;
        }

        public void calcDeterminant(double[][] a, int N, out double determinant)
        {
            double realFit = 0.0;
            double imagFit = 0.0;
            int eigen_rows;
            var wr = new List<double>();
            var wi = new List<double>();
            Complex[] read_eigen_array = LA.GetEigenValues(a);
            eigen_rows = read_eigen_array.Length;
            N = eigen_rows;

            for (int i = 0; i < N; i++)
            {
                wr.Add(read_eigen_array[i].Real);
                wi.Add(read_eigen_array[i].Imag);
            }


            /*Calculate the product of eigen values*/
            realFit = wr[0];
            imagFit = wi[0];
            double tempRealFit = 0.0;
            for (int i = 1; i < N; i++)
            {
                tempRealFit = realFit;
                realFit = realFit * wr[i] - imagFit * wi[i];
                imagFit = tempRealFit * wi[i] + imagFit * wr[i];
            }
            determinant = realFit; //Using only real part because, we assume the eigen values
            //come in complex conjugates, so the imag parts will be gone
            //when we take the product.
        }

        private bool checkBiStability(List<double> optPValuesArray, List<string> pNames, int checkParamIndex,
            int speciesIndex)
        {
            bool isBiStable = false;
            int numOfParameters = optPValuesArray.Count;
            double tempCheckParamValue;
            var decFloatingSpeciesArray = new double[20];
            var incFloatingSpeciesArray = new double[20];
            double speciesValsSum = 0.0;            
            double[] speciesConcArray;

            //Set the parameter values to Optimized Parameter Values
            for (int i = 0; i < pNames.Count; i++)
            {
                rr.setValue(pNames[i], optPValuesArray[i]);
            }
            
            rr.setTimeStart(0);
            rr.setTimeEnd(simulationTime);

            BringToSteadyStateOrThrow();


            //Now decrement the parameter value and simulate untill steady state is reached.
            //Do this 20 times and store the species concentration in an array.
            for (int i = 0; i < 20; i++)
            {
                //Now Decrement the ith parameter value and calculate the species conc. at steady state
                tempCheckParamValue = (1.00 - 0.01 * (i + 1)) * optPValuesArray[checkParamIndex];

                rr.setValue(pNames[checkParamIndex], tempCheckParamValue);
                BringToSteadyStateOrThrow();
                speciesConcArray = rr.getFloatingSpeciesConcentrations();
                decFloatingSpeciesArray[i] = speciesConcArray[speciesIndex];
            }

            rr.reset();

            rr.setValue(pNames[checkParamIndex], optPValuesArray[checkParamIndex]);
            rr.setTimeStart(0);
            rr.setTimeEnd(simulationTime);

            BringToSteadyStateOrThrow(1);

            //Now increment the parameter value and simulate untill steady state is reached.
            //Do this 20 times and store the species concentration in an array.
            for (int i = 0; i < 20; i++)
            {
                //Now Increment the ith parameter value and calculate the species conc. at steady state
                tempCheckParamValue = (1.00 + 0.01 * (i + 1)) * optPValuesArray[checkParamIndex];
                rr.setValue(pNames[checkParamIndex], tempCheckParamValue);
                BringToSteadyStateOrThrow();
                speciesConcArray = rr.getFloatingSpeciesConcentrations();
                incFloatingSpeciesArray[i] = speciesConcArray[speciesIndex];
            }

            speciesValsSum = 0.0;
            for (int i = 2; i < 20; i++)
            {
                speciesValsSum = speciesValsSum + decFloatingSpeciesArray[i];
            }
            double decFloatSpeciesMean = speciesValsSum / 18;

            speciesValsSum = 0.0;
            for (int i = 2; i < 20; i++)
            {
                speciesValsSum = speciesValsSum + incFloatingSpeciesArray[i];
            }
            double incFloatSpeciesMean = speciesValsSum / 18;

            if (decFloatSpeciesMean > 2 * incFloatSpeciesMean || incFloatSpeciesMean > 2 * decFloatSpeciesMean)
            {
                isBiStable = true;
            }

            return isBiStable;
        }

        public bool checkHopfBifurcation(List<double> optPValuesArray, List<string> pNames, int checkParamIndex)
        {
            //Set the parameter values to Optimized Parameter Values
            for (int i = 0; i < pNames.Count; i++)
            {
                rr.setValue(pNames[i], optPValuesArray[i]);
            }

            double optEigenRealPart = getRealPart_ComplexEigenVal_MinReal();

            rr.setValue(pNames[checkParamIndex], (1.05) * optPValuesArray[checkParamIndex]);

            double incEigenRealPart = getRealPart_ComplexEigenVal_MinReal();

            if (optEigenRealPart * incEigenRealPart < 0.0)
            {
                return true;
            }

            rr.setValue(pNames[checkParamIndex], (0.95) * optPValuesArray[checkParamIndex]);


            double decEigenRealPart = getRealPart_ComplexEigenVal_MinReal();

            if (optEigenRealPart * decEigenRealPart < 0.0)
            {
                return true;
            }
            return false;
        }

        public bool checkToConfrimSwitch(List<double> pValues, List<string> pNames, int pIndex, int speciesIndex)
        {
            bool isSwitchFlag = false;
            string parameterName;
            int numOfParameters = pValues.Count;
            double optDeterminant = 0;
            double determinant = 0;
            var wr = new List<double>();
            var wi = new List<double>();


            rr.reset();
            for (int i = 0; i < numOfParameters; i++)
            {
                rr.setValue(pNames[i], pValues[i]);
            }
                                
            rr.setTimeStart(0);
            rr.setTimeEnd(simulationTime);
            BringToSteadyStateOrThrow(1);

            double[][] read_jacobian_array = rr.getReducedJacobian();
            int N = read_jacobian_array.Length; 


            double[][] a = rr.getReducedJacobian();
                
            //calculate determinant
            calcDeterminant(read_jacobian_array, N, out optDeterminant);


            var incRatesOfChange = new List<double>();
            var decRatesOfChange = new List<double>();
                
            var ratesOfChangeVector = new List<double>();
                
            double[] reactionRates;


            int Nr_rows, Nr_cols;
            double[][] read_Nr_array = rr.getNrMatrix();
            Nr_rows = read_Nr_array.Length;
            Nr_cols = read_Nr_array[0].Length;

            double sumRates = 0.0;

            //Now increment parameter value corresponding to pIndex and recalcute the steadystate.
            rr.setValue(pNames[pIndex], pValues[pIndex] * 1.05);

            reactionRates = rr.getReactionRates();


            incRatesOfChange.Clear();
            for (int i = 0; i < Nr_rows; i++)
            {
                sumRates = 0.0;
                for (int j = 0; j < reactionRates.Length; j++)
                {
                    sumRates = sumRates + read_Nr_array[i][j] * reactionRates[j];
                }
                incRatesOfChange.Add(sumRates);
            }

            //Now decrement parameter value corresponding to pIndex and recalcute the steadystate.
            rr.setValue(pNames[pIndex], pValues[pIndex] * 0.95);

            reactionRates = rr.getReactionRates();

            decRatesOfChange.Clear();
            for (int i = 0; i < Nr_rows; i++)
            {
                sumRates = 0.0;
                for (int j = 0; j < reactionRates.Length; j++)
                {
                    sumRates = sumRates + read_Nr_array[i][j] * reactionRates[j];
                }
                decRatesOfChange.Add(sumRates);
            }

            ratesOfChangeVector.Clear();
            //Rates of change = ( f(x+delta_h) - f(x-delta_h) )/ (2*delta_h)
            for (int i = 0; i < incRatesOfChange.Count; i++)
            {
                ratesOfChangeVector.Add((incRatesOfChange[i] - decRatesOfChange[i]) / (0.1 * pValues[pIndex]));
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    a[j][i] = ratesOfChangeVector[j];
                }
                calcDeterminant(a, N, out determinant);

                if ((Math.Abs(determinant) > Math.Abs((1.1) * optDeterminant)))
                {
                    isSwitchFlag = true;
                    break;
                }
                    
                for (int j = 0; j < N; j++)
                {
                    a[j][i] = read_jacobian_array[j][i];
                }
            }
            return isSwitchFlag;
        }


        private void SetCurrentValues(List<string> names, List<double> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                rr.setValue(names[i], values[i]);
            }
        }
        private double ComputeScore(int N, List<double> wr, List<double> wi)
        {
            double norm;
            double realFit;
            double imagFit;

            if (evalOption == OSCILLATOR)
            {
                /*Eliminate networks with small real and imag eigen values*/
                bool fndZeroEigenValueFlag = false;
                for (int i = 0; i < N; i++)
                {
                    if ((Math.Abs(wr[i]) < 10E-6) && (Math.Abs(wi[i]) < 10E-3))
                    {
                        fndZeroEigenValueFlag = true;
                    }
                }

                if (fndZeroEigenValueFlag)
                {
                    var ae = new BifException("Error in sbwBifGAOptimize",
                        "Need to reset parameterValues!!!");
                    throw ae;
                }

                /*Calculate the fitness based on the eigen values*/
                realFit = 1;
                imagFit = 1;
                for (int i = 0; i < N; i++)
                {
                    realFit = realFit * wr[i];
                    imagFit = imagFit * (1 - (0.99) * Math.Exp(-1 * Math.Abs(wi[i])));
                }
                realFit = Math.Abs(realFit);
                norm = realFit / imagFit;
            }
            else
            {
                double eigenValsProd = 0.0;
                realFit = wr[0];
                imagFit = wi[0];
                double tempRealFit;
                for (int i = 1; i < N; i++)
                {
                    tempRealFit = realFit;
                    realFit = realFit * wr[i] - imagFit * wi[i];
                    imagFit = tempRealFit * wi[i] + imagFit * wr[i];
                }
                eigenValsProd = Math.Abs(realFit); //Using only real part because, we assume the eigen values
                //come in complex conjugates, so the imag parts will be gone
                //when we take the product.

                int minEigenIndex;
                bool hasRealEigenValue;
                var bufferArray = new List<double>();
                double eigenValsExceptMinProd = 0.0;

                hasRealEigenValue = false;
                for (int i = 0; i < N; i++)
                {
                    if (Math.Abs(wi[i]) > 0.0) // Check if the imaginary part is greater than zero.
                    {
                        // If it is so then assign 10^6 to bufferArray Element.
                        bufferArray.Add(10E6);
                    }
                    else // Else store the absolute value of real eigen value.
                    {
                        bufferArray.Add(Math.Abs(wr[i]));
                        hasRealEigenValue = true;
                    }
                }

                if (hasRealEigenValue)
                {
                    minEigenIndex = findMinRealEigenValueIndex(bufferArray);

                    realFit = 1.0;
                    imagFit = 0.0;
                    for (int i = 0; i < N; i++)
                    {
                        if (i != minEigenIndex)
                        {
                            tempRealFit = realFit;
                            realFit = realFit * wr[i] - imagFit * wi[i];
                            imagFit = tempRealFit * wi[i] + imagFit * wr[i];
                        }
                    }
                    if (N == 1) //If there is only one eigenValue and that is real then
                    {
                        // the product of eigen values without that will be zero
                        eigenValsExceptMinProd = 0.0;
                    }
                    else
                    {
                        eigenValsExceptMinProd = Math.Abs(realFit);
                    }
                }
                else
                {
                    eigenValsExceptMinProd = eigenValsProd;
                }
                norm = eigenValsProd / (1.000 - 0.999 * Math.Exp(-1.0 * eigenValsExceptMinProd));
            }
            return norm;
        }
        //Objective Function for Bifurcation
        //Description: This function returns the fitness value of the input member
        //Inputs: pNames			- parameterNames
        //		  numOfParameters	- number of Parameters
        //		  m					- Member whose fitness value is to be calculated
        //		  origPValues		- Initial values of the input parameters
        //		  evalOption		- Objective function to be evaluated - 'TurningPoint' or 'oscillator'
        //Output: Fitness value of the member.
        //This method calls the simulate and steady state methods of Simulator module to get the s
        public void compObjFnFitnessForInitMem(ref Member m)
        {
            double norm;

            try
            {

                SetCurrentValues(pNames, pValues);

                rr.setTimeStart(0);
                rr.setTimeEnd(simulationTime);

                BringToSteadyStateOrThrow(5, true);

                norm = ComputeScoreAndUpdateEigenValues(m);
            }
            catch
            {

                throw new BifException("Could not compute Objective function for initial guess",
                    "Please give another guess");

            }
            m.memFitScore = norm;
        }

        //CrossOver: - Generates two children from two parents using cross over probability Pc.
        //Description: This method generates a random number 'r'. If the generated random number is less than
        //cross over probability Pc, then cross over takes place between the two parents to give two children.
        //If the random number generated is less than Pc/2 then a cross over takes place between the two children
        //that were generated in the previous step. If the random number generated is greater than 'r' then children
        //are cloned directly from parents without crossover.
        public void crossOver(Member parent1, Member parent2, ref Member child1, ref Member child2)
        {
            double r = rnd.NextDouble();

            if (r >= Pc)
            {
                child1 = new Member(parent1);
                child2 = new Member(parent2);
                return;
            }


            int numParams = parent1.numOfParameters;
            var lambdaArray = new double[numParams];
            for (int i = 0; i < numParams; i++)
            {
                lambdaArray[i] = rnd.NextDouble() * 2 - 0.5;
            }

            child1.pValuesArray.Clear();
            child1.numOfParameters = numParams;
            for (int i = 0; i < numParams; i++)
            {
                child1.pValuesArray.Add(
                    Math.Abs(lambdaArray[i] * parent1.pValuesArray[i] + (1 - lambdaArray[i]) * parent2.pValuesArray[i]));
                child1.checkMinMaxBounds(i, minBoundsArray, maxBoundsArray);
            }

            child2.pValuesArray.Clear();
            child2.numOfParameters = numParams;
            for (int i = 0; i < numParams; i++)
            {
                child2.pValuesArray.Add(
                    Math.Abs((1 - lambdaArray[i]) * parent1.pValuesArray[i] + lambdaArray[i] * parent2.pValuesArray[i]));
                child2.checkMinMaxBounds(i, minBoundsArray, maxBoundsArray);
            }


        }

        private double ComputeScoreAndUpdateEigenValues(Member m)
        {
            double norm;
            double[][] jacobian = rr.getReducedJacobian();
            Complex[] eigenValues = LA.GetEigenValues(jacobian);
            int N = eigenValues.Length;
            var wr = new List<double>();
            var wi = new List<double>();
            for (int i = 0; i < N; i++)
            {
                wr.Add(eigenValues[i].Real);
                wi.Add(eigenValues[i].Imag);
            }

            norm = ComputeScore(N, wr, wi);

            m.SetEigenValues(eigenValues);
            return norm;
        }
        public void evalObjFnFitness(ref Member m)
        {
            double norm;

            try
            {

                SetCurrentValues(pNames, m.pValuesArray);

                rr.setTimeStart(0);
                rr.setTimeEnd(simulationTime);

                BringToSteadyStateOrThrow(5, true);

                norm = ComputeScoreAndUpdateEigenValues(m);
            }
            catch
            {

                m = initMember;
                norm = initMember.memFitScore;

            }
            m.memFitScore = norm;
        }

        private int findMinRealEigenValueIndex(List<double> eigenArray)
        {

            int minIndex = 0;
            double minValue = Math.Abs(eigenArray[0]);
            for (int i = 1; i < eigenArray.Count; i++)
            {
                if (Math.Abs(eigenArray[i]) < minValue)
                {
                    minValue = Math.Abs(eigenArray[i]);
                    minIndex = i;
                }
            }
            return minIndex;
        }

        /// <summary>
        /// Brings the model to steady state or throws an exception if it cant
        /// </summary>
        /// <param name="verifyConcentrations">if true, the floating species concentration will be checked</param>
        /// <param name="cutoff">the value considered as close enough to steady state</param>
        /// <param name="retries">number of tries</param>
        /// <returns></returns>
        private double BringToSteadyStateOrThrow(int retries = 20, bool verifyConcentrations=false, double cutoff = 10E-5)
        {
            double steadyStateValue = 1;
            bool fndSteadyState = false;
            int simCount = 1;
            
            while (!fndSteadyState && simCount < retries)
            {
                rr.reset();
                try
                {
                    for (int k = 0; k < simCount; k++)
                    {
                        rr.simulate();
                    }
                    steadyStateValue = rr.steadyState();
                }
                catch (SBWException)
                {
                    //simCount++;
                }
                if (steadyStateValue < cutoff)
                {
                    fndSteadyState = true;
                    if (verifyConcentrations)
                    {
                        double[] ststSpeciesValues = rr.getFloatingSpeciesConcentrations();
                        for (int i = 0; i < ststSpeciesValues.Length; i++)
                        {
                            if (ststSpeciesValues[i] < 0)
                            {
                                fndSteadyState = false;
                                simCount++;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    simCount++;
                }
            }
            if (simCount == retries)
            {
                var ae = new BifException("Error in sbwBifGAOptimize",
                    "Model has not reached steady state!!!");
                throw ae;
            }
            return steadyStateValue;
        }
        //Mutation
        //Description: The members are mutated based on mutation probability - Pm.
        //Generates a random number 'r'. If the random number generated is less than Pm then mutation is carried.
        //The selection of the parameter that undergoes mutation is again based on a random number - 'randomParam'
        //which can take any value from 0 to num_of_parameters.

        public double getRealPart_ComplexEigenVal_MinReal()
        {
            bool hasImagEigenValue = false;
            rr.setTimeStart(0);
            rr.setTimeEnd(simulationTime);

            BringToSteadyStateOrThrow();
            
            var redJacobian = rr.getReducedJacobian();
            var eigenValues = LA.GetEigenValues(redJacobian);

            var bufferArray = new List<double>();
            for (int i = 0; i < eigenValues.Length; i++)
            {
                if (Math.Abs(eigenValues[i].Imag) == 0.0) // Check if the imaginary part is greater than zero. 
                {
                    // If it is so then assign 10^6 to bufferArray Element.
                    bufferArray.Add(10E6);
                }
                else // Else store the absolute value of real eigen value.
                {
                    bufferArray.Add(Math.Abs(eigenValues[i].Real));
                    hasImagEigenValue = true;
                }
            }

            double result;
            if (hasImagEigenValue)
            {
                int minEigenIndex = findMinRealEigenValueIndex(bufferArray);
                result = eigenValues[minEigenIndex].Real;
            }
            else
            {
                result = 0.0;
            }

            return result;
        }

        public void mutate(ref Member child)
        {
            double r = rnd.NextDouble();

            if (!(r < Pm)) return;
            
            int numParams = child.numOfParameters;

            r = rnd.NextDouble();
            var randomParam = (int)Math.Floor(r * (numParams));

            r = rnd.NextDouble();
            //var old = child.pValuesArray[randomParam];
            
            child.pValuesArray[randomParam] = pValues[randomParam] * (0.5 + r);
            child.checkMinMaxBounds(randomParam, minBoundsArray, maxBoundsArray);

            //System.Diagnostics.Debug.WriteLine(string.Format(
            //    "mutate {0} from {1} to {2}", pNames[randomParam], old, child.pValuesArray[randomParam]
            //    ));


        }


        private void NotifyAboutNumberOfSImulations(Action<ArrayList> updateStatus, int i)
        {
            if (i % nUpdate != 0) return;

            if (!hasUpdateStatusMethod) return;

            try
            {
                updateStatus(new ArrayList(new object[] { 0, i }));
            }
            catch (SBWException ae)
            {
                ae = new BifException("Error in sbwBifGAOptimize",
                    "Unable to call update status!!!");
                throw ae;
            }
        }

        public OptimizationResult optimizeWithGeneticAlgo(string sbmlInput, AlgorithmArguments algoArgs
                , Action<ArrayList> updateStatus = null)
        {
            hasUpdateStatusMethod = updateStatus != null;
            evalOption = algoArgs.EvalOption;
            try
            {
                isProcessActive = true;
                if (rr == null) rr = new RoadRunner();

                rr.loadSBML(sbmlInput);
                rr.setNumPoints(numPoints);

                #region OSCillator

                if (algoArgs.EvalOption == OSCILLATOR || algoArgs.EvalOption == TURNINGPOINT)
                {
                    //Read num of Generations
                    numOfGenerations = algoArgs.Args.NumOfGenerations;

                    //Read num of Members in Population
                    numOfMembers = algoArgs.Args.NumOfMembers;

                    //Read num of Parameters in Member
                    numOfParameters = 0;

                    pNames.Clear();
                    pValues.Clear();
                    minBoundsArray.Clear();
                    maxBoundsArray.Clear();
                    foreach (var nameValueBound in algoArgs.NameValueBounds)
                    {
                        pNames.Add(nameValueBound.Item1);
                        pValues.Add(nameValueBound.Item2);
                        minBoundsArray.Add(nameValueBound.Item3);
                        maxBoundsArray.Add(nameValueBound.Item4);
                        ++numOfParameters;
                    }

                    //Read num of Members to Select from Population
                    numOfMemSelect = algoArgs.Args.NumOfMemSelect;

                    //Read the value of Pc - Crossover probability
                    Pc = algoArgs.Args.Pc;

                    //Read the value of Pm - Mutation probability
                    Pm = algoArgs.Args.Pm;

                    //Read the value of TOLERANCE Level
                    Tolerance = algoArgs.Args.Tolerance;

                    //Read the value of Random_Number Seed
                    randomSeed = algoArgs.Args.RandomSeed;

                    simulationTime = algoArgs.Args.SimulationTime;

                    //Read the number of iterations after which you want to receive the update
                    nUpdate = algoArgs.Args.NUpdate;

                    //Try to compute the steady state using the initial parameter values.
                    //If steady state cannot be reached throw an exception to user asking him
                    //to reset the initial parameter values.
                    initMember = new Member { numOfParameters = pValues.Count, pValuesArray = new List<double>(pValues) };

                    try
                    {
                        compObjFnFitnessForInitMem(ref initMember);
                    }
                    catch (SBWException)
                    {
                        var ex =
                            new BifException(
                                "Unable to calculate steadystate value with the current initial parameter values.",
                                "Please start with different set of parameter values!!! ");
                        throw ex;
                    }

                    var generationArray = new[] { new Generation(), new Generation() };

                    //using only two generation objects
                    generationArray[0].allocMemberArray(algoArgs.Args.NumOfMembers);
                    var m = new Member();

                    rnd = algoArgs.Args.RandomSeed == 0
                        ? new MT19937Generator()
                        : new MT19937Generator(algoArgs.Args.RandomSeed);

                    for (int i = 0; i < algoArgs.Args.NumOfMembers; i++)
                    {
                        m.initParamArray(pValues, minBoundsArray, maxBoundsArray);
                        evalObjFnFitness(ref m);
                        generationArray[0].setMember(i, m);
                        //cout << "Member: " << i << endl;
                        if (!isProcessActive)
                        {
                            return new OptimizationResult
                            {
                                Iterations = 0,
                                Score = generationArray[0].genFitScore,
                                Values = generationArray[0].memArray[0].pValuesArray,
                                RealEigenValues = generationArray[0].memArray[0].realEigenValueArray,
                                ImagEigenValues = generationArray[0].memArray[0].complexEigenValueArray
                            };
                        }

                        //Send status update after every nUpdate simulations.
                        NotifyAboutNumberOfSImulations(updateStatus, i);
                    }

                    generationArray[0].sortMemberArray();
                    generationArray[0].setFitScore();


                    //update status after first generation
                    var updateParamValuesArray = new List<double>();
                    Member best = generationArray[0].getMember(0);
                    for (int i = 0; i < numOfParameters; i++)
                    {
                        updateParamValuesArray.Add(best.pValuesArray[i]);
                    }

                    int minRealEigenIndex;
                    if (hasUpdateStatusMethod)
                    {
                        try
                        {
                            var update = new ArrayList { 1, 0, generationArray[0].genFitScore, updateParamValuesArray };
                            if (best.realEigenValueArray.Count > 0)
                            {
                                minRealEigenIndex = findMinRealEigenValueIndex(
                                    best.realEigenValueArray);
                                update.Add(best.realEigenValueArray[minRealEigenIndex]);
                                update.Add(best.complexEigenValueArray[minRealEigenIndex]);
                            }
                            else
                            {
                                update.Add(0.0);
                                update.Add(0.0);
                            }
                            update.Add(best.realEigenValueArray);
                            update.Add(best.complexEigenValueArray);
                            updateStatus(update);
                        }
                        catch (SBWException ae)
                        {
                            ae = new BifException("Error in sbwBifGAOptimize",
                                "Unable to call update status!!!");
                            throw ae;
                        }
                    }

                    generationArray[1].allocMemberArray(algoArgs.Args.NumOfMembers);
                    if (algoArgs.Args.NumOfGenerations == 1)
                    {
                        generationArray[1] = generationArray[0];
                    }

                    int Iterations = 1;
                    for (int j = 1; j < algoArgs.Args.NumOfGenerations; j++)
                    {
                        
                        for (int k = 0; k < (algoArgs.Args.NumOfMembers); k = k + 2)
                        {
                            var parent1 = selectMember(generationArray[0]);

                            var parent2 = selectMember(generationArray[0]);

                            var child1 = new Member();
                            var child2 = new Member();

                            crossOver(parent1, parent2, ref child1, ref child2);

                            mutate(ref child1);

                            evalObjFnFitness(ref child1);

                            mutate(ref child2);

                            evalObjFnFitness(ref child2);

                            Member bestMem1;
                            Member bestMem2;
                            selectBestTwo(parent1, parent2, child1, child2, out bestMem1, out bestMem2);


                            generationArray[1].setMember(k, bestMem1);
                            generationArray[1].setMember(k + 1, bestMem2);

                            if (!isProcessActive)
                            {
                                return new OptimizationResult
                                {
                                    Iterations = 0,
                                    Score = generationArray[0].genFitScore,
                                    Values = generationArray[0].memArray[0].pValuesArray,
                                    RealEigenValues = generationArray[0].memArray[0].realEigenValueArray,
                                    ImagEigenValues = generationArray[0].memArray[0].complexEigenValueArray
                                };
                            }

                            //Send status update after every predefined number of simulations.
                            NotifyAboutNumberOfSImulations(updateStatus, k);


                        }
                        generationArray[1].sortMemberArray();
                        generationArray[1].setFitScore();

                        if (generationArray[1].genFitScore > generationArray[0].genFitScore)
                        {
                            generationArray[1].setMember(0, best);
                            generationArray[1].genFitScore = generationArray[0].genFitScore;
                        }
                        //os << j << "\t" << generationArray[1].getGenFitScore() << "\t";

                        updateParamValuesArray.Clear();
                        best = generationArray[1].getMember(0);
                        for (int i = 0; i < numOfParameters; i++)
                        {
                            updateParamValuesArray.Add(best.pValuesArray[i]);
                            //os << generationArray[1].getMember(0).pValuesArray[i] << "\t";
                        }
                        //os << endl;

                        //Call update status once you finish calculating the fitness score.
                        if (hasUpdateStatusMethod)
                        {
                            try
                            {
                                var update = new ArrayList
                                {
                                    1,
                                    j,
                                    generationArray[1].genFitScore,
                                    updateParamValuesArray
                                };


                                if (best.realEigenValueArray.Count > 0)
                                {
                                    minRealEigenIndex = findMinRealEigenValueIndex(best.realEigenValueArray);
                                    update.Add(best.realEigenValueArray[minRealEigenIndex]);
                                    update.Add(best.complexEigenValueArray[minRealEigenIndex]);
                                }
                                else
                                {
                                    update.Add(0.0);
                                    update.Add(0.0);
                                }
                                update.Add(best.realEigenValueArray);
                                update.Add(best.complexEigenValueArray);
                                updateStatus(update);
                            }
                            catch (SBWException ae)
                            {
                                ae = new BifException("Error in sbwBifGAOptimize",
                                    "Unable to call update status!!!");
                                throw ae;
                            }
                        }

                        //Code to check if the Tolerance level has reached                
                        var bufferArray = new List<double>();
                        bufferArray.Clear();
                        double realPart;
                        double complexPart;
                        int eigenArraySize = best.eigenValueArrayDim;

                        if (algoArgs.EvalOption == OSCILLATOR)
                        {
                            for (int i = 0; i < eigenArraySize; i++)
                            {
                                realPart = best.realEigenValueArray[i];
                                complexPart = best.complexEigenValueArray[i];
                                if (Math.Abs(complexPart) == 0.0) // Check if the imaginary part is eqaul to zero. 
                                {
                                    // If it is so then assign 10^6 to bufferArray Element.
                                    bufferArray.Add(10E6);
                                }
                                else // Else store the absolute value of real eigen value.
                                {
                                    bufferArray.Add(Math.Abs(realPart));
                                }
                            }
                        }
                        if (algoArgs.EvalOption == TURNINGPOINT)
                        {
                            for (int i = 0; i < eigenArraySize; i++)
                            {
                                realPart = best.realEigenValueArray[i];
                                complexPart = best.complexEigenValueArray[i];
                                if (Math.Abs(complexPart) > 0.0) // Check if the imaginary part is greater than zero. 
                                {
                                    // If it is so then assign 10^6 to bufferArray Element.
                                    bufferArray.Add(10E6);
                                }
                                else // Else store the absolute value of real eigen value.
                                {
                                    bufferArray.Add(Math.Abs(realPart));
                                }
                            }
                        }

                        if (bufferArray.Count > 0)
                        {
                            minRealEigenIndex = findMinRealEigenValueIndex(bufferArray);

                            if (Math.Abs(bufferArray[minRealEigenIndex]) < algoArgs.Args.Tolerance)
                            {
                                break;
                            }
                        }

                        generationArray[0] = generationArray[1];

                        ++Iterations;

                        //Check the isProcessActive Flag here and gracefully exit the optimizer if it is false.
                        if (!isProcessActive)
                        {
                            break;
                        }
                    }

                    return new OptimizationResult
                    {
                        Iterations = Iterations,
                        Score = generationArray[0].genFitScore,
                        Values = generationArray[0].memArray[0].pValuesArray,
                        RealEigenValues = generationArray[0].memArray[0].realEigenValueArray,
                        ImagEigenValues = generationArray[0].memArray[0].complexEigenValueArray
                    };
                }

                #endregion
            }
            catch (SBWException)
            {
                throw;
            }
            catch
            {
                throw new BifException("Problem occured in main method", " ");
            }

            return null;
        }

        public void selectBestTwo(Member parent1, Member parent2, Member child1, Member child2, out Member bestMem1,
            out Member bestMem2)
        {
            var memberArray = new []{parent1, parent2, child1, child2};
            sortMember(memberArray);
            bestMem1 = memberArray[0];

            var bArray = new double[3];
            double fitnessSum = 0;
            for (int i = 0; i < 3; i++)
            {
                bArray[i] = memberArray[i + 1].memFitScore;
                fitnessSum = fitnessSum + bArray[i];
            }

            for (int i = 0; i < 3; i++)
            {
                bArray[i] = bArray[i] / fitnessSum;
            }

            var EArray = new double[3];
            double exp_bSum = 0;
            for (int i = 0; i < 3; i++)
            {
                EArray[i] = Math.Exp(-1 * bArray[i]);
                exp_bSum = exp_bSum + EArray[i];
            }

            for (int i = 0; i < 3; i++)
            {
                EArray[i] = EArray[i] / exp_bSum;
            }
            
            double r = rnd.NextDouble();

            if (r < EArray[0])
            {
                bestMem2 = memberArray[1];
            }
            else if (r > EArray[0] && r < EArray[0] + EArray[1])
            {
                bestMem2 = memberArray[2];
            }
            else
            {
                bestMem2 = memberArray[3];
            }
        }

        public Member selectMember(Generation genSample)
        {
            var selectMemberArray = new Member[numOfMemSelect];
            for (int i = 0; i < numOfMemSelect; i++)
            {
                double randomNumb = rnd.NextDouble();
                var index = (int)Math.Floor(randomNumb * genSample.numOfMembers);
                selectMemberArray[i] = genSample.getMember(index);
            }
            sortMember(selectMemberArray);
            return selectMemberArray[0];
        }

        private static void sortMember(Member[] selectMemberArray)
        {
            Array.Sort(selectMemberArray, (a, b) => a.memFitScore.CompareTo(b.memFitScore));
        }
    }
}