using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SinjulMSBH_E06_Web.Models;

namespace SinjulMSBH_E06_Web.Controllers
{
	public class HomeController: Controller
	{
		public IActionResult SinjulMSBHDetails ( ) => View( );

		public IActionResult OpenUrlWithRedirectTempdataInOtherAction ( )
		{
			return RedirectToAction( nameof( SinjulMSBHDetails ) )
			.WithSuccess( "SinjulMSBH WithSuccess .. !!!!" , "SinjulMSBH Body WithSuccess Message .. !!!!" )
			.WithDanger( "SinjulMSBH WithDanger .. !!!!" , "SinjulMSBH Body WithDanger Message .. !!!!" )
			.WithInfo( "SinjulMSBH WithInfo .. !!!!" , "SinjulMSBH Body WithInfo Message .. !!!!" )
			.WithWarning( "SinjulMSBH WithWarning .. !!!!" , "SinjulMSBH Body WithWarning Message .. !!!!" )
			;
		}

		public IActionResult Index ( ) => View( );

		public IActionResult About ( )
		{
			ViewData[ "Message" ] = "Your application description page.";

			return RedirectToAction( "Contact" );
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