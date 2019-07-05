using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TwoTypeExample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static void GarbCollect()
        {
            GC.Collect();
            GC.Collect();

            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("GarbCollect() - " + test);
            System.Diagnostics.Debug.Flush();
        }

        public static void DatabaseError(string errorLocation, string errorMessage)
        {
            System.Diagnostics.Debug.WriteLine("DatabaseError() - " +
                " Database connection failed. Failure location: " +
                errorLocation);

            //If this isn't done, then the display will get an "Object reference not set to an instance of an object."
            Application.Current.MainPage = new Page();

            // Pop up error message to user
            Device.BeginInvokeOnMainThread(async () =>
            { await Application.Current.MainPage.DisplayAlert("Database Error", "Could not access the database.\nPlease restart this app; if that doesn't work please try to restart your device. Error:\n" + errorMessage, "Ok"); });
        }

    }
}
