using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using SBW;
using bifurcation;

namespace BifurcationFrontend
{
    /// <summary>
    /// the class FrontendService represents the sbw service that implements the updateStatus service
    /// method for the frontend. the sbw bifurcation module will call the updataStatus method from 
    /// time to time to transmit preliminay results. 
    /// </summary>
    public class FrontendService
    {
        internal static FrontendService updateService;
        /// <summary>
        /// the windows form representing the frontend for the bifurcation module
        /// </summary>
        private static BifurcationForm m_oForm = null;

        /// <summary>
        /// main method for this application ... this will set up the 
        /// SBW module representing the frontend
        /// </summary>
        /// <param name="args">the command line arguments</param>
        [Ignore(), STAThread]
        public static void Main(string[] args)
        {
            try
            {
                BifurcationForm.m_sModuleName = "Bifurcation";
                m_oForm = new BifurcationForm();
                updateService = new FrontendService();
                object oFrontendObject = updateService;

                ModuleImplementation oFrontendModule = new ModuleImplementation(BifurcationForm.m_sModuleName, "Bifurcation Frontend",
                    LowLevel.ModuleManagementType.UniqueModule,
                    "this module implements a frontend for the bifurcation discovery module");
                oFrontendModule.addService("update", "Bifurcation Discovery Tool", "Analysis",
                    "implementation of the update function that will be called by the bifurcation module",
                    ref oFrontendObject);
                oFrontendModule.EnableServices(args);

                oFrontendModule.ModuleShutdown += new EventHandler(oFrontendModule_ModuleShutdown);
                Application.EnableVisualStyles();
                Application.DoEvents();
                Application.Run(m_oForm);
            }
            catch (SBWException)
            {
                MessageBox.Show("An exception occured while trying to register the module. The only possible explenation would be an outdated SBW DLL.", "Error druning module initialization");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error druning module initialization");
            }
        }


        /// <summary>
        /// the analysis method that will be called by JDesigner
        /// </summary>
        /// <param name="sSBML"></param>
        [Help("starts the bifurcation discovery tool")]
        public void doAnalysis(string sSBML)
        {
            m_oForm.m_sFilename = "JDesigner model";
            m_oForm.loadSBML(sSBML);
        }

        [Help("Returns the currently loaded SBML model")]
        public string getSBML()
        {
            if (m_oForm.m_sSBML != null && m_oForm.m_sSBML.Length > 0)
                return m_oForm.m_sSBML;
            else throw new SBWApplicationException("No model Loaded");
        }

        /// <summary>
        /// the updateStatus method will be called by the bifurcation module to 
        /// send preliminary data to the frontend. this data will then be displayed.
        /// </summary>
        /// <param name="oList">the arguments</param>
        [Help("the update method that will be called each iteration")]
        public void updateStatus(ArrayList oList)
        {
            if (oList == null)
            {
                Application.DoEvents();
                return;
            }
            try
            {
                int nStatus = (int)oList[0];
                if (nStatus == 0)
                {
                    int nNumberOfSimulations = (int)oList[1];
                    m_oForm.updateNumberOfSimulations(nNumberOfSimulations);
                }
                else
                {
                    int nIteration = (int)oList[1];
                    //int nNumberOfSimulations;
                    double dFitness = (double)oList[2];
                    double[] dParameter = ((List<double>)oList[3]).ToArray();
                    double dEigenValueReal = (double)oList[4];
                    double dEigenValueComplex = (double)oList[5];
                    double[] dRealEigen = ((List<double>)oList[6]).ToArray();
                    double[] dComplexEigen = ((List<double>)oList[7]).ToArray();
                    m_oForm.update(nIteration, dFitness, dEigenValueReal, dEigenValueComplex, dParameter, dRealEigen, dComplexEigen);
                }

                Application.DoEvents();
            }
            catch (Exception)
            {
                throw new SBWException("error in updateSatus",
                    "please make sure that the correct update information is given the expected form is: {int, double, double[], double,double, double[],double[]}");
            }
        }


        /// <summary>
        /// the module shutdown handler. This will be called if the broker shuts down this module
        /// </summary>
        /// <param name="sender">frontend</param>
        /// <param name="e">empty argument</param>
        private static void oFrontendModule_ModuleShutdown(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
