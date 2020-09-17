using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GameStoreUI.Images.Helper
{
   
        public static class Config
        {
            public static string GameImagePath { get { return ConfigurationManager.AppSettings["GameImagePath"]; } }
            public static string Domain { get { return ConfigurationManager.AppSettings[" DomainProject"]; } }

        }
    
}