﻿using Access2Justice.Shared.Models.Integration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Access2Justice.Api.Tests.TestData
{
    public static class OnboardingInfoBusinessLogicTestData
    {
        public static IEnumerable<object[]> OnboardingInfoTestData()
        {
            yield return new object[] { Guid.Parse("2b281b6f-9668-4771-b16b-c2f10a317aac").ToString(), null, null };
            yield return new object[] { Guid.Parse("2b281b6f-9668-4771-b16b-c2f10a317aac").ToString(), onboardingInfo, userFieldsJObject };
        }
        public static IEnumerable<object[]> PostOnboardingInfoTestData()
        {
            yield return new object[] { StaticOnboardingInfo, null };
            yield return new object[] { StaticOnboardingInfoWebApi, null };
            yield return new object[] { StaticOnboardingInfoEmail, null };
        }
        private static readonly JObject userFieldsJObject = JObject.Parse(@"{'userFields':[{'name':'FirstName','value':'1'},{'name':'Age','value':'10'},{'name':'Telephone','value':'5454633'}],'payloadTemplate':'','deliveryMethod':1,'deliveryDestination':'abc@email.com'}");
        public static OnboardingInfo StaticOnboardingInfo =>
          new OnboardingInfo
          {
              DeliveryDestination = new Uri("http://www.bing.com"),
              DeliveryMethod = DeliveryMethod.Fax,
              PayloadTemplate = "",
              UserFields = { }
          };
        public static OnboardingInfo StaticOnboardingInfoWebApi =>
         new OnboardingInfo
         {
             DeliveryDestination = new Uri("http://www.bing.com"),
             DeliveryMethod = DeliveryMethod.WebApi,             
             PayloadTemplate = JObject.Parse(@"{'FirstName': '','LastName': '','Age': '','Gender': '','Name': '','DateOfBirth': '','Telephone': ''}"),
             UserFields = new List<UserField>() { userField }
         };
        public static OnboardingInfo StaticOnboardingInfoEmail =>
         new OnboardingInfo
         {
             DeliveryDestination = new Uri("http://www.bing.com"),
             DeliveryMethod = DeliveryMethod.Email,
             PayloadTemplate = "<html><body><h1>The following information was submitted through the Legal Navigator portal:</h1><ul>Here are the details submitted as part of the onboarding process.<li>Full Name: @Full Name@</li><li>First Name: @FirstName@</li><li>Last Name: @Last Name@</li><li>Gender: @Gender@</li><li>Age: @Age@</li><li>Telephone: @Telephone@</li></ul></body></html>",             
             UserFields = new List<UserField>() { userField }
         };
        
        private static readonly UserField userField = new UserField
        {
            Name = "FirstName",
            Value = "TestValue"
        };
                
        private static readonly JObject userFields = JObject.Parse(@"{'userFields':[{'name':'FullName','value':'1'},{'name':'Age','value':'10'},{'name':'Telephone','value':'5454633'}]}");

        private static readonly JObject onboardingInfo = JObject.Parse(@"{
        'externalId':'1357',
        'email':'rcpc@rcpcfairbanks.org',
        'availability':null,
        'acceptanceCriteria':{
                            'description':'testdesc',
                            'evaluatedRequirements':'testrequire'
                            },
        'onboardingInfo':{
                        'userFields':[
                        {
                        'name':'FirstName',
                        'value':'1'
                        },
                        {
                        'name':'Age',
                        'value':'10'
                        },
                        {
                        'name':'Telephone',
                        'value':'5454633'
                        }
                        ],
        'deliveryMethod':'Email',
        'deliveryDestination':'abc@email.com',
        'payloadTemplate':''
        },
        'isFormAvailable':true,
        'address':'72626thAvenue,Suite2,Fairbanks,FairbanksNorthStarBorough,AK,99701',
        'telephone':'(907)456-9000Main|(907)451-8125Fax',
        'overview':null,
        'eligibilityInformation':null,
        'reviewer':[],
        'specialties':null,
        'qualifications':null,
        'businessHours':null,
        'id':'50bb98c4-a285-ebe4-a281-3475f03a3d5f',
        'name':'RESOURCECENTERFORPARENTSANDCHILDREN-EmailTesting',
        'resourceCategory':null,
        'description':'Facilitatesamultidisciplinaryapproachtotheinvestigationandtreatmentofchildabusecases',
        'resourceType':'ServiceProviders',
        'url':'www.rcpcfairbanks.org',
        'organizationalUnit':'AK',
        'location':[
        {
        'state':'AK',
        'county':'FairbanksNorthStarBorough',
        'city':'Fairbanks',
        'zipCode':'99701'
        },
        {
        'state':'HI'
        },
        {
        'state':'AK'
        }
        ],
        'topicTags':[
        {
        'id':'647175b5-6852-4316-8bfe-81db007389b3'
        },
        {
        'id':'6cdb3845-8e31-44d1-9175-d4e712ff8706'
        },
        {
        'id':'ba05397b-086e-475b-b5bf-ebac94e98872'
        }
        ]
        }");
    }
}
