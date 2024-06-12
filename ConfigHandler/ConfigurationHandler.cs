using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

namespace ConfigHandler
{
    public static class ConfigurationHandler
    {
        private static bool isConfigSet = false; //Make sure it does not load the configuration multiple times.
        private static AppConfiguration config = new();
        private static ConfigurationHandlerReturnModel returnModel = new();
        private static readonly string CONFIG_FILE_PATH = @$"C:\SPT\{AppDomain.CurrentDomain.FriendlyName}.config.json";

        /// <summary>
        /// Check if the file exists on the filesystem. Location at the same place as where the app is installed
        /// </summary>
        /// <returns>True if exists</returns>
        public static Tuple<ConfigurationHandlerReturnModel, bool> CheckIfConfigFileExists()
        {
            InitializeStatusForSuccess();
            bool result = false;
            try
            {
                result = File.Exists(CONFIG_FILE_PATH);
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            return new Tuple<ConfigurationHandlerReturnModel, bool>(returnModel, result);
        }

        /// <summary>
        /// Create a new default configuration file and sets it.
        /// </summary>
        /// <param name="isOverwrite">Overwrite the current configuration with the provided one</param>
        /// <returns>Configuration Object</returns>
        public static Tuple<ConfigurationHandlerReturnModel, AppConfiguration> CreateNewDefaultConfigFile(bool isOverwrite = false)
        {
            InitializeStatusForSuccess();
            var resultExist = CheckIfConfigFileExists();
            try
            {
                if (resultExist.Item1.IsSuccess)
                {
                    if (!resultExist.Item2 || isOverwrite)
                    {
                        config = new();
                        string newConfigJson = JsonConvert.SerializeObject(config, Formatting.Indented);
                        File.WriteAllText(CONFIG_FILE_PATH, newConfigJson);
                        isConfigSet = true;
                    }
                    else
                    {
                        SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {CONFIG_FILE_PATH} already exists and was not meant to be overwritten.");
                    }
                }
                else
                {
                    returnModel = resultExist.Item1;
                }
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            return new Tuple<ConfigurationHandlerReturnModel, AppConfiguration>(returnModel, config);
        }

        /// <summary>
        /// Save the configuration file on the filesystem and updates the loaded one.
        /// </summary>
        /// <param name="configFile">Configuration file to save</param>
        /// <returns>Configuration Object</returns>
        public static Tuple<ConfigurationHandlerReturnModel, AppConfiguration> SaveConfigFile(AppConfiguration configFile)
        {
            InitializeStatusForSuccess();
            try
            {
                if (isConfigSet)
                {
                    string newConfigJson = JsonConvert.SerializeObject(configFile, Formatting.Indented);
                    File.WriteAllText(CONFIG_FILE_PATH, newConfigJson);
                    config = configFile;
                }
                else
                {
                    SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {CONFIG_FILE_PATH} file needs to be loaded before being able to save it.");
                }
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            return new Tuple<ConfigurationHandlerReturnModel, AppConfiguration>(returnModel, config);
        }

        /// <summary>
        /// Loads configuration from the filesystem. If none, creates one.
        /// </summary>
        /// <returns>Configuration Object</returns>
        public static Tuple<ConfigurationHandlerReturnModel, AppConfiguration> LoadConfigFile()
        {
            InitializeStatusForSuccess();
            try
            {
                if (!isConfigSet)
                {
                    var resultExist = CheckIfConfigFileExists();
                    if (resultExist.Item1.IsSuccess)
                    {
                        if (resultExist.Item2)
                        {
                            string configFileJson = File.ReadAllText(CONFIG_FILE_PATH);
                            AppConfiguration? convertedJson = JsonConvert.DeserializeObject<AppConfiguration>(configFileJson);
                            if (convertedJson != null)
                            {
                                config = convertedJson;
                                isConfigSet = true;
                            }
                            else
                            {
                                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: Could not load {CONFIG_FILE_PATH}");
                            }
                        }
                        else
                        {
                            var resultCreate = CreateNewDefaultConfigFile();
                            if (resultExist.Item1.IsSuccess)
                            {
                                config = resultCreate.Item2;
                                isConfigSet = true;
                            }
                            else
                            {
                                returnModel = resultCreate.Item1;
                            }
                        }
                    }
                    else
                    {
                        returnModel = resultExist.Item1;
                    }
                }
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            return new Tuple<ConfigurationHandlerReturnModel, AppConfiguration>(returnModel, config);
        }

        /// <summary>
        /// Checks if configuration file is the lastest revision.
        /// </summary>
        /// <returns>List of revisions newer than the current one.</returns>
        private static Tuple<ConfigurationHandlerReturnModel, List<double>> CheckIfConfigurationIsLatestRevision()
        {
            InitializeStatusForSuccess();
            List<double> requiredRevisions = [];
            try
            {
                string configFileJson = File.ReadAllText(CONFIG_FILE_PATH);
                JObject jsonObject = JObject.Parse(configFileJson);

                if (jsonObject.ContainsKey("ConfigurationRevision"))
                {
                    JToken? token = jsonObject["ConfigurationRevision"];
                    if (token != null)
                    {
                        double configurationRevisionValue = token.Value<double>();
                        if (RevisionHistory.Revisions.Any(s => s == configurationRevisionValue))
                        {
                            requiredRevisions = RevisionHistory.Revisions.Where(d => d > configurationRevisionValue).ToList();
                        }
                        else
                        {
                            SetFailureStatus($"{CONFIG_FILE_PATH} contains an invalid revision.");
                        }
                    }
                    else
                    {
                        SetFailureStatus($"{CONFIG_FILE_PATH} contains an invalid revision.");
                    }
                }
                else
                {
                    requiredRevisions = RevisionHistory.Revisions;
                }
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            return new Tuple<ConfigurationHandlerReturnModel, List<double>>(returnModel, requiredRevisions);
        }

        /// <summary>
        /// Update the configuration file to the latest revision
        /// </summary>
        /// <returns>True if success</returns>
        public static Tuple<ConfigurationHandlerReturnModel, bool> UpdateConfigurationToCurrentRevision()
        {
            InitializeStatusForSuccess();
            bool isSuccess = false;
            try
            {
                var resultRevisionCheck = CheckIfConfigurationIsLatestRevision();
                if (resultRevisionCheck.Item1.IsSuccess)
                {
                    var resultUpdate = UpdateRevisions(resultRevisionCheck.Item2);
                    if (resultUpdate.Item1.IsSuccess)
                    {
                        isSuccess = resultUpdate.Item2;
                    }
                    else
                    {
                        returnModel = resultRevisionCheck.Item1;
                    }
                }
                else
                {
                    returnModel = resultRevisionCheck.Item1;
                }
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            return new Tuple<ConfigurationHandlerReturnModel, bool>(returnModel, isSuccess);
        }

        /// <summary>
        /// Sets initial status assuming success until stated otherwise
        /// </summary>
        private static void InitializeStatusForSuccess()
        {
            returnModel.IsSuccess = true;
            returnModel.Message = string.Empty;
        }

        /// <summary>
        /// Sets things to failure if there is an error
        /// </summary>
        /// <param name="message">Error message to display</param>
        private static void SetFailureStatus(string message)
        {
            returnModel.IsSuccess = false;
            returnModel.Message = message;
        }

        /// <summary>
        /// Updates the configuration sequentially to the latest revision for compatibility purposes. 
        /// </summary>
        /// <param name="revisions">List of revisions to update the configuration file.</param>
        /// <returns>True if success</returns>
        private static Tuple<ConfigurationHandlerReturnModel, bool> UpdateRevisions(List<double> revisions)
        {
            InitializeStatusForSuccess();
            string error = string.Empty;
            bool isSuccess;
            try
            {
                revisions.Sort();
                foreach (double revision in revisions)
                {
                    if (revision == 2406.01) error = UpdateToRevision240601();
                    //More versions below
                    //if(string.IsNullOrWhiteSpace(error)) if(revision == XXXX) error = UpdateToRevisionXXXX();

                    isSuccess = string.IsNullOrWhiteSpace(error);
                }

                if (!string.IsNullOrWhiteSpace(error))
                {
                    SetFailureStatus(error);
                }
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            isSuccess = string.IsNullOrWhiteSpace(error) && returnModel.IsSuccess;
            return new Tuple<ConfigurationHandlerReturnModel, bool>(returnModel, isSuccess);

        }

        /// <summary>
        /// Update the configuration to the revision 240601
        /// </summary>
        /// <returns>Error if any.</returns>
        private static string UpdateToRevision240601()
        {
            //CHANGE FROM v1.0 to 1.1
            //(change) ServerFile (string) -> ServerFilePath (string)
            //(change) LauncherFile (string) -> LauncherFilePath (string)
            //(add) ConfigurationRevision (double)
            //(add) IsServerLocal (bool)
            //(add) PauseIfAppsNotFound (bool)

            //Change this to revision after whenever update app
            var error = string.Empty;
            try
            {
                string configFile100Json = File.ReadAllText(CONFIG_FILE_PATH);
                JObject jsonObject = JObject.Parse(configFile100Json);
                AppConfiguration newConfig = new()
                {
                    ServerWaitTimeInSeconds = jsonObject["ServerWaitTimeInSeconds"].Value<int>(),
                    ServerFilePath = jsonObject["ServerFile"].Value<string>(),
                    LauncherFilePath = jsonObject["LauncherFile"].Value<string>(),
                    ExternalApps = jsonObject["ExternalApps"].Children().Select(token => token.ToObject<ExternalApp>()).ToList()
                };

                //Remove the null or empty external Apps
                newConfig.ExternalApps = newConfig.ExternalApps.Where(x => !string.IsNullOrWhiteSpace(x.FilePath)).ToList();

                var resultLoad = LoadConfigFile();
                if (resultLoad.Item1.IsSuccess)
                {
                    var resultSave = SaveConfigFile(newConfig);
                    if (!resultSave.Item1.IsSuccess)
                    {
                        error = resultSave.Item1.Message;
                    }
                }
                else { error = resultLoad.Item1.Message; }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return error;
        }
    }
}
