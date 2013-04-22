using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BifurcationFrontend
{
    partial class AboutBifurcation
    {
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBifurcation));
            this.cmdClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblBuild = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSBWVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdClose.Location = new System.Drawing.Point(392, 296);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(75, 23);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "OK";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(192, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = "This application represents the frontend to the SBW bifurcation discovery module." +
    " It allows the discovery of Oscillation, Turning Points and Switches for a given" +
    " SBML model";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(192, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "For more information about SBW see:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(203, 112);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(242, 23);
            this.linkLabel1.TabIndex = 3;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://fbergmann.github.io/bifurcation-discovery/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(200, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "Frontend Version:";
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(336, 144);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(136, 23);
            this.lblVersion.TabIndex = 6;
            // 
            // lblBuild
            // 
            this.lblBuild.Location = new System.Drawing.Point(336, 168);
            this.lblBuild.Name = "lblBuild";
            this.lblBuild.Size = new System.Drawing.Size(136, 23);
            this.lblBuild.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(200, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 7;
            this.label6.Text = "Build:";
            // 
            // lblSBWVersion
            // 
            this.lblSBWVersion.Location = new System.Drawing.Point(336, 192);
            this.lblSBWVersion.Name = "lblSBWVersion";
            this.lblSBWVersion.Size = new System.Drawing.Size(136, 48);
            this.lblSBWVersion.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(200, 192);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "SBW C# Version:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(192, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(280, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "This Frontend uses the ZedGraph graphing library. Version 3.0";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(184, 296);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "(c) 2013 Frank T. Bergmann";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BifurcationFrontend.Properties.Resources.About_BifTool;
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(168, 320);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // AboutBifurcation
            // 
            this.AcceptButton = this.cmdClose;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(474, 328);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSBWVersion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblBuild);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutBifurcation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
