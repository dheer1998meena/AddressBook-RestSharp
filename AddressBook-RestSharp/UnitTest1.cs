// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Dheer Singh Meena"/>
// --------------------------------------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace AddressBook_RestSharp
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:4000");
        }
        private IRestResponse GetContactList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            RestRequest request = new RestRequest("/contacts/list", Method.GET);
            //Act
            // Execute the request
            RestSharp.IRestResponse response = client.Execute(request);
            return response;
        }
        /// <summary>
        /// UC22 Ability to reads the entries from json server.
        /// </summary>
        [TestMethod]
        public void ReadEntriesFromJsonServer()
        {
            IRestResponse response = GetContactList();
            /// Check if the status code of response equals the default code for the method requested
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            /// Convert the response object to list of employees
            List<Contact> employeeList = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(3, employeeList.Count);
            foreach (Contact c in employeeList)
            {
                Console.WriteLine($"Id: {c.Id}\tFullName: {c.FirstName} {c.LastName}\tPhoneNo: {c.PhoneNumber}\tAddress: {c.Address}\tCity: {c.City}\tState: {c.State}\tZip: {c.Zip}\tEmail: {c.Email}");
            }
        }
    }
}
