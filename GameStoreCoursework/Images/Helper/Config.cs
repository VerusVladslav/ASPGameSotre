using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GameStoreCoursework.Images.Helper
{
    public static class Config
    {
        public static string GameImagePath { get { return ConfigurationManager.AppSettings["GameImagePath"]; } }
        public static string UserImagePath { get { return ConfigurationManager.AppSettings["UserImagePath"]; } }

        public static string Domain { get { return ConfigurationManager.AppSettings[" DomainProject"]; } }

    }
}