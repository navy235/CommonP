﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Maitonn.Core;

namespace CommonP.Setting
{
    public static class ConfigSetting
    {
        public static string Default_AvtarUrl { get; set; }

        public static string DomainUrl { get; set; }

        public static string AllDomainUrl { get; set; }

        public static string BankupPath { get; set; }

        public static string DataBaseName { get; set; }

        public static string DefaultRoute { get; set; }

        static ConfigSetting()
        {
            Default_AvtarUrl = ConfigurationManager.AppSettings["Default_AvtarUrl"];

            DomainUrl = ConfigurationManager.AppSettings["LocalDomain"];

            AllDomainUrl = ConfigurationManager.AppSettings["AllDomain"];

            BankupPath = ConfigurationManager.AppSettings["BankupPath"];

            DataBaseName = ConfigurationManager.AppSettings["DataBaseName"];

            DefaultRoute = "Style2Default";
        }
    }
}