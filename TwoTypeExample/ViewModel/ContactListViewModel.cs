using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using TwoTypeExample.Views;
using Xamarin.Forms;

namespace TwoTypeExample.ViewModel
{
    public class ContactListViewModel : BaseContactViewModel
    {
        public ICommand AddCommand { get; private set; }

        public ContactListViewModel(INavigation navigation)
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("ContactListViewModel() constructor -1- start - timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            _navigation = navigation;

            GetDBConnection dbConnection = new GetDBConnection();

            if (dbConnection == null || dbConnection.Connection == null)
            {
                string errMsg = dbConnection.ConnException.Message;
                App.DatabaseError("ContactListViewModel() constructor", errMsg);
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
                        textPointer = dbConnection.Connection.Handle.ptr.ToString();
                    }
                }

                System.Diagnostics.Debug.WriteLine("ContactListViewModel() constructor -2- sqlite connection: " + textPointer);
            }

            _repository = new Repository(dbConnection.Connection);

            AddCommand = new Command(async () => await ShowAddContact());

            FetchContacts();

            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("ContactListViewModel() constructor -3- end - timestamp: " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        ~ContactListViewModel()
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("ContactListViewModel destructor -1- timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("ContactListViewModel destructor -2- post Garb Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        void FetchContacts()
        {
            ContactList = _repository.GetAllContactsData();
        }

        async Task ShowAddContact()
        {
            await _navigation.PushAsync(new AddContact());
        }

        async Task ShowContactDetails(int selectedContactID)
        {
            await _navigation.PushAsync(new DetailsContactPage(selectedContactID));
        }

        ContactInfo _selectedContactItem;
        public ContactInfo SelectedContactItem
        {
            get => _selectedContactItem;
            set
            {
                if (value != null)
                {
                    _selectedContactItem = value;
                    NotifyPropertyChanged("SelectedContactItem");
                    ShowContactDetails(value.Id);
                }
            }
        }
    }
}
