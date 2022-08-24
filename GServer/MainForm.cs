using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GServer
{
    public partial class MainForm : Form
    {
        private Main m_main;
        public MainForm()
        {
            InitializeComponent();
            m_main = new Main();
            
            m_main.Initialize();
        }
    }
}
