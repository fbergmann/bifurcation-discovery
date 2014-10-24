using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BifurcationFrontend
{
    partial class BifurcationForm
    {
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
        private Button m_oCmdLoad;
        private Button m_oCmdStart;
        private Button m_oCmdStartTest;
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
        private ComboBox oMode;
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

        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BifurcationForm));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.mainContent = new System.Windows.Forms.Panel();
      this.panel4 = new System.Windows.Forms.Panel();
      this.lblDisplayResult = new System.Windows.Forms.Label();
      this.panel7 = new System.Windows.Forms.Panel();
      this.panel8 = new System.Windows.Forms.Panel();
      this.dataGrid1 = new System.Windows.Forms.DataGrid();
      this.gridMenu = new System.Windows.Forms.ContextMenu();
      this.menuItem2 = new System.Windows.Forms.MenuItem();
      this.viewParameters = new System.Data.DataView();
      this.tblParameters = new System.Data.DataTable();
      this.dataColumn1 = new System.Data.DataColumn();
      this.dataColumn2 = new System.Data.DataColumn();
      this.dataColumn3 = new System.Data.DataColumn();
      this.dataColumn4 = new System.Data.DataColumn();
      this.dataColumn5 = new System.Data.DataColumn();
      this.dataColumn6 = new System.Data.DataColumn();
      this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
      this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGridBoolColumn1 = new System.Windows.Forms.DataGridBoolColumn();
      this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
      this.dataGrid2 = new System.Windows.Forms.DataGrid();
      this.viewEigenValues = new System.Data.DataView();
      this.tblEigenValues = new System.Data.DataTable();
      this.dataColumn7 = new System.Data.DataColumn();
      this.dataColumn8 = new System.Data.DataColumn();
      this.panel3 = new System.Windows.Forms.Panel();
      this.cmdUnselect = new System.Windows.Forms.Button();
      this.cmdSave = new System.Windows.Forms.Button();
      this.cmdExport = new System.Windows.Forms.Button();
      this.cmdCopy = new System.Windows.Forms.Button();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.panel1 = new System.Windows.Forms.Panel();
      this.panel5 = new System.Windows.Forms.Panel();
      this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
      this.panel6 = new System.Windows.Forms.Panel();
      this.toolBar1 = new System.Windows.Forms.ToolBar();
      this.toolBarZoomIn = new System.Windows.Forms.ToolBarButton();
      this.toolBarAuto = new System.Windows.Forms.ToolBarButton();
      this.toolBarZoomOut = new System.Windows.Forms.ToolBarButton();
      this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
      this.toolBarClear = new System.Windows.Forms.ToolBarButton();
      this.toolBarGrad = new System.Windows.Forms.ToolBarButton();
      this.imageList1 = new System.Windows.Forms.ImageList(this.components);
      this.resultPanel = new System.Windows.Forms.Panel();
      this.label5 = new System.Windows.Forms.Label();
      this.txtSimulations = new System.Windows.Forms.TextBox();
      this.txtIteration = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtFitness = new System.Windows.Forms.TextBox();
      this.txtEigenValue = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.cmdSteadyState = new System.Windows.Forms.Button();
      this.cmdCancel = new System.Windows.Forms.Button();
      this.m_oCmdLoad = new System.Windows.Forms.Button();
      this.cmdStop = new System.Windows.Forms.Button();
      this.m_oCmdStart = new System.Windows.Forms.Button();
      this.cmdConfigure = new System.Windows.Forms.Button();
      this.oMode = new System.Windows.Forms.ComboBox();
      this.plotMenu = new System.Windows.Forms.ContextMenu();
      this.menuItem1 = new System.Windows.Forms.MenuItem();
      this.oSBWMenu = new System.Windows.Forms.ContextMenu();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label3 = new System.Windows.Forms.Label();
      this.comboSpecies = new System.Windows.Forms.ComboBox();
      this.m_oCmdStartTest = new System.Windows.Forms.Button();
      this.dataGrid3 = new System.Windows.Forms.DataGrid();
      this.viewSwitch = new System.Data.DataView();
      this.tblSwitch = new System.Data.DataTable();
      this.dataColumn9 = new System.Data.DataColumn();
      this.dataColumn10 = new System.Data.DataColumn();
      this.dataColumn11 = new System.Data.DataColumn();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.cmdTestOscillator = new System.Windows.Forms.Button();
      this.dataGrid4 = new System.Windows.Forms.DataGrid();
      this.viewOscillator = new System.Data.DataView();
      this.tblOscillator = new System.Data.DataTable();
      this.dataColumn12 = new System.Data.DataColumn();
      this.dataColumn13 = new System.Data.DataColumn();
      this.dataColumn14 = new System.Data.DataColumn();
      this.m_oParameterDataSet = new System.Data.DataSet();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
      this.menuItem3 = new System.Windows.Forms.MenuItem();
      this.mnuOpen = new System.Windows.Forms.MenuItem();
      this.mnuSaveAs = new System.Windows.Forms.MenuItem();
      this.mnuExport = new System.Windows.Forms.MenuItem();
      this.menuItem10 = new System.Windows.Forms.MenuItem();
      this.mnuExit = new System.Windows.Forms.MenuItem();
      this.mnuSBW = new System.Windows.Forms.MenuItem();
      this.menuItem5 = new System.Windows.Forms.MenuItem();
      this.menuItem6 = new System.Windows.Forms.MenuItem();
      this.menuItem4 = new System.Windows.Forms.MenuItem();
      this.mnuImport = new System.Windows.Forms.MenuItem();
      this.mnuSBWExport = new System.Windows.Forms.MenuItem();
      this.tabControl1.SuspendLayout();
      this.mainContent.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel7.SuspendLayout();
      this.panel8.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewParameters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblParameters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewEigenValues)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblEigenValues)).BeginInit();
      this.panel3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.panel5.SuspendLayout();
      this.panel6.SuspendLayout();
      this.resultPanel.SuspendLayout();
      this.panel2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid3)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewSwitch)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblSwitch)).BeginInit();
      this.tabPage2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid4)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewOscillator)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblOscillator)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_oParameterDataSet)).BeginInit();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(862, 466);
      this.tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Size = new System.Drawing.Size(854, 440);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Configuration";
      // 
      // mainContent
      // 
      this.mainContent.Controls.Add(this.panel4);
      this.mainContent.Controls.Add(this.panel3);
      this.mainContent.Controls.Add(this.splitter1);
      this.mainContent.Controls.Add(this.panel1);
      this.mainContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainContent.Location = new System.Drawing.Point(0, 0);
      this.mainContent.Name = "mainContent";
      this.mainContent.Padding = new System.Windows.Forms.Padding(10);
      this.mainContent.Size = new System.Drawing.Size(862, 466);
      this.mainContent.TabIndex = 5;
      // 
      // panel4
      // 
      this.panel4.Controls.Add(this.lblDisplayResult);
      this.panel4.Controls.Add(this.panel7);
      this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel4.Location = new System.Drawing.Point(370, 42);
      this.panel4.Name = "panel4";
      this.panel4.Size = new System.Drawing.Size(482, 414);
      this.panel4.TabIndex = 5;
      // 
      // lblDisplayResult
      // 
      this.lblDisplayResult.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.lblDisplayResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lblDisplayResult.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.lblDisplayResult.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblDisplayResult.ForeColor = System.Drawing.Color.White;
      this.lblDisplayResult.Location = new System.Drawing.Point(0, 382);
      this.lblDisplayResult.Name = "lblDisplayResult";
      this.lblDisplayResult.Size = new System.Drawing.Size(482, 32);
      this.lblDisplayResult.TabIndex = 4;
      this.lblDisplayResult.Visible = false;
      // 
      // panel7
      // 
      this.panel7.Controls.Add(this.panel8);
      this.panel7.Controls.Add(this.dataGrid2);
      this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel7.Location = new System.Drawing.Point(0, 0);
      this.panel7.Name = "panel7";
      this.panel7.Size = new System.Drawing.Size(482, 414);
      this.panel7.TabIndex = 5;
      // 
      // panel8
      // 
      this.panel8.Controls.Add(this.dataGrid1);
      this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel8.Location = new System.Drawing.Point(0, 0);
      this.panel8.Name = "panel8";
      this.panel8.Size = new System.Drawing.Size(482, 238);
      this.panel8.TabIndex = 5;
      // 
      // dataGrid1
      // 
      this.dataGrid1.AlternatingBackColor = System.Drawing.Color.LightGray;
      this.dataGrid1.BackColor = System.Drawing.Color.DarkGray;
      this.dataGrid1.CaptionBackColor = System.Drawing.Color.White;
      this.dataGrid1.CaptionFont = new System.Drawing.Font("Verdana", 10F);
      this.dataGrid1.CaptionForeColor = System.Drawing.Color.Navy;
      this.dataGrid1.CaptionText = "  Select and modify parameters;";
      this.dataGrid1.CaptionVisible = false;
      this.dataGrid1.ContextMenu = this.gridMenu;
      this.dataGrid1.DataMember = "";
      this.dataGrid1.DataSource = this.viewParameters;
      this.dataGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGrid1.ForeColor = System.Drawing.Color.Black;
      this.dataGrid1.GridLineColor = System.Drawing.Color.Black;
      this.dataGrid1.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
      this.dataGrid1.HeaderBackColor = System.Drawing.Color.Silver;
      this.dataGrid1.HeaderForeColor = System.Drawing.Color.Black;
      this.dataGrid1.LinkColor = System.Drawing.Color.Navy;
      this.dataGrid1.Location = new System.Drawing.Point(0, 0);
      this.dataGrid1.Name = "dataGrid1";
      this.dataGrid1.ParentRowsBackColor = System.Drawing.Color.White;
      this.dataGrid1.ParentRowsForeColor = System.Drawing.Color.Black;
      this.dataGrid1.SelectionBackColor = System.Drawing.Color.Navy;
      this.dataGrid1.SelectionForeColor = System.Drawing.Color.White;
      this.dataGrid1.Size = new System.Drawing.Size(482, 238);
      this.dataGrid1.TabIndex = 3;
      this.dataGrid1.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
      this.dataGrid1.Validated += new System.EventHandler(this.dataGrid1_Validated);
      // 
      // gridMenu
      // 
      this.gridMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
      // 
      // menuItem2
      // 
      this.menuItem2.Index = 0;
      this.menuItem2.Text = "Copy Values";
      this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click_1);
      // 
      // viewParameters
      // 
      this.viewParameters.AllowDelete = false;
      this.viewParameters.AllowNew = false;
      this.viewParameters.Table = this.tblParameters;
      // 
      // tblParameters
      // 
      this.tblParameters.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6});
      this.tblParameters.Locale = new System.Globalization.CultureInfo("en-US");
      this.tblParameters.TableName = "Parameters";
      // 
      // dataColumn1
      // 
      this.dataColumn1.ColumnName = "parameter name";
      this.dataColumn1.ReadOnly = true;
      // 
      // dataColumn2
      // 
      this.dataColumn2.ColumnName = "enabled";
      this.dataColumn2.DataType = typeof(bool);
      this.dataColumn2.DefaultValue = true;
      // 
      // dataColumn3
      // 
      this.dataColumn3.ColumnName = "initial value";
      this.dataColumn3.DataType = typeof(double);
      this.dataColumn3.DefaultValue = 0D;
      // 
      // dataColumn4
      // 
      this.dataColumn4.ColumnName = "optimized value";
      this.dataColumn4.DataType = typeof(double);
      this.dataColumn4.DefaultValue = 0D;
      // 
      // dataColumn5
      // 
      this.dataColumn5.ColumnName = "MIN";
      this.dataColumn5.DataType = typeof(double);
      this.dataColumn5.DefaultValue = 0D;
      // 
      // dataColumn6
      // 
      this.dataColumn6.ColumnName = "MAX";
      this.dataColumn6.DataType = typeof(double);
      this.dataColumn6.DefaultValue = 10D;
      // 
      // dataGridTableStyle1
      // 
      this.dataGridTableStyle1.DataGrid = this.dataGrid1;
      this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridBoolColumn1,
            this.dataGridTextBoxColumn2,
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn5});
      this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
      // 
      // dataGridTextBoxColumn1
      // 
      this.dataGridTextBoxColumn1.Format = "";
      this.dataGridTextBoxColumn1.FormatInfo = null;
      this.dataGridTextBoxColumn1.NullText = "";
      this.dataGridTextBoxColumn1.Width = 75;
      // 
      // dataGridBoolColumn1
      // 
      this.dataGridBoolColumn1.MappingName = "enabled";
      this.dataGridBoolColumn1.NullText = "False";
      this.dataGridBoolColumn1.NullValue = "False";
      this.dataGridBoolColumn1.Width = 75;
      // 
      // dataGridTextBoxColumn2
      // 
      this.dataGridTextBoxColumn2.Format = "";
      this.dataGridTextBoxColumn2.FormatInfo = null;
      this.dataGridTextBoxColumn2.NullText = "";
      this.dataGridTextBoxColumn2.Width = 75;
      // 
      // dataGridTextBoxColumn3
      // 
      this.dataGridTextBoxColumn3.Format = "";
      this.dataGridTextBoxColumn3.FormatInfo = null;
      this.dataGridTextBoxColumn3.NullText = "";
      this.dataGridTextBoxColumn3.Width = 75;
      // 
      // dataGridTextBoxColumn4
      // 
      this.dataGridTextBoxColumn4.Format = "";
      this.dataGridTextBoxColumn4.FormatInfo = null;
      this.dataGridTextBoxColumn4.NullText = "";
      this.dataGridTextBoxColumn4.Width = 75;
      // 
      // dataGridTextBoxColumn5
      // 
      this.dataGridTextBoxColumn5.Format = "";
      this.dataGridTextBoxColumn5.FormatInfo = null;
      this.dataGridTextBoxColumn5.NullText = "";
      this.dataGridTextBoxColumn5.Width = 75;
      // 
      // dataGrid2
      // 
      this.dataGrid2.AlternatingBackColor = System.Drawing.Color.LightGray;
      this.dataGrid2.BackColor = System.Drawing.Color.DarkGray;
      this.dataGrid2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.dataGrid2.CaptionBackColor = System.Drawing.Color.White;
      this.dataGrid2.CaptionFont = new System.Drawing.Font("Verdana", 10F);
      this.dataGrid2.CaptionForeColor = System.Drawing.Color.Navy;
      this.dataGrid2.CaptionText = "eigenvalues of the last run:";
      this.dataGrid2.DataMember = "";
      this.dataGrid2.DataSource = this.viewEigenValues;
      this.dataGrid2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.dataGrid2.ForeColor = System.Drawing.Color.Black;
      this.dataGrid2.GridLineColor = System.Drawing.Color.Black;
      this.dataGrid2.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
      this.dataGrid2.HeaderBackColor = System.Drawing.Color.Silver;
      this.dataGrid2.HeaderForeColor = System.Drawing.Color.Black;
      this.dataGrid2.LinkColor = System.Drawing.Color.Navy;
      this.dataGrid2.Location = new System.Drawing.Point(0, 238);
      this.dataGrid2.Name = "dataGrid2";
      this.dataGrid2.ParentRowsBackColor = System.Drawing.Color.White;
      this.dataGrid2.ParentRowsForeColor = System.Drawing.Color.Black;
      this.dataGrid2.PreferredColumnWidth = 200;
      this.dataGrid2.SelectionBackColor = System.Drawing.Color.Navy;
      this.dataGrid2.SelectionForeColor = System.Drawing.Color.White;
      this.dataGrid2.Size = new System.Drawing.Size(482, 176);
      this.dataGrid2.TabIndex = 4;
      this.toolTip1.SetToolTip(this.dataGrid2, "displays the list of the eigenvalues returned last");
      // 
      // viewEigenValues
      // 
      this.viewEigenValues.AllowDelete = false;
      this.viewEigenValues.AllowNew = false;
      this.viewEigenValues.Table = this.tblEigenValues;
      // 
      // tblEigenValues
      // 
      this.tblEigenValues.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn7,
            this.dataColumn8});
      this.tblEigenValues.Locale = new System.Globalization.CultureInfo("en-US");
      this.tblEigenValues.TableName = "EigenValues";
      // 
      // dataColumn7
      // 
      this.dataColumn7.AllowDBNull = false;
      this.dataColumn7.ColumnName = "RealPart";
      this.dataColumn7.DataType = typeof(double);
      this.dataColumn7.DefaultValue = 0D;
      // 
      // dataColumn8
      // 
      this.dataColumn8.AllowDBNull = false;
      this.dataColumn8.ColumnName = "ComplexPart";
      this.dataColumn8.DataType = typeof(double);
      this.dataColumn8.DefaultValue = 0D;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.cmdUnselect);
      this.panel3.Controls.Add(this.cmdSave);
      this.panel3.Controls.Add(this.cmdExport);
      this.panel3.Controls.Add(this.cmdCopy);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(370, 10);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(482, 32);
      this.panel3.TabIndex = 4;
      // 
      // cmdUnselect
      // 
      this.cmdUnselect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cmdUnselect.Location = new System.Drawing.Point(0, 5);
      this.cmdUnselect.Name = "cmdUnselect";
      this.cmdUnselect.Size = new System.Drawing.Size(75, 23);
      this.cmdUnselect.TabIndex = 8;
      this.cmdUnselect.Text = "Unselect All";
      this.cmdUnselect.Click += new System.EventHandler(this.OnUnselectClick);
      // 
      // cmdSave
      // 
      this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cmdSave.Location = new System.Drawing.Point(324, 5);
      this.cmdSave.Name = "cmdSave";
      this.cmdSave.Size = new System.Drawing.Size(75, 23);
      this.cmdSave.TabIndex = 7;
      this.cmdSave.Text = "SaveAs";
      this.toolTip1.SetToolTip(this.cmdSave, "Exports the optimized values to an SBML level 2 file");
      this.cmdSave.Click += new System.EventHandler(this.OnSaveClick);
      // 
      // cmdExport
      // 
      this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cmdExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cmdExport.Location = new System.Drawing.Point(401, 5);
      this.cmdExport.Name = "cmdExport";
      this.cmdExport.Size = new System.Drawing.Size(75, 23);
      this.cmdExport.TabIndex = 6;
      this.cmdExport.Text = "Export";
      this.toolTip1.SetToolTip(this.cmdExport, "exports the optimized values to JDesigner");
      this.cmdExport.Click += new System.EventHandler(this.OnExportClick);
      // 
      // cmdCopy
      // 
      this.cmdCopy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cmdCopy.Location = new System.Drawing.Point(192, 5);
      this.cmdCopy.Name = "cmdCopy";
      this.cmdCopy.Size = new System.Drawing.Size(75, 23);
      this.cmdCopy.TabIndex = 5;
      this.cmdCopy.Text = "<<";
      this.toolTip1.SetToolTip(this.cmdCopy, "copies the optimized values to the row of initial values");
      this.cmdCopy.Click += new System.EventHandler(this.cmdCopy_Click_1);
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(362, 10);
      this.splitter1.MinExtra = 100;
      this.splitter1.MinSize = 352;
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(8, 446);
      this.splitter1.TabIndex = 3;
      this.splitter1.TabStop = false;
      this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.panel5);
      this.panel1.Controls.Add(this.resultPanel);
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
      this.panel1.Location = new System.Drawing.Point(10, 10);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(352, 446);
      this.panel1.TabIndex = 1;
      // 
      // panel5
      // 
      this.panel5.Controls.Add(this.zedGraphControl1);
      this.panel5.Controls.Add(this.panel6);
      this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel5.Location = new System.Drawing.Point(0, 104);
      this.panel5.Name = "panel5";
      this.panel5.Size = new System.Drawing.Size(352, 286);
      this.panel5.TabIndex = 8;
      // 
      // zedGraphControl1
      // 
      this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zedGraphControl1.IsAutoScrollRange = false;
      this.zedGraphControl1.IsEnableHPan = true;
      this.zedGraphControl1.IsEnableHZoom = true;
      this.zedGraphControl1.IsEnableVPan = true;
      this.zedGraphControl1.IsEnableVZoom = true;
      this.zedGraphControl1.IsPrintFillPage = true;
      this.zedGraphControl1.IsPrintKeepAspectRatio = true;
      this.zedGraphControl1.IsScrollY2 = false;
      this.zedGraphControl1.IsShowContextMenu = true;
      this.zedGraphControl1.IsShowCopyMessage = true;
      this.zedGraphControl1.IsShowCursorValues = false;
      this.zedGraphControl1.IsShowHScrollBar = false;
      this.zedGraphControl1.IsShowPointValues = false;
      this.zedGraphControl1.IsShowVScrollBar = false;
      this.zedGraphControl1.IsZoomOnMouseCenter = false;
      this.zedGraphControl1.Location = new System.Drawing.Point(0, 32);
      this.zedGraphControl1.Name = "zedGraphControl1";
      this.zedGraphControl1.PanButtons = System.Windows.Forms.MouseButtons.Left;
      this.zedGraphControl1.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
      this.zedGraphControl1.PanModifierKeys2 = System.Windows.Forms.Keys.None;
      this.zedGraphControl1.PointDateFormat = "g";
      this.zedGraphControl1.PointValueFormat = "G";
      this.zedGraphControl1.ScrollMaxX = 0D;
      this.zedGraphControl1.ScrollMaxY = 0D;
      this.zedGraphControl1.ScrollMaxY2 = 0D;
      this.zedGraphControl1.ScrollMinX = 0D;
      this.zedGraphControl1.ScrollMinY = 0D;
      this.zedGraphControl1.ScrollMinY2 = 0D;
      this.zedGraphControl1.Size = new System.Drawing.Size(352, 254);
      this.zedGraphControl1.TabIndex = 10;
      this.zedGraphControl1.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
      this.zedGraphControl1.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
      this.zedGraphControl1.ZoomModifierKeys = System.Windows.Forms.Keys.None;
      this.zedGraphControl1.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
      this.zedGraphControl1.ZoomStepFraction = 0.1D;
      // 
      // panel6
      // 
      this.panel6.Controls.Add(this.toolBar1);
      this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel6.Location = new System.Drawing.Point(0, 0);
      this.panel6.Name = "panel6";
      this.panel6.Size = new System.Drawing.Size(352, 32);
      this.panel6.TabIndex = 9;
      // 
      // toolBar1
      // 
      this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
      this.toolBar1.AutoSize = false;
      this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarZoomIn,
            this.toolBarAuto,
            this.toolBarZoomOut,
            this.toolBarButton1,
            this.toolBarClear,
            this.toolBarGrad});
      this.toolBar1.DropDownArrows = true;
      this.toolBar1.ImageList = this.imageList1;
      this.toolBar1.Location = new System.Drawing.Point(0, 0);
      this.toolBar1.Name = "toolBar1";
      this.toolBar1.ShowToolTips = true;
      this.toolBar1.Size = new System.Drawing.Size(352, 38);
      this.toolBar1.TabIndex = 0;
      this.toolBar1.TextAlign = System.Windows.Forms.ToolBarTextAlign.Right;
      this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
      // 
      // toolBarZoomIn
      // 
      this.toolBarZoomIn.ImageIndex = 1;
      this.toolBarZoomIn.Name = "toolBarZoomIn";
      this.toolBarZoomIn.Tag = "+";
      this.toolBarZoomIn.ToolTipText = "Increase Y Axis scale";
      // 
      // toolBarAuto
      // 
      this.toolBarAuto.ImageIndex = 4;
      this.toolBarAuto.Name = "toolBarAuto";
      this.toolBarAuto.Tag = "[]";
      this.toolBarAuto.ToolTipText = "compute axis scale automatically";
      // 
      // toolBarZoomOut
      // 
      this.toolBarZoomOut.ImageIndex = 0;
      this.toolBarZoomOut.Name = "toolBarZoomOut";
      this.toolBarZoomOut.Tag = "-";
      this.toolBarZoomOut.ToolTipText = "Decrease Y Axis scale";
      // 
      // toolBarButton1
      // 
      this.toolBarButton1.Name = "toolBarButton1";
      this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
      // 
      // toolBarClear
      // 
      this.toolBarClear.ImageIndex = 3;
      this.toolBarClear.Name = "toolBarClear";
      this.toolBarClear.Tag = "clear";
      this.toolBarClear.ToolTipText = "Clear the graph";
      // 
      // toolBarGrad
      // 
      this.toolBarGrad.ImageIndex = 2;
      this.toolBarGrad.Name = "toolBarGrad";
      this.toolBarGrad.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
      this.toolBarGrad.Tag = "on/off";
      this.toolBarGrad.ToolTipText = "switch on or off gradient";
      // 
      // imageList1
      // 
      this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
      this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "arrow-down.png");
      this.imageList1.Images.SetKeyName(1, "arrow-up.png");
      this.imageList1.Images.SetKeyName(2, "configure.png");
      this.imageList1.Images.SetKeyName(3, "edit-clear.png");
      this.imageList1.Images.SetKeyName(4, "transform-move.png");
      // 
      // resultPanel
      // 
      this.resultPanel.Controls.Add(this.label5);
      this.resultPanel.Controls.Add(this.txtSimulations);
      this.resultPanel.Controls.Add(this.txtIteration);
      this.resultPanel.Controls.Add(this.label4);
      this.resultPanel.Controls.Add(this.txtFitness);
      this.resultPanel.Controls.Add(this.txtEigenValue);
      this.resultPanel.Controls.Add(this.label1);
      this.resultPanel.Controls.Add(this.label2);
      this.resultPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.resultPanel.Location = new System.Drawing.Point(0, 390);
      this.resultPanel.Name = "resultPanel";
      this.resultPanel.Size = new System.Drawing.Size(352, 56);
      this.resultPanel.TabIndex = 7;
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(280, 8);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(72, 23);
      this.label5.TabIndex = 11;
      this.label5.Text = "# Simulations";
      // 
      // txtSimulations
      // 
      this.txtSimulations.Location = new System.Drawing.Point(288, 32);
      this.txtSimulations.Name = "txtSimulations";
      this.txtSimulations.ReadOnly = true;
      this.txtSimulations.Size = new System.Drawing.Size(56, 20);
      this.txtSimulations.TabIndex = 10;
      this.txtSimulations.TabStop = false;
      this.txtSimulations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // txtIteration
      // 
      this.txtIteration.Location = new System.Drawing.Point(224, 32);
      this.txtIteration.Name = "txtIteration";
      this.txtIteration.ReadOnly = true;
      this.txtIteration.Size = new System.Drawing.Size(56, 20);
      this.txtIteration.TabIndex = 9;
      this.txtIteration.TabStop = false;
      this.txtIteration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(224, 8);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(56, 23);
      this.label4.TabIndex = 7;
      this.label4.Text = "Iteration";
      // 
      // txtFitness
      // 
      this.txtFitness.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtFitness.Location = new System.Drawing.Point(8, 32);
      this.txtFitness.Name = "txtFitness";
      this.txtFitness.ReadOnly = true;
      this.txtFitness.Size = new System.Drawing.Size(80, 20);
      this.txtFitness.TabIndex = 7;
      this.txtFitness.TabStop = false;
      this.txtFitness.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txtFitness, "displays the current fitness value");
      // 
      // txtEigenValue
      // 
      this.txtEigenValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtEigenValue.Location = new System.Drawing.Point(96, 32);
      this.txtEigenValue.Name = "txtEigenValue";
      this.txtEigenValue.ReadOnly = true;
      this.txtEigenValue.Size = new System.Drawing.Size(120, 20);
      this.txtEigenValue.TabIndex = 8;
      this.txtEigenValue.TabStop = false;
      this.txtEigenValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.toolTip1.SetToolTip(this.txtEigenValue, "displays the lowest eigenvalue found");
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.Location = new System.Drawing.Point(8, 8);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(80, 23);
      this.label1.TabIndex = 5;
      this.label1.Text = "Fitness value";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.Location = new System.Drawing.Point(96, 8);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(136, 23);
      this.label2.TabIndex = 6;
      this.label2.Text = "Current lowest eigenvalue";
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.groupBox1);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(352, 104);
      this.panel2.TabIndex = 1;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cmdSteadyState);
      this.groupBox1.Controls.Add(this.cmdCancel);
      this.groupBox1.Controls.Add(this.m_oCmdLoad);
      this.groupBox1.Controls.Add(this.cmdStop);
      this.groupBox1.Controls.Add(this.m_oCmdStart);
      this.groupBox1.Controls.Add(this.cmdConfigure);
      this.groupBox1.Controls.Add(this.oMode);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
      this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(352, 100);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Discover";
      // 
      // cmdSteadyState
      // 
      this.cmdSteadyState.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdSteadyState.Location = new System.Drawing.Point(280, 24);
      this.cmdSteadyState.Name = "cmdSteadyState";
      this.cmdSteadyState.Size = new System.Drawing.Size(56, 23);
      this.cmdSteadyState.TabIndex = 6;
      this.cmdSteadyState.Text = "Steady";
      this.toolTip1.SetToolTip(this.cmdSteadyState, "Compute Steady State and update Values");
      this.cmdSteadyState.Click += new System.EventHandler(this.cmdSteadyState_Click);
      // 
      // cmdCancel
      // 
      this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdCancel.Location = new System.Drawing.Point(256, 56);
      this.cmdCancel.Name = "cmdCancel";
      this.cmdCancel.Size = new System.Drawing.Size(80, 23);
      this.cmdCancel.TabIndex = 5;
      this.cmdCancel.Text = "Kill";
      this.toolTip1.SetToolTip(this.cmdCancel, "If a simulation is running this button cancels the Simulation");
      this.cmdCancel.Click += new System.EventHandler(this.OnKillClick);
      // 
      // m_oCmdLoad
      // 
      this.m_oCmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.m_oCmdLoad.Location = new System.Drawing.Point(16, 56);
      this.m_oCmdLoad.Name = "m_oCmdLoad";
      this.m_oCmdLoad.Size = new System.Drawing.Size(64, 23);
      this.m_oCmdLoad.TabIndex = 2;
      this.m_oCmdLoad.Text = "Load";
      this.toolTip1.SetToolTip(this.m_oCmdLoad, "load a new sbml file to be analyzed");
      this.m_oCmdLoad.Click += new System.EventHandler(this.OnLoadClick);
      // 
      // cmdStop
      // 
      this.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdStop.Location = new System.Drawing.Point(170, 56);
      this.cmdStop.Name = "cmdStop";
      this.cmdStop.Size = new System.Drawing.Size(80, 24);
      this.cmdStop.TabIndex = 4;
      this.cmdStop.Text = "Stop";
      this.toolTip1.SetToolTip(this.cmdStop, "stop the current operation");
      this.cmdStop.Click += new System.EventHandler(this.OnStopClicked);
      // 
      // m_oCmdStart
      // 
      this.m_oCmdStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.m_oCmdStart.Location = new System.Drawing.Point(85, 56);
      this.m_oCmdStart.Name = "m_oCmdStart";
      this.m_oCmdStart.Size = new System.Drawing.Size(80, 24);
      this.m_oCmdStart.TabIndex = 3;
      this.m_oCmdStart.Text = "Start";
      this.toolTip1.SetToolTip(this.m_oCmdStart, "start the discovery");
      this.m_oCmdStart.Click += new System.EventHandler(this.OnStartClicked);
      // 
      // cmdConfigure
      // 
      this.cmdConfigure.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdConfigure.Location = new System.Drawing.Point(192, 24);
      this.cmdConfigure.Name = "cmdConfigure";
      this.cmdConfigure.Size = new System.Drawing.Size(80, 24);
      this.cmdConfigure.TabIndex = 1;
      this.cmdConfigure.Text = "Configure";
      this.toolTip1.SetToolTip(this.cmdConfigure, "configure the selected method");
      this.cmdConfigure.Click += new System.EventHandler(this.OnConfigureClick);
      // 
      // oMode
      // 
      this.oMode.Items.AddRange(new object[] {
            "Oscillation",
            "Turning Point"});
      this.oMode.Location = new System.Drawing.Point(16, 24);
      this.oMode.Name = "oMode";
      this.oMode.Size = new System.Drawing.Size(168, 21);
      this.oMode.TabIndex = 0;
      this.oMode.SelectedIndexChanged += new System.EventHandler(this.oMode_SelectedIndexChanged);
      // 
      // plotMenu
      // 
      this.plotMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
      // 
      // menuItem1
      // 
      this.menuItem1.Index = 0;
      this.menuItem1.Text = "Clear Graph";
      this.menuItem1.Click += new System.EventHandler(this.menuItem2_Click);
      // 
      // tabPage3
      // 
      this.tabPage3.Controls.Add(this.groupBox2);
      this.tabPage3.Controls.Add(this.dataGrid3);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Size = new System.Drawing.Size(854, 440);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Test parameters for Switch";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label3);
      this.groupBox2.Controls.Add(this.comboSpecies);
      this.groupBox2.Controls.Add(this.m_oCmdStartTest);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox2.Location = new System.Drawing.Point(0, 384);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(854, 56);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = " Start the Test: ";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(8, 24);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(192, 23);
      this.label3.TabIndex = 2;
      this.label3.Text = "Select Species to test parameter for:";
      // 
      // comboSpecies
      // 
      this.comboSpecies.Location = new System.Drawing.Point(208, 16);
      this.comboSpecies.Name = "comboSpecies";
      this.comboSpecies.Size = new System.Drawing.Size(121, 21);
      this.comboSpecies.TabIndex = 1;
      this.toolTip1.SetToolTip(this.comboSpecies, "select the species in regard to which the selected parameter(s) is(are) (a) switc" +
        "h(es)");
      // 
      // m_oCmdStartTest
      // 
      this.m_oCmdStartTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.m_oCmdStartTest.Location = new System.Drawing.Point(360, 16);
      this.m_oCmdStartTest.Name = "m_oCmdStartTest";
      this.m_oCmdStartTest.Size = new System.Drawing.Size(75, 23);
      this.m_oCmdStartTest.TabIndex = 0;
      this.m_oCmdStartTest.Text = "Start";
      this.toolTip1.SetToolTip(this.m_oCmdStartTest, "start looking for a switch");
      this.m_oCmdStartTest.Click += new System.EventHandler(this.OnStartTest);
      // 
      // dataGrid3
      // 
      this.dataGrid3.AlternatingBackColor = System.Drawing.Color.LightGray;
      this.dataGrid3.BackColor = System.Drawing.Color.DarkGray;
      this.dataGrid3.CaptionBackColor = System.Drawing.Color.White;
      this.dataGrid3.CaptionFont = new System.Drawing.Font("Verdana", 10F);
      this.dataGrid3.CaptionForeColor = System.Drawing.Color.Navy;
      this.dataGrid3.CaptionText = "Select the Parameters to be tested:";
      this.dataGrid3.DataMember = "";
      this.dataGrid3.DataSource = this.viewSwitch;
      this.dataGrid3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGrid3.ForeColor = System.Drawing.Color.Black;
      this.dataGrid3.GridLineColor = System.Drawing.Color.Black;
      this.dataGrid3.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
      this.dataGrid3.HeaderBackColor = System.Drawing.Color.Silver;
      this.dataGrid3.HeaderForeColor = System.Drawing.Color.Black;
      this.dataGrid3.LinkColor = System.Drawing.Color.Navy;
      this.dataGrid3.Location = new System.Drawing.Point(0, 0);
      this.dataGrid3.Name = "dataGrid3";
      this.dataGrid3.ParentRowsBackColor = System.Drawing.Color.White;
      this.dataGrid3.ParentRowsForeColor = System.Drawing.Color.Black;
      this.dataGrid3.SelectionBackColor = System.Drawing.Color.Navy;
      this.dataGrid3.SelectionForeColor = System.Drawing.Color.White;
      this.dataGrid3.Size = new System.Drawing.Size(854, 440);
      this.dataGrid3.TabIndex = 0;
      this.dataGrid3.Validated += new System.EventHandler(this.dataGrid3_Validated);
      // 
      // viewSwitch
      // 
      this.viewSwitch.AllowDelete = false;
      this.viewSwitch.AllowNew = false;
      this.viewSwitch.Table = this.tblSwitch;
      // 
      // tblSwitch
      // 
      this.tblSwitch.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn9,
            this.dataColumn10,
            this.dataColumn11});
      this.tblSwitch.Locale = new System.Globalization.CultureInfo("en-US");
      this.tblSwitch.TableName = "SwitchParameter";
      // 
      // dataColumn9
      // 
      this.dataColumn9.AllowDBNull = false;
      this.dataColumn9.ColumnName = "Parametername";
      this.dataColumn9.DefaultValue = "";
      this.dataColumn9.ReadOnly = true;
      // 
      // dataColumn10
      // 
      this.dataColumn10.ColumnName = "Test for Switch";
      this.dataColumn10.DataType = typeof(bool);
      this.dataColumn10.DefaultValue = true;
      // 
      // dataColumn11
      // 
      this.dataColumn11.ColumnName = "is switch";
      this.dataColumn11.DefaultValue = "";
      this.dataColumn11.ReadOnly = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Controls.Add(this.groupBox3);
      this.tabPage2.Controls.Add(this.dataGrid4);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Size = new System.Drawing.Size(854, 440);
      this.tabPage2.TabIndex = 3;
      this.tabPage2.Text = "Test parameters for Oscillator";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.cmdTestOscillator);
      this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.groupBox3.Location = new System.Drawing.Point(0, 392);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(854, 48);
      this.groupBox3.TabIndex = 1;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = " Start the Test: ";
      // 
      // cmdTestOscillator
      // 
      this.cmdTestOscillator.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cmdTestOscillator.Location = new System.Drawing.Point(8, 16);
      this.cmdTestOscillator.Name = "cmdTestOscillator";
      this.cmdTestOscillator.Size = new System.Drawing.Size(75, 23);
      this.cmdTestOscillator.TabIndex = 0;
      this.cmdTestOscillator.Text = "Start";
      this.toolTip1.SetToolTip(this.cmdTestOscillator, "start looking for a switch");
      this.cmdTestOscillator.Click += new System.EventHandler(this.cmdTestOscillator_Click);
      // 
      // dataGrid4
      // 
      this.dataGrid4.AlternatingBackColor = System.Drawing.Color.LightGray;
      this.dataGrid4.BackColor = System.Drawing.Color.DarkGray;
      this.dataGrid4.CaptionBackColor = System.Drawing.Color.White;
      this.dataGrid4.CaptionFont = new System.Drawing.Font("Verdana", 10F);
      this.dataGrid4.CaptionForeColor = System.Drawing.Color.Navy;
      this.dataGrid4.CaptionText = "Select the Parameters to be tested:";
      this.dataGrid4.DataMember = "";
      this.dataGrid4.DataSource = this.viewOscillator;
      this.dataGrid4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGrid4.ForeColor = System.Drawing.Color.Black;
      this.dataGrid4.GridLineColor = System.Drawing.Color.Black;
      this.dataGrid4.GridLineStyle = System.Windows.Forms.DataGridLineStyle.None;
      this.dataGrid4.HeaderBackColor = System.Drawing.Color.Silver;
      this.dataGrid4.HeaderForeColor = System.Drawing.Color.Black;
      this.dataGrid4.LinkColor = System.Drawing.Color.Navy;
      this.dataGrid4.Location = new System.Drawing.Point(0, 0);
      this.dataGrid4.Name = "dataGrid4";
      this.dataGrid4.ParentRowsBackColor = System.Drawing.Color.White;
      this.dataGrid4.ParentRowsForeColor = System.Drawing.Color.Black;
      this.dataGrid4.SelectionBackColor = System.Drawing.Color.Navy;
      this.dataGrid4.SelectionForeColor = System.Drawing.Color.White;
      this.dataGrid4.Size = new System.Drawing.Size(854, 440);
      this.dataGrid4.TabIndex = 0;
      this.dataGrid4.Validated += new System.EventHandler(this.dataGrid4_Validated);
      // 
      // viewOscillator
      // 
      this.viewOscillator.AllowDelete = false;
      this.viewOscillator.AllowNew = false;
      this.viewOscillator.Table = this.tblOscillator;
      // 
      // tblOscillator
      // 
      this.tblOscillator.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn12,
            this.dataColumn13,
            this.dataColumn14});
      this.tblOscillator.TableName = "OscillatorParameter";
      // 
      // dataColumn12
      // 
      this.dataColumn12.ColumnName = "Parametername";
      this.dataColumn12.DefaultValue = "";
      this.dataColumn12.ReadOnly = true;
      // 
      // dataColumn13
      // 
      this.dataColumn13.ColumnName = "Test for Hopf";
      this.dataColumn13.DataType = typeof(bool);
      this.dataColumn13.DefaultValue = false;
      // 
      // dataColumn14
      // 
      this.dataColumn14.ColumnName = "is Hopf?";
      this.dataColumn14.DefaultValue = "undecided";
      this.dataColumn14.ReadOnly = true;
      // 
      // m_oParameterDataSet
      // 
      this.m_oParameterDataSet.DataSetName = "parameter data set";
      this.m_oParameterDataSet.Locale = new System.Globalization.CultureInfo("en-US");
      this.m_oParameterDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.tblParameters,
            this.tblEigenValues,
            this.tblSwitch,
            this.tblOscillator});
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.DefaultExt = "xml";
      this.openFileDialog1.Filter = "SBML files | *.xml|All files|*.*";
      this.openFileDialog1.Title = "choose model to load";
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.Filter = "SBML files|*.xml|All files|*.*";
      // 
      // mainMenu1
      // 
      this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3,
            this.mnuSBW,
            this.menuItem5});
      // 
      // menuItem3
      // 
      this.menuItem3.Index = 0;
      this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuOpen,
            this.mnuSaveAs,
            this.mnuExport,
            this.menuItem4,
            this.mnuImport,
            this.mnuSBWExport,
            this.menuItem10,
            this.mnuExit});
      this.menuItem3.Text = "&File";
      // 
      // mnuOpen
      // 
      this.mnuOpen.Index = 0;
      this.mnuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
      this.mnuOpen.Text = "&Open";
      this.mnuOpen.Click += new System.EventHandler(this.OnLoadClick);
      // 
      // mnuSaveAs
      // 
      this.mnuSaveAs.Index = 1;
      this.mnuSaveAs.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftS;
      this.mnuSaveAs.Text = "Save &As";
      this.mnuSaveAs.Click += new System.EventHandler(this.OnSaveClick);
      // 
      // mnuExport
      // 
      this.mnuExport.Index = 2;
      this.mnuExport.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
      this.mnuExport.Text = "&Export";
      this.mnuExport.Click += new System.EventHandler(this.OnExportClick);
      // 
      // menuItem10
      // 
      this.menuItem10.Index = 6;
      this.menuItem10.Text = "-";
      // 
      // mnuExit
      // 
      this.mnuExit.Index = 7;
      this.mnuExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
      this.mnuExit.Text = "E&xit";
      this.mnuExit.Click += new System.EventHandler(this.BifurcationForm_Closed);
      // 
      // mnuSBW
      // 
      this.mnuSBW.Index = 1;
      this.mnuSBW.Text = "&SBW";
      // 
      // menuItem5
      // 
      this.menuItem5.Index = 2;
      this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6});
      this.menuItem5.Text = "&Help";
      // 
      // menuItem6
      // 
      this.menuItem6.Index = 0;
      this.menuItem6.Shortcut = System.Windows.Forms.Shortcut.F1;
      this.menuItem6.Text = "&About";
      this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
      // 
      // menuItem4
      // 
      this.menuItem4.Index = 3;
      this.menuItem4.Text = "-";
      // 
      // mnuImport
      // 
      this.mnuImport.Index = 4;
      this.mnuImport.Text = "SBW Import";
      // 
      // mnuSBWExport
      // 
      this.mnuSBWExport.Index = 5;
      this.mnuSBWExport.Text = "SBW Export";
      // 
      // BifurcationForm
      // 
      this.AllowDrop = true;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(862, 466);
      this.Controls.Add(this.mainContent);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Menu = this.mainMenu1;
      this.MinimumSize = new System.Drawing.Size(864, 500);
      this.Name = "BifurcationForm";
      this.Text = "Bifurcation Discovery Tool";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BifurcationForm_Closed);
      this.Load += new System.EventHandler(this.BifurcationForm_Load);
      this.DragDrop += new System.Windows.Forms.DragEventHandler(this.BifurcationForm_DragDrop);
      this.DragEnter += new System.Windows.Forms.DragEventHandler(this.BifurcationForm_DragEnter);
      this.tabControl1.ResumeLayout(false);
      this.mainContent.ResumeLayout(false);
      this.panel4.ResumeLayout(false);
      this.panel7.ResumeLayout(false);
      this.panel8.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewParameters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblParameters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewEigenValues)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblEigenValues)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel5.ResumeLayout(false);
      this.panel6.ResumeLayout(false);
      this.resultPanel.ResumeLayout(false);
      this.resultPanel.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid3)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewSwitch)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblSwitch)).EndInit();
      this.tabPage2.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid4)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.viewOscillator)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tblOscillator)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.m_oParameterDataSet)).EndInit();
      this.ResumeLayout(false);

        }

        private ZedGraph.ZedGraphControl zedGraphControl1;
        private MenuItem menuItem4;
        private MenuItem mnuImport;
        private MenuItem mnuSBWExport;

    }
}
