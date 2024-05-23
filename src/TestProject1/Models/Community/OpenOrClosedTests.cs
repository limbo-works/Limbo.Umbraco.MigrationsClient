using Limbo.Umbraco.MigrationsClient.Models.Community.OpenOrClosed;
using Skybrud.Essentials.Json.Newtonsoft;

namespace TestProject1.Models.Community;

[TestClass]
public class OpenOrClosedTests {

    /// <remarks>
    /// The JSON in this example is based on a saved value from one of our Umbraco 8 sites, which is currently using
    /// version <c>0.1.5</c> of the <c>OpenOrClosed</c> package.
    /// </remarks>
    [TestMethod]
    public void ParseModel() {

        const string json = """
                      [
                        {
                          "id": "764dc03a-6f77-4607-b7b6-f650200a39a5",
                          "dayoftheweek": "Monday",
                          "isOpen": true,
                          "hoursOfBusiness": [
                            {
                              "opensAt": "10:00:00",
                              "closesAt": "15:00:00",
                              "id": "64bc3620-39ac-45d1-9fe6-45e76614cdcc"
                            }
                          ]
                        },
                        {
                          "id": "b138f333-b366-4f8a-891f-ea5d04e948b5",
                          "dayoftheweek": "Tuesday",
                          "isOpen": true,
                          "hoursOfBusiness": [
                            {
                              "opensAt": "10:00:00",
                              "closesAt": "15:00:00",
                              "id": "e6a7e40b-decb-417a-8fd5-e1ecc3a8ddf9"
                            }
                          ]
                        },
                        {
                          "id": "2238d1fa-4d27-4b37-a12a-173518d0d105",
                          "dayoftheweek": "Wednesday",
                          "isOpen": false,
                          "hoursOfBusiness": []
                        },
                        {
                          "id": "28020ef9-698a-4926-b5c4-b0a98fa15feb",
                          "dayoftheweek": "Thursday",
                          "isOpen": true,
                          "hoursOfBusiness": [
                            {
                              "opensAt": "10:00:00",
                              "closesAt": "16:30:00",
                              "id": "69826beb-f8f3-4802-b612-5476f8cd5796"
                            }
                          ]
                        },
                        {
                          "id": "0f818d67-10a7-41ed-a2eb-ae5a4bbd213e",
                          "dayoftheweek": "Friday",
                          "isOpen": true,
                          "hoursOfBusiness": [
                            {
                              "opensAt": "10:00:00",
                              "closesAt": "13:00:00",
                              "id": "ee4a0497-de53-47bc-b634-940633ace8a5"
                            }
                          ]
                        },
                        {
                          "id": "2667efc2-6747-4a2a-a758-f934b0ec6952",
                          "dayoftheweek": "Saturday",
                          "isOpen": false,
                          "hoursOfBusiness": []
                        },
                        {
                          "id": "b3cc2ff7-ac75-47e8-a615-567f9d8f6b72",
                          "dayoftheweek": "Sunday",
                          "isOpen": false,
                          "hoursOfBusiness": []
                        }
                      ]
                      """;

        OpenOrClosedModel model = OpenOrClosedModel.Parse(JsonUtils.ParseJsonArray(json));

        Assert.AreEqual(7, model.Count);

        Assert.AreEqual("764dc03a-6f77-4607-b7b6-f650200a39a5", model[0].Id.ToString());
        Assert.AreEqual(DayOfWeek.Monday, model[0].DayOfTheWeek);
        Assert.AreEqual(true, model[0].IsOpen);
        Assert.AreEqual(1, model[0].HoursOfBusiness.Count);
        Assert.AreEqual("64bc3620-39ac-45d1-9fe6-45e76614cdcc", model[0].HoursOfBusiness[0].Id.ToString());
        Assert.AreEqual("10:00:00", model[0].HoursOfBusiness[0].OpensAt.ToString());
        Assert.AreEqual("15:00:00", model[0].HoursOfBusiness[0].ClosesAt.ToString());

    }

}