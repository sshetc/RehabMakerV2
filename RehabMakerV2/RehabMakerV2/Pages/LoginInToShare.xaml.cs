using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RehabMakerV2.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginInToShare : ContentPage
	{
		public LoginInToShare ()
		{
			InitializeComponent ();
            LogoRM.Source = ImageSource.FromResource("RehabMakerV2.Picture.logo_svg.png");
            LogItSh.Source = ImageSource.FromResource("RehabMakerV2.Picture.LoginInToShare.png");
            RehabMker.Source = ImageSource.FromResource("RehabMakerV2.Picture.RehabMaker.png");
        }
    }
}