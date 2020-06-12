using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Export_3d_views
{
    public partial class Select_Settings : Form
    {
        
        public Select_Settings()
        {
            InitializeComponent();
        }

        public string rdoChecked { get; set; }
        public string LrdoChecked { get; set; }

        public string MChecked(string coordinate)
        {
            if (coordinate == "internal")
            {
                rdoChecked = "internal";
                //MessageBox.Show(rdoChecked);
                return rdoChecked;
            }
            else
            {
                rdoChecked = "shared";
                //MessageBox.Show(rdoChecked);
                return rdoChecked;
            }
        }

        public string LChecked(string links)
        {
            if (links == "yes")
            {
                LrdoChecked = "yes";
                //MessageBox.Show(rdoChecked);
                return LrdoChecked;
            }
            else
            {
                LrdoChecked = "no";
                //MessageBox.Show(rdoChecked);
                return LrdoChecked;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                MChecked("internal");
            }
            else if (radioButton2.Checked)
            {
                MChecked("shared");
            }

            if (radioButton3.Checked)
            {
                LChecked("yes");
            }
            else if (radioButton4.Checked)
            {
                LChecked("no");
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                //MessageBox.Show("Shared coordnates selected.");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                //MessageBox.Show("Internal coordnates selected.");
            }
        }


        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                //MessageBox.Show("Linked Files Exporting.");
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                //MessageBox.Show("Linked Files not Exporting.");
            }
        }

        private void Select_Coordinates_Load(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
