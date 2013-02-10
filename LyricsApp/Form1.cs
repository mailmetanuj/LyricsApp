using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Collections;

namespace LyricsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //JSON object.
        JObject o = null;

        ArrayList SongList = new ArrayList();
     //   TrackInformation[] SongList = new TrackInformation[10];

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //clear listview every time button is clicked
            listView1.Items.Clear();
            SongList.Clear();
            //form the query string
            if (radioButton13.Checked)
                QueryString = "q_track=" + textBox3.Text + "&q_artist=" + textBox4.Text + "&q_lyrics=" + textBox5.Text;
            else
                QueryString = "q=" + textBox1.Text;
            
            
            using (BackgroundWorker bg = new BackgroundWorker())
            {
                bg.DoWork += (sender1, args) => Fetch_Data();
                bg.RunWorkerAsync();
            }
            
        }

        //default values
        String MustHaveLyrics = "1";
        String SortBy = "s_track_rating";
        String SortOrder = "ASC";
        String QueryString = "q=";

        public void ConnectToMusixMatch()
        {
            try
            {
                WebClient c = new WebClient();
                String Complete_Query = "http://api.musixmatch.com/ws/1.1/track.search?apikey=8bd381af020445a89d2197f0fb6545d6&" + QueryString + "&format=json&f_has_lyrics=" + MustHaveLyrics + "&" + SortBy + "=" + SortOrder + "&page_size=10";
                var data = c.DownloadString(Complete_Query);

                o = JObject.Parse(data);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error", "Check internet connection", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Close();

            }
        }


        TrackInformation track = null;
        ListViewItem item = null;
        int upper_bound = 0;
        private void Fetch_Data()
        {

            ConnectToMusixMatch();

            //header information
            Header vHeaderInfo = new Header(o);
            label1.Text = vHeaderInfo.Available() + " tracks available";
            label2.Text = "Status Code : " + vHeaderInfo.StatusCode();
            label3.Text = "Execution Time :" + vHeaderInfo.ExecuteTime();


            

            //number of item to be fetched. For more than 10 item, change the query string
            
            if (Convert.ToInt32(vHeaderInfo.Available()) < 10)
                upper_bound = Convert.ToInt32(vHeaderInfo.Available());
            else upper_bound = 10;

            
            //fetch all track informtaion
            for (int i = 0; i < upper_bound; i++)
            {
                track = new TrackInformation(o, i);
                SongList.Add(track);
            }
            using (BackgroundWorker bg = new BackgroundWorker())
            {
                bg.DoWork += (sender, args) => Populate_ListView();
                bg.RunWorkerAsync();
            }

            



        }

        private void Populate_ListView()
        {
            int i = 0;
            foreach(TrackInformation t in SongList)
            {
                

              item = new ListViewItem(t.TrackName(),i);
              i++;
                item.SubItems.Add(t.ArtistName());
                item.SubItems.Add(t.AlbumName());
                item.SubItems.Add(t.AlbumCoverArt_100by100());

                if (t.AlbumCoverArt_100by100() != "")
                {
                    images.Images.Add(LoadImage(t.AlbumCoverArt_100by100()));
                }
                else
                    images.Images.Add(Image.FromFile(@"C:\Users\ta\Pictures\1.jpg"));


                listView1.SmallImageList = images;
                listView1.LargeImageList = images;

                listView1.Items.AddRange(new ListViewItem[] { item });


                progressBar1.Value += 10;
            }
            progressBar1.Value = 0;
        }
       

        private Image LoadImage(string url)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();

            Bitmap bmp = new Bitmap(responseStream);

            responseStream.Dispose();

            return bmp;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                MustHaveLyrics = "1";
            else
                MustHaveLyrics = "0";
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
                SortOrder = "ASC";
            else
                SortOrder = "DESC";
        }

        public void SortLyrics(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                SortBy = "s_track_rating";
            else if (radioButton4.Checked)
                SortBy = "s_artist_rating";
            else if (radioButton5.Checked)
                SortBy = "s_track_release_date";
        }

        private void View(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
                listView1.View = System.Windows.Forms.View.LargeIcon;
            else if (radioButton9.Checked)
                listView1.View = System.Windows.Forms.View.SmallIcon;
            else if (radioButton10.Checked)
                listView1.View = System.Windows.Forms.View.Details;
            else if (radioButton11.Checked)
                listView1.View = System.Windows.Forms.View.Tile;
            else if (radioButton12.Checked)
                listView1.View = System.Windows.Forms.View.List;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton13.Checked)
            {
                textBox1.Enabled = false;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox1.Enabled = true;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (BackgroundWorker bg = new BackgroundWorker())
            {
                bg.DoWork += (sender1, args) => show_lyrics_and_labels();
                bg.RunWorkerAsync();
            }

            using (BackgroundWorker bg = new BackgroundWorker())
            {
                bg.DoWork += (sender1, args) => Get_Lyrics();
                bg.RunWorkerAsync();
            }

           
        }

        public void show_lyrics_and_labels()
        {
            if (listView1.SelectedIndices.Count > 0)
            {

                TrackInformation t = (TrackInformation)SongList[listView1.SelectedIndices[0]];


                label7.Text = t.TrackName();
                label8.Text = t.ArtistName();
                label9.Text = t.AlbumName();
                label10.Text = t.TrackRating();
                label11.Text = t.TrackLength();
                pictureBox1.ImageLocation = t.AlbumCoverArt_100by100();
               
            }
        }

        public void Get_Lyrics()
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                TrackInformation t = (TrackInformation)SongList[listView1.SelectedIndices[0]];

                try
                {
                    WebClient c = new WebClient();
                    String Complete_Query = "http://api.musixmatch.com/ws/1.1/track.lyrics.get?apikey=8bd381af020445a89d2197f0fb6545d6&format=json&track_id="+t.TrackId();
                    var data = c.DownloadString(Complete_Query);

                    o = JObject.Parse(data);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error", "Check internet connection", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    this.Close();

                }

                Lyrics lyrics = new Lyrics(o);
                textBox2.Text = lyrics.GetLyrics();
            } 
        }

        

        
       
    }
}