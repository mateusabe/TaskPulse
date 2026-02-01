using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskPulse.Tests.API.Controllers
{
    [TestFixture]
public class TasksControllerTests
{
    private TaskApiFactory _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _factory = new TaskApiFactory();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task Post_ShouldCreateTask()
    {
        var form = new MultipartFormDataContent
        {
            { new StringContent("Task teste"), "Title" },
            { new StringContent("2"), "SlaHours" }
        };

        var response = await _client.PostAsync("api/v1/Tasks", form);
            Console.Write(response.Content);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }
}
}
