using RestSharp;
using System.Collections.Generic;
using System.Net;

public bool FollowRedirects {get; set;}
public IDictionary<string, string> DefaultHeaders {get; set;}
public IList<Cookie> DefaultCookies {get; set;}

public IRestResponse Get(string url, IDictionary<string, string> headers = null, IList<Cookie> cookies = null)
{
    var result = CreateRequest(url, Method.GET, headers, cookies);
    var response = result.Item1.Execute(result.Item2);
    return response;
}

public T Get<T>(string url, IDictionary<string, string> headers = null, IList<Cookie> cookies = null) where T: new()
{
    var result = CreateRequest(url, Method.GET, headers, cookies);
    var response = result.Item1.Execute<T>(result.Item2);
    return response.Data;
}

public IRestResponse Delete(string url, IDictionary<string, string> headers = null, IList<Cookie> cookies = null)
{
    var result = CreateRequest(url, Method.DELETE, headers, cookies);
    var response = result.Item1.Execute(result.Item2);
    return response;
}

public IRestResponse Put<T>(string url, T data, IDictionary<string, string> headers = null, IList<Cookie> cookies = null)
{
    var result = CreateRequest(url, Method.PUT, headers, cookies);

    result.Item2.AddBody(data);

    var response = result.Item1.Execute(result.Item2);

    return response;
}

public IRestResponse Post<T>(string url, T data, IDictionary<string, string> headers = null, IList<Cookie> cookies = null)
{
    var result = CreateRequest(url, Method.POST, headers, cookies);

    result.Item2.AddBody(data);

    var response = result.Item1.Execute(result.Item2);

    return response;
}


private Tuple<RestClient,RestRequest> CreateRequest(string url, Method method, IDictionary<string, string> headers = null, IList<Cookie> cookies = null)
{
    var uri = new Uri(url);
    var host = uri.GetLeftPart(UriPartial.Authority);
    var resource = uri.PathAndQuery;

    var client = new RestClient(host);
    var cookieContainer = new CookieContainer();
    
    if (this.DefaultCookies != null)
    {
        foreach (var cookie in this.DefaultCookies)
        {
            cookieContainer.Add(cookie);
        }
    }    
    
    if (cookies != null)
    {
        foreach (var cookie in cookies)
        {
            cookieContainer.Add(cookie);
        }
    }    
    
    client.CookieContainer = cookieContainer;
    client.FollowRedirects = this.FollowRedirects;
    
    var request = new RestRequest(resource, method);
    
    if (this.DefaultHeaders != null)
    {
        foreach (var item in this.DefaultHeaders)
        {
            request.AddHeader(item.Key, item.Value);
        }
    }
    
    
    if (headers != null)
    {
        foreach (var item in headers)
        {
            request.AddHeader(item.Key, item.Value);
        }
    }

    return Tuple.Create(client, request);
}