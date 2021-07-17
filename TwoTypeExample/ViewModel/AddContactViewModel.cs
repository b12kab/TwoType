using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using TwoTypeExample.Validator;
using TwoTypeExample.Views;
using Xamarin.Forms;

namespace TwoTypeExample.ViewModel
{
    public class AddContactViewModel : BaseContactViewModel
    {
        public ICommand AddContactCommand { get; private set; }

        public AddContactViewModel(INavigation navigation)
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddContactViewModel() constructor -1- start - timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            _navigation = navigation;
            _contactValidator = new ContactValidator();
            _contact = new ContactInfo();

            GetDBConnection dbConnection = new GetDBConnection();

            if (dbConnection == null || dbConnection.Connection == null)
            {
                string errMsg = dbConnection.ConnException.Message;
                App.DatabaseError("AddContactViewModel() constructor", errMsg);
                return;
            }
            else
            {
                string textPointer = "pointer is null";
                if (dbConnection.Connection != null)
                {
                    if (dbConnection.Connection.Handle == null)
                    {
                        textPointer = "handle is null";
                    }
                    else
                    {
                        textPointer = dbConnection.Connection.Handle.ToString();
                    }
                }

                System.Diagnostics.Debug.WriteLine("AddContactViewModel() constructor -2- sqlite connection: " + textPointer);
            }

            _repository = new Repository(dbConnection.Connection);

            AddContactCommand = new Command(async () => await AddContact());

            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddContactViewModel() constructor -3- end - timestamp: " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        ~AddContactViewModel()
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddContactViewModel destructor -1- timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("AddContactViewModel destructor -2- post Garb Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        async Task AddContact()
        {
            var validationResults = _contactValidator.Validate(_contact);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Contact", "Do you want to save Contact details?", "OK", "Cancel");
                if (isUserAccept)
                {
                    _repository.InsertContact(_contact);
                    await _navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Contact", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }

        async Task ShowContactList()
        {
            await _navigation.PushAsync(new ContactList());
        }
    }
}