using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// sql kutuphane unutma 
using System.Data.SqlClient;

namespace Person_save
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        // global alan

        // Sql Sınıfdan bir tane nesne turetmem gerekiyor ki kullanabilmem lazım 
        SqlConnection baglanti = new SqlConnection("Data Source=DEVRAN-PC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            // mantık olarak username ile db veriyi okut n.satırdaki 1.veri ile 2. veri esit ise her ikiside yani and o zaman giris yap  
            SqlCommand komut = new SqlCommand("Select * From Tbl_Admin where Username = @p1 and Password = @p2", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtUsername.Text);
            komut.Parameters.AddWithValue("@p2", TxtPassword.Text);
            SqlDataReader dr = komut.ExecuteReader(); // verileri oku 

            // dikkar if var 
            if (dr.Read())
            {
                FrmAnaForm frm = new FrmAnaForm();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or password incorrect");
            }
            baglanti.Close();
           



        }
    }
}
