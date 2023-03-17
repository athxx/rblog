using System;
using Elasticsearch.Net;
using Nest;

namespace Core.Util.ElasticSearch;

public class ElasticSearch
{
    private readonly ElasticClient _client;

    public ElasticSearch(string url)
    {
        var settings = new ConnectionSettings(new Uri(url));
        _client = new ElasticClient(settings);
    }

    public void IndexDocument<T>(T document, string indexName) where T : class
    {
        var response = _client.Index(document, i => i.Index(indexName));
        if (!response.IsValid)
        {
            throw new Exception(response.DebugInformation);
        }
    }

    public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, ISearchRequest> searchSelector) where T : class
    {
        return _client.Search(searchSelector);
    }
}