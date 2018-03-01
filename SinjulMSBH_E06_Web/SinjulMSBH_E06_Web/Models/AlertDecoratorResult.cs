using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace SinjulMSBH_E06_Web.Models
{
	public class Alert
	{
		public string Command { get; set; }
		public string Message { get; set; }

		public Alert ( string command , string message )
		{
			Command = command; Message = message;
		}
	}

	public static class AlertExtensions
	{
		private const string Alerts = "_Alerts";

		public static List<Alert> GetAlerts ( this ITempDataDictionary tempData )
		{
			if ( !tempData.ContainsKey( Alerts ) )
				tempData[ Alerts ] = new List<Alert>( );

			return ( List<Alert> ) tempData[ Alerts ];
		}

		public static ActionResult WithSuccess ( this ActionResult result , string message ) =>
			 new AlertDecoratorResult( result , "alert alert-success" , message );

		public static ActionResult WithInfo ( this ActionResult result , string message ) =>
			 new AlertDecoratorResult( result , "alert alert-info" , message );

		public static ActionResult WithWarning ( this ActionResult result , string message ) =>
			 new AlertDecoratorResult( result , "alert alert-warning" , message );

		public static ActionResult WithError ( this ActionResult result , string message ) =>
			 new AlertDecoratorResult( result , "alert alert-danger" , message );
	}

	public class AlertDecoratorResult: ActionResult
	{
		public ActionResult InnerResult { get; set; }
		public string Command { get; set; }
		public string Message { get; set; }

		public AlertDecoratorResult ( ActionResult innerResult , string command , string message )
		{
			InnerResult = innerResult; Command = command; Message = message;
		}

		public override async Task ExecuteResultAsync ( ActionContext context )
		{
			var executor = context
			  .HttpContext
			  .RequestServices
			  .GetRequiredService<ITempDataDictionaryFactory>().GetTempData(context.HttpContext);
			//await executor.ExecuteAsync( context , this );

			var alerts2 = executor.GetAlerts();
			alerts2.Add( new Alert( Command , Message ) );

			//ITempDataDictionaryFactory factory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
			//ITempDataDictionary tempData = factory.GetTempData(context.HttpContext);

			//var alerts = tempData.GetAlerts();
			//alerts.Add( new Alert( Command , Message ) );

			await InnerResult.ExecuteResultAsync( context );
		}
	}
}