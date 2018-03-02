using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace SinjulMSBH_E06_Web.Models
{
	public class AlertDecoratorResult: IActionResult
	{
		public IActionResult Result { get; }
		public string Type { get; }
		public string Title { get; }
		public string Body { get; }

		public AlertDecoratorResult ( IActionResult result , string type , string title , string body )
		{
			Result = result;
			Type = type;
			Title = title;
			Body = body;
		}

		public async Task ExecuteResultAsync ( ActionContext context )
		{
			var factory = context.HttpContext.RequestServices.GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
			var tempData = factory.GetTempData(context.HttpContext);

			tempData.AddAlert( new SinjulMSBH( Type , Title , Body ) );

			await Result.ExecuteResultAsync( context );
		}
	}

	public class SinjulMSBH
	{
		public string Type { get; }
		public string Title { get; }
		public string Body { get; }

		public SinjulMSBH ( string type , string title , string body )
		{
			Type = type;
			Title = title;
			Body = body;
		}
	}

	public static class AlertExtensions
	{
		public static IActionResult WithSuccess ( this IActionResult result , string title , string body ) =>
			 Alert( result , "alert alert-success" , title , body );

		public static IActionResult WithInfo ( this IActionResult result , string title , string body ) =>
			 Alert( result , "alert alert-info" , title , body );

		public static IActionResult WithWarning ( this IActionResult result , string title , string body ) =>
			 Alert( result , "alert alert-warning" , title , body );

		public static IActionResult WithDanger ( this IActionResult result , string title , string body ) =>
		 	 Alert( result , "alert alert-danger" , title , body );

		public static List<SinjulMSBH> GetAlertList ( this ITempDataDictionary tempData , string value ) =>
			 value != null ? JsonConvert.DeserializeObject<List<SinjulMSBH>>( value ) : null;

		private const string Alerts = "sinjulmsbhall";

		public static List<SinjulMSBH> GetAlert ( this ITempDataDictionary tempData )
		{
			CreateAlertTempData( tempData );
			return DeserializeAlerts( tempData[ Alerts ] as string );
		}

		public static void CreateAlertTempData ( this ITempDataDictionary tempData )
		{
			if ( !tempData.ContainsKey( Alerts ) )
			{
				tempData[ Alerts ] = "";
			}
		}

		public static void AddAlert ( this ITempDataDictionary tempData , SinjulMSBH alert )
		{
			if ( alert == null )
			{
				throw new ArgumentNullException( nameof( alert ) );
			}
			var deserializeAlertList = tempData.GetAlert();
			deserializeAlertList.Add( alert );
			tempData[ Alerts ] = SerializeAlerts( deserializeAlertList );
		}

		public static string SerializeAlerts ( List<SinjulMSBH> tempData )
		{
			return JsonConvert.SerializeObject( tempData );
		}

		public static List<SinjulMSBH> DeserializeAlerts ( string tempData )
		{
			if ( tempData.Length == 0 )
			{
				return new List<SinjulMSBH>( );
			}
			return JsonConvert.DeserializeObject<List<SinjulMSBH>>( tempData );
		}

		private static IActionResult Alert ( IActionResult result , string type , string title , string body ) =>
	 		 new AlertDecoratorResult( result , type , title , body );
	}
}