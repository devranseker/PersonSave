using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Person_save
{
    public partial class Frmİstatistik : Form
    {
        public Frmİstatistik()
        {
            InitializeComponent();
        }
        // Global alan 
        //Sql'i baglandıralım 
        // Sql Sınıfdan bir tane nesne turetmem gerekiyor ki kullanabilmem lazım 

        SqlConnection baglanti = new SqlConnection("Data Source=DEVRAN-PC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");


        /*
         SqlDataReader'ın kullanımı genellikle aşağıdaki gibi bir işlem sırasını izler:

Veritabanı bağlantısı oluşturulur (SqlConnection kullanılarak).
SQL sorgusu hazırlanır (SqlCommand kullanılarak).
SQL sorgusu çalıştırılır ve SqlDataReader ile sonuçlar alınır (ExecuteReader kullanılarak).
SqlDataReader kullanılarak veriler satır satır okunur ve işlenir.
Veritabanı bağlantısı kapatılır.
         */


        // Total personel sayısı  
        private void Frmİstatistik_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("Select Count(*) From Tbl_NewPerson ", baglanti);
            SqlDataReader veri_okuyucu1 = komut1.ExecuteReader();    // komut1'deki veriyi oku 

            // veri_okuyucu1 veri okudugu muddetce 
            while (veri_okuyucu1.Read())
            {
                LblPersonelSayisi.Text = veri_okuyucu1[0].ToString();

            }
            baglanti.Close();



            //evli personel sayısı (number of married staff )
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select Count(*) From Tbl_NewPerson where PersonDurum = 1 ", baglanti);
            SqlDataReader veri_okuyucu2 = komut2.ExecuteReader();

            // veri_okuyucu2 veri okudugu muddetce 
            while (veri_okuyucu2.Read())
            {
                // veriokuyucu2'den gelen degeri yazdıralım 
                LblEvliPersonSayisi.Text = veri_okuyucu2[0].ToString();

            }
            baglanti.Close();





            //Bekar personel sayisi 
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand(" Select Count(*) From Tbl_NewPerson where PersonDurum = 0 ", baglanti);
            // sonra saydıgı degerleri okutalım 
            SqlDataReader veri_okuyucu3 = komut3.ExecuteReader();   // calıstırdık

            // veri_okuyucu3 veri okudugu muddetce
            while (veri_okuyucu3.Read())
            {
                LblBekarPersonSayisi.Text = veri_okuyucu3[0].ToString();
            }
            baglanti.Close();





            // sehir sayısı 
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("Select count(distinct(PersonCity))  From Tbl_NewPerson", baglanti);
            SqlDataReader veri_okuyucu4 = komut4.ExecuteReader();

            while (veri_okuyucu4.Read())
            {
                LblSehirSayisi.Text = veri_okuyucu4[0].ToString();

            }
            baglanti.Close();



            // Toplam maas 
            baglanti.Open();
            SqlCommand komut5 = new SqlCommand("Select Sum(PersonSalary) From Tbl_NewPerson", baglanti);
            SqlDataReader veri_okuyucu5 = komut5.ExecuteReader();

            // veri_okuyucu5 okudugu muddetce
            while (veri_okuyucu5.Read())
            {
                LblMaas.Text = veri_okuyucu5[0].ToString();

            }
            baglanti.Close();


            // ortalama maas Aveage salary 
            baglanti.Open();
            SqlCommand komut6 = new SqlCommand("Select Avg(PersonSalary) From Tbl_NewPerson", baglanti);
            SqlDataReader veri_okuyucu6 = komut6.ExecuteReader();

            // veri_okuyucu6 okudugu muddetce
            while (veri_okuyucu6.Read())
            {
                Lblort_maas.Text = veri_okuyucu6[0].ToString();
            }
            baglanti.Close();



        }
    }
}
