using ConfigHandler;
using System.Diagnostics;
using System.Text;

namespace Ika_SPT_Launcher_ConfigEditor
{
    public partial class ConfigForm : Form
    {
        private readonly string LAUNCHER_FILE = "Ika-SPT-Launcher.exe";
        private bool isDirtyFlag = false;
        private double configRevision;
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            //If sent here via firstrun from the ika-spt-launcher, there is no configuration file.
            if (args.Length > 0 && args.Contains("/firstrun"))
                MessageBox.Show("You are redirected to the configuration editor for the first time configuration.\n\nIf you want to edit the configuration later,\nyou can do so by opening the editor yourself.", "New configuration", MessageBoxButtons.OK);

            //Loads the config
            var resultLoad = ConfigurationHandler.LoadConfigFile();
            if (resultLoad.Item1.IsSuccess)
            {
                DisplayConfiguration(resultLoad.Item2);
                ShowStatus(false, "Configuration loaded!");
            }
            else ShowStatus(true, resultLoad.Item1.Message);

            HandleIsLocalCheck();
            SetListButtons();
            isDirtyFlag = false;
        }

        /// <summary>
        /// Add an external App
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ea_add_btn_Click(object sender, EventArgs e)
        {
            //Get file path
            string path = GetFilePath();

            //If a file is selected
            if (!string.IsNullOrWhiteSpace(path))
            {
                AddExternalApp(path);
                isDirtyFlag = true;
            }
        }

        /// <summary>
        /// Updates the row with the value of its selected Minimized dropdownlist value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleMinimized(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            //Form GUID
            int index = comboBox.Name.IndexOf('_');

            if (index != -1)
            {
                //Extracts the guid to find the row.
                string textAfterUnderscore = comboBox.Name[(index + 1)..];

                //Updates the correct row with dropdown value
                foreach (ListViewItem item in ea_apps_lv.Items)
                {
                    if (item.SubItems[2].Text == textAfterUnderscore) //Assuming ID is in the third column (index 2)
                    {
                        item.SubItems[1].Text = comboBox.Text;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Removes selected selected external applications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ea_remove_btn_Click(object sender, EventArgs e)
        {
            ClearSelectedApplicationFromList();

            //Make sure the app list buttons are enamed or not if app count is 0, 10 or in between
            SetListButtons();
        }

        /// <summary>
        /// Selects all external applications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ea_selectall_btn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ea_apps_lv.Items)
                if (item.SubItems.Count > 0) item.Checked = true;
        }

        /// <summary>
        /// Unselects all external applications
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ea_unselectall_btn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in ea_apps_lv.Items)
                if (item.SubItems.Count > 0) item.Checked = false;
        }

        /// <summary>
        /// Toggles the server section depending on the status of se_islocal_chk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Se_islocal_chk_CheckedChanged(object sender, EventArgs e)
        {
            HandleIsLocalCheck();
            isDirtyFlag = true;
        }

        /// <summary>
        /// Handles the status of server fields depending on the status of the islocal checkmark
        /// </summary>
        private void HandleIsLocalCheck()
        {
            se_browse_btn.Enabled = se_islocal_chk.Checked;
            se_path_txt.Enabled = se_islocal_chk.Checked;
            se_wait_txt.Enabled = se_islocal_chk.Checked;
            se_erase_lbl.Enabled = se_islocal_chk.Checked;
            se_path_lbl.Enabled = se_islocal_chk.Checked;
            se_wait_lbl.Enabled = se_islocal_chk.Checked;
            se_waitsec_lbl.Enabled = se_islocal_chk.Checked;
        }

        /// <summary>
        /// Toggle buttons depending on the number of external applications.
        /// </summary>
        private void SetListButtons()
        {
            ea_add_btn.Enabled = ea_apps_lv.Items.Count < 10;
            ea_remove_btn.Enabled = ea_apps_lv.Items.Count > 0;
            ea_selectall_btn.Enabled = ea_apps_lv.Items.Count > 0;
            ea_unselectall_btn.Enabled = ea_apps_lv.Items.Count > 0;
        }

