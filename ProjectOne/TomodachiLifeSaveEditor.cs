using LoginNameSpace;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace ProjectOne
{
    public partial class TomodachiLife : Form
    {
        private bool serverDetailsProvided = false;
        private string server;
        private int port;
        private string username;
        private string password;
        private string currentDirectory = "/";
        private string savedataArcPath = Path.Combine(Environment.CurrentDirectory, "savedataArc.txt");
        private long expectedFileLength = 0;
        private string region = "";
        private Stack<string> directoryHistory = new Stack<string>();


        public class ConnectionDetails
        {
            public string? Name { get; set; }
            public string? Server { get; set; }
            public int Port { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
        }


        public TomodachiLife()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            connectMainButton.Click += ConnectMain_Click;
            nintendoDirectoryView.DoubleClick += nintendoDirectory_DoubleClick;
            backButton.Click += backButton_Click;
            downloadSaveFileButton.Click += loadSaveFile_Click;
            timePenaltyButton.Click += timePenaltyButton_Click;
            loadSaveButton.Click += getSaveFileLengthButton_Click;
            saveConnectionButton.Click += saveConnectionButton_Click;
            rootDisplayButton.Click += rootDisplayButton_Click;
            loadConnectionButton.Click += loadConnectionButton_Click;
        }

        private void TomodachiLife_Load(object sender, EventArgs e)
        {
            nintendoDirectoryView.View = View.Details;
            nintendoDirectoryView.Columns.Add("File Name", 200);
        }

        private async void ConnectMain_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (ShowLoginDialog(out server, out port, out username, out password))
                {
                    serverDetailsProvided = true;
                    this.Invoke((MethodInvoker)async delegate
                    {
                        await UpdateDirectoryListAsync();
                        connectMainButton.Enabled = true;
                    });
                }
                else
                {
                    connectMainButton.Enabled = true;
                }
            });
        }

        private bool ShowLoginDialog(out string server, out int port, out string username, out string password)
        {
            using (var loginDialog = new Login())
            {
                var result = loginDialog.ShowDialog();

                server = loginDialog.Server;
                port = loginDialog.Port;
                username = loginDialog.Username;
                password = loginDialog.Password;

                return result == DialogResult.OK;
            }
        }


        private async Task UpdateDirectoryListAsync()
        {
            if (serverDetailsProvided)
            {
                try
                {
                    nintendoDirectoryView.Items.Clear();
                    string[] directoryListing = await GetDirectoryListingAsync(server, port, username, password, currentDirectory);
                    foreach (string item in directoryListing)
                    {
                        nintendoDirectoryView.Items.Add(item);
                    }
                }
                catch (WebException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please provide server details first.");
            }
        }

        private async void nintendoDirectory_DoubleClick(object sender, EventArgs e)
        {
            if (serverDetailsProvided && nintendoDirectoryView.SelectedItems.Count > 0)
            {
                string selectedItemText = nintendoDirectoryView.SelectedItems[0].Text;
                string fullItemPath = currentDirectory.TrimEnd('/') + "/" + selectedItemText;
                directoryHistory.Push(currentDirectory);

                try
                {
                    nintendoDirectoryView.Items.Clear();
                    currentDirectory = fullItemPath;
                    nintendoDirectoryView.Tag = currentDirectory;
                    string[] directoryListing = await GetDirectoryListingAsync(server, port, username, password, fullItemPath);
                    foreach (string item in directoryListing)
                    {
                        nintendoDirectoryView.Items.Add(item);
                    }
                }
                catch (WebException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private async Task<string[]> GetDirectoryListingAsync(string server, int port, string username, string password, string directory)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{server}:{port}/{directory}");
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(username, password);

                using (WebResponse response = await request.GetResponseAsync())
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return new string[0];
            }
        }

        private async void backButton_Click(object sender, EventArgs e)
        {
            if (directoryHistory.Count > 0)
            {
                string previousDirectory = directoryHistory.Pop();
                string[] directoryListing = await GetDirectoryListingAsync(server, port, username, password, previousDirectory);
                nintendoDirectoryView.Items.Clear();
                currentDirectory = previousDirectory;
                nintendoDirectoryView.Tag = currentDirectory;
                foreach (string item in directoryListing)
                {
                    nintendoDirectoryView.Items.Add(new ListViewItem(item));
                }
            }
            else
            {
                MessageBox.Show("No previous directory to go back to.");
            }
        }

        private void loadSaveFile_Click(object sender, EventArgs e)
        {
            if (nintendoDirectoryView.SelectedItems.Count == 1)
            {
                string selectedFileName = nintendoDirectoryView.SelectedItems[0].Text;
                string fullFilePath = currentDirectory.TrimEnd('/') + "/" + selectedFileName;

                string currentAppDirectory = Environment.CurrentDirectory;

                string backupFolderName = "Backup";
                string backupFolderPath = Path.Combine(currentAppDirectory, backupFolderName);

                if (!Directory.Exists(backupFolderPath))
                {
                    Directory.CreateDirectory(backupFolderPath);
                }

                string localFilePath = Path.Combine(currentAppDirectory, selectedFileName);

                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Credentials = new NetworkCredential(username, password);
                        client.DownloadFile($"ftp://{server}:{port}/{fullFilePath}", localFilePath);
                        string backupFilePath = Path.Combine(backupFolderPath, selectedFileName);
                        File.Copy(localFilePath, backupFilePath, true);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error downloading file: {ex.Message}");
                }
            }
        }
        private void timePenaltyButton_Click(object sender, EventArgs e)
        {
            try
            {
                timePenaltyButton.Enabled = false;
                byte[] newBytes;

                if (string.IsNullOrEmpty(region))
                {
                    MessageBox.Show("Region information is missing. Click 'Load Save' first.");
                    return;
                }

                using (FileStream fs = new FileStream(savedataArcPath, FileMode.Open, FileAccess.Write))
                {
                    if (region == "JP")
                    {
                        newBytes = new byte[] { 0x40, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x02, 0x00 };
                        fs.Position = 0x14BD40;
                    }
                    else if (region == "USA")
                    {
                        newBytes = new byte[] { 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x03, 0x03, 0x02, 0x00 };
                        fs.Position = 0x1E4C70;
                    }
                    else
                    {
                        MessageBox.Show("Unknown region. Cannot modify the file.");
                        return;
                    }

                    fs.Write(newBytes, 0, newBytes.Length);
                }

                MessageBox.Show("Time penalty removed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                timePenaltyButton.Enabled = true;
            }
        }

        private void getSaveFileLengthButton_Click(object sender, EventArgs e)
        {
            try
            {
                loadSaveButton.Enabled = false;
                if (File.Exists(savedataArcPath))
                {
                    FileInfo fileInfo = new FileInfo(savedataArcPath);
                    expectedFileLength = fileInfo.Length;

                    if (expectedFileLength == 1359208)
                    {
                        region = "JP";
                    }
                    else if (expectedFileLength == 1985688)
                    {
                        region = "USA";
                    }
                    else
                    {
                        MessageBox.Show("Unknown region. Cannot modify the file.");
                        return;
                    }

                    MessageBox.Show($"File length set to {expectedFileLength} bytes for {region} region.");
                }
                else
                {
                    MessageBox.Show("The file does not exist.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally { loadSaveButton.Enabled = true; }
        }

        private void SaveConnectionDetails(ConnectionDetails connectionDetails)
        {
            try
            {
                string connectionDetailsFilePath = Path.Combine(Environment.CurrentDirectory, "connectionDetails.txt");
                string json = JsonConvert.SerializeObject(connectionDetails);
                File.WriteAllText(connectionDetailsFilePath, json);

                MessageBox.Show("Connection details saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving connection details: {ex.Message}");
            }
        }


        private void saveConnectionButton_Click(object sender, EventArgs e)
        {
            ConnectionDetails connectionDetails = new ConnectionDetails
            {
                Name = DateTime.Now.ToShortDateString(),
                Server = server,
                Port = port,
                Username = username,
                Password = password
            };
            SaveConnectionDetails(connectionDetails);
        }


        private void rootDisplayButton_Click(object sender, EventArgs e)
        {
        }
        private async void loadConnectionButton_Click(object sender, EventArgs e)
        {
            try
            {
                loadConnectionButton.Enabled = false;

                await Task.Run(() =>
                {
                    string connectionDetailsFilePath = Path.Combine(Environment.CurrentDirectory, "connectionDetails.txt");

                    if (File.Exists(connectionDetailsFilePath))
                    {
                        string json = File.ReadAllText(connectionDetailsFilePath);

                        ConnectionDetails connectionDetails = JsonConvert.DeserializeObject<ConnectionDetails>(json);

                        using (Login loginDialog = new Login(connectionDetails.Server, connectionDetails.Port, connectionDetails.Username, connectionDetails.Password))
                        {
                            var result = loginDialog.ShowDialog();

                            if (result == DialogResult.OK)
                            {
                                server = loginDialog.Server;
                                port = loginDialog.Port;
                                username = loginDialog.Username;
                                password = loginDialog.Password;

                                serverDetailsProvided = true;

                                this.Invoke((MethodInvoker)async delegate
                                {
                                    await UpdateDirectoryListAsync();
                                });
                            }
                        }
                    }
                });
            }
            finally
            {
                loadConnectionButton.Enabled = true;
            }
        }

        private void applyMoneyButton_Click(object sender, EventArgs e)
        {

            if (int.TryParse(moneyInput.Text, out int moneyValue))
            {

                byte[] newBytes = new byte[16];

                byte[] moneyBytes = BitConverter.GetBytes(moneyValue);


                if (!BitConverter.IsLittleEndian)
                {
                    Array.Reverse(moneyBytes);
                }



                using (FileStream fs = new FileStream(savedataArcPath, FileMode.Open, FileAccess.Write))
                {
                    if (region == "JP")
                    {
                        int moneyOffset = 8;
                        Array.Copy(moneyBytes, 0, newBytes, moneyOffset, moneyBytes.Length);
                        fs.Position = 0x14BCA0;


                        fs.Write(newBytes, 0, newBytes.Length);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid input for money.");
            }
        }

    }
}