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
using BifurcationFrontend;
using LibBifurcationDiscovery;
using LibRoadRunner;
using SBMLSupport;
using SBW;
using SBW.Utils;
using ZedGraph;

namespace bifurcation
{
    public class BifurcationForm : Form
    {
        BackgroundWorker worker;
        private const int _MCW_EW = 524319;
        private const int _EM_INVALID = 16;
        internal static string m_sModuleName;
        private readonly ZedGraphControl m_oControl = new ZedGraphControl();
        private readonly DBGraphics m_oMemGraphics;
        private readonly ResourceManager resourceManager = new ResourceManager(typeof(BifurcationForm));
        private readonly SBWMenu sbwMenuHandler;
        private RoadRunner sim;
        private Button cmdCancel;
        private Button cmdConfigure;
        private Button cmdCopy;
        private Button cmdExport;
        private Button cmdSave;
        private Button cmdSteadyState;
        private Button cmdStop;
        private Button cmdTestOscillator;
        private Button cmdUnselect;
        private ComboBox comboSpecies;
        private IContainer components;
        private DataColumn dataColumn1;
        private DataColumn dataColumn10;
        private DataColumn dataColumn11;
        private DataColumn dataColumn12;
        private DataColumn dataColumn13;
        private DataColumn dataColumn14;
        private DataColumn dataColumn2;
        private DataColumn dataColumn3;
        private DataColumn dataColumn4;
        private DataColumn dataColumn5;
        private DataColumn dataColumn6;
        private DataColumn dataColumn7;
        private DataColumn dataColumn8;
        private DataColumn dataColumn9;
        private DataGrid dataGrid1;
        private DataGrid dataGrid2;
        private DataGrid dataGrid3;
        private DataGrid dataGrid4;
        private DataGridBoolColumn dataGridBoolColumn1;
        private DataGridTableStyle dataGridTableStyle1;
        private DataGridTextBoxColumn dataGridTextBoxColumn1;
        private DataGridTextBoxColumn dataGridTextBoxColumn2;
        private DataGridTextBoxColumn dataGridTextBoxColumn3;
        private DataGridTextBoxColumn dataGridTextBoxColumn4;
        private DataGridTextBoxColumn dataGridTextBoxColumn5;
        private ContextMenu gridMenu;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblDisplayResult;
        private bool m_bConfigurationChanged;
        private bool m_bThreadStarted;
        private double[] m_dParameters;
        private int m_nLastCall;
        private AboutBifurcation m_oAbout = null;
        private Thread m_oCallThread = null;
        private Button m_oCmdLoad;
        private Button m_oCmdStart;
        private Button m_oCmdStartTest;
        private ConfigDialog m_oDialog = null;
        private ArrayList m_oLastSelection;
        private DataSet m_oParameterDataSet;
        private Panel m_oPlotPane;
        public string m_sFilename = "";
        public string m_sSBML = null;
        private Panel mainContent;
        private MainMenu mainMenu1;
        private MenuItem menuItem1;
        private MenuItem menuItem10;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private MenuItem mnuExit;
        private MenuItem mnuExport;
        private MenuItem mnuOpen;
        private MenuItem mnuSBW;
        private MenuItem mnuSaveAs;
        private Hashtable oAnalyzers;
        private ComboBox oMode;
        private ContextMenu oSBWMenu;
        private OpenFileDialog openFileDialog1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private ContextMenu plotMenu;
        private Panel resultPanel;
        private SaveFileDialog saveFileDialog1;
        private Splitter splitter1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private DataTable tblEigenValues;
        private DataTable tblOscillator;
        private DataTable tblParameters;
        private DataTable tblSwitch;
        private ToolBar toolBar1;
        private ToolBarButton toolBarAuto;
        private ToolBarButton toolBarButton1;
        private ToolBarButton toolBarClear;
        private ToolBarButton toolBarGrad;
        private ToolBarButton toolBarZoomIn;
        private ToolBarButton toolBarZoomOut;
        private ToolTip toolTip1;
        private TextBox txtEigenValue;
        private TextBox txtFitness;
        private TextBox txtIteration;
        private TextBox txtSimulations;
        private DataView viewEigenValues;
        private DataView viewOscillator;
        private DataView viewParameters;
        private DataView viewSwitch;

