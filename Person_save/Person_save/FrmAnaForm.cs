using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// SQL kullanmamı saglaycak kutuphane 
using System.Data.SqlClient;


namespace Person_save
{
    public partial class FrmAnaForm : Form       
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        // Global alan 
        // Sql Sınıfdan bir tane nesne turetmem gerekiyor ki kullanabilmem lazım 
        SqlConnection baglanti = new SqlConnection("Data Source=DEVRAN-PC\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");

        // Clear method()'u tanımlayalım 
        void temizle()
        {
            TxtPersonId.Text = "";
            TxtPersonName.Text = "";
            TxtPersonSurname.Text = "";
            CmbCity.Text = "";
            MskSalary.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            TxtPersonJob.Text = "";
            // imlec odaklanmasınıda PersonName goturelelim 
            TxtPersonName.Focus();



        }
        private void Form1_Load(object sender, EventArgs e)
        {   // basta buradan gelen kodları BntList'e ekledim  cunku BtnListe tıkladıgımda verileri listelesin diye 
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_NewPerson' table. You can move, or remove it, as needed.
            this.tbl_NewPersonTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_NewPerson);


        }

        private void BtnList_Click(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_NewPerson' table. You can move, or remove it, as needed.
            this.tbl_NewPersonTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_NewPerson);

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            // yapacagım islemşler fgdfghdfshbgsdfhgds  fjgdfs gjdfshgdsfgjhd
            // komut nesnesi olusturduk 
            //SqlCommand komut = new SqlCommand("insert into Tbl_NewPerson (PersonName, PersonSurname) values (@p1,@p2)" , baglanti);// suan sadece Tablodan name ve surname aldık 
           
            SqlCommand komut = new SqlCommand("insert into Tbl_NewPerson  (PersonName,PersonSurname,PersonCity,PersonSalary, PersonJob, PersonDurum) values (@p1,@p2,@p3,@p4, @p5, @p6)", baglanti);   // baglanti   unutma                                                                                                                       // sutun adları aynı olmak zorunda (Tablodaki ile) p == parametre 

            // bu kısım atama var 
            komut.Parameters.AddWithValue("@p1", TxtPersonName.Text);  
            komut.Parameters.AddWithValue("@p2", TxtPersonSurname.Text);
            // yeni verileri ekleyelim 
            komut.Parameters.AddWithValue("@p3", CmbCity.Text);
            komut.Parameters.AddWithValue("@p4", MskSalary.Text);
            komut.Parameters.AddWithValue("@p5", TxtPersonJob.Text);
            komut.Parameters.AddWithValue("@p6", label9.Text);




            komut.ExecuteNonQuery();     // ExecuteNonQuery yöntemi, veritabanına bir sorgu veya komut gönderir ve veritabanında değişiklik yapar, ancak sorgudan dönen veri kümesini dikkate almaz. Özellikle INSERT, UPDATE veya DELETE gibi veritabanı kayıtlarını değiştiren sorgular için yaygın olarak kullanılır.

            // Executenonquery == sorguyu calıstır ( Ekle-Sil-Güncelle) 
            baglanti.Close();
            MessageBox.Show("Personel Eklendi.");

        }

        //  radioButton1 == evli 
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                label9.Text = "True";

            }


        }

        //  radioButton2 == bekar 
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           if(radioButton2.Checked == true)
            {
                label9.Text = "False";

            }

        }
        //Clear butonu 
        private void BtnClear_Click(object sender, EventArgs e)
        {
            //  bir method()  olusturalım ve bu methodu her cagırdıgımızda Clear işlemi yapsın method()'u dogal olarak global alanda tanımlamak lazım ü

            // artık global alandaki temizle()  methodumu cagırabilirim 
            temizle();

        }

        // asagıdaki dataGridView 
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;    // burada herhangi birine tıkladıgımızda RowIndex birinci satıra kaydetsin 

            TxtPersonId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();  // dataGridWiew Satırları icerisinde 
                                                                                       // secilen satırın hucreleri icerisinde 0. index 
            TxtPersonName.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtPersonSurname.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbCity.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskSalary.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            label9.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            TxtPersonJob.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        private void label9_TextChanged(object sender, EventArgs e)
        {
            if(label9.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if(label9.Text == "False")
            {
                radioButton2.Checked = true;   // false olursa devre dısi biraktik demek  !! dikkat!!  
            }
        }

        // silme butonu 
        private void BtnDelete_Click(object sender, EventArgs e)
        {    // Delete From Tbl_NewPerson SQL'de bu tarz kodları komutları where'suz calistirmamak lazım
             // cunku mesela burada where'suz olursa hepsini siler cok dikkat etmemiz gerekiyor !!!!!!!!!!!!

            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("Delete From Tbl_NewPerson where PersonId = @k1" , baglanti);
            komutsil.Parameters.AddWithValue("@k1", TxtPersonId.Text);
            komutsil.ExecuteNonQuery();

            baglanti.Close();
            MessageBox.Show("Kayıt Silindi");
        }

        // Guncelleme butonu 
        // Update parametre ataması biraz degisik digerlerinden
        //Update TableName Set Alan1 = @a1, Alan2 = @a2,  ......
        private void BtnUpdate_Click(object sender, EventArgs e)
        {  // where    unutma!!!!!!!!!!!!!!!!!
            baglanti.Open();
            SqlCommand komutguncelle = new SqlCommand("Update Tbl_NewPerson Set PersonName =@a1, PersonSurname = @a2, PersonCity = @a3, PersonSalary = @a4, PersonJob = @a6  where PersonId = @a7" , baglanti);
            // parametre atama bolumu 
            // bilerek a7 en basa  yazdım dikkat çeksin diye 

            komutguncelle.Parameters.AddWithValue("a7", TxtPersonId.Text);


            komutguncelle.Parameters.AddWithValue("a1", TxtPersonName.Text);
            komutguncelle.Parameters.AddWithValue("a2", TxtPersonSurname.Text);
            komutguncelle.Parameters.AddWithValue("a3", CmbCity.Text);
            komutguncelle.Parameters.AddWithValue("a4", MskSalary.Text);
            komutguncelle.Parameters.AddWithValue("a6", TxtPersonJob.Text);

            // ne kaldı ???????????? komutguncelle calıstırmak 
            komutguncelle.ExecuteNonQuery(); 
            
            baglanti.Close();
            MessageBox.Show("Personel Bilgileri Güncellendi...");

        }

        private void BtnStatictic_Click(object sender, EventArgs e)
        {
            Frmİstatistik Frmist = new Frmİstatistik();
            Frmist.Show();          
        }

        private void BtnGraphics_Click(object sender, EventArgs e)
        {
            FrmGraphics frmgrafik = new FrmGraphics();
            frmgrafik.Show();

        }

        // Rapolar butonu 
        private void BtnRaporlar_Click(object sender, EventArgs e)
        {
            FrmRaporlar frmrapor = new FrmRaporlar();
            frmrapor.Show();  

        }

       
    }
}
