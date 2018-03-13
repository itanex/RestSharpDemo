using System.Text;
using System.Web.Script.Serialization;
using RestSharp;

namespace RestSharp_Demo
{
    public class RestAgent
    {
        public IRestRequest HttpPost(object PostContent)
        {
            var client = new RestClient("http://localhost");
            var request = new RestRequest();

            var strJSONContent = new StringBuilder();

            (new JavaScriptSerializer()).Serialize(PostContent, strJSONContent);

            request.Method = Method.POST;
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.AddParameter("application/json", strJSONContent, ParameterType.RequestBody);

            client.Execute(request);

            return request;
        }
    }
}
