// Type: bifurcation.BifurcationForm
// Assembly: BifurcationFrontend, Version=2.0.2753.40611, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\fbergmann\Desktop\BifurcationFrontend.exe

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Threading;
using System.Windows.Forms;
using LibBifurcationDiscovery;
using LibRoadRunner;
using SBMLSupport;
using SBW;
using SBW.Utils;
using ZedGraph;
using bifurcation;

namespace BifurcationFrontend
{
    public partial class BifurcationForm : Form
    {
        internal static string m_sModuleName;
        private readonly SBWMenu sbwMenuHandler;

        private bool m_bConfigurationChanged;
        private bool m_bThreadStarted;
        private double[] m_dParameters;
        private int m_nLastCall;
        private AboutBifurcation m_oAbout;
        private ConfigDialog m_oDialog;
        private Thread m_oCallThread;

        private ArrayList m_oLastSelection;
        private DataSet m_oParameterDataSet;
        public string m_sFilename = "";
        public string m_sSBML = null;
        private ContextMenu oSBWMenu;
        private RoadRunner sim;
        private WorkerThread thread = new WorkerThread();

        public BifurcationForm()
        {
            InitializeComponent();
            oMode.Text = (string) oMode.Items[0];
            zedGraphControl1.GraphPane.Legend.IsVisible = false;
            TransparencyKey = Color.Empty;
            zedGraphControl1.BackColor = Color.Empty;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            m_oCmdStart.Enabled = false;
            zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
            enableTest(false);
            sbwMenuHandler = new SBWMenu(mnuSBW, "Bifurcation", () => getNewSBML());
            zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
        }

        private void BifurcationCaller()
        {
            try
            {
                var opt = new Optimizer();
                OptimizationResult result = opt.optimizeWithGeneticAlgo(m_sSBML,
                    getArguments(m_nLastCall),
                    (a) => FrontendService.updateService.updateStatus(a));

                setResults(result);
            }
            catch (SBWDisconnectException)
            {
            }
            catch (SBWException ex)
            {
                MessageBox.Show(
                    "An error occured in the Bifurcation Module. Most common solutions are:\n\r\n\r" +
                    " 1. Increase / Decrease simulation time.\n\r" +
                    " 2. Check that initial species concentrations are not negative.\n\r" +
                    " 3. Please check the FAQ's for common errors.\n\r\n\r" +
                    " If all of the above fails, try saving the model and starting over.\n\r\n\r" +
                    "(Error was:  " + ex.Message + ")", "Bifurcation Module Error");
            }
            finally
            {
                setEnable(true);
            }
        }

        private void BifurcationForm_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void BifurcationForm_DragDrop(object sender, DragEventArgs e)
        {
            var strArray = (string[]) e.Data.GetData(DataFormats.FileDrop);
            var fileInfo = new FileInfo(strArray[0]);
            if (!(fileInfo.Extension.ToLower() == ".xml") && !(fileInfo.Extension.ToLower() == ".sbml"))
                return;
            m_sFilename = fileInfo.Name;
            loadFile(strArray[0]);
        }

        private void BifurcationForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileInfo = new FileInfo(((string[]) e.Data.GetData(DataFormats.FileDrop))[0]);
                if (fileInfo.Extension.ToLower() == ".xml" || fileInfo.Extension.ToLower() == ".sbml")
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }


        private void BifurcationForm_Load(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.PaneFill = new Fill(Color.WhiteSmoke);
            zedGraphControl1.GraphPane.AxisFill = new Fill(Color.White);
            zedGraphControl1.GraphPane.Title = "Fitness";
            zedGraphControl1.GraphPane.XAxis.Title = "Iteration";
            zedGraphControl1.GraphPane.YAxis.Title = "Fitness";
            zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
            zedGraphControl1.GraphPane.YAxis.IsVisible = true;
            zedGraphControl1.GraphPane.XAxis.IsVisible = true;
            zedGraphControl1.GraphPane.XAxis.IsShowTitle = true;
            zedGraphControl1.GraphPane.XAxis.IsTic = true;
            zedGraphControl1.GraphPane.XAxis.Color = Color.Black;
            zedGraphControl1.GraphPane.YAxis.Color = Color.Black;
            zedGraphControl1.AxisChange();
            TransparencyKey = Color.Empty;
            sbwMenuHandler.UpdateSBWMenu();
            thread.Start();
        }

