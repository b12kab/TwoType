using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using TwoTypeExample.Validator;
using Xamarin.Forms;

namespace TwoTypeExample.ViewModel
{
    public class DetailsContactViewModel : BaseContactViewModel
    {
        public ICommand UpdateContactCommand { get; private set; }
        public ICommand DeleteContactCommand { get; private set; }

        public DetailsContactViewModel(INavigation navigation, int selectedContactID)
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactViewModel() constructor -1- start - timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            _navigation = navigation;
            _contactValidator = new ContactValidator();
            _contact = new ContactInfo();
            _contact.Id = selectedContactID;

            GetDBConnection dbConnection = new GetDBConnection();

            if (dbConnection == null || dbConnection.Connection == null)
            {
                string errMsg = dbConnection.ConnException.Message;
                App.DatabaseError("DetailsContactViewModel() constructor", errMsg);
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

                System.Diagnostics.Debug.WriteLine("DetailsContactViewModel() constructor -2- sqlite connection: " + textPointer);
            }

            _repository = new Repository(dbConnection.Connection);

            UpdateContactCommand = new Command(async () => await UpdateContact());
            DeleteContactCommand = new Command(async () => await DeleteContact());

            FetchContactDetails();

            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactViewModel() constructor -3- end - timestamp: " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        ~DetailsContactViewModel()
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactViewModel destructor -1- timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("DetailsContactViewModel destructor -2- post Garb Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        void FetchContactDetails()
        {
            _contact = _repository.GetContactData(_contact.Id);
        }

        async Task UpdateContact()
        {
            var validationResults = _contactValidator.Validate(_contact);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Contact Details", "Update Contact Details", "OK", "Cancel");
                if (isUserAccept)
                {
                    _repository.UpdateContact(_contact);
                    await _navigation.PopAsync();
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Contact Details", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }

        async Task DeleteContact()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Contact Details", "Delete Contact Details", "OK", "Cancel");
            if (isUserAccept)
            {
                _repository.DeleteContact(_contact.Id);
                await _navigation.PopAsync();
            }
        }
    }
}
