using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using TwoTypeExample.Views;
using Xamarin.Forms;

namespace TwoTypeExample.ViewModel
{
    public class MessageListViewModel : BaseMessageViewModel
    {
        public ICommand AddCommand { get; private set; }

        public MessageListViewModel(INavigation navigation)
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageListViewModel() constructor -1- start - timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            _navigation = navigation;

            GetDBConnection dbConnection = new GetDBConnection();

            if (dbConnection == null || dbConnection.Connection == null)
            {
                string errMsg = dbConnection.ConnException.Message;
                App.DatabaseError("MessageListViewModel() constructor", errMsg);
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

                System.Diagnostics.Debug.WriteLine("MessageListViewModel() constructor -2- sqlite connection: " + textPointer);
            }

            _repository = new Repository(dbConnection.Connection);

            AddCommand = new Command(async () => await ShowAddMessage());

            FetchContacts();

            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageListViewModel() constructor -3- end - timestamp: " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        ~MessageListViewModel()
        {
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageListViewModel destructor -1- timestamp: " + test);
            System.Diagnostics.Debug.Flush();

            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("MessageListViewModel destructor -2- post Garb Collect - " + test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        void FetchContacts()
        {
            ContactList = _repository.GetAllContactsData();
            MessageAndContactList = _repository.GetAllMessagesWithContacts();
        }

        async Task ShowAddMessage()
        {
            await _navigation.PushAsync(new AddMessage());
        }

        async Task ShowMessageDetail(int id)
        {
            await _navigation.PushAsync(new DetailsMessagePage(id));
        }

        private MessageInfoWithContact _selectedMessageAndContactItem;
        public MessageInfoWithContact SelectedMessageAndContact
        {
            get => _selectedMessageAndContactItem;
            set
            {
                if (value != null)
                {
                    _selectedMessageAndContactItem = value;
                    NotifyPropertyChanged("SelectedMessageAndContact");
                    ShowMessageDetail(value.MessageId);
                }
            }
        }
    }
}
