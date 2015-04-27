using RestSharp;
using System.Collections.Generic;

public IRestResponse Get(string url, IDictionary<string, string> headers = null)
{
    var result = CreateRequest(url, Method.GET, headers);
    var response = result.Item1.Execute(result.Item2);
    return response;
}

public T Get<T>(string url, IDictionary<string, string> headers = null) where T: new()
{
    var result = CreateRequest(url, Method.GET, headers);
    var response = result.Item1.Execute<T>(result.Item2);
    return response.Data;
}

public IRestResponse Delete(string url, IDictionary<string, string> headers = null)
{
    var result = CreateRequest(url, Method.DELETE, headers);
    var response = result.Item1.Execute(result.Item2);
    return response;
}

public IRestResponse Put<T>(string url, T data, IDictionary<string, string> headers = null)
{
    var result = CreateRequest(url, Method.PUT, headers);

    result.Item2.AddBody(data);

    var response = result.Item1.Execute(result.Item2);

    return response;
}

public IRestResponse Post<T>(string url, T data, IDictionary<string, string> headers = null)
{
    var result = CreateRequest(url, Method.POST, headers);

    result.Item2.AddBody(data);

    var response = result.Item1.Execute(result.Item2);

    return response;
}

private Tuple<RestClient,RestRequest> CreateRequest(string url, Method method, IDictionary<string, string> headers = null)
{
    var uri = new Uri(url);
    var host = uri.GetLeftPart(UriPartial.Authority);
    var resource = uri.PathAndQuery;

    var client = new RestClient(host);
    var request = new RestRequest(resource, method);
    if (headers != null)
    {
        foreach (var item in headers)
        {
            request.AddHeader(item.Key, item.Value);
        }
    }

    return Tuple.Create(client, request);
}