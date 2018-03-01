using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SinjulMSBH_E06_Web.Models;

namespace SinjulMSBH_E06_Web.Controllers
{
	public class HomeController: Controller
	{
		[AcceptVerbs( "Get" , "Post" )]
		public IActionResult SinjulMSBHDetails ( ) =>
			 View( )
				.WithSuccess( "SinjulMSBH WithSuccess .. !!!!" )
				.WithError( "SinjulMSBH WithError .. !!!!" )
				.WithInfo( "SinjulMSBH WithInfo .. !!!!" )
				.WithWarning( "SinjulMSBH WithWarning .. !!!!" )
				;

		public IActionResult Index ( )
		{
			return View( );
		}

		public IActionResult About ( )
		{
			ViewData[ "Message" ] = "Your application description page.";

			return View( );
		}

		public IActionResult Contact ( )
		{
			ViewData[ "Message" ] = "Your contact page.";

			return View( );
		}

		public IActionResult Error ( )
		{
			return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
		}
	}
}