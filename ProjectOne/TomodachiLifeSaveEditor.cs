using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectOne
{
    public partial class TomodachiLife : Form
    {
        private bool serverDetailsProvided = false;
        private string server;
        private string username;
        private string password;
        private string currentDirectory = "/";
        private Stack<string> directoryHistory = new Stack<string>();


        public TomodachiLife()
        {
            InitializeComponent();
        }

        private void TomodachiLife_Load(object sender, EventArgs e)
        {
            nintendoDirectory.View = View.Details;
            nintendoDirectory.Columns.Add("File Name", 200);
            ConnectMain.Click += new EventHandler(ConnectMain_Click);
            nintendoDirectory.DoubleClick += new EventHandler(nintendoDirectory_DoubleClick);
        }

        private void ConnectMain_Click(object sender, EventArgs e)
        {
            if (!serverDetailsProvided)
            {
                server = Microsoft.VisualBasic.Interaction.InputBox("Enter FTP Server:", "FTP Server", "");
                username = Microsoft.VisualBasic.Interaction.InputBox("Enter Username:", "Username", "");
                password = Microsoft.VisualBasic.Interaction.InputBox("Enter Password:", "Password", "");
                serverDetailsProvided = true;
                string directory = "/";

                string[] directoryListing = GetDirectoryListing(server, username, password, directory);
                nintendoDirectory.Items.Clear();
                foreach (string item in directoryListing)
                {
                    nintendoDirectory.Items.Add(new ListViewItem(item.Trim('/')));
                }
            }
        }

        private void nintendoDirectory_DoubleClick(object sender, EventArgs e)
        {
            if (serverDetailsProvided)
            {
                if (nintendoDirectory.SelectedItems.Count > 0)
                {
                    string selectedItemText = nintendoDirectory.SelectedItems[0].Text;
                    string fullItemPath = currentDirectory.TrimEnd('/') + "/" + selectedItemText;
                    directoryHistory.Push(currentDirectory);

                    try
                    {
                        nintendoDirectory.Items.Clear();
                        currentDirectory = fullItemPath;
                        nintendoDirectory.Tag = currentDirectory;
                        System.Diagnostics.Debug.WriteLine(fullItemPath + "ItemPath");
                        System.Diagnostics.Debug.WriteLine(currentDirectory);
                        string[] directoryListing = GetDirectoryListing(server, username, password, fullItemPath);
                        foreach (string item in directoryListing)
                        {
                            nintendoDirectory.Items.Add(item);
                        }
                    }
                    catch (WebException ex)
                    {
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Please provide server details first.");
            }
        }




        private string[] GetDirectoryListing(string server, string username, string password, string directory)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{server}/{directory}");
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(username, password);

            using (WebResponse response = request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string listing = reader.ReadToEnd();
                string[] lines = listing.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> filesAndDirectories = new List<string>();

                foreach (string line in lines)
                {
                    Match match = Regex.Match(line, @"([d-])\S+\s+\d+\s+\S+\s+\S+\s+(\d+)\s+(\S+\s+\S+\s+\S+)\s+(.*)");
                    if (match.Success)
                    {
                        char itemType = match.Groups[1].Value[0];
                        string name = match.Groups[4].Value;
                        filesAndDirectories.Add(name);
                    }
                }

                return filesAndDirectories.ToArray();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            if (directoryHistory.Count > 0)
            {
                string previousDirectory = directoryHistory.Pop();
                string[] directoryListing = GetDirectoryListing(server, username, password, previousDirectory);
                nintendoDirectory.Items.Clear();
                currentDirectory = previousDirectory;
                nintendoDirectory.Tag = currentDirectory;
                foreach (string item in directoryListing)
                {
                    nintendoDirectory.Items.Add(new ListViewItem(item));
                }
            }
            else
            {
                MessageBox.Show("No previous directory to go back to.");
            }
        }

        private void loadSaveFile_Click(object sender, EventArgs e)
        {
            if (nintendoDirectory.SelectedItems.Count > 0)
            {
                string selectedFileName = nintendoDirectory.SelectedItems[0].Text;
                string fullFilePath = currentDirectory.TrimEnd('/') + "/" + selectedFileName;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = selectedFileName;
                DialogResult result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string localFilePath = saveFileDialog.FileName;

                    try
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Credentials = new NetworkCredential(username, password);
                            client.DownloadFile($"ftp://{server}/{fullFilePath}", localFilePath);
                            MessageBox.Show("File downloaded successfully.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error downloading file: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a file to download.");
            }
        }
    }
}
