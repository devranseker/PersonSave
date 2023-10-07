using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Sql kutuphane unutma 
using System.Data.SqlClient;


namespace Person_save
{
    public partial class FrmGraphics : Form
    {
        public FrmGraphics()
        {
            InitializeComponent();
        }
        // Global alan 

        // Sql Sınıfdan bir tane nesne turetmem gerekiyor ki kullanabilmem lazım 
        SqlConnection baglanti = new SqlConnection("Data Source=DEVRAN-PC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        private void FrmGraphics_Load(object sender, EventArgs e)
        {
            //sehirler grafigi  Citys graphic 
            baglanti.Open();
            SqlCommand komutgrfcity = new SqlCommand("Select PersonCity, Count(*) From Tbl_NewPerson Group By PersonCity ", baglanti);
            // veriyi okusun 
            SqlDataReader dr1 = komutgrfcity.ExecuteReader();
            // grafik_okuyucu okudugu muddetce 
            while (dr1.Read())
            {
                // X koordinatı[0], Y kooardinatı[1]  
                chart1.Series["Sehirler"].Points.AddXY(dr1[0], dr1[1]);

            }
            baglanti.Close();

            // maas grafigi Salary graphic
            baglanti.Open();
            SqlCommand komutgrfSalary  = new SqlCommand("Select PersonJob, Avg(PersonSalary) From Tbl_NewPerson Group By PersonJob ", baglanti);
            // veriyi okusun 
            SqlDataReader salaryGrafik_okuyucu = komutgrfSalary.ExecuteReader();
            // veri okudugu muddetce 
            while (salaryGrafik_okuyucu.Read())
            {
                chart2.Series["Meslek-Maas"].Points.AddXY(salaryGrafik_okuyucu[0], salaryGrafik_okuyucu[1]);
            } // ["Meslek - Maas"]  burası cok onemli okumaz tek bir karakter hata cıkarsa 

            baglanti.Close();




        }
    }
}
