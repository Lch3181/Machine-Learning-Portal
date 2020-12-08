using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string Dir = string.Format(@"{0}\JupyterNotebook\Machine Learning Portal", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));

        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown1;
        }

        private void CheckDependency()
        {
            bool done = false;
            Dictionary<string, string> dependencies = new Dictionary<string, string>();
            dependencies.Add("python", "python --version");
            dependencies.Add("pip", "pip --version");
            dependencies.Add("notebook", "pip install notebook");
            dependencies.Add("Numpy", "pip install 'Numpy==1.19.3'");
            dependencies.Add("Scipy", "pip install Scipy");
            dependencies.Add("Pandas", "pip install Pandas");
            dependencies.Add("Statsmodels", "pip install Statsmodels");
            dependencies.Add("Matplotlib", "pip install Matplotlib");
            dependencies.Add("Scikit-learn", "pip install Scikit-learn");
            //dependencies.Add("Mlpy", "pip install Mlpy");
            dependencies.Add("Theano", "pip install Theano");
            dependencies.Add("Scapy", "pip install Scapy");
            dependencies.Add("NLTK", "pip install NLTK");
            dependencies.Add("Pattern", "pip install Pattern");
            dependencies.Add("Seaborn", "pip install Seaborn");
            dependencies.Add("Bokeh", "pip install Bokeh");
            //dependencies.Add("Basemap", "pip install Basemap");
            dependencies.Add("NetworkX", "pip install NetworkX");
            dependencies.Add("pulp", "pip install pulp");

            //refresh environment variables, not working
            //Program.CMD("/C " + @"Resources\RefreshEnv.cmd");

            progressBar1.Maximum = dependencies.Count;
            string pipList = Program.CMD("/C " + "pip list");
            foreach (KeyValuePair<string, string> dependency in dependencies)
            {
                progressBar1.Value++;
                label11.Text = dependency.Key;
                Refresh();
                switch (dependency.Key)
                {
                    case "pip":
                    case "python":
                        if (Program.CMD("/C " + dependency.Value) != "")
                            break;
                        MessageBox.Show("Python not found, downloading python.");
                        InstallPython();
                        done = true;
                        break;
                    default:
                        if (pipList.Contains(dependency.Key.ToLower()) || pipList.Contains(dependency.Key))
                            break;
                        label11.Text = "Installing " + dependency.Key;
                        Refresh();
                        Program.CMD("/C " + dependency.Value);

                        if (dependency.Key == "notebook")
                        {
                            JupyterConfig();
                        }
                        break;
                }
                if (done)
                    break;
            }
            if(!done)
            {
                progressBar1.Value = progressBar1.Maximum;
                label11.Text = "Ready";
            }
        }

        private void JupyterConfig()
        {
            //create jupyter notebook startup dir
            string path = string.Format(@"{0}\JupyterNotebook", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            DirectoryInfo directoryInfo = Directory.CreateDirectory(path);

            //unzip jupyter files in startup dir
            ZipFile.ExtractToDirectory(@"Resources\Machine Learning Portal.zip", directoryInfo.FullName + @"\Machine Learning Portal");
        }

        private void Form1_Shown1(object sender, EventArgs e)
        {
            CheckDependency();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string filePath = @"\Linear Regression";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string filePath = @"\Logistic Regression";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            string filePath = @"\Bayesian Classifcation";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string filePath = @"\Decision Tree & Random Forest";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            string filePath = @"\Decision Tree & Random Forest";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string filePath = @"\Cluster Analysis";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string filePath = @"\Fuzzy Data Matching";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            string filePath = @"\Multi-Layer Nerural Networks";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            string filePath = @"\Linear Programming";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            string filePath = @"\Massively Parallel Programming with Spark";
            Program.Jupyter("/C jupyter notebook \"" + Dir + filePath + "\"");
        }
        public void InstallPython()
        {
            string fileName = "python-3.8.5-amd64.exe";
            string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";

            try
            {
                if (File.Exists(downloadFolder + "\\" + fileName))
                {
                    File.Delete(downloadFolder + "\\" + fileName);
                }
                label11.Visible = true;
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                progressBar1.Maximum = 100;
                WebClient webClient = new WebClient();
                webClient.DownloadFileAsync(new System.Uri("https://www.python.org/ftp/python/3.8.5/python-3.8.5-amd64.exe"),
                    downloadFolder + "\\" + fileName);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
                label11.Text = "Downloading Python";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                label11.Text = "Installing Python";
            }
        }
        public void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string fileName = "python-3.8.5-amd64.exe";
            string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            if (e.Error == null)
            {
                Program.CMD("/C " + downloadFolder + "\\" + fileName + " /quiet InstallAllUsers=1 PrependPath=1 Include_test=0");

                //refresh env not working
                //CheckDependency();

                //temp
                label11.Text = "Installed Python";
                MessageBox.Show("Python installed, App restart required.");
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Unable to download exe, please check your connection", "Download failed!");
            }
        }
    }
}
