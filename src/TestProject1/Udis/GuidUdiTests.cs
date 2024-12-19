using Limbo.Umbraco.MigrationsClient.Models.Umbraco;

namespace TestProject1.Udis;

[TestClass]
public class GuidUdiTests {

    [TestMethod]
    public void Equals() {

        GuidUdi udi1 = new("document", "357098af-905b-4292-8b9e-e2735dc371f2");
        GuidUdi udi2 = new("document", "357098af-905b-4292-8b9e-e2735dc371f2");
        GuidUdi udi3 = new("document", "3d701d37-bf46-41f0-9807-9360755960eb");
        GuidUdi udi4 = new("media", "3d701d37-bf46-41f0-9807-9360755960eb");

        Assert.AreEqual(udi1, udi2, "#1");
        Assert.AreNotEqual(udi1, udi3, "#2");
        Assert.AreNotEqual(udi3, udi4, "#3");

    }

    [TestMethod]
    public void EqualsOperator() {

        GuidUdi udi1 = new("document", "357098af-905b-4292-8b9e-e2735dc371f2");
        GuidUdi udi2 = new("document", "357098af-905b-4292-8b9e-e2735dc371f2");
        GuidUdi udi3 = new("document", "3d701d37-bf46-41f0-9807-9360755960eb");
        GuidUdi udi4 = new("media", "3d701d37-bf46-41f0-9807-9360755960eb");

        Assert.IsTrue(udi1 == udi2, "#1");
        Assert.IsFalse(udi1 == udi3, "#2");
        Assert.IsFalse(udi3 == udi4, "#3");

    }

}