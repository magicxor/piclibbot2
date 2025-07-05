namespace PicLibBot.Tests;

[TestFixture]
[Parallelizable(scope: ParallelScope.All)]
internal class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        Assert.Pass();
    }
}
