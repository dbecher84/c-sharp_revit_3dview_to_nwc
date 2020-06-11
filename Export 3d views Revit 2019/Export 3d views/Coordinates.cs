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
    public partial class Select_Coordinates : Form
    {
        
        public Select_Coordinates()
        {
            InitializeComponent();
        }

        public string rdoChecked { get; set; }

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
    }
}
