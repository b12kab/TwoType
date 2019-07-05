using System;
using System.Collections.Generic;
using SQLite;
using TwoTypeExample.Helpers;
using TwoTypeExample.Models;
using Xamarin.Forms;

namespace TwoTypeExample.Services
{
    public class Repository : IRepository
    {
        DatabaseHelper _databaseHelper;
        SQLiteConnection sqliteConnection;
        private string originalPointer = "is null";

        public Repository(SQLiteConnection connection)
        {
            if (originalPointer.Contains("is null"))
            {
                if (connection.Handle != null)
                {
                    originalPointer = connection.Handle.ptr.ToString();
                }
            }

            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("Repository() constructor start - " +
                test + " - sqliteconnection ptr: " + originalPointer);
            System.Diagnostics.Debug.Flush();

            sqliteConnection = connection;
            _databaseHelper = new DatabaseHelper(sqliteConnection);

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("Repository() constructor completed - " +
                test);
            System.Diagnostics.Debug.Flush();
        }

        ~Repository()
        {
            DateTime date = DateTime.Now;
            var test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("Repository() destructor -1- - " +
                test);
            System.Diagnostics.Debug.WriteLine("Repository() destructor -2- sqliteconnection pointer original: "
                + originalPointer);
            System.Diagnostics.Debug.WriteLine("Repository() destructor -3- sqliteconnection pointer current : "
                + GetConnectionHandlePtr());
            System.Diagnostics.Debug.Flush();

            //https://www.dotnetforall.com/how-to-use-dispose-and-finalize-csharp/

            System.Diagnostics.Debug.WriteLine("Repository() destructor -4- in txn: " + sqliteConnection.IsInTransaction);
            System.Diagnostics.Debug.Flush();

            //if (sqliteConnection != null)
            //{
            //    if (sqliteConnection.Handle != null)
            //    {
            //        date = DateTime.Now;
            //        test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            //        System.Diagnostics.Debug.WriteLine("Repository() destructor -5- handle is not null - " +
            //            test);
            //        System.Diagnostics.Debug.Flush();

            //        sqliteConnection.Close();
            //        date = DateTime.Now;
            //        test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            //        System.Diagnostics.Debug.WriteLine("Repository() destructor -6- POST close - " +
            //            test);
            //        System.Diagnostics.Debug.Flush();
            //    }
            //}

            //_sqliteconnection.Dispose();
            //date = DateTime.Now; 
            //test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            //System.Diagnostics.Debug.WriteLine("Repository() destructor -7- POST dispose - " +
            //    test);
            //System.Diagnostics.Debug.Flush();

            // This should dispose of the connection as part of the cleanup
            sqliteConnection = null;

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("Repository() destructor -8- POST null, pre garb - " +
                test);
            System.Diagnostics.Debug.Flush();
            App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("Repository() destructor -9- POST GC #1 - " +
                test);
            System.Diagnostics.Debug.Flush();

            _databaseHelper = null;
            //App.GarbCollect();

            date = DateTime.Now;
            test = date.ToString("yyyy-MM-dd H:mm:ss.fffffffzzz");
            System.Diagnostics.Debug.WriteLine("Repository() destructor -A- POST _databaseHelper null - " +
                test);
            System.Diagnostics.Debug.WriteLine("==================================");
            System.Diagnostics.Debug.Flush();
        }

        public string GetConnectionHandlePtr()
        {
            string pointerText = " pointer is null?";
            if (sqliteConnection != null)
            {
                if (sqliteConnection.Handle != null)
                {
                    pointerText = sqliteConnection.Handle.ptr.ToString();
                }
                else
                {
                    pointerText = " handle is null?";
                }
            }

            return pointerText;
        }

        public bool DeleteContact(int contactID)
        {
            return _databaseHelper.DeleteContact(sqliteConnection, contactID);
        }

        public List<ContactInfo> GetAllContactsData()
        {
            return _databaseHelper.GetAllContactsData(sqliteConnection);
        }

        public IList<int> GetAllContactKeys()
        {
            return _databaseHelper.GetAllContactKeys(sqliteConnection);
        }

        public ContactInfo GetContactData(int contactID)
        {
            return _databaseHelper.GetContactData(sqliteConnection, contactID);
        }

        public bool InsertContact(ContactInfo contact)
        {
            if (contact != null)
            {
                return _databaseHelper.InsertContact(sqliteConnection, contact);
            }

            return false;
        }

        public bool UpdateContact(ContactInfo contact)
        {
            if (contact != null)
            {
                return _databaseHelper.UpdateContact(sqliteConnection, contact);
            }

            return false;
        }

        public bool DeleteAllContacts()
        {
            return _databaseHelper.DeleteAllContacts(sqliteConnection);
        }

        //---------------------------------------------

        public IList<MessageInfo> GetAllMessages()
        {
            return _databaseHelper.GetAllMessages(sqliteConnection);
        }

        public MessageInfo GetMessage(int messageID)
        {
            return _databaseHelper.GetMessage(sqliteConnection, messageID);
        }

        public bool DeleteMessage(int id)
        {
            return _databaseHelper.DeleteMessage(sqliteConnection, id);
        }

        public bool InsertMessage(MessageInfo messageInfo)
        {
            if (messageInfo != null)
            {
                return _databaseHelper.InsertMessage(sqliteConnection, messageInfo);
            }

            return false;
        }

        public bool UpdateMessage(MessageInfo messageInfo)
        {
            if (messageInfo != null)
            {
                return _databaseHelper.UpdateMessage(sqliteConnection, messageInfo);
            }

            return false;
        }

        public IList<MessageInfoWithContact> GetAllMessagesWithContacts()
        {
            return _databaseHelper.GetAllMessagesWithContacts(sqliteConnection);
        }

        public MessageInfoWithContact GetMessageWithContacts(int messageID)
        {
            return _databaseHelper.GetMessageWithContacts(sqliteConnection, messageID);
        }
    }
}
