using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using TwoTypeExample.Helpers;
using TwoTypeExample.Models;
using TwoTypeExample.Services;
using Xunit;

namespace RepoTest
{
    public class TestContactInfo
    {
        [Fact]
        public void ValidateContactInfo()
        {
            SQLiteConnection connection = GetConnection();

            Repository repo = new Repository(connection);

            var contact = new ContactInfo()
            {
                Name = "hello",
                Age = "22",
                Gender = "Male",
                DOB = DateTime.UtcNow,
                Address = "123 main st.",
                MobileNumber = "1234567890"
            };

            bool worked = repo.DeleteAllContacts();

            if (worked)
            {
                Assert.True(worked, "Delete all contacts failed");
            }

            worked = repo.InsertContact(contact);

            if (worked)
            {
                Assert.True(worked, "Insert failed");
            }

            List<ContactInfo> list = repo.GetAllContactsData();

            Assert.True(list.Count == 1, "contacts count != 1");

            ContactInfo listContactInfo;

            Assert.NotNull(list[0]);

            try
            {
                listContactInfo = list[0];

                Assert.True(listContactInfo.Name.Equals(contact.Name), "contact name does not match");
                Assert.True(listContactInfo.Age.Equals(contact.Age), "contact age does not match");
                Assert.True(listContactInfo.Gender.Equals(contact.Gender), "contact gender does not match");
                Assert.True(listContactInfo.DOB.Equals(contact.DOB), "contact DOB does not match");
                Assert.True(listContactInfo.Address.Equals(contact.Address), "contact address does not match");
                Assert.True(listContactInfo.MobileNumber.Equals(contact.MobileNumber), "contact mobile # does not match");
            }
            catch (Exception e)
            {
                Assert.False(true, "list[0] assignment failed: " + e.Message);
            }
        }

        public SQLiteConnection GetConnection()
        {
            string thisPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            var path = Path.Combine(thisPath, DatabaseHelper.DbFileName);

            return new SQLiteConnection(path);
        }
    }
}
