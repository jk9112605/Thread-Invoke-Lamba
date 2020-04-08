using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppThreadInvokeLambda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Form frm2;
        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                for(int i= 0; i<10; i++)
                {
                    Invoke(new Action(()=>
                    {
                        Text = i.ToString();
                    }));
                    Thread.Sleep(1000);
                }
            }).Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm2 = new Form();
            Button btnOk = new Button() { Enabled = false, Text = "OK" };
            btnOk.Click += BtnOk_Click;
            
            frm2.Controls.Add(btnOk);

            Thread trd = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Invoke(new Action(() =>
                    {
                        Text = i.ToString();
                        frm2.Text = i.ToString();
                        
                    }));
                    Thread.Sleep(1000);
                }
                //complete
                Invoke(new Action(()=> {
                    btnOk.Enabled = true;
                }));
                
            });
            trd.Start();
            frm2.ShowDialog();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            frm2.Close();
        }
    }
}
