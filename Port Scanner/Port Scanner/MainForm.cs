using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace Port_Scanner
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            TcpConnectionInformation[] tcpArray = ipProperties.GetActiveTcpConnections();
            
            int index = 0;

            foreach (TcpConnectionInformation info in tcpArray)
            {
                dataGridView.Rows.Add(new DataGridViewRow());
                dataGridView.Rows[index].Cells[0].Value = info.LocalEndPoint.Address;
                dataGridView.Rows[index].Cells[1].Value = info.LocalEndPoint.Port;
                dataGridView.Rows[index].Cells[2].Value = info.RemoteEndPoint.Address;
                
                if (hostNameCheckBox.Checked)
                {
                    try
                    {                        
                        dataGridView.Rows[index].Cells[3].Value = Dns.GetHostEntry(info.RemoteEndPoint.Address).HostName;
                    }
                    catch (Exception ex)
                    {
                        dataGridView.Rows[index].Cells[3].Value = "Host name could not be found";
                    }
                }
                else
                {
                    dataGridView.Rows[index].Cells[3].Value = "N/A";
                }

                index++;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            exportDialog.Filter = "CSV files (*.csv)|*.csv";
            exportDialog.ShowDialog();

            bool doContinue = true;

            while (exportDialog.FileName == String.Empty)
            {
                DialogResult reply = MessageBox.Show("You must select a file.", "Error", MessageBoxButtons.OKCancel);

                if (reply == DialogResult.OK)
                {
                    exportDialog.ShowDialog();
                }
                else
                {
                    doContinue = false;
                    break;
                }
            }

            if (doContinue)
            {
                StreamWriter writer = new StreamWriter(exportDialog.FileName);

                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int x = 0; x < dataGridView.Rows[i].Cells.Count - 1; x++)
                    {
                        writer.Write(dataGridView.Rows[i].Cells[x].Value.ToString() + ",");
                    }

                    writer.Write(dataGridView.Rows[i].Cells[dataGridView.Rows[i].Cells.Count - 1].Value.ToString());
                    writer.WriteLine();
                }

                writer.Close();
            }
        }
    }
}