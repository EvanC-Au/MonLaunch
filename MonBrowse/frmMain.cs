using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace MonBrowse
{
    public partial class frmMain : Form
    {
        public Rectangle ScrWA;
        public string SiteURL;

        public frmMain(Rectangle xScrWA, string xSiteURL)
        {
            ScrWA = xScrWA;
            SiteURL = xSiteURL;

            InitializeComponent();


        }



        private void frmMain_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(SiteURL);
       
            base.Location = new Point(ScrWA.Left, ScrWA.Top); // As form loads, set location 
            base.WindowState = FormWindowState.Maximized;
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            base.Text = webBrowser1.DocumentTitle + " | " + webBrowser1.Url.ToString();
        }
    }
}
