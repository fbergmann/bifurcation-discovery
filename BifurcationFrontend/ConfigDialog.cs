using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace bifurcation
{
    /// <summary>
    ///     Summary description for ConfigDialog.
    /// </summary>
    public class ConfigDialog : Form
    {
        #region // Designer Variables

        private Button button1;
        private Button button2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;

        #endregion

        /// <summary>
        ///     Required designer variable.
        /// </summary>
        private readonly Container components = null;

        private Label label4;

        private Label label9;
        private TextBox txtSimulationTime;
        private TextBox txtUpdateDelay;

        /// <summary>
        ///     constructs a new configuration dialog
        /// </summary>
        public ConfigDialog()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }


        /// <summary>
        ///     return the configured random seed
        /// </summary>
        public int RandomNumberSeed
        {
            get { return Convert.ToInt32(textBox8.Text); }
        }


        /// <summary>
        ///     return the configured stopping tolerance
        /// </summary>
        public double StoppingTolerance
        {
            get { return Convert.ToDouble(textBox7.Text); }
        }


        /// <summary>
        ///     return the configured mutation probability
        /// </summary>
        public double MutationProbability
        {
            get { return Convert.ToDouble(textBox6.Text); }
        }


        /// <summary>
        ///     return the configured crossover probability
        /// </summary>
        public double CrossoverProbability
        {
            get { return Convert.ToDouble(textBox5.Text); }
        }

        /// <summary>
        ///     return the configured number of tournament members
        /// </summary>
        public int NumberOfTournament

        {
            get { return Convert.ToInt32(textBox3.Text); }
        }


        /// <summary>
        ///     return the configured number of members
        /// </summary>
        public int NumberOfMembers
        {
            get { return Convert.ToInt32(textBox2.Text); }
        }


        /// <summary>
        ///     return the configured number of generations
        /// </summary>
        public int NumberOfGenerations
        {
            get { return Convert.ToInt32(textBox1.Text); }
        }

        /// <summary>
        ///     return the configured simulation end time
        /// </summary>
        public double SimulationTime
        {
            get { return Convert.ToDouble(txtSimulationTime.Text); }
        }

        public int UpdateDelay
        {
            get { return Convert.ToInt32(txtUpdateDelay.Text); }
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtSimulationTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUpdateDelay = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of Generations";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(280, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "100";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(280, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "100";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Number of Members";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(280, 64);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "5";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of Tournament Members to be Selected";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(280, 88);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 9;
            this.textBox5.Text = "0.8";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Crossover Probability";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(280, 112);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 11;
            this.textBox6.Text = "0.3";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(248, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Mutation Probability";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(280, 136);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 13;
            this.textBox7.Text = "0.0001";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(248, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Stopping Tolerance";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(280, 160);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(100, 20);
            this.textBox8.TabIndex = 15;
            this.textBox8.Text = "0";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(16, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(248, 23);
            this.label8.TabIndex = 14;
            this.label8.Text = "Random Number Seed";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(304, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Cancel";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(224, 256);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(72, 24);
            this.button2.TabIndex = 17;
            this.button2.Text = "OK";
            // 
            // txtSimulationTime
            // 
            this.txtSimulationTime.Location = new System.Drawing.Point(280, 184);
            this.txtSimulationTime.Name = "txtSimulationTime";
            this.txtSimulationTime.Size = new System.Drawing.Size(100, 20);
            this.txtSimulationTime.TabIndex = 19;
            this.txtSimulationTime.Text = "100";
            this.txtSimulationTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 184);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(248, 23);
            this.label9.TabIndex = 18;
            this.label9.Text = "Simulation Time:";
            // 
            // txtUpdateDelay
            // 
            this.txtUpdateDelay.Location = new System.Drawing.Point(279, 208);
            this.txtUpdateDelay.Name = "txtUpdateDelay";
            this.txtUpdateDelay.Size = new System.Drawing.Size(100, 20);
            this.txtUpdateDelay.TabIndex = 21;
            this.txtUpdateDelay.Text = "10";
            this.txtUpdateDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(248, 23);
            this.label4.TabIndex = 20;
            this.label4.Text = "Update Delay:";
            // 
            // ConfigDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(394, 288);
            this.Controls.Add(this.txtUpdateDelay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSimulationTime);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfigDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configure Optimizer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}