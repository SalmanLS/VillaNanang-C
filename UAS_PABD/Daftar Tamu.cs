﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UAS_PABD
{
    
    public partial class Daftar_Tamu : Form
    {
        private string stringConnection = "data source=LUTHFI\\MCH35;" +
           "database=Villa;User ID=sa; Password=12345";
        private SqlConnection koneksi;
        private string cari,hapus;
        public Daftar_Tamu()
        {
            InitializeComponent();
            koneksi = new SqlConnection(stringConnection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Daftar_Data dd = new Daftar_Data();
            dd.Show();
            this.Hide();
        }

        private void dataGridView()
        {
            koneksi.Open();
            string str = "select*from Data_Tamu";
            SqlDataAdapter da = new SqlDataAdapter(str, koneksi);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            koneksi.Close();
        }

        private void Daftar_Tamu_Load(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            dataGridView();
            btnOpen.Enabled = false;
        }
        private void deleteData()
        {
            hapus = txtSearch.Text;
            koneksi.Open();
            string sr = "delete from Data_Tamu where id_tamu=@ds";
            SqlCommand sc = new SqlCommand(sr, koneksi);
            sc.CommandType = CommandType.Text;
            sc.Parameters.Add(new SqlParameter("ds", hapus));
            sc.ExecuteNonQuery();
            koneksi.Close();

            MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void searchData()
        {
            cari = txtSearch.Text;
            string str = "select*from Data_Tamu where id_tamu=@dd";
            SqlCommand cm = new SqlCommand(str,koneksi);
            koneksi.Open();
            cm.CommandType = CommandType.Text;
            cm.Parameters.AddWithValue("@dd",cari);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cm);
            da.Fill(ds);
            dataGridView1.DataSource= ds.Tables[0];
            koneksi.Close();
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchData();
            btnSearch.Enabled = false;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteData();
            dataGridView();
        }
    }
}