        public BifurcationForm()
        {
            InitializeComponent();
            imageList1.ImageStream = (ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
            m_oMemGraphics = new DBGraphics();
            oMode.Text = (string)oMode.Items[0];
            m_oControl.GraphPane.Legend.IsVisible = false;
            TransparencyKey = Color.Empty;
            m_oPlotPane.BackColor = Color.Empty;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            m_oCmdStart.Enabled = false;
            m_oControl.GraphPane.YAxis.MaxAuto = true;
            enableTest(false);
            sbwMenuHandler = new SBWMenu(mnuSBW, "Bifurcation", () => getNewSBML());
            m_oControl.GraphPane.YAxis.MaxAuto = true;
        }

        WorkerThread thread = new WorkerThread();

        private void BifurcationCaller()
        {
            try
            {
                var opt = new Optimizer();
                var result = opt.optimizeWithGeneticAlgo(m_sSBML,
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
            var strArray = (string[])e.Data.GetData(DataFormats.FileDrop);
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
                var fileInfo = new FileInfo(((string[])e.Data.GetData(DataFormats.FileDrop))[0]);
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
            DBGraphics dbGraphics = m_oMemGraphics;
            Graphics graphics = m_oPlotPane.CreateGraphics();
            Rectangle clientRectangle = m_oPlotPane.ClientRectangle;
            int width = clientRectangle.Width;
            clientRectangle = m_oPlotPane.ClientRectangle;
            int height = clientRectangle.Height;
            dbGraphics.CreateDoubleBuffer(graphics, width, height);
            m_oControl.GraphPane.PaneFill = new Fill(Color.WhiteSmoke);
            m_oControl.GraphPane.AxisFill = new Fill(Color.White);
            m_oControl.GraphPane.Title = "Fitness";
            m_oControl.GraphPane.XAxis.Title = "Iteration";
            m_oControl.GraphPane.YAxis.Title = "Fitness";
            m_oControl.GraphPane.YAxis.MaxAuto = true;
            m_oControl.GraphPane.YAxis.IsVisible = true;
            m_oControl.GraphPane.XAxis.IsVisible = true;
            m_oControl.GraphPane.XAxis.IsShowTitle = true;
            m_oControl.GraphPane.XAxis.IsTic = true;
            m_oControl.GraphPane.XAxis.Color = Color.Black;
            m_oControl.GraphPane.YAxis.Color = Color.Black;
            SetSize();
            m_oControl.AxisChange();
            TransparencyKey = Color.Empty;
            sbwMenuHandler.UpdateSBWMenu();
            thread.Start();
        }

        private void BifurcationForm_Resize(object sender, EventArgs e)
        {
            DBGraphics dbGraphics = m_oMemGraphics;
            Graphics graphics = m_oPlotPane.CreateGraphics();
            Rectangle clientRectangle = m_oPlotPane.ClientRectangle;
            int width = clientRectangle.Width;
            clientRectangle = m_oPlotPane.ClientRectangle;
            int height = clientRectangle.Height;
            dbGraphics.CreateDoubleBuffer(graphics, width, height);
            SetSize();
            m_oPlotPane.Invalidate();
        }

        private void InitializeComponent()
        {
            components = new Container();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            mainContent = new Panel();
            panel4 = new Panel();
            lblDisplayResult = new Label();
            panel7 = new Panel();
            panel8 = new Panel();
            dataGrid1 = new DataGrid();
            gridMenu = new ContextMenu();
            menuItem2 = new MenuItem();
            viewParameters = new DataView();
            tblParameters = new DataTable();
            dataColumn1 = new DataColumn();
            dataColumn2 = new DataColumn();
            dataColumn3 = new DataColumn();
            dataColumn4 = new DataColumn();
            dataColumn5 = new DataColumn();
            dataColumn6 = new DataColumn();
            dataGridTableStyle1 = new DataGridTableStyle();
            dataGridTextBoxColumn1 = new DataGridTextBoxColumn();
            dataGridBoolColumn1 = new DataGridBoolColumn();
            dataGridTextBoxColumn2 = new DataGridTextBoxColumn();
            dataGridTextBoxColumn3 = new DataGridTextBoxColumn();
            dataGridTextBoxColumn4 = new DataGridTextBoxColumn();
            dataGridTextBoxColumn5 = new DataGridTextBoxColumn();
            dataGrid2 = new DataGrid();
            viewEigenValues = new DataView();
            tblEigenValues = new DataTable();
            dataColumn7 = new DataColumn();
            dataColumn8 = new DataColumn();
            panel3 = new Panel();
            cmdUnselect = new Button();
            cmdSave = new Button();
            cmdExport = new Button();
            cmdCopy = new Button();
            splitter1 = new Splitter();
            panel1 = new Panel();
            panel5 = new Panel();
            m_oPlotPane = new Panel();
            plotMenu = new ContextMenu();
            menuItem1 = new MenuItem();
            panel6 = new Panel();
            toolBar1 = new ToolBar();
            toolBarZoomIn = new ToolBarButton();
            toolBarAuto = new ToolBarButton();
            toolBarZoomOut = new ToolBarButton();
            toolBarButton1 = new ToolBarButton();
            toolBarClear = new ToolBarButton();
            toolBarGrad = new ToolBarButton();
            imageList1 = new ImageList(components);
            resultPanel = new Panel();
            label5 = new Label();
            txtSimulations = new TextBox();
            txtIteration = new TextBox();
            label4 = new Label();
            txtFitness = new TextBox();
            txtEigenValue = new TextBox();
            label1 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            groupBox1 = new GroupBox();
            cmdSteadyState = new Button();
            cmdCancel = new Button();
            m_oCmdLoad = new Button();
            cmdStop = new Button();
            m_oCmdStart = new Button();
            cmdConfigure = new Button();
            oMode = new ComboBox();
            oSBWMenu = new ContextMenu();
            tabPage3 = new TabPage();
            groupBox2 = new GroupBox();
            label3 = new Label();
            comboSpecies = new ComboBox();
            m_oCmdStartTest = new Button();
            dataGrid3 = new DataGrid();
            viewSwitch = new DataView();
            tblSwitch = new DataTable();
            dataColumn9 = new DataColumn();
            dataColumn10 = new DataColumn();
            dataColumn11 = new DataColumn();
            tabPage2 = new TabPage();
            groupBox3 = new GroupBox();
            cmdTestOscillator = new Button();
            dataGrid4 = new DataGrid();
            viewOscillator = new DataView();
            tblOscillator = new DataTable();
            dataColumn12 = new DataColumn();
            dataColumn13 = new DataColumn();
            dataColumn14 = new DataColumn();
            m_oParameterDataSet = new DataSet();
            openFileDialog1 = new OpenFileDialog();
            toolTip1 = new ToolTip(components);
            saveFileDialog1 = new SaveFileDialog();
            mainMenu1 = new MainMenu();
            menuItem3 = new MenuItem();
            mnuOpen = new MenuItem();
            mnuSaveAs = new MenuItem();
            mnuExport = new MenuItem();
            menuItem10 = new MenuItem();
            mnuExit = new MenuItem();
            mnuSBW = new MenuItem();
            menuItem5 = new MenuItem();
            menuItem6 = new MenuItem();
            tabControl1.SuspendLayout();
            mainContent.SuspendLayout();
            panel4.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            dataGrid1.BeginInit();
            viewParameters.BeginInit();
            tblParameters.BeginInit();
            dataGrid2.BeginInit();
            viewEigenValues.BeginInit();
            tblEigenValues.BeginInit();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            resultPanel.SuspendLayout();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox2.SuspendLayout();
            dataGrid3.BeginInit();
            viewSwitch.BeginInit();
            tblSwitch.BeginInit();
            tabPage2.SuspendLayout();
            groupBox3.SuspendLayout();
            dataGrid4.BeginInit();
            viewOscillator.BeginInit();
            tblOscillator.BeginInit();
            m_oParameterDataSet.BeginInit();
            SuspendLayout();
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(862, 466);
            tabControl1.TabIndex = 0;
            ((Control)tabPage1).Location = new Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(854, 440);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Configuration";
            mainContent.Controls.Add(panel4);
            mainContent.Controls.Add(panel3);
            mainContent.Controls.Add(splitter1);
            mainContent.Controls.Add(panel1);
            mainContent.Dock = DockStyle.Fill;
            mainContent.DockPadding.All = 10;
            mainContent.Location = new Point(0, 0);
            mainContent.Name = "mainContent";
            mainContent.Size = new Size(862, 466);
            mainContent.TabIndex = 5;
            panel4.Controls.Add(lblDisplayResult);
            panel4.Controls.Add(panel7);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(370, 42);
            panel4.Name = "panel4";
            panel4.Size = new Size(482, 414);
            panel4.TabIndex = 5;
            lblDisplayResult.BackColor = SystemColors.AppWorkspace;
            lblDisplayResult.BorderStyle = BorderStyle.FixedSingle;
            lblDisplayResult.Dock = DockStyle.Bottom;
            lblDisplayResult.Font = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDisplayResult.ForeColor = Color.White;
            lblDisplayResult.Location = new Point(0, 382);
            lblDisplayResult.Name = "lblDisplayResult";
            lblDisplayResult.Size = new Size(482, 32);
            lblDisplayResult.TabIndex = 4;
            lblDisplayResult.Visible = false;
            panel7.Controls.Add(panel8);
            panel7.Controls.Add(dataGrid2);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 0);
            panel7.Name = "panel7";
            panel7.Size = new Size(482, 414);
            panel7.TabIndex = 5;
            panel8.Controls.Add(dataGrid1);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(0, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(482, 238);
            panel8.TabIndex = 5;
            dataGrid1.AlternatingBackColor = Color.LightGray;
            dataGrid1.BackColor = Color.DarkGray;
            dataGrid1.CaptionBackColor = Color.White;
            dataGrid1.CaptionFont = new Font("Verdana", 10f);
            dataGrid1.CaptionForeColor = Color.Navy;
            dataGrid1.CaptionText = "  Select and modify parameters;";
            dataGrid1.CaptionVisible = false;
            dataGrid1.ContextMenu = gridMenu;
            dataGrid1.DataMember = "";
            dataGrid1.DataSource = viewParameters;
            dataGrid1.Dock = DockStyle.Fill;
            dataGrid1.ForeColor = Color.Black;
            dataGrid1.GridLineColor = Color.Black;
            dataGrid1.GridLineStyle = DataGridLineStyle.None;
            dataGrid1.HeaderBackColor = Color.Silver;
            dataGrid1.HeaderForeColor = Color.Black;
            dataGrid1.LinkColor = Color.Navy;
            dataGrid1.Location = new Point(0, 0);
            dataGrid1.Name = "dataGrid1";
            dataGrid1.ParentRowsBackColor = Color.White;
            dataGrid1.ParentRowsForeColor = Color.Black;
            dataGrid1.SelectionBackColor = Color.Navy;
            dataGrid1.SelectionForeColor = Color.White;
            dataGrid1.Size = new Size(482, 238);
            dataGrid1.TabIndex = 3;
            dataGrid1.TableStyles.AddRange(new DataGridTableStyle[1]
            {
                dataGridTableStyle1
            });
            dataGrid1.Validated += dataGrid1_Validated;
            gridMenu.MenuItems.AddRange(new MenuItem[1]
            {
                menuItem2
            });
            menuItem2.Index = 0;
            menuItem2.Text = "Copy Values";
            menuItem2.Click += menuItem2_Click_1;
            viewParameters.AllowDelete = false;
            viewParameters.AllowNew = false;
            viewParameters.Table = tblParameters;
            tblParameters.Columns.AddRange(new DataColumn[6]
            {
                dataColumn1,
                dataColumn2,
                dataColumn3,
                dataColumn4,
                dataColumn5,
                dataColumn6
            });
            tblParameters.Locale = new CultureInfo("en-US");
            tblParameters.TableName = "Parameters";
            dataColumn1.ColumnName = "parameter name";
            dataColumn1.ReadOnly = true;
            dataColumn2.ColumnName = "enabled";
            dataColumn2.DataType = typeof(bool);
            dataColumn2.DefaultValue = true;
            dataColumn3.ColumnName = "initial value";
            dataColumn3.DataType = typeof(double);
            dataColumn3.DefaultValue = 0;
            dataColumn4.ColumnName = "optimized value";
            dataColumn4.DataType = typeof(double);
            dataColumn4.DefaultValue = 0;
            dataColumn5.ColumnName = "MIN";
            dataColumn5.DataType = typeof(double);
            dataColumn5.DefaultValue = 0;
            dataColumn6.ColumnName = "MAX";
            dataColumn6.DataType = typeof(double);
            dataColumn6.DefaultValue = 10;
            dataGridTableStyle1.DataGrid = dataGrid1;
            dataGridTableStyle1.GridColumnStyles.AddRange(new DataGridColumnStyle[6]
            {
                dataGridTextBoxColumn1,
                dataGridBoolColumn1,
                dataGridTextBoxColumn2,
                dataGridTextBoxColumn3,
                dataGridTextBoxColumn4,
                dataGridTextBoxColumn5
            });
            dataGridTableStyle1.HeaderForeColor = SystemColors.ControlText;
            dataGridTableStyle1.MappingName = "";
            dataGridTextBoxColumn1.Format = "";
            dataGridTextBoxColumn1.FormatInfo = null;
            dataGridTextBoxColumn1.MappingName = "";
            dataGridTextBoxColumn1.NullText = "";
            dataGridTextBoxColumn1.Width = 75;
            dataGridBoolColumn1.FalseValue = false;
            dataGridBoolColumn1.MappingName = "enabled";
            dataGridBoolColumn1.NullText = "False";
            dataGridBoolColumn1.NullValue = "False";
            dataGridBoolColumn1.TrueValue = true;
            dataGridBoolColumn1.Width = 75;
            dataGridTextBoxColumn2.Format = "";
            dataGridTextBoxColumn2.FormatInfo = null;
            dataGridTextBoxColumn2.MappingName = "";
            dataGridTextBoxColumn2.NullText = "";
            dataGridTextBoxColumn2.Width = 75;
            dataGridTextBoxColumn3.Format = "";
            dataGridTextBoxColumn3.FormatInfo = null;
            dataGridTextBoxColumn3.MappingName = "";
            dataGridTextBoxColumn3.NullText = "";
            dataGridTextBoxColumn3.Width = 75;
            dataGridTextBoxColumn4.Format = "";
            dataGridTextBoxColumn4.FormatInfo = null;
            dataGridTextBoxColumn4.MappingName = "";
            dataGridTextBoxColumn4.NullText = "";
            dataGridTextBoxColumn4.Width = 75;
            dataGridTextBoxColumn5.Format = "";
            dataGridTextBoxColumn5.FormatInfo = null;
            dataGridTextBoxColumn5.MappingName = "";
            dataGridTextBoxColumn5.NullText = "";
            dataGridTextBoxColumn5.Width = 75;
            dataGrid2.AlternatingBackColor = Color.LightGray;
            dataGrid2.BackColor = Color.DarkGray;
            dataGrid2.BorderStyle = BorderStyle.FixedSingle;
            dataGrid2.CaptionBackColor = Color.White;
            dataGrid2.CaptionFont = new Font("Verdana", 10f);
            dataGrid2.CaptionForeColor = Color.Navy;
            dataGrid2.CaptionText = "eigenvalues of the last run:";
            dataGrid2.DataMember = "";
            dataGrid2.DataSource = viewEigenValues;
            dataGrid2.Dock = DockStyle.Bottom;
            dataGrid2.ForeColor = Color.Black;
            dataGrid2.GridLineColor = Color.Black;
            dataGrid2.GridLineStyle = DataGridLineStyle.None;
            dataGrid2.HeaderBackColor = Color.Silver;
            dataGrid2.HeaderForeColor = Color.Black;
            dataGrid2.LinkColor = Color.Navy;
            dataGrid2.Location = new Point(0, 238);
            dataGrid2.Name = "dataGrid2";
            dataGrid2.ParentRowsBackColor = Color.White;
            dataGrid2.ParentRowsForeColor = Color.Black;
            dataGrid2.PreferredColumnWidth = 200;
            dataGrid2.SelectionBackColor = Color.Navy;
            dataGrid2.SelectionForeColor = Color.White;
            dataGrid2.Size = new Size(482, 176);
            dataGrid2.TabIndex = 4;
            toolTip1.SetToolTip(dataGrid2, "displays the list of the eigenvalues returned last");
            viewEigenValues.AllowDelete = false;
            viewEigenValues.AllowNew = false;
            viewEigenValues.Table = tblEigenValues;
            tblEigenValues.Columns.AddRange(new DataColumn[2]
            {
                dataColumn7,
                dataColumn8
            });
            tblEigenValues.Locale = new CultureInfo("en-US");
            tblEigenValues.TableName = "EigenValues";
            dataColumn7.AllowDBNull = false;
            dataColumn7.ColumnName = "RealPart";
            dataColumn7.DataType = typeof(double);
            dataColumn7.DefaultValue = 0;
            dataColumn8.AllowDBNull = false;
            dataColumn8.ColumnName = "ComplexPart";
            dataColumn8.DataType = typeof(double);
            dataColumn8.DefaultValue = 0;
            panel3.Controls.Add(cmdUnselect);
            panel3.Controls.Add(cmdSave);
            panel3.Controls.Add(cmdExport);
            panel3.Controls.Add(cmdCopy);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(370, 10);
            panel3.Name = "panel3";
            panel3.Size = new Size(482, 32);
            panel3.TabIndex = 4;
            cmdUnselect.FlatStyle = FlatStyle.Popup;
            cmdUnselect.Location = new Point(0, 5);
            cmdUnselect.Name = "cmdUnselect";
            cmdUnselect.TabIndex = 8;
            cmdUnselect.Text = "Unselect All";
            cmdUnselect.Click += OnUnselectClick;
            cmdSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdSave.FlatStyle = FlatStyle.Popup;
            cmdSave.Location = new Point(324, 5);
            cmdSave.Name = "cmdSave";
            cmdSave.TabIndex = 7;
            cmdSave.Text = "SaveAs";
            toolTip1.SetToolTip(cmdSave, "Exports the optimized values to an SBML level 2 file");
            cmdSave.Click += OnSaveClick;
            cmdExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmdExport.FlatStyle = FlatStyle.Popup;
            cmdExport.Location = new Point(401, 5);
            cmdExport.Name = "cmdExport";
            cmdExport.TabIndex = 6;
            cmdExport.Text = "Export";
            toolTip1.SetToolTip(cmdExport, "exports the optimized values to JDesigner");
            cmdExport.Click += OnExportClick;
            cmdCopy.FlatStyle = FlatStyle.Popup;
            cmdCopy.Location = new Point(192, 5);
            cmdCopy.Name = "cmdCopy";
            cmdCopy.TabIndex = 5;
            cmdCopy.Text = "<<";
            toolTip1.SetToolTip(cmdCopy, "copies the optimized values to the row of initial values");
            cmdCopy.Click += cmdCopy_Click_1;
            splitter1.Location = new Point(362, 10);
            splitter1.MinExtra = 100;
            splitter1.MinSize = 352;
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(8, 446);
            splitter1.TabIndex = 3;
            splitter1.TabStop = false;
            splitter1.SplitterMoved += splitter1_SplitterMoved;
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(resultPanel);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(10, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 446);
            panel1.TabIndex = 1;
            panel5.Controls.Add(m_oPlotPane);
            panel5.Controls.Add(panel6);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(0, 104);
            panel5.Name = "panel5";
            panel5.Size = new Size(352, 286);
            panel5.TabIndex = 8;
            m_oPlotPane.BackColor = SystemColors.ControlDark;
            m_oPlotPane.ContextMenu = plotMenu;
            m_oPlotPane.Dock = DockStyle.Fill;
            m_oPlotPane.Location = new Point(0, 32);
            m_oPlotPane.Name = "m_oPlotPane";
            m_oPlotPane.Size = new Size(352, 254);
            m_oPlotPane.TabIndex = 2;
            toolTip1.SetToolTip(m_oPlotPane, "displays the current fitness (right click to clear the graph)");
            m_oPlotPane.Paint += plotPane_Paint;
            plotMenu.MenuItems.AddRange(new MenuItem[1]
            {
                menuItem1
            });
            menuItem1.Index = 0;
            menuItem1.Text = "Clear Graph";
            menuItem1.Click += menuItem2_Click;
            panel6.Controls.Add(toolBar1);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(352, 32);
            panel6.TabIndex = 9;
            toolBar1.Appearance = ToolBarAppearance.Flat;
            toolBar1.Buttons.AddRange(new ToolBarButton[6]
            {
                toolBarZoomIn,
                toolBarAuto,
                toolBarZoomOut,
                toolBarButton1,
                toolBarClear,
                toolBarGrad
            });
            toolBar1.DropDownArrows = true;
            toolBar1.ImageList = imageList1;
            toolBar1.Location = new Point(0, 0);
            toolBar1.Name = "toolBar1";
            toolBar1.ShowToolTips = true;
            toolBar1.Size = new Size(352, 32);
            toolBar1.TabIndex = 0;
            toolBar1.TextAlign = ToolBarTextAlign.Right;
            toolBar1.ButtonClick += toolBar1_ButtonClick;
            toolBarZoomIn.ImageIndex = 1;
            toolBarZoomIn.Tag = "+";
            toolBarZoomIn.ToolTipText = "Increase Y Axis scale";
            toolBarAuto.ImageIndex = 2;
            toolBarAuto.Tag = "[]";
            toolBarAuto.ToolTipText = "compute axis scale automatically";
            toolBarZoomOut.ImageIndex = 0;
            toolBarZoomOut.Tag = "-";
            toolBarZoomOut.ToolTipText = "Decrease Y Axis scale";
            toolBarButton1.Style = ToolBarButtonStyle.Separator;
            toolBarClear.ImageIndex = 3;
            toolBarClear.Tag = "clear";
            toolBarClear.ToolTipText = "Clear the graph";
            toolBarGrad.ImageIndex = 4;
            toolBarGrad.Style = ToolBarButtonStyle.ToggleButton;
            toolBarGrad.Tag = "on/off";
            toolBarGrad.ToolTipText = "switch on or off gradient";
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageSize = new Size(20, 20);

            imageList1.TransparentColor = Color.Silver;
            resultPanel.Controls.Add(label5);
            resultPanel.Controls.Add(txtSimulations);
            resultPanel.Controls.Add(txtIteration);
            resultPanel.Controls.Add(label4);
            resultPanel.Controls.Add(txtFitness);
            resultPanel.Controls.Add(txtEigenValue);
            resultPanel.Controls.Add(label1);
            resultPanel.Controls.Add(label2);
            resultPanel.Dock = DockStyle.Bottom;
            resultPanel.Location = new Point(0, 390);
            resultPanel.Name = "resultPanel";
            resultPanel.Size = new Size(352, 56);
            resultPanel.TabIndex = 7;
            label5.Location = new Point(280, 8);
            label5.Name = "label5";
            label5.Size = new Size(72, 23);
            label5.TabIndex = 11;
            label5.Text = "# Simulations";
            txtSimulations.Location = new Point(288, 32);
            txtSimulations.Name = "txtSimulations";
            txtSimulations.ReadOnly = true;
            txtSimulations.Size = new Size(56, 20);
            txtSimulations.TabIndex = 10;
            txtSimulations.TabStop = false;
            txtSimulations.Text = "";
            txtSimulations.TextAlign = HorizontalAlignment.Right;
            txtIteration.Location = new Point(224, 32);
            txtIteration.Name = "txtIteration";
            txtIteration.ReadOnly = true;
            txtIteration.Size = new Size(56, 20);
            txtIteration.TabIndex = 9;
            txtIteration.TabStop = false;
            txtIteration.Text = "";
            txtIteration.TextAlign = HorizontalAlignment.Right;
            label4.Location = new Point(224, 8);
            label4.Name = "label4";
            label4.Size = new Size(56, 23);
            label4.TabIndex = 7;
            label4.Text = "Iteration";
            txtFitness.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtFitness.Location = new Point(8, 32);
            txtFitness.Name = "txtFitness";
            txtFitness.ReadOnly = true;
            txtFitness.Size = new Size(80, 20);
            txtFitness.TabIndex = 7;
            txtFitness.TabStop = false;
            txtFitness.Text = "";
            txtFitness.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(txtFitness, "displays the current fitness value");
            txtEigenValue.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtEigenValue.Location = new Point(96, 32);
            txtEigenValue.Name = "txtEigenValue";
            txtEigenValue.ReadOnly = true;
            txtEigenValue.Size = new Size(120, 20);
            txtEigenValue.TabIndex = 8;
            txtEigenValue.TabStop = false;
            txtEigenValue.Text = "";
            txtEigenValue.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(txtEigenValue, "displays the lowest eigenvalue found");
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.Location = new Point(8, 8);
            label1.Name = "label1";
            label1.Size = new Size(80, 23);
            label1.TabIndex = 5;
            label1.Text = "Fitness value";
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.Location = new Point(96, 8);
            label2.Name = "label2";
            label2.Size = new Size(136, 23);
            label2.TabIndex = 6;
            label2.Text = "Current lowest eigenvalue";
            panel2.Controls.Add(groupBox1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(352, 104);
            panel2.TabIndex = 1;
            groupBox1.Controls.Add(cmdSteadyState);
            groupBox1.Controls.Add(cmdCancel);
            groupBox1.Controls.Add(m_oCmdLoad);
            groupBox1.Controls.Add(cmdStop);
            groupBox1.Controls.Add(m_oCmdStart);
            groupBox1.Controls.Add(cmdConfigure);
            groupBox1.Controls.Add(oMode);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.FlatStyle = FlatStyle.System;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(352, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Discover";
            cmdSteadyState.FlatStyle = FlatStyle.System;
            cmdSteadyState.Location = new Point(280, 24);
            cmdSteadyState.Name = "cmdSteadyState";
            cmdSteadyState.Size = new Size(56, 23);
            cmdSteadyState.TabIndex = 6;
            cmdSteadyState.Text = "Steady";
            toolTip1.SetToolTip(cmdSteadyState, "Compute Steady State and update Values");
            cmdSteadyState.Click += cmdSteadyState_Click;
            cmdCancel.FlatStyle = FlatStyle.System;
            cmdCancel.Location = new Point(256, 56);
            cmdCancel.Name = "cmdCancel";
            cmdCancel.Size = new Size(80, 23);
            cmdCancel.TabIndex = 5;
            cmdCancel.Text = "Kill";
            toolTip1.SetToolTip(cmdCancel, "If a simulation is running this button cancels the Simulation");
            cmdCancel.Click += OnKillClick;
            m_oCmdLoad.FlatStyle = FlatStyle.System;
            m_oCmdLoad.Location = new Point(16, 56);
            m_oCmdLoad.Name = "m_oCmdLoad";
            m_oCmdLoad.Size = new Size(64, 23);
            m_oCmdLoad.TabIndex = 2;
            m_oCmdLoad.Text = "Load";
            toolTip1.SetToolTip(m_oCmdLoad, "load a new sbml file to be analyzed");
            m_oCmdLoad.Click += OnLoadClick;
            cmdStop.FlatStyle = FlatStyle.System;
            cmdStop.Location = new Point(170, 56);
            cmdStop.Name = "cmdStop";
            cmdStop.Size = new Size(80, 24);
            cmdStop.TabIndex = 4;
            cmdStop.Text = "Stop";
            toolTip1.SetToolTip(cmdStop, "stop the current operation");
            cmdStop.Click += OnStopClicked;
            m_oCmdStart.FlatStyle = FlatStyle.System;
            m_oCmdStart.Location = new Point(85, 56);
            m_oCmdStart.Name = "m_oCmdStart";
            m_oCmdStart.Size = new Size(80, 24);
            m_oCmdStart.TabIndex = 3;
            m_oCmdStart.Text = "Start";
            toolTip1.SetToolTip(m_oCmdStart, "start the discovery");
            m_oCmdStart.Click += OnStartClicked;
            cmdConfigure.FlatStyle = FlatStyle.System;
            cmdConfigure.Location = new Point(192, 24);
            cmdConfigure.Name = "cmdConfigure";
            cmdConfigure.Size = new Size(80, 24);
            cmdConfigure.TabIndex = 1;
            cmdConfigure.Text = "Configure";
            toolTip1.SetToolTip(cmdConfigure, "configure the selected method");
            cmdConfigure.Click += OnConfigureClick;
            oMode.Items.AddRange(new object[2]
            {
                "Oscillation",
                "Turning Point"
            });
            oMode.Location = new Point(16, 24);
            oMode.Name = "oMode";
            oMode.Size = new Size(168, 21);
            oMode.TabIndex = 0;
            oMode.SelectedIndexChanged += oMode_SelectedIndexChanged;
            tabPage3.Controls.Add(groupBox2);
            tabPage3.Controls.Add(dataGrid3);
            ((Control)tabPage3).Location = new Point(4, 22);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(854, 440);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Test parameters for Switch";
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(comboSpecies);
            groupBox2.Controls.Add(m_oCmdStartTest);
            groupBox2.Dock = DockStyle.Bottom;
            groupBox2.Location = new Point(0, 384);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(854, 56);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = " Start the Test: ";
            label3.Location = new Point(8, 24);
            label3.Name = "label3";
            label3.Size = new Size(192, 23);
            label3.TabIndex = 2;
            label3.Text = "Select Species to test parameter for:";
            comboSpecies.Location = new Point(208, 16);
            comboSpecies.Name = "comboSpecies";
            comboSpecies.Size = new Size(121, 21);
            comboSpecies.TabIndex = 1;
            toolTip1.SetToolTip(comboSpecies,
                "select the species in regard to which the selected parameter(s) is(are) (a) switch(es)");
            m_oCmdStartTest.FlatStyle = FlatStyle.System;
            m_oCmdStartTest.Location = new Point(360, 16);
            m_oCmdStartTest.Name = "m_oCmdStartTest";
            m_oCmdStartTest.TabIndex = 0;
            m_oCmdStartTest.Text = "Start";
            toolTip1.SetToolTip(m_oCmdStartTest, "start looking for a switch");
            m_oCmdStartTest.Click += OnStartTest;
            dataGrid3.AlternatingBackColor = Color.LightGray;
            dataGrid3.BackColor = Color.DarkGray;
            dataGrid3.CaptionBackColor = Color.White;
            dataGrid3.CaptionFont = new Font("Verdana", 10f);
            dataGrid3.CaptionForeColor = Color.Navy;
            dataGrid3.CaptionText = "Select the Parameters to be tested:";
            dataGrid3.DataMember = "";
            dataGrid3.DataSource = viewSwitch;
            dataGrid3.Dock = DockStyle.Fill;
            dataGrid3.ForeColor = Color.Black;
            dataGrid3.GridLineColor = Color.Black;
            dataGrid3.GridLineStyle = DataGridLineStyle.None;
            dataGrid3.HeaderBackColor = Color.Silver;
            dataGrid3.HeaderForeColor = Color.Black;
            dataGrid3.LinkColor = Color.Navy;
            dataGrid3.Location = new Point(0, 0);
            dataGrid3.Name = "dataGrid3";
            dataGrid3.ParentRowsBackColor = Color.White;
            dataGrid3.ParentRowsForeColor = Color.Black;
            dataGrid3.SelectionBackColor = Color.Navy;
            dataGrid3.SelectionForeColor = Color.White;
            dataGrid3.Size = new Size(854, 440);
            dataGrid3.TabIndex = 0;
            dataGrid3.Validated += dataGrid3_Validated;
            viewSwitch.AllowDelete = false;
            viewSwitch.AllowNew = false;
            viewSwitch.Table = tblSwitch;
            tblSwitch.Columns.AddRange(new DataColumn[3]
            {
                dataColumn9,
                dataColumn10,
                dataColumn11
            });
            tblSwitch.Locale = new CultureInfo("en-US");
            tblSwitch.TableName = "SwitchParameter";
            dataColumn9.AllowDBNull = false;
            dataColumn9.ColumnName = "Parametername";
            dataColumn9.DefaultValue = "";
            dataColumn9.ReadOnly = true;
            dataColumn10.ColumnName = "Test for Switch";
            dataColumn10.DataType = typeof(bool);
            dataColumn10.DefaultValue = true;
            dataColumn11.ColumnName = "is switch";
            dataColumn11.DefaultValue = "";
            dataColumn11.ReadOnly = true;
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Controls.Add(dataGrid4);
            ((Control)tabPage2).Location = new Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new Size(854, 440);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Test parameters for Oscillator";
            groupBox3.Controls.Add(cmdTestOscillator);
            groupBox3.Dock = DockStyle.Bottom;
            groupBox3.Location = new Point(0, 392);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(854, 48);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = " Start the Test: ";
            cmdTestOscillator.FlatStyle = FlatStyle.System;
            cmdTestOscillator.Location = new Point(8, 16);
            cmdTestOscillator.Name = "cmdTestOscillator";
            cmdTestOscillator.TabIndex = 0;
            cmdTestOscillator.Text = "Start";
            toolTip1.SetToolTip(cmdTestOscillator, "start looking for a switch");
            cmdTestOscillator.Click += cmdTestOscillator_Click;
            dataGrid4.AlternatingBackColor = Color.LightGray;
            dataGrid4.BackColor = Color.DarkGray;
            dataGrid4.CaptionBackColor = Color.White;
            dataGrid4.CaptionFont = new Font("Verdana", 10f);
            dataGrid4.CaptionForeColor = Color.Navy;
            dataGrid4.CaptionText = "Select the Parameters to be tested:";
            dataGrid4.DataMember = "";
            dataGrid4.DataSource = viewOscillator;
            dataGrid4.Dock = DockStyle.Fill;
            dataGrid4.ForeColor = Color.Black;
            dataGrid4.GridLineColor = Color.Black;
            dataGrid4.GridLineStyle = DataGridLineStyle.None;
            dataGrid4.HeaderBackColor = Color.Silver;
            dataGrid4.HeaderForeColor = Color.Black;
            dataGrid4.LinkColor = Color.Navy;
            dataGrid4.Location = new Point(0, 0);
            dataGrid4.Name = "dataGrid4";
            dataGrid4.ParentRowsBackColor = Color.White;
            dataGrid4.ParentRowsForeColor = Color.Black;
            dataGrid4.SelectionBackColor = Color.Navy;
            dataGrid4.SelectionForeColor = Color.White;
            dataGrid4.Size = new Size(854, 440);
            dataGrid4.TabIndex = 0;
            dataGrid4.Validated += dataGrid4_Validated;
            viewOscillator.AllowDelete = false;
            viewOscillator.AllowNew = false;
            viewOscillator.Table = tblOscillator;
            tblOscillator.Columns.AddRange(new DataColumn[3]
            {
                dataColumn12,
                dataColumn13,
                dataColumn14
            });
            tblOscillator.TableName = "OscillatorParameter";
            dataColumn12.ColumnName = "Parametername";
            dataColumn12.DefaultValue = "";
            dataColumn12.ReadOnly = true;
            dataColumn13.ColumnName = "Test for Hopf";
            dataColumn13.DataType = typeof(bool);
            dataColumn13.DefaultValue = false;
            dataColumn14.ColumnName = "is Hopf?";
            dataColumn14.DefaultValue = "undecided";
            dataColumn14.ReadOnly = true;
            m_oParameterDataSet.DataSetName = "parameter data set";
            m_oParameterDataSet.Locale = new CultureInfo("en-US");
            m_oParameterDataSet.Tables.AddRange(new DataTable[4]
            {
                tblParameters,
                tblEigenValues,
                tblSwitch,
                tblOscillator
            });
            openFileDialog1.DefaultExt = "xml";
            openFileDialog1.Filter = "SBML files | *.xml|All files|*.*";
            openFileDialog1.Title = "choose model to load";
            saveFileDialog1.Filter = "SBML files|*.xml|All files|*.*";
            mainMenu1.MenuItems.AddRange(new MenuItem[3]
            {
                menuItem3,
                mnuSBW,
                menuItem5
            });
            menuItem3.Index = 0;
            menuItem3.MenuItems.AddRange(new MenuItem[5]
            {
                mnuOpen,
                mnuSaveAs,
                mnuExport,
                menuItem10,
                mnuExit
            });
            menuItem3.Text = "&File";
            mnuOpen.Index = 0;
            mnuOpen.Shortcut = Shortcut.CtrlO;
            mnuOpen.Text = "&Open";
            mnuOpen.Click += OnLoadClick;
            mnuSaveAs.Index = 1;
            mnuSaveAs.Shortcut = Shortcut.CtrlShiftS;
            mnuSaveAs.Text = "Save &As";
            mnuSaveAs.Click += OnSaveClick;
            mnuExport.Index = 2;
            mnuExport.Shortcut = Shortcut.CtrlE;
            mnuExport.Text = "&Export";
            mnuExport.Click += OnExportClick;
            menuItem10.Index = 3;
            menuItem10.Text = "-";
            mnuExit.Index = 4;
            mnuExit.Shortcut = Shortcut.CtrlQ;
            mnuExit.Text = "E&xit";
            mnuExit.Click += BifurcationForm_Closed;
            mnuSBW.Index = 1;
            mnuSBW.Text = "&SBW";
            menuItem5.Index = 2;
            menuItem5.MenuItems.AddRange(new MenuItem[1]
            {
                menuItem6
            });
            menuItem5.Text = "&Help";
            menuItem6.Index = 0;
            menuItem6.Shortcut = Shortcut.F1;
            menuItem6.Text = "&About";
            menuItem6.Click += menuItem6_Click;
            AllowDrop = true;
            AutoScaleBaseSize = new Size(5, 13);
            ClientSize = new Size(862, 466);
            Controls.Add(mainContent);
            Menu = mainMenu1;
            MinimumSize = new Size(864, 500);
            Name = "BifurcationForm";
            Text = "BioSPICE Bifurcation Discovery Tool";
            Resize += BifurcationForm_Resize;
            Load += BifurcationForm_Load;
            DragDrop += BifurcationForm_DragDrop;
            Closed += BifurcationForm_Closed;
            DragEnter += BifurcationForm_DragEnter;
            tabControl1.ResumeLayout(false);
            mainContent.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            dataGrid1.EndInit();
            viewParameters.EndInit();
            tblParameters.EndInit();
            dataGrid2.EndInit();
            viewEigenValues.EndInit();
            tblEigenValues.EndInit();
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            resultPanel.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            dataGrid3.EndInit();
            viewSwitch.EndInit();
            tblSwitch.EndInit();
            tabPage2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            dataGrid4.EndInit();
            viewOscillator.EndInit();
            tblOscillator.EndInit();
            m_oParameterDataSet.EndInit();
            ResumeLayout(false);
        }

        private void SetSize()
        {
            Rectangle clientRectangle = m_oPlotPane.ClientRectangle;
            clientRectangle.Inflate(-1, -1);
            m_oControl.GraphPane.PaneRect = clientRectangle;
        }

        private void BindData(bool bDoBind)
        {
            if (bDoBind)
            {
                dataGrid1.DataSource = viewParameters;
                dataGrid2.DataSource = viewEigenValues;
                dataGrid3.DataSource = viewSwitch;
                dataGrid4.DataSource = viewOscillator;
                ((DataGridBoolColumn)dataGrid1.TableStyles[0].GridColumnStyles[1]).AllowNull = false;
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

        private void OnConfigureClick(object sender, EventArgs e)
        {
            if (m_oDialog == null)
                m_oDialog = new ConfigDialog();
            if (m_oDialog.ShowDialog() != DialogResult.OK)
                return;
            m_bConfigurationChanged = true;
        }

        private void cmdCopy_Click_1(object sender, EventArgs e)
        {
            menuItem2_Click_1(this, e);
        }

        private void OnExportClick(object sender, EventArgs e)
        {
            try
            {
                Service service = new Module("JDesigner").getService("model");
                var a = new ArrayList();
                foreach (DataRow dataRow in (InternalDataCollectionBase)m_oParameterDataSet.Tables[0].Rows)
                {
                    if ((bool)dataRow["enabled"])
                        a.Add(new ArrayList()
                        {
                            (object) (string) dataRow[0],
                            (object) (double) dataRow[3]
                        });
                }
                var oArguments = new DataBlockWriter();
                oArguments.add(a);
                service.getMethod("void setParameterValues({})").call(oArguments);
            }
            catch (SBWException ex)
            {
                var num =
                    (int)MessageBox.Show(ex.Message, "an error occured while exporting optimized values to JDesigner");
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

        private void OnStartClicked(object sender, EventArgs e)
        {
            if (m_bThreadStarted)
            {
                var num1 = (int)MessageBox.Show("call already in progress ... ");
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
                m_nLastCall = !(oMode.Text == (string)oMode.Items[0]) ? 2 : 1;


                //m_oCallThread = new Thread(BifurcationCaller);
                setEnable(false);
                m_oControl.GraphPane.YAxis.MaxAuto = true;
                m_oControl.AxisChange();
                m_oPlotPane.Refresh();
                //m_oCallThread.Start();
                m_oControl.GraphPane.YAxis.MaxAuto = true;
                m_oControl.AxisChange();
                m_oPlotPane.Refresh();
                thread.QueueItem(() => BifurcationCaller());
                
            }
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
                                NOM.setValue((string)floatingSpeciesNames[index], speciesConcentrations1[index]);
                            for (int index = 0; index < boundarySpeciesNames.Count; ++index)
                                NOM.setValue((string)boundarySpeciesNames[index], speciesConcentrations2[index]);
                            foreach (ArrayList arrayList in parameterTupleList)
                                NOM.setValue((string)arrayList[0], (double)arrayList[1]);
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

        private void OnStopClicked(object sender, EventArgs e)
        {
            Optimizer.isProcessActive = false;
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

        private void OnUnselectClick(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase)m_oParameterDataSet.Tables[0].Rows)
            {
                if ((bool)dataRow["enabled"])
                    dataRow["enabled"] = false;
            }
        }

        private void dataGrid1_Validated(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase)m_oParameterDataSet.Tables[0].Rows)
            {
                try
                {
                    var flag = (bool)dataRow["enabled"];
                    dataRow["enabled"] = (bool)(flag ? true : false);
                }
                catch (Exception ex)
                {
                    dataRow["enabled"] = false;
                }
            }
        }

        private void dataGrid3_Validated(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase)m_oParameterDataSet.Tables[2].Rows)
            {
                try
                {
                    var flag = (bool)dataRow[1];
                    dataRow[1] = (bool)(flag ? true : false);
                }
                catch (Exception ex)
                {
                    dataRow[1] = false;
                }
            }
        }

        private void dataGrid4_Validated(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase)m_oParameterDataSet.Tables[3].Rows)
            {
                try
                {
                    var flag = (bool)dataRow[1];
                    dataRow[1] = (bool)(flag ? true : false);
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
                return (AlgorithmArguments)Invoke(new getArgumentsDelegate(getArguments), new object[1]
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
            List<Tuple<string, double>> newValues = new List<Tuple<string, double>>();
            foreach (DataRow dataRow in (InternalDataCollectionBase)m_oParameterDataSet.Tables[0].Rows)
            {
                if ((bool)dataRow["enabled"])
                    newValues.Add(new Tuple<string, double>((string)dataRow[0], (double)dataRow[3]));
                
            }

            string result = "";
            AutoResetEvent autoEvent = new AutoResetEvent(false);
            thread.QueueItem(() =>
                {
                    NOM.loadSBML(m_sSBML);
                    foreach (Tuple<string, double> newValue in newValues)
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
                if ((bool)dataRow["enabled"])
                {
                    result.Add(new Tuple<string, double, double, double>(

                        (string)dataRow["parameter name"],
                        (double)dataRow["initial value"],
                        (double)dataRow["MIN"],
                        (double)dataRow["MAX"]
                    ));
                    m_oLastSelection.Add(index);
                }
            }
            return result;
        }

        private void loadFile(string sFileName)
        {
            StreamReader streamReader = File.OpenText(sFileName);
            m_sSBML = streamReader.ReadToEnd();
            m_sFilename = new FileInfo(sFileName).Name;
            streamReader.Close();
            loadSBML(m_sSBML);
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
                    m_oControl.GraphPane.CurveList.Clear();
                    m_oControl.GraphPane.YAxis.MaxAuto = true;
                    m_oControl.GraphPane.Title = m_sFilename;
                    m_oControl.AxisChange();
                    m_oPlotPane.Refresh();
                    m_oParameterDataSet.Tables["Parameters"].Rows.Clear();

                    foreach (ArrayList arrayList in globalParameters)
                    {
                        var num = (double)arrayList[1];
                        var str = (string)arrayList[0];
                        DataRow row = m_oParameterDataSet.Tables[0].NewRow();
                        row["parameter name"] = str;
                        row["enabled"] = true;
                        row["initial value"] = num;
                        row["optimized value"] = num;
                        row["MIN"] = num * 0.1;
                        row["MAX"] = num > 10.0 ? num * 2.0 : 10.0;
                        m_oParameterDataSet.Tables[0].Rows.Add(row);
                    }
                    foreach (ArrayList arrayList in boundarySpecies)
                    {
                        var num = (double)arrayList[1];
                        var str = (string)arrayList[0];
                        DataRow row = m_oParameterDataSet.Tables["Parameters"].NewRow();
                        row["parameter name"] = str;
                        row["enabled"] = true;
                        row["initial value"] = num;
                        row["optimized value"] = num;
                        row["MIN"] = num * 0.1;
                        row["MAX"] = num > 10.0 ? num * 2.0 : 10.0;
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
            m_oControl.GraphPane.CurveList.Clear();
            m_oControl.AxisChange();
            m_oPlotPane.Refresh();
        }

        private void menuItem2_Click_1(object sender, EventArgs e)
        {
            foreach (DataRow dataRow in (InternalDataCollectionBase)m_oParameterDataSet.Tables["Parameters"].Rows)
            {
                if ((bool)dataRow["enabled"])
                    dataRow[2] = dataRow[3];
            }
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            if (m_oAbout == null)
                m_oAbout = new AboutBifurcation(HighLevel.getSBWVersion());
            var num = (int)m_oAbout.ShowDialog();
        }


        private void oMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (oMode.SelectedIndex != 0)
                return;
            enableTest(false);
        }

        private void plotPane_Paint(object sender, PaintEventArgs e)
        {
            var solidBrush1 = new SolidBrush(Color.Gray);
            Rectangle clientRectangle = m_oPlotPane.ClientRectangle;
            if (m_oMemGraphics.CanDoubleBuffer())
            {
                try
                {
                    Graphics g = m_oMemGraphics.g;
                    var solidBrush2 = new SolidBrush(SystemColors.Window);
                    int x = e.ClipRectangle.X;
                    Rectangle clipRectangle = e.ClipRectangle;
                    int y = clipRectangle.Y;
                    clipRectangle = e.ClipRectangle;
                    int width = clipRectangle.Width;
                    clipRectangle = e.ClipRectangle;
                    int height = clipRectangle.Height;
                    g.FillRectangle(solidBrush2, x, y, width, height);
                    m_oMemGraphics.g.FillRectangle(solidBrush1, clientRectangle);
                    m_oControl.GraphPane.AxisChange(m_oMemGraphics.g);
                    m_oControl.GraphPane.Draw(m_oMemGraphics.g);
                    m_oMemGraphics.Render(e.Graphics);
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
            else
            {
                e.Graphics.FillRectangle(solidBrush1, clientRectangle);
                m_oControl.GraphPane.Draw(e.Graphics);
            }
        }

        private void setEnable(bool bEnable)
        {
            if (InvokeRequired)
                Invoke(new enableDelegate(setEnable), new object[1]
                {
                    (bool) (bEnable ? true : false)
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
                    var num1 = (int)oResults.Iterations;
                    var num2 = (double)oResults.Score;
                    var numArray = (double[])oResults.Values.ToArray();
                    var realEigenValues = (double[])oResults.RealEigenValues.ToArray();
                    var complexEigenValues = (double[])oResults.ImagEigenValues.ToArray();
                    int num3 = 0;
                    for (int index = 0; index < m_oLastSelection.Count; ++index)
                        m_oParameterDataSet.Tables["Parameters"].Rows[(int)m_oLastSelection[index]]["optimized value"]
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
                                (InternalDataCollectionBase)m_oParameterDataSet.Tables["Parameters"].Rows)
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
                                (InternalDataCollectionBase)m_oParameterDataSet.Tables["Parameters"].Rows)
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
            DBGraphics dbGraphics = m_oMemGraphics;
            Graphics graphics = m_oPlotPane.CreateGraphics();
            Rectangle clientRectangle = m_oPlotPane.ClientRectangle;
            int width = clientRectangle.Width;
            clientRectangle = m_oPlotPane.ClientRectangle;
            int height = clientRectangle.Height;
            dbGraphics.CreateDoubleBuffer(graphics, width, height);
            SetSize();
            m_oPlotPane.Refresh();
        }

        private void toolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            try
            {
                if ((string)e.Button.Tag == "clear")
                    m_oControl.GraphPane.CurveList.Clear();
                else if ((string)e.Button.Tag == "+")
                {
                    m_oControl.GraphPane.YAxis.MaxAuto = false;
                    YAxis yaxis = m_oControl.GraphPane.YAxis;
                    double num = yaxis.Max * 2.0;
                    yaxis.Max = num;
                }
                else if ((string)e.Button.Tag == "-")
                {
                    m_oControl.GraphPane.YAxis.MaxAuto = false;
                    if (m_oControl.GraphPane.YAxis.Max != 0.0 && m_oControl.GraphPane.YAxis.Max > 1E-15)
                    {
                        YAxis yaxis = m_oControl.GraphPane.YAxis;
                        double num = yaxis.Max / 2.0;
                        yaxis.Max = num;
                    }
                }
                else if ((string)e.Button.Tag == "SBW")
                    oSBWMenu.Show(e.Button.Parent, e.Button.Rectangle.Location);
                else if ((string)e.Button.Tag == "on/off")
                {
                    if (!e.Button.Pushed)
                    {
                        m_oControl.GraphPane.PaneFill = new Fill(Color.WhiteSmoke);
                        m_oControl.GraphPane.AxisFill = new Fill(Color.White);
                    }
                    else
                    {
                        m_oControl.GraphPane.PaneFill = new Fill(Color.WhiteSmoke, Color.Lavender, 0.0f);
                        m_oControl.GraphPane.AxisFill = new Fill(Color.White,
                            Color.FromArgb(byte.MaxValue, byte.MaxValue, 166), 90f);
                    }
                }
                else if ((string)e.Button.Tag == "[]")
                    m_oControl.GraphPane.YAxis.MaxAuto = true;
                else if ((string)e.Button.Tag == "About")
                {
                    if (m_oAbout == null)
                        m_oAbout = new AboutBifurcation(HighLevel.getSBWVersion());
                    var num = (int)m_oAbout.ShowDialog();
                }
                m_oControl.AxisChange();
                m_oPlotPane.Refresh();
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
                    BeginInvoke(new updateDelegate(update), (object)nIteration, (object)dFitness,
                        (object)dLowestEigenvalueReal, (object)dLowestEigenvalueComplex, (object)dParams,
                        (object)dRealEigen, (object)dComplexEigen);
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
                    m_oParameterDataSet.Tables["Parameters"].Rows[(int)m_oLastSelection[index]]["optimized value"] =
                        dParams[num++];
                BindData(true);
                if (m_oControl.GraphPane.CurveList.Count > 0)
                {
                    ((PointPairList)m_oControl.GraphPane.CurveList[0].Points).Add(nIteration, dFitness);
                }
                else
                {
                    LineItem lineItem = m_oControl.GraphPane.AddCurve("fitness", new double[1]
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
                    if (dFitness >= m_oControl.GraphPane.YAxis.Max)
                        return;
                    m_oControl.AxisChange();
                    m_oPlotPane.Refresh();
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

        private delegate void UpdateUiDelegate(ArrayList globalParameters, ArrayList boundarySpecies, ArrayList floatingSpeciesIds);
        #region Nested type: updateNumberDelegate

        private delegate void updateNumberDelegate(int nNumber);

        #endregion
    }
}