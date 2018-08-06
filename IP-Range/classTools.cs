using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace IP_Range
{
    public static class classTools
    {
        //List of Containers
        public static ObservableCollection<classContainer> Containers = new ObservableCollection<classContainer>
        {
            //new classContainer
            //{
            //    Name = "Users",
            //    Hosts = new ObservableCollection<classHost>
            //    {
            //        new classHost{ DNS_Name = "WS-NG-001", IP = "10.200.8.1", Description = "PC-01"},
            //        new classHost{ DNS_Name = "WS-NG-002", IP = "10.200.8.2", Description = "PC-02"},
            //        new classHost{ DNS_Name = "WS-NG-003", IP = "10.200.8.3", Description = "PC-03"},
            //        new classHost{ DNS_Name = "WS-NG-004", IP = "10.200.8.4", Description = "PC-04"},
            //        new classHost{ DNS_Name = "WS-NG-005", IP = "10.200.8.5", Description = "PC-05"},
            //        new classHost{ DNS_Name = "WS-NG-006", IP = "10.200.8.6", Description = "PC-06"}
            //    }
            //},
            //new classContainer
            //{
            //    Name = "Servers",
            //    Hosts = new ObservableCollection<classHost>
            //    {
            //        new classHost{ DNS_Name = "r0-m1-sngp", IP = "10.200.128.70", Description = "SNGP Server"},
            //        new classHost{ DNS_Name = "r0-m1-norma1", IP = "10.200.128.73", Description = "NormaCS Server"}
            //    }
            //},
            //new classContainer
            //{
            //    Name="Regions",
            //    Children = new ObservableCollection<classContainer>
            //    {
            //        new classContainer {Name = "RO"},
            //        new classContainer
            //        {
            //            Name="SNGP",
            //            Children = new ObservableCollection<classContainer>
            //            {
            //                new classContainer {Name = "Moscow"},
            //                new classContainer {Name = "Stavropol"},
            //                new classContainer {Name = "Noyabr'sk"},
            //                new classContainer {Name = "Orenburg"}
            //            }
            //        }
            //    }
            //}
        };

        //Path to DataBase
        public static string DatabasePath = "";

        //Variable for fixing changes in the database
        public static bool IsChanged = false;

        //Save settings to registry
        public static void SaveSettings()
        {
            string path = @"Software\IP-Range\";
            Microsoft.Win32.RegistryKey clRegistryKey;
            
            if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path) == null)
            {
                clRegistryKey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(path);
            }
            else clRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path, true);
            
            try
            {
                clRegistryKey.SetValue("DatabasePath", DatabasePath);
                clRegistryKey.Close();
            }
            catch (Exception ex)
            {
                windowMessage.Show(ex.Message);
            }
        }

        //Load settings to registry
        public static void LoadSettings()
        {
            string path = @"Software\IP-Range\";
            Microsoft.Win32.RegistryKey clRegistryKey;
            
            if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path) != null)
            {
                clRegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(path, false);
                
                try
                {
                    DatabasePath = (string)clRegistryKey.GetValue("DatabasePath");
                    if (DatabasePath != null) DeSerialize(DatabasePath);
                }
                catch (Exception ex)
                {
                    windowMessage.Show(ex.Message);
                }
            }
        }

        //Serialize
        public static void Serialize(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<classContainer>));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fs, Containers);
                    IsChanged = false;
                    SaveSettings();
                }
            }
            catch (Exception ex)
            {
                windowMessage.Show(ex.Message);
            }
        }

        //DeSerialize
        public static void DeSerialize(string path)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<classContainer>));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    Containers.Clear();
                    var collection = (ObservableCollection<classContainer>)formatter.Deserialize(fs);
                    foreach (var item in collection) Containers.Add(item);
                    IsChanged = false;
                    SaveSettings();
                }
            }
            catch (Exception ex)
            {
                windowMessage.Show(ex.Message);
            }
        }
    }
}
