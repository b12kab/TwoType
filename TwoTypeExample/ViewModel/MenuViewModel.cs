using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using TwoTypeExample.Views;
using Xamarin.Forms;

namespace TwoTypeExample.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public ICommand ContactsCommand { get; private set; }
        public ICommand MessageCommand { get; private set; }
        public INavigation _navigation;

        public MenuViewModel(INavigation navigation)
        {
            _navigation = navigation;
            ContactsCommand = new Command(async () => await ContactsList());
            MessageCommand = new Command(async () => await MessageList());
        }

        async Task ContactsList()
        {
            await _navigation.PushAsync(new ContactList());
        }

        async Task MessageList()
        {
            await _navigation.PushAsync(new MessageList());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