        private void BindData(bool bDoBind)
        {
            if (bDoBind)
            {
                dataGrid1.DataSource = viewParameters;
                dataGrid2.DataSource = viewEigenValues;
                dataGrid3.DataSource = viewSwitch;
                dataGrid4.DataSource = viewOscillator;
                ((DataGridBoolColumn) dataGrid1.TableStyles[0].GridColumnStyles[1]).AllowNull = false;
            }
            else
            {
                dataGrid1.DataSource = null;
                dataGrid2.DataSource = null;
                dataGrid3.DataSource = null;
                dataGrid4.DataSource = null;
            }
        }

        private string CheckResult(int nLastCall, double[] realEigenValues, double[] complexEigenValues)
        {
            lblDisplayResult.Visible = false;
            if (nLastCall == 2)
            {
                bool flag = false;
                foreach (double num in realEigenValues)
                {
                    if (Math.Abs(num) < 0.0001)
                        flag = true;
                }
                if (flag)
                {
                    lblDisplayResult.Visible = true;
                    return "Possible 'Turning Point'";
                }
            }
            else if (nLastCall == 1)
            {
                bool flag = false;
                for (int index = 0; index < realEigenValues.Length; ++index)
                {
                    if (complexEigenValues[index] == 0.0 && Math.Abs(realEigenValues[index]) < 0.0001)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    int num = 0;
                    for (int index = 0; index < realEigenValues.Length - 1; ++index)
                    {
                        if (realEigenValues[index] == realEigenValues[index + 1] &&
                            complexEigenValues[index] == -complexEigenValues[index + 1])
                            ++num;
                    }
                    if (num > 0)
                    {
                        lblDisplayResult.Visible = true;
                        return "Possible 'Hopf Bifurcation'";
                    }
                }
            }
            return "";
        }

       
        private void OnConfigureClick(object sender, EventArgs e)
        {
            if (m_oDialog == null)
                m_oDialog = new ConfigDialog();
            if (m_oDialog.ShowDialog() != DialogResult.OK)
                return;
            m_bConfigurationChanged = true;
        }

        private void OnExportClick(object sender, EventArgs e)
        {
            try
            {
                Service service = new Module("JDesigner").getService("model");
                var a = new ArrayList();
                foreach (DataRow dataRow in (InternalDataCollectionBase) m_oParameterDataSet.Tables[0].Rows)
                {
                    if ((bool) dataRow["enabled"])
                        a.Add(new ArrayList
                        {
                            (string) dataRow[0],
                            (double) dataRow[3]
                        });
                }
                var oArguments = new DataBlockWriter();
                oArguments.add(a);
                service.getMethod("void setParameterValues({})").call(oArguments);
            }
            catch (SBWException ex)
            {
                MessageBox.Show(ex.Message, "an error occured while exporting optimized values to JDesigner");
            }
        }

        private void OnKillClick(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(this,
                    "Are you sure you want to cancel the simulation? This will interrupt the current simulation and no return values will be available",
                    "Cancel Simulation?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            try
            {
                if (!m_bThreadStarted)
                    return;
                thread.Abort();
                thread = new WorkerThread();

                setEnable(true);
            }
            catch (Exception ex)
            {
            }
        }

        private void OnLoadClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            loadFile(openFileDialog1.FileName);
        }

