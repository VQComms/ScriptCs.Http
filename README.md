# ScriptCs.Http [![NuGet](http://img.shields.io/nuget/v/ScriptCs.Http.svg?style=flat)](https://www.nuget.org/packages/ScriptCs.Http/)

This is a cross platform ScriptCs library that allows you to make REST style Http requests whilst also providing the ability to use strongly typed objects.

## Usage

`scriptcs -install ScriptCs.Http` 

```
public class Person
{
    public string FirstName{ get; set; }

    public string LastName{ get; set; }

    public string FullName{ get { return FirstName + " " + LastName; } }
}

var req = new Http();
req.FollowRedirects = false;
req.DefaultHeaders = new Dictionary<string,string>{{"Accept", "application/json"}};
req.DefaultCookies = new List<Cookie>(new []{new Cookie()
                {
                    Domain = "localhost",
                    Value = "rocks",
                    Path = "/",
                    Name = "mycookie"
                }});

var resp = req.Get("http://localhost:5678/");
Console.WriteLine(resp.ContentType);

var resp2 = req.Get<List<Person>>("http://localhost:5678/",  new Dictionary<string, string>{ { "Accept", "application/xml" } }, new List<Cookie>(new []{new Cookie()
                {
                    Domain = "localhost",
                    Value = "rocksbetterthantheprevious",
                    Path = "/",
                    Name = "mycookie"
                }}));
Console.WriteLine(resp2.First().FullName);

var resp3 = req.Delete("http://localhost:5678/1");
Console.WriteLine(resp3.StatusCode);

var resp4 = req.Get<Person>("http://localhost:5678/3", new Dictionary<string, string>{ { "Accept", "application/xml" } });
Console.WriteLine(resp4.FullName);

var resp5 = req.Put("http://localhost:5678/1", new Person{ FirstName = "Changed", LastName = "Person" });
Console.WriteLine(resp5.StatusCode);

var resp6 = req.Post("http://localhost:5678/", new Person{ FirstName = "New", LastName = "Person" });
Console.WriteLine(resp6.StatusCode);
```
