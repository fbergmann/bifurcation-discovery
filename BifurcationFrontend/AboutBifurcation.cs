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
    public partial class AboutBifurcation : Form
    {
        private readonly Container components = null;
        

        public AboutBifurcation()
        {
            InitializeComponent();
            cmdClose.DialogResult = DialogResult.OK;
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

        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = "http://fbergmann.github.io/bifurcation-discovery/";
                //"http://www.sys-bio.org/";
                try
                {
                    try
                    {
                        Process.Start(url);
                    }
                    catch (Exception ex)
                    {
                        Process.Start("IExplore.exe", url);
                    }
                }
                catch (Exception ex)
                {
                    Process.Start("firefox.exe", url);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}