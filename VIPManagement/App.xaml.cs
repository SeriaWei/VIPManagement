using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Easy.Extend;

namespace VIPManagement
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Type moduleType = typeof(Easy.IOCAdapter.IModule);
            AppDomain.CurrentDomain.GetAssemblies().Each(m =>
            {
                if (m.GetName().Name == "VIP.Core")
                {
                    m.GetTypes().Where(t => moduleType.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface).Each(t => (Easy.Loader.CreateInstance(t) as Easy.IOCAdapter.IModule).Load());
                }
            });
            new Easy.Modules.DataDictionary.DataDicttionaryModule().Load();
            new Easy.Modules.MutiLanguage.MutiLanguageModule().Load();
        }
    }
}