        /// <summary>
        /// Opens an open file dialog to get paths of external applications
        /// </summary>
        /// <returns>Selected file path</returns>
        private string GetFilePath()
        {
            string filePath = string.Empty;
            getFile_dia.InitialDirectory = "C:\\"; //Set initial directory
            getFile_dia.Title = "Select a File";
            getFile_dia.Filter = "All Files|*.*"; //File filter to All
            getFile_dia.FileName = ""; //Reset selectedFileName, if any.
            getFile_dia.CheckFileExists = true; //Makes sure there is a file selected on open

            DialogResult result = getFile_dia.ShowDialog();

            if (result == DialogResult.OK) filePath = getFile_dia.FileName;
            return filePath;
        }

        /// <summary>
        /// Handles the launcher path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Le_browse_btn_Click(object sender, EventArgs e)
        {
            string newPath = GetFilePath();
            if (!string.IsNullOrWhiteSpace(newPath))
            {
                le_path_txt.Text = newPath;
                isDirtyFlag = true;
            }
        }

        /// <summary>
        /// Handles the server path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Se_browse_btn_Click(object sender, EventArgs e)
        {
            string newPath = GetFilePath();
            if (!string.IsNullOrWhiteSpace(newPath))
            {
                se_path_txt.Text = newPath;
                isDirtyFlag = true;
            }
        }

        /// <summary>
        /// Erases the launcher path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Le_erase_lbl_Click(object sender, EventArgs e)
        {
            le_path_txt.Text = string.Empty;
            isDirtyFlag = true;
        }

        /// <summary>
        /// Erases the server path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Se_erase_lbl_Click(object sender, EventArgs e)
        {
            se_path_txt.Text = string.Empty;
            isDirtyFlag = true;
        }

        /// <summary>
        /// Exits the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_quit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// On close, check if there are unsaved changes. If so, warn the user, then close the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.ApplicationExitCall) && isDirtyFlag)
            {
                DialogResult result = MessageBox.Show("There are unsaved changes. Are you sure you want to exit?", "Unsaved Changes", MessageBoxButtons.YesNo);

                //Cancels the close
                if (result == DialogResult.No) e.Cancel = true;
            }
        }

        /// <summary>
        /// Display a status at the bottom of the window
        /// </summary>
        /// <param name="isError">Status is an error</param>
        /// <param name="message">Status to display</param>
        /// <param name="fromSave">If it is not from save, we disallow saving to prevent corruption</param>
        private void ShowStatus(bool isError, string message, bool fromSave = false)
        {
            StringBuilder sb = new();
            sb.Append(isError ? "ERROR: " : string.Empty);
            sb.Append(message);
            status_lbl.Text = sb.ToString();
            status_lbl.ForeColor = isError ? Color.Red : Color.Green;

            if (isError && !fromSave)
            {
                form_reset_btn.Enabled = false;
                form_save_btn.Enabled = false;
                form_savelaunch_btn.Enabled = false;
            }
        }

        /// <summary>
        /// Fill the form with the loaded configuration
        /// </summary>
        /// <param name="config">configuration to display</param>
        /// <param name="deleteAllApps">Delete all apps from the list before filling again</param>
        private void DisplayConfiguration(AppConfiguration config, bool deleteAllApps = false)
        {
            le_path_txt.Text = config.LauncherFilePath;
            se_islocal_chk.Checked = config.IsServerLocal;
            se_path_txt.Text = config.ServerFilePath;
            se_wait_txt.Text = config.ServerWaitTimeInSeconds.ToString();
            deb_pause_chk.Checked = config.PauseIfAppsNotFound;
            revision_data_lbl.Text = config.ConfigurationRevision.ToString();
            configRevision = config.ConfigurationRevision;

            if (deleteAllApps) ClearSelectedApplicationFromList(deleteAllApps);

            foreach (ExternalApp app in config.ExternalApps)
                AddExternalApp(app.FilePath, app.LaunchMinimized);
        }

        /// <summary>
        /// Adds an external application to the list
        /// </summary>
        /// <param name="path">Path of the application selected</param>
        /// <param name="launchMinimized">Sets if it will run minimized or not</param>
        private void AddExternalApp(string path, bool launchMinimized = true)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                //Create new item
                string newId = Guid.NewGuid().ToString();
                ListViewItem newItem = new(path);
                newItem.SubItems.Add(launchMinimized ? "Yes" : "No");
                newItem.SubItems.Add(newId);

                //Create a ComboBox for the Minimized column
                ComboBox comboBox = new()
                {
                    Name = "eacombo_" + newId, //Add GUID to find it when needed
                    DropDownStyle = ComboBoxStyle.DropDownList //Disallow user input
                };
                comboBox.Items.AddRange(["Yes", "No"]);
                comboBox.SelectedIndex = launchMinimized ? 0 : 1; //Set default value to "Yes"

                //Adds dropdown in Minimized column
                newItem.SubItems.Add(new ListViewItem.ListViewSubItem(newItem, ""));
                ea_apps_lv.Items.Add(newItem);
                ea_apps_lv.Controls.Add(comboBox);

                //Place combobox properly
                comboBox.Bounds = ea_apps_lv.Items[^1].SubItems[1].Bounds;

                //Set event
#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                comboBox.SelectedIndexChanged += ToggleMinimized;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

                //Make sure the app list buttons are enabled or not if app count is 0, 10 or in between
                SetListButtons();
            }
        }

        /// <summary>
        /// Validates that the time entered is a number and is between 0 and 600.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Se_wait_txt_TextChanged(object sender, EventArgs e)
        {
            string value = se_wait_txt.Text;

            if (int.TryParse(value, out int number))
            {
                if (number < 0) se_wait_txt.Text = "0";
                if (number > 600) se_wait_txt.Text = "600";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(se_wait_txt.Text))
                {
                    AppConfiguration? file = ConfigurationHandler.GetConfigurationFile();
                    se_wait_txt.Text = file == null ? "0" : file.ServerWaitTimeInSeconds.ToString();
                }
            }

            if (se_wait_txt.Text == "-0") se_wait_txt.Text = "0";

            isDirtyFlag = true;
        }

        /// <summary>
        /// Just sets the dirty flag to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deb_pause_chk_CheckedChanged(object sender, EventArgs e)
        {
            isDirtyFlag = true;
        }

        /// <summary>
        /// Resets the whole form. Saves and load a default form instead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_reset_btn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to reset your configuration to their default values?\nThis will also remove your external applications.\n\nTHIS ACTION IS IRREVERSIBLE!", "Reset Configuration", MessageBoxButtons.YesNo);

            //Cancels the close
            if (result == DialogResult.Yes)
            {
                var resultLoad = ConfigurationHandler.LoadConfigFile(true);
                if (resultLoad.Item1.IsSuccess)
                {
                    DisplayConfiguration(resultLoad.Item2, true);
                    isDirtyFlag = false;
                    ShowStatus(false, "Configuration reset successful!");

                }
                else ShowStatus(true, resultLoad.Item1.Message);
            }
        }

        /// <summary>
        /// Removes selected (or all) external apps from the list
        /// </summary>
        /// <param name="deleteAll">Delete all, even those not selected</param>
        private void ClearSelectedApplicationFromList(bool deleteAll = false)
        {
            Control controlToDelete;
            foreach (ListViewItem item in ea_apps_lv.Items)
            {
                //Making sure the row exists
                if (item.SubItems.Count > 0)
                {
                    if (item.Checked || deleteAll)
                    {
                        //Removes the combobox
                        controlToDelete = Controls.Find($"eacombo_{item.SubItems[2].Text}", true).First();
                        Controls.Remove(controlToDelete);
                        controlToDelete.Dispose();

                        //Removes row
                        item.Remove();
                        isDirtyFlag = true;
                    }
                }
            }

            //Since comboboxes positions are static, we need to rearrange them manually.
            foreach (ListViewItem item in ea_apps_lv.Items)
            {
                Control control = Controls.Find($"eacombo_{item.SubItems[2].Text}", true).First();
                control.Bounds = item.SubItems[1].Bounds;
            }
        }

        /// <summary>
        /// Save configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_save_btn_Click(object sender, EventArgs e)
        {
            SaveConfiguration();
        }

        /// <summary>
        /// Save configuration and launch the launcher, if found
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_savelaunch_btn_Click(object sender, EventArgs e)
        {
            if (SaveConfiguration())
            {
                if (File.Exists(LAUNCHER_FILE))
                {
                    ProcessStartInfo startInfo = new()
                    {
                        FileName = LAUNCHER_FILE,
                        UseShellExecute = true,
                        CreateNoWindow = false,
                        WorkingDirectory = Path.GetDirectoryName(LAUNCHER_FILE), //Sets its working directory to where the app to run is located.
                    };
                    Process.Start(startInfo);
                }
                Application.Exit();
            }
        }

        /// <summary>
        /// Save the configuration
        /// </summary>
        /// <returns>True if success</returns>
        private bool SaveConfiguration()
        {
            bool isSuccess = false;
            try
            {
                if (ValidateForm())
                {
                    int timeToWait = 0;
                    if (int.TryParse(se_wait_txt.Text, out int number))
                        timeToWait = number;

                    AppConfiguration config = new()
                    {
                        ConfigurationRevision = configRevision,
                        LauncherFilePath = le_path_txt.Text,
                        IsServerLocal = se_islocal_chk.Checked,
                        ServerFilePath = se_path_txt.Text,
                        ServerWaitTimeInSeconds = timeToWait,
                        PauseIfAppsNotFound = deb_pause_chk.Checked,
                        ExternalApps = GetExternalApplications()
                    };

                    var resultSave = ConfigurationHandler.SaveConfigFile(config, false);
                    if (resultSave.Item1.IsSuccess)
                    {
                        ShowStatus(false, "Save successful!");
                        isSuccess = true;
                        isDirtyFlag = false;
                    }
                    else ShowStatus(true, resultSave.Item1.Message, true);
                }
            }
            catch (Exception ex)
            {
                ShowStatus(true, ex.Message, true);
            }

            return isSuccess;
        }

        /// <summary>
        /// Validates the form before saving
        /// </summary>
        /// <returns>True is validated</returns>
        private bool ValidateForm()
        {
            //Laddered validation for simplicity.
            string errorValidation = string.Empty;

            if (string.IsNullOrWhiteSpace(le_path_txt.Text))
                errorValidation = "Launcher path is not valid";

            if (se_islocal_chk.Checked && string.IsNullOrWhiteSpace(se_path_txt.Text) && string.IsNullOrWhiteSpace(errorValidation))
                errorValidation = "Server path is not valid";

            if (!int.TryParse(se_wait_txt.Text, out _) && string.IsNullOrWhiteSpace(errorValidation))
                errorValidation = "Time to wait is not valid";

            if (string.IsNullOrWhiteSpace(errorValidation)) return true;
            else
            {
                ShowStatus(true, errorValidation, true);
                return false;
            }
        }

        /// <summary>
        /// Get list of applications from list view to save
        /// </summary>
        /// <returns>List of Applications to save</returns>
        private List<ExternalApp> GetExternalApplications()
        {
            List<ExternalApp> returnModel = [];
            foreach (ListViewItem item in ea_apps_lv.Items)
            {
                returnModel.Add(new ExternalApp
                {
                    FilePath = item.SubItems[0].Text,
                    LaunchMinimized = item.SubItems[1].Text == "Yes"
                });
            }
            return returnModel;
        }
    }
}