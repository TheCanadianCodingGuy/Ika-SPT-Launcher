using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

namespace ConfigHandler
{
    public static class ConfigurationHandler
    {
        private static bool isConfigSet = false;
        private static AppConfiguration config = new();
        private static ConfigurationHandlerReturnModel returnModel = new();
        private static readonly string CONFIG_FILE_PATH = @$"C:\SPT\{AppDomain.CurrentDomain.FriendlyName}.config.json";

        public static string GetConfigFilePath()
        {
            return CONFIG_FILE_PATH;
        }

        public static AppConfiguration? GetAppConfiguration()
        {
            return isConfigSet ? config : null;
        }

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

        private static void InitializeStatusForSuccess()
        {
            returnModel.IsSuccess = true;
            returnModel.Message = string.Empty;
        }

        private static void SetFailureStatus(string message)
        {
            returnModel.IsSuccess = false;
            returnModel.Message = message;
        }

        //TODO: if corrupted, warn user and remake config
        public static Tuple<ConfigurationHandlerReturnModel, List<double>> CheckIfConfigurationIsLatestRevision()
        {
            InitializeStatusForSuccess();
            List<double> requiredRevisions = [];
            try
            {
                string configFileJson = File.ReadAllText(CONFIG_FILE_PATH);
                JObject jsonObject = JObject.Parse(configFileJson);

                if (jsonObject.ContainsKey("ConfigurationRevision"))
                {
                    double configurationRevisionValue = jsonObject["ConfigurationRevision"].Value<double>();

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
                    requiredRevisions = RevisionHistory.Revisions;
                }
            }
            catch (Exception ex)
            {
                SetFailureStatus($"{MethodBase.GetCurrentMethod()?.Name}: {ex.Message}");
            }
            return new Tuple<ConfigurationHandlerReturnModel, List<double>>(returnModel, requiredRevisions);
        }
    }
}
