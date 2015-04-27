using RestSharp;
using System.Collections.Generic;

public T Get<T>(string url, Dictionary<string, string> headers = null) where T: new()
{
    var uri = new Uri(url);
    var host = uri.GetLeftPart(UriPartial.Authority);
    var resource = uri.PathAndQuery;

    var client = new RestClient(host);
    var request = new RestRequest(resource, Method.GET);
    if( headers != null)
    {
        foreach (var item in headers) 
        {
            request.AddHeader(item.Key, item.Value);
        }
    }
    var response = client.Execute<T>(request);
    return response.Data;
}

public IRestResponse Delete(string url, Dictionary<string, string> headers = null)
{
    var uri = new Uri(url);
    var host = uri.GetLeftPart(UriPartial.Authority);
    var resource = uri.PathAndQuery;

    var client = new RestClient(host);
    var request = new RestRequest(resource, Method.DELETE);
    if (headers != null)
    {
        foreach (var item in headers)
        {
            request.AddHeader(item.Key, item.Value);
        }
    }
    var response = client.Execute(request);
    return response;
}

public IRestResponse Put<T>(string url, T data, Dictionary<string, string> headers = null)
{
    var uri = new Uri(url);
    var host = uri.GetLeftPart(UriPartial.Authority);
    var resource = uri.PathAndQuery;

    var client = new RestClient(host);
    var request = new RestRequest(resource, Method.PUT);
    if (headers != null)
    {
        foreach (var item in headers)
        {
            request.AddHeader(item.Key, item.Value);
        }
    }

    request.AddBody(data);

    var response = client.Execute(request);
    return response;
}

public IRestResponse Post<T>(string url, T data, Dictionary<string, string> headers = null)
{
    var uri = new Uri(url);
    var host = uri.GetLeftPart(UriPartial.Authority);
    var resource = uri.PathAndQuery;

    var client = new RestClient(host);
    var request = new RestRequest(resource, Method.POST);
    if (headers != null)
    {
        foreach (var item in headers)
        {
            request.AddHeader(item.Key, item.Value);
        }
    }

    request.AddBody(data);

    var response = client.Execute(request);
    return response;
}