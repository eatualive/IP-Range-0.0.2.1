using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP_Range
{
    public static class classTools
    {
        // List of Containers
        public static ObservableCollection<classContainer> Containers = new ObservableCollection<classContainer>
        {
            new classContainer
            {
                Name = "Users",
                Hosts = new ObservableCollection<classHost>
                {
                    new classHost{ DNS_Name = "WS-NG-777", IP = "10.200.8.1", Description = "Сороковиков Виталий Юрьевич"},
                    new classHost{ DNS_Name = "WS-NG-007", IP = "10.200.8.5", Description = "Пятаков Виктор Викторович"}
                }
            },
            new classContainer
            {
                Name = "Servers",
                Hosts = new ObservableCollection<classHost>
                {
                    new classHost{ DNS_Name = "r0-m1-sngp", IP = "10.200.128.70", Description = "Сервер СНГП"},
                    new classHost{ DNS_Name = "r0-m1-norma1", IP = "10.200.128.73", Description = "Сервер NormaCS"}
                }
            },
            new classContainer
            {
                Name="Regions",
                Children = new ObservableCollection<classContainer>
                {
                    new classContainer {Name = "RO"},
                    new classContainer
                    {
                        Name="СНГП",
                        Children = new ObservableCollection<classContainer>
                        {
                            new classContainer {Name = "Москва"},
                            new classContainer {Name = "Ставрополь"},
                            new classContainer {Name = "Ноябрьск"},
                            new classContainer {Name = "Оренбург"}
                        }
                    }
                }
            },





            new classContainer
            {
                Name = "Users",
                Hosts = new ObservableCollection<classHost>
                {
                    new classHost{ DNS_Name = "WS-NG-777", IP = "10.200.8.1", Description = "Сороковиков Виталий Юрьевич"},
                    new classHost{ DNS_Name = "WS-NG-007", IP = "10.200.8.5", Description = "Пятаков Виктор Викторович"}
                }
            },
            new classContainer
            {
                Name = "Servers",
                Hosts = new ObservableCollection<classHost>
                {
                    new classHost{ DNS_Name = "r0-m1-sngp", IP = "10.200.128.70", Description = "Сервер СНГП"},
                    new classHost{ DNS_Name = "r0-m1-norma1", IP = "10.200.128.73", Description = "Сервер NormaCS"}
                }
            },
            new classContainer
            {
                Name="Regions",
                Children = new ObservableCollection<classContainer>
                {
                    new classContainer {Name = "RO"},
                    new classContainer
                    {
                        Name="СНГП",
                        Children = new ObservableCollection<classContainer>
                        {
                            new classContainer {Name = "Москва"},
                            new classContainer {Name = "Ставрополь"},
                            new classContainer {Name = "Ноябрьск"},
                            new classContainer {Name = "Оренбург"}
                        }
                    }
                }
            }
        };
    }
}
