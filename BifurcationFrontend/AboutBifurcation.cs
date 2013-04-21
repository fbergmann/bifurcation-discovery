// Type: bifurcation.AboutBifurcation
// Assembly: BifurcationFrontend, Version=2.0.2753.40611, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Users\fbergmann\Desktop\BifurcationFrontend.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace BifurcationFrontend
{
    public class AboutBifurcation : Form
    {
        private readonly Container components = null;
        private Button cmdClose;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label lblBuild;
        private Label lblSBWVersion;
        private Label lblVersion;
        private LinkLabel linkLabel1;
        private PictureBox pictureBox1;

        public AboutBifurcation()
        {
            InitializeComponent();
        }

        public AboutBifurcation(string sSBWVersion)
            : this()
        {
            lblSBWVersion.Text = sSBWVersion;
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            lblVersion.Text = string.Format("{0}{1}{2}", version.Major, ".", version.Minor);
            lblBuild.Text = version.Build.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            cmdClose = new Button();
            label1 = new Label();
            label2 = new Label();
            linkLabel1 = new LinkLabel();
            label4 = new Label();
            lblVersion = new Label();
            lblBuild = new Label();
            label6 = new Label();
            lblSBWVersion = new Label();
            label5 = new Label();
            label3 = new Label();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            ((ISupportInitialize) (pictureBox1)).BeginInit();
            SuspendLayout();
            // 
            // cmdClose
            // 
            cmdClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cmdClose.DialogResult = DialogResult.OK;
            cmdClose.FlatStyle = FlatStyle.System;
            cmdClose.Location = new Point(392, 296);
            cmdClose.Name = "cmdClose";
            cmdClose.Size = new Size(75, 23);
            cmdClose.TabIndex = 0;
            cmdClose.Text = "OK";
            // 
            // label1
            // 
            label1.Location = new Point(192, 32);
            label1.Name = "label1";
            label1.Size = new Size(280, 48);
            label1.TabIndex = 1;
            label1.Text = "This application represents the frontend to the SBW bifurcation discovery module." +
                " It allows the discovery of Oscillation, Turning Points and Switches for a given" +
                " SBML model";
            // 
            // label2
            // 
            label2.Location = new Point(192, 88);
            label2.Name = "label2";
            label2.Size = new Size(200, 23);
            label2.TabIndex = 2;
            label2.Text = "For more information about SBW see:";
            // 
            // linkLabel1
            // 
            linkLabel1.Location = new Point(272, 112);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(128, 23);
            linkLabel1.TabIndex = 3;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "http://www.sys-bio.org";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label4
            // 
            label4.Location = new Point(200, 144);
            label4.Name = "label4";
            label4.Size = new Size(100, 23);
            label4.TabIndex = 5;
            label4.Text = "Frontend Version:";
            // 
            // lblVersion
            // 
            lblVersion.Location = new Point(336, 144);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(136, 23);
            lblVersion.TabIndex = 6;
            // 
            // lblBuild
            // 
            lblBuild.Location = new Point(336, 168);
            lblBuild.Name = "lblBuild";
            lblBuild.Size = new Size(136, 23);
            lblBuild.TabIndex = 8;
            // 
            // label6
            // 
            label6.Location = new Point(200, 168);
            label6.Name = "label6";
            label6.Size = new Size(100, 23);
            label6.TabIndex = 7;
            label6.Text = "Build:";
            // 
            // lblSBWVersion
            // 
            lblSBWVersion.Location = new Point(336, 192);
            lblSBWVersion.Name = "lblSBWVersion";
            lblSBWVersion.Size = new Size(136, 48);
            lblSBWVersion.TabIndex = 10;
            // 
            // label5
            // 
            label5.Location = new Point(200, 192);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 9;
            label5.Text = "SBW C# Version:";
            // 
            // label3
            // 
            label3.Location = new Point(192, 256);
            label3.Name = "label3";
            label3.Size = new Size(280, 24);
            label3.TabIndex = 11;
            label3.Text = "This Frontend uses the ZedGraph graphing library. Version 3.0";
            // 
            // label7
            // 
            label7.Location = new Point(184, 296);
            label7.Name = "label7";
            label7.Size = new Size(144, 23);
            label7.TabIndex = 12;
            label7.Text = "(c) 2006 Frank Bergmann";
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(8, 8);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(168, 320);
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // AboutBifurcation
            // 
            AcceptButton = cmdClose;
            AutoScaleBaseSize = new Size(5, 13);
            BackColor = Color.White;
            ClientSize = new Size(474, 328);
            Controls.Add(pictureBox1);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(lblSBWVersion);
            Controls.Add(label5);
            Controls.Add(lblBuild);
            Controls.Add(label6);
            Controls.Add(lblVersion);
            Controls.Add(label4);
            Controls.Add(linkLabel1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmdClose);
            Name = "AboutBifurcation";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "About";
            ((ISupportInitialize) (pictureBox1)).EndInit();
            ResumeLayout(false);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                try
                {
                    try
                    {
                        Process.Start("http://www.sys-bio.org/");
                    }
                    catch (Exception ex)
                    {
                        Process.Start("IExplore.exe", "http://www.sys-bio.org/");
                    }
                }
                catch (Exception ex)
                {
                    Process.Start("firefox.exe", "http://www.sys-bio.org/");
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}