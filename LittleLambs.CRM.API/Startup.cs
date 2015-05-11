using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(LittleLambs.CRM.API.Startup))]
namespace LittleLambs.CRM.API
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();
			WebApiConfig.Register(config);
			app.UseAutofac(config);
			app.UseCors(CorsOptions.AllowAll);
			app.UseWebApi(config);
		}
	}
}