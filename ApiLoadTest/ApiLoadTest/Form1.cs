using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ApiLoadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = fdlg.FileName;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string path = @"E:\test_data_generator\ApiLoadTest\SaveData.xml";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

           
           
            SaveData test = new SaveData();
            test.Path = textBox6.Text;
            test.HeaderList = textBox5.Text;
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                Headers newHeader = new Headers();
                Request newRequest = new Request();

                newRequest.Url =(string)row.Cells[0].Value;
                newRequest.MethodType = (string)row.Cells[1].Value;
                newRequest.Payload = (string)row.Cells[2].Value;

                newHeader.Name = (string)row.Cells[3].Value;
                newHeader.Value = (string)row.Cells[4].Value;
                
                newRequest.Headers.Add(newHeader);

                test.RequestList.Add(newRequest);
                 
            }



           

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                Headers commonHeader = new Headers();
                commonHeader.Name = (string)row.Cells[0].Value;
                commonHeader.Value = (string)row.Cells[1].Value;
                test.CommonHeaders.Add(commonHeader);

            }
           
            SaveXML.Savedata(test, path);
            //writing new values to XML
           
            MessageBox.Show("Successfully added ", "Success");
            //this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
