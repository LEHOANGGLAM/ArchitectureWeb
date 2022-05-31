using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Sebox.Models
{
    public class ConfigFileSetting
    {
        public  IList<Setting> GetAllSettings()
        {
            var settings = new List<Setting>();
            var appSettings = ConfigurationManager.AppSettings;
            foreach (var setting in appSettings.AllKeys)
            {
                settings.Add(new Setting()
                {
                    Name = setting.ToLowerInvariant(),
                    Value = appSettings[setting]
                });
            }

            return settings;
        }


        public void UpdateSettings<T>(string Key, T Value)
        {
            //var config = WebConfigurationManager.OpenWebConfiguration("~/Web.config");

            ////set value in key
            //config.AppSettings.Settings["" + Key + ""].Value = Value.ToString();

            ////save value
            //config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");

            //Properties.Settings.Default.Reload();
            //var saveData = Properties.Settings.Default.Context;
            //saveData["PathAdmin"] = Value;
            //Properties.Settings.Default.Save();
        }
    }
}