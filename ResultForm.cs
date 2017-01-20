using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StelthXmlFilling
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public ResultForm(String resultTxt)
        {
            InitializeComponent();
            resultTextbox.Text = resultTxt;
        }
    }
}
