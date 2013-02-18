using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Feng;

namespace CarTrackService
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();

            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(new Microsoft.Practices.ServiceLocation.ServiceLocatorProvider(
                delegate()
                {
                    var p = Feng.DefaultServiceProvider.Instance;
                    p.EnableLog();
                    p.EnableNHibernate();
                    p.EnableScript();

                    p.SetDefaultService<ICache>(new HashtableCache());
                    IDataBuffers bufs = new DataBuffers();
                    bufs.AddDataBuffer(new Cache());
                    bufs.AddDataBuffer(DBDataBuffer.Instance);
                    p.SetDefaultService<IDataBuffers>(bufs);

                    p.SetDefaultService<IApplicationDirectory>(new Feng.Windows.WindowsDirectory());
                    p.SetDefaultService<IExceptionProcess>(new LoggerExceptionProcess());
                    p.SetDefaultService<IManagerFactory>(new Feng.Windows.Utils.WinFormManagerFactory());
                    return p;
                }));

            IDataBuffers db = ServiceProvider.GetService<IDataBuffers>();
            if (db != null)
            {
                db.LoadData();
            }

            //System.Web.Mvc.MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}