        private void OnSaveClick(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                    return;
                string fileName = saveFileDialog1.FileName;

                string newSbml = getNewSBML();
                var streamWriter = new StreamWriter(fileName);
                streamWriter.Write(newSbml);
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (SBWException ex)
            {
                MessageBox.Show(ex.Message, "An error occured while saving the optimized Values");
            }
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            if (m_bThreadStarted)
            {
                var num1 = (int) MessageBox.Show("call already in progress ... ");
            }
            else if (m_sSBML == null)
            {
                var num2 =
                    (int)
                        MessageBox.Show("please load a model first.", "No Model Loaded", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
            }
            else
            {
                txtEigenValue.Text = "";
                lblDisplayResult.Visible = false;
                m_oParameterDataSet.Tables[1].Rows.Clear();
                m_nLastCall = !(oMode.Text == (string) oMode.Items[0]) ? 2 : 1;


                setEnable(false);
                zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
                zedGraphControl1.AxisChange();
                zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
                zedGraphControl1.AxisChange();
                zedGraphControl1.Refresh();
                thread.QueueItem(() => BifurcationCaller());
            }
        }

        private void OnStartTest(object sender, EventArgs e)
        {
            lblDisplayResult.Visible = false;
            m_nLastCall = 3;
            m_oCallThread = new Thread(BifurcationCaller);
            m_bThreadStarted = true;
            m_oCmdStart.Enabled = false;
            m_oCmdLoad.Enabled = false;
            enableTest(false);
            m_oCallThread.Start();
        }

        private void OnStopClicked(object sender, EventArgs e)
        {
            Optimizer.isProcessActive = false;
        }

        private void OnUnselectClick(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase) m_oParameterDataSet.Tables[0].Rows)
            {
                if ((bool) dataRow["enabled"])
                    dataRow["enabled"] = false;
            }
        }

        
        private void UpdateUi(ArrayList globalParameters, ArrayList boundarySpecies, ArrayList floatingSpeciesIds)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateUiDelegate(UpdateUi), new object[]
                {
                    globalParameters, boundarySpecies, floatingSpeciesIds
                });
            }
            else
            {
                BindData(false);
                try
                {
                    zedGraphControl1.GraphPane.CurveList.Clear();
                    zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
                    zedGraphControl1.GraphPane.Title = m_sFilename;
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    m_oParameterDataSet.Tables["Parameters"].Rows.Clear();

                    foreach (ArrayList arrayList in globalParameters)
                    {
                        var num = (double) arrayList[1];
                        var str = (string) arrayList[0];
                        DataRow row = m_oParameterDataSet.Tables[0].NewRow();
                        row["parameter name"] = str;
                        row["enabled"] = true;
                        row["initial value"] = num;
                        row["optimized value"] = num;
                        row["MIN"] = num*0.1;
                        row["MAX"] = num > 10.0 ? num*2.0 : 10.0;
                        m_oParameterDataSet.Tables[0].Rows.Add(row);
                    }
                    foreach (ArrayList arrayList in boundarySpecies)
                    {
                        var num = (double) arrayList[1];
                        var str = (string) arrayList[0];
                        DataRow row = m_oParameterDataSet.Tables["Parameters"].NewRow();
                        row["parameter name"] = str;
                        row["enabled"] = true;
                        row["initial value"] = num;
                        row["optimized value"] = num;
                        row["MIN"] = num*0.1;
                        row["MAX"] = num > 10.0 ? num*2.0 : 10.0;
                        m_oParameterDataSet.Tables["Parameters"].Rows.Add(row);
                    }
                    ArrayList list = floatingSpeciesIds;
                    comboSpecies.Items.Clear();
                    foreach (string str in list)
                        comboSpecies.Items.Add(str);
                    comboSpecies.SelectedIndex = 0;
                    BindData(true);
                    m_oCmdStart.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured trying to analyze the given SBML model. ",
                        "Error analyzing the model.");
                }
            }
        }

        private void cmdCopy_Click_1(object sender, EventArgs e)
        {
            menuItem2_Click_1(this, e);
        }

        private void cmdSteadyState_Click(object sender, EventArgs e)
        {
            thread.QueueItem(() =>
            {
                if (m_sSBML != null)
                {
                    if (m_sSBML.Length > 0)
                    {
                        try
                        {
                            if (sim == null)
                                sim = new RoadRunner();

                            sim.loadSBML(m_sSBML);
                            sim.simulate();
                            double steadyState = sim.steadyState();
                            if (
                                MessageBox.Show(
                                    "Found steady state with sums of squares: " + steadyState +
                                    ". Do you want to use this state to be used?", "Steady state calculated",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                                return;
                            sim.simulate();
                            ArrayList floatingSpeciesNames = sim.getFloatingSpeciesNames();
                            double[] speciesConcentrations1 = sim.getFloatingSpeciesConcentrations();
                            ArrayList boundarySpeciesNames = sim.getBoundarySpeciesNames();
                            double[] speciesConcentrations2 = sim.getBoundarySpeciesConcentrations();
                            ArrayList parameterTupleList = sim.getAllGlobalParameterTupleList();
                            NOM.loadSBML(m_sSBML);
                            for (int index = 0; index < floatingSpeciesNames.Count; ++index)
                                NOM.setValue((string) floatingSpeciesNames[index], speciesConcentrations1[index]);
                            for (int index = 0; index < boundarySpeciesNames.Count; ++index)
                                NOM.setValue((string) boundarySpeciesNames[index], speciesConcentrations2[index]);
                            foreach (ArrayList arrayList in parameterTupleList)
                                NOM.setValue((string) arrayList[0], (double) arrayList[1]);
                            m_sSBML = NOM.getSBML();
                            loadSBML(m_sSBML);
                            return;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not compute steady state due to: " + ex.Message,
                                "Steady state could not be computed ...", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                            return;
                        }
                    }
                }
                MessageBox.Show("There is no model to analyze. Load a model first.", "No Model loaded",
                    MessageBoxButtons.OK, MessageBoxIcon.Hand);
            });
        }

        private void cmdTestOscillator_Click(object sender, EventArgs e)
        {
            lblDisplayResult.Visible = false;
            m_nLastCall = 4;
            m_oCallThread = new Thread(BifurcationCaller);
            m_bThreadStarted = true;
            m_oCmdStart.Enabled = false;
            m_oCmdLoad.Enabled = false;
            enableTest(false);
            m_oCallThread.Start();
        }

        private void dataGrid1_Validated(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase) m_oParameterDataSet.Tables[0].Rows)
            {
                try
                {
                    var flag = (bool) dataRow["enabled"];
                    dataRow["enabled"] = flag ? true : false;
                }
                catch (Exception ex)
                {
                    dataRow["enabled"] = false;
                }
            }
        }

        private void dataGrid3_Validated(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase) m_oParameterDataSet.Tables[2].Rows)
            {
                try
                {
                    var flag = (bool) dataRow[1];
                    dataRow[1] = flag ? true : false;
                }
                catch (Exception ex)
                {
                    dataRow[1] = false;
                }
            }
        }

        private void dataGrid4_Validated(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase) m_oParameterDataSet.Tables[3].Rows)
            {
                try
                {
                    var flag = (bool) dataRow[1];
                    dataRow[1] = flag ? true : false;
                }
                catch (Exception ex)
                {
                    dataRow[1] = false;
                }
            }
        }

        private void enableTest(bool bEnable)
        {
            if (bEnable)
            {
                if (m_nLastCall == 2)
                {
                    m_oCmdStartTest.Enabled = true;
                    dataGrid3.Enabled = true;
                    comboSpecies.Enabled = true;
                }
                else
                {
                    if (m_nLastCall != 1)
                        return;
                    cmdTestOscillator.Enabled = true;
                    dataGrid4.Enabled = true;
                }
            }
            else
            {
                m_oCmdStartTest.Enabled = false;
                dataGrid3.Enabled = false;
                comboSpecies.Enabled = false;
                cmdTestOscillator.Enabled = false;
                dataGrid4.Enabled = false;
            }
        }

        private AlgorithmArguments getArguments(int nCall)
        {
            if (InvokeRequired)
            {
                return (AlgorithmArguments) Invoke(new getArgumentsDelegate(getArguments), new object[1]
                {
                    nCall
                });
            }
            else
            {
                var args = new AlgorithmArguments();
                args.EvalOption = nCall;
                //if (nCall == LibBifurcationDiscovery.Optimizer.HOPFBIFURCATION )
                //{
                //    var arrayList2 = new ArrayList();
                //    var arrayList3 = new ArrayList();
                //    for (int index = 0; index < tblOscillator.Rows.Count; ++index)
                //    {
                //        try
                //        {
                //            if ((bool) tblOscillator.Rows[index][1])
                //            {
                //                arrayList2.Add(tblOscillator.Rows[index][0]);
                //                arrayList3.Add(m_dParameters[index]);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //    args.Add(arrayList3.ToArray(typeof (double)));
                //    args.Add(arrayList2.ToArray(typeof (string)));
                //}
                //else if (nCall == Optimizer.SWITCH)
                //{
                //    var arrayList2 = new ArrayList();
                //    var arrayList3 = new ArrayList();
                //    for (int index = 0; index < m_oParameterDataSet.Tables["SwitchParameter"].Rows.Count; ++index)
                //    {
                //        try
                //        {
                //            if ((bool) m_oParameterDataSet.Tables["SwitchParameter"].Rows[index][1])
                //            {
                //                arrayList2.Add(m_oParameterDataSet.Tables["SwitchParameter"].Rows[index][0]);
                //                arrayList3.Add(m_dParameters[index]);
                //            }
                //        }
                //        catch (Exception ex)
                //        {
                //        }
                //    }
                //    args.Add(arrayList3.ToArray(typeof (double)));
                //    args.Add(arrayList2.ToArray(typeof (string)));
                //    args.Add(comboSpecies.SelectedIndex);
                //}
                //else
                {
                    args.NameValueBounds = getParameterList();
                    args.Args = new Arguments(
                        m_bConfigurationChanged ? m_oDialog.NumberOfGenerations : 100,
                        m_bConfigurationChanged ? m_oDialog.NumberOfMembers : 100,
                        m_bConfigurationChanged ? m_oDialog.NumberOfTournament : 5,
                        m_bConfigurationChanged ? m_oDialog.CrossoverProbability : 0.75,
                        m_bConfigurationChanged ? m_oDialog.MutationProbability : 0.3,
                        m_bConfigurationChanged ? m_oDialog.StoppingTolerance : 0.0001,
                        m_bConfigurationChanged ? m_oDialog.RandomNumberSeed : 0,
                        m_bConfigurationChanged ? m_oDialog.SimulationTime : 100.0,
                        m_bConfigurationChanged ? m_oDialog.UpdateDelay : 10
                        );
                }
                return args;
            }
        }

        private string getNewSBML()
        {
            var newValues = new List<Tuple<string, double>>();
            foreach (DataRow dataRow in (InternalDataCollectionBase) m_oParameterDataSet.Tables[0].Rows)
            {
                if ((bool) dataRow["enabled"])
                    newValues.Add(new Tuple<string, double>((string) dataRow[0], (double) dataRow[3]));
            }

            string result = "";
            var autoEvent = new AutoResetEvent(false);
            thread.QueueItem(() =>
            {
                NOM.loadSBML(m_sSBML);
                foreach (var newValue in newValues)
                {
                    NOM.setValue(newValue.Item1, newValue.Item2);
                }
                result = NOM.getSBML();
                autoEvent.Set();
            });
            autoEvent.WaitOne();
            return result;
        }

        private List<Tuple<string, double, double, double>> getParameterList()
        {
            m_oLastSelection = new ArrayList();
            var result = new List<Tuple<string, double, double, double>>();
            for (int index = 0; index < m_oParameterDataSet.Tables["Parameters"].Rows.Count; ++index)
            {
                DataRow dataRow = m_oParameterDataSet.Tables["Parameters"].Rows[index];
                if ((bool) dataRow["enabled"])
                {
                    result.Add(new Tuple<string, double, double, double>(
                        (string) dataRow["parameter name"],
                        (double) dataRow["initial value"],
                        (double) dataRow["MIN"],
                        (double) dataRow["MAX"]
                        ));
                    m_oLastSelection.Add(index);
                }
            }
            return result;
        }

        private void SetTitle(string filename)
        {
            this.Text = "Bifurcation Discovery Tool - ["+Path.GetFileNameWithoutExtension(filename) +"]";
        }
        private void loadFile(string sFileName)
        {
            using (StreamReader streamReader = File.OpenText(sFileName))
            {
                m_sSBML = streamReader.ReadToEnd();
                m_sFilename = new FileInfo(sFileName).Name;
                streamReader.Close();
                loadSBML(m_sSBML);
                SetTitle(sFileName);
            }
        }

        public void loadSBML(string sSBML)
        {
            if (InvokeRequired)
            {
                Invoke(new loadSBMLDelegate(loadSBML), new object[1]
                {
                    sSBML
                });
            }
            else
            {                
                thread.QueueItem(() =>
                {
                    m_sSBML = NOM.getParamPromotedSBML(sSBML);
                    NOM.loadSBML(m_sSBML);
                    ArrayList globalParameters = NOM.getListOfParameters();
                    ArrayList boundarySpecies = NOM.getListOfBoundarySpecies();
                    ArrayList floatingSpeciesIds = NOM.getListOfFloatingSpeciesIds();

                    UpdateUi(globalParameters, boundarySpecies, floatingSpeciesIds);
                });
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.AxisChange();
            zedGraphControl1.Refresh();
        }

        private void menuItem2_Click_1(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase) m_oParameterDataSet.Tables["Parameters"].Rows)
            {
                if ((bool) dataRow["enabled"])
                    dataRow[2] = dataRow[3];
            }
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            if (m_oAbout == null)
                m_oAbout = new AboutBifurcation(HighLevel.getSBWVersion());
            var num = (int) m_oAbout.ShowDialog();
        }


        private void oMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oMode.SelectedIndex != 0)
                return;
            enableTest(false);
        }

        private void setEnable(bool bEnable)
        {
            if (InvokeRequired)
                Invoke(new enableDelegate(setEnable), new object[1]
                {
                    bEnable ? true : false
                });
            else if (bEnable)
            {
                m_oCmdStart.Enabled = true;
                m_oCmdLoad.Enabled = true;
                m_bThreadStarted = false;
                cmdCancel.Enabled = false;
                cmdStop.Enabled = false;
                lblDisplayResult.Visible = true;
                if (!(lblDisplayResult.Text == "Simulating ..."))
                    return;
                lblDisplayResult.Text = "";
            }
            else
            {
                m_oCmdStart.Enabled = false;
                m_oCmdLoad.Enabled = false;
                cmdCancel.Enabled = true;
                cmdStop.Enabled = true;
                m_bThreadStarted = true;
                lblDisplayResult.Visible = true;
                lblDisplayResult.Text = "Simulating ...";
            }
        }

        private void setResults(OptimizationResult oResults)
        {
            if (InvokeRequired)
            {
                Invoke(new setResultsDelegate(setResults), new object[1]
                {
                    oResults
                });
            }
            else
            {
                BindData(false);
                //if (m_nLastCall == 4)
                //{
                //    foreach (DataRow dataRow in (InternalDataCollectionBase) tblOscillator.Rows)
                //        dataRow[2] = "undecided";
                //    foreach (ArrayList arrayList in oResults)
                //    {
                //        var str = (string) arrayList[0];
                //        var flag = (bool) arrayList[1];
                //        foreach (DataRow dataRow in (InternalDataCollectionBase) tblOscillator.Rows)
                //        {
                //            if ((string) dataRow[0] == str)
                //            {
                //                dataRow.BeginEdit();
                //                dataRow[2] = flag.ToString();
                //                dataRow.EndEdit();
                //            }
                //        }
                //        enableTest(true);
                //    }
                //}
                //else if (m_nLastCall == 3)
                //{
                //    foreach (
                //        DataRow dataRow in
                //            (InternalDataCollectionBase) m_oParameterDataSet.Tables["SwitchParameter"].Rows)
                //        dataRow[2] = "undecided";
                //    foreach (ArrayList arrayList in oResults)
                //    {
                //        var str = (string) arrayList[0];
                //        var flag = (bool) arrayList[1];
                //        foreach (
                //            DataRow dataRow in
                //                (InternalDataCollectionBase) m_oParameterDataSet.Tables["SwitchParameter"].Rows)
                //        {
                //            if ((string) dataRow[0] == str)
                //            {
                //                dataRow.BeginEdit();
                //                dataRow[2] = flag.ToString();
                //                dataRow.EndEdit();
                //            }
                //        }
                //        enableTest(true);
                //    }
                //}
                //else
                {
                    int num1 = oResults.Iterations;
                    double num2 = oResults.Score;
                    double[] numArray = oResults.Values.ToArray();
                    double[] realEigenValues = oResults.RealEigenValues.ToArray();
                    double[] complexEigenValues = oResults.ImagEigenValues.ToArray();
                    int num3 = 0;
                    for (int index = 0; index < m_oLastSelection.Count; ++index)
                        m_oParameterDataSet.Tables["Parameters"].Rows[(int) m_oLastSelection[index]]["optimized value"]
                            = numArray[num3++];
                    m_oParameterDataSet.Tables["EigenValues"].Rows.Clear();
                    for (int index = 0; index < realEigenValues.Length; ++index)
                    {
                        DataRow row = m_oParameterDataSet.Tables["EigenValues"].NewRow();
                        row[0] = realEigenValues[index];
                        row[1] = complexEigenValues[index];
                        m_oParameterDataSet.Tables["EigenValues"].Rows.Add(row);
                    }
                    txtEigenValue.Text = "done";
                    txtFitness.Text = num2.ToString("E5");
                    if (m_nLastCall == 1)
                    {
                        m_dParameters = numArray;
                        tblOscillator.Rows.Clear();
                        foreach (
                            DataRow dataRow in
                                (InternalDataCollectionBase) m_oParameterDataSet.Tables["Parameters"].Rows)
                        {
                            DataRow row = tblOscillator.NewRow();
                            row[0] = dataRow[0];
                            row[1] = true;
                            row[2] = "undecided";
                            tblOscillator.Rows.Add(row);
                        }
                        enableTest(true);
                    }
                    else if (m_nLastCall == 2)
                    {
                        m_dParameters = numArray;
                        m_oParameterDataSet.Tables["SwitchParameter"].Rows.Clear();
                        foreach (
                            DataRow dataRow in
                                (InternalDataCollectionBase) m_oParameterDataSet.Tables["Parameters"].Rows)
                        {
                            DataRow row = m_oParameterDataSet.Tables["SwitchParameter"].NewRow();
                            row[0] = dataRow[0];
                            row[1] = true;
                            row[2] = "undecided";
                            m_oParameterDataSet.Tables["SwitchParameter"].Rows.Add(row);
                        }
                        enableTest(true);
                    }
                    lblDisplayResult.Text = CheckResult(m_nLastCall, realEigenValues, complexEigenValues);
                }
                BindData(true);
            }
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            zedGraphControl1.Refresh();
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            try
            {
                if ((string) e.Button.Tag == "clear")
                    zedGraphControl1.GraphPane.CurveList.Clear();
                else if ((string) e.Button.Tag == "+")
                {
                    zedGraphControl1.GraphPane.YAxis.MaxAuto = false;
                    YAxis yaxis = zedGraphControl1.GraphPane.YAxis;
                    double num = yaxis.Max*2.0;
                    yaxis.Max = num;
                }
                else if ((string) e.Button.Tag == "-")
                {
                    zedGraphControl1.GraphPane.YAxis.MaxAuto = false;
                    if (zedGraphControl1.GraphPane.YAxis.Max != 0.0 && zedGraphControl1.GraphPane.YAxis.Max > 1E-15)
                    {
                        YAxis yaxis = zedGraphControl1.GraphPane.YAxis;
                        double num = yaxis.Max/2.0;
                        yaxis.Max = num;
                    }
                }
                else if ((string) e.Button.Tag == "SBW")
                    oSBWMenu.Show(e.Button.Parent, e.Button.Rectangle.Location);
                else if ((string) e.Button.Tag == "on/off")
                {
                    if (!e.Button.Pushed)
                    {
                        zedGraphControl1.GraphPane.PaneFill = new Fill(Color.WhiteSmoke);
                        zedGraphControl1.GraphPane.AxisFill = new Fill(Color.White);
                    }
                    else
                    {
                        zedGraphControl1.GraphPane.PaneFill = new Fill(Color.WhiteSmoke, Color.Lavender, 0.0f);
                        zedGraphControl1.GraphPane.AxisFill = new Fill(Color.White,
                            Color.FromArgb(byte.MaxValue, byte.MaxValue, 166), 90f);
                    }
                }
                else if ((string) e.Button.Tag == "[]")
                    zedGraphControl1.GraphPane.YAxis.MaxAuto = true;
                else if ((string) e.Button.Tag == "About")
                {
                    if (m_oAbout == null)
                        m_oAbout = new AboutBifurcation(HighLevel.getSBWVersion());
                    var num = (int) m_oAbout.ShowDialog();
                }
                zedGraphControl1.AxisChange();
                zedGraphControl1.Refresh();
            }
            catch (Exception ex)
            {
                var num =
                    (int)
                        MessageBox.Show(
                            "Error displaying the graph in this resolution. Please increase the scale, or let the scale be determined automatically.",
                            "Error displaying graph");
            }
        }

        public void update(int nIteration, double dFitness, double dLowestEigenvalueReal,
            double dLowestEigenvalueComplex, double[] dParams, double[] dRealEigen, double[] dComplexEigen)
        {
            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new updateDelegate(update), (object) nIteration, (object) dFitness,
                        (object) dLowestEigenvalueReal, (object) dLowestEigenvalueComplex, (object) dParams,
                        (object) dRealEigen, (object) dComplexEigen);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error invoking update: " + ex);
                }
            }
            else
            {
                BindData(false);
                txtEigenValue.Text = dLowestEigenvalueReal.ToString("E10");
                txtFitness.Text = dFitness.ToString("E5");
                txtIteration.Text = nIteration.ToString("D7");
                m_oParameterDataSet.Tables[1].Rows.Clear();
                for (int index = 0; index < dRealEigen.Length; ++index)
                {
                    DataRow row = m_oParameterDataSet.Tables[1].NewRow();
                    row[0] = dRealEigen[index];
                    row[1] = dComplexEigen[index];
                    m_oParameterDataSet.Tables[1].Rows.Add(row);
                }
                int num = 0;
                for (int index = 0; index < m_oLastSelection.Count; ++index)
                    m_oParameterDataSet.Tables["Parameters"].Rows[(int) m_oLastSelection[index]]["optimized value"] =
                        dParams[num++];
                BindData(true);
                if (zedGraphControl1.GraphPane.CurveList.Count > 0)
                {
                    ((PointPairList)zedGraphControl1.GraphPane.CurveList[0].Points).Add(nIteration, dFitness);
                }
                else
                {
                    LineItem lineItem = zedGraphControl1.GraphPane.AddCurve("fitness", new double[1]
                    {
                        nIteration
                    }, new double[1]
                    {
                        dFitness
                    }, Color.Blue, SymbolType.Circle);
                    lineItem.Symbol.Fill.IsVisible = true;
                    lineItem.Line.IsVisible = false;
                }
                try
                {
                    if (dFitness >= zedGraphControl1.GraphPane.YAxis.Max)
                        return;
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public void updateNumberOfSimulations(int nNumber)
        {
            if (InvokeRequired)
            {
                try
                {
                    BeginInvoke(new updateNumberDelegate(updateNumberOfSimulations), new object[1]
                    {
                        nNumber
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error invoking update: " + ex);
                }
            }
            else
                txtSimulations.Text = nNumber.ToString("D7");
        }

        #region Nested type: UpdateUiDelegate

        private delegate void UpdateUiDelegate(
            ArrayList globalParameters, ArrayList boundarySpecies, ArrayList floatingSpeciesIds);

        #endregion

        #region Nested type: enableDelegate

        private delegate void enableDelegate(bool bEnable);

        #endregion

        #region Nested type: getArgumentsDelegate

        private delegate AlgorithmArguments getArgumentsDelegate(int nCall);

        #endregion

        #region Nested type: loadSBMLDelegate

        private delegate void loadSBMLDelegate(string sSBML);

        #endregion

        #region Nested type: setResultsDelegate

        private delegate void setResultsDelegate(OptimizationResult oResults);

        #endregion

        #region Nested type: updateDelegate

        private delegate void updateDelegate(
            int nIteration, double dx, double dy, double dComplexPart, double[] dParams, double[] dRealEigen,
            double[] dComplexEigen);

        #endregion

        #region Nested type: updateNumberDelegate

        private delegate void updateNumberDelegate(int nNumber);

        #endregion

    }
}