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

var cookieContainer = new CookieContainer();
cookieContainer.Add(new Cookie()
                {
                    Domain = "localhost",
                    Value = "rocks",
                    Name = "mycookie",
                    Path = "/",
                }
            );

var resp = req.Get<List<Person>>("http://localhost:5678/", new Dictionary<string, string>{ { "Accept", "application/json" } }, cookieContainer);
Console.WriteLine(resp.First().FullName);

var resp2 = req.Delete("http://localhost:5678/1");
Console.WriteLine(resp2.StatusCode);

var resp3 = req.Get<Person>("http://localhost:5678/3", new Dictionary<string, string>{ { "Accept", "application/xml" } });
Console.WriteLine(resp3.FullName);

var resp4 = req.Put("http://localhost:5678/1", new Person{ FirstName = "Changed", LastName = "Person" });
Console.WriteLine(resp4.StatusCode);

var resp5 = req.Post("http://localhost:5678/", new Person{ FirstName = "New", LastName = "Person" });
Console.WriteLine(resp5.StatusCode);

var resp = req.Get("http://localhost:5678/");
Console.WriteLine(resp.ContentType);
```
