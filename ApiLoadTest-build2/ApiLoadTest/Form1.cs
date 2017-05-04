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
using ApiLoadTest.ApiLoad;

namespace ApiLoadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void Try_Click(object sender, EventArgs e)
        {
            LoadTest test = new LoadTest();
            test.ApiTest();

        }

        private void Save_Click(object sender, EventArgs e)
        {


            string path = @"E:\test_data_generator\ApiLoadTest\SaveData.xml";

            SaveData data = new SaveData();
            data.Path = textBox6.Text;
            data.HeaderList = textBox5.Text;

            Headers newHeader = new Headers();
            Request newRequest = new Request();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    data.RequestList.Add(newRequest);
                    newRequest = new Request();
                    newHeader = new Headers();
                    newRequest.Url = (string)row.Cells[0].Value;
                    newRequest.MethodType = (string)row.Cells[1].Value;
                    newRequest.Payload = (string)row.Cells[2].Value;

                    newHeader.Name = (string)row.Cells[3].Value;
                    newHeader.Value = (string)row.Cells[4].Value;

                    
                   
                }
                else
                {
                    newHeader = new Headers();
                    newHeader.Name = (string)row.Cells[3].Value;
                    newHeader.Value = (string)row.Cells[4].Value;

                    
                }

                newRequest.Headers.Add(newHeader);
            }

            data.RequestList.Add(newRequest);

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
               
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    Headers commonHeader = new Headers();
                    commonHeader.Name = (string)row.Cells[0].Value;
                    commonHeader.Value = (string)row.Cells[1].Value;
                    data.CommonHeaders.Add(commonHeader);
                }
                

            }

            SaveXML.Savedata(data, path);
            //writing new values to XML

            MessageBox.Show("Successfully added ", "Success");
            //this.Close();
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
        }




        private void Load_Click(object sender, EventArgs e)
        {
            string path = @"E:\test_data_generator\ApiLoadTest\SaveData.xml";
            SaveData data = new SaveData();

            SaveXML.Getdata(ref data, path);

            textBox6.Text = data.Path;
            textBox5.Text = data.HeaderList;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            foreach (Headers commonHeaders in data.CommonHeaders)
            {

                dataGridView2.Rows.Add(commonHeaders.Value, commonHeaders.Value);

            }


            foreach (Request request in data.RequestList)
            {
                List<Headers> headers = request.Headers;
                if (request.Url != null && request.MethodType != null && request.Payload != null)
                    dataGridView1.Rows.Add(request.Url, request.MethodType, request.Payload, headers[0].Name, headers[0].Value);
                else
                {
                    bool i = false;
                    foreach (Headers header in headers)
                    {
                        if (i == false)
                        {
                            i = true;
                            continue;
                        }
                        dataGridView1.Rows.Add(request.Url, request.MethodType, request.Payload, header.Name, header.Value);
                    }
                }


            }

        }
    }//Class
}//Namespace
