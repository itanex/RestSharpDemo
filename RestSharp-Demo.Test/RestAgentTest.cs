using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharp_Demo.Test
{
    [TestClass]
    public class RestAgentTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var agent = new RestAgent();
            var testObject = new { key1 = "value1", key2 = "value2" };
            var testParameter = new Parameter()
            {
                Name = "application/json",
                Type= ParameterType.RequestBody,
                Value = "{\"key1\":\"value1\",\"key2\":\"value2\"}"
            };
            IRestRequest actual = agent.HttpPost(testObject);

            // in case that the casted object fails
            Assert.IsNotNull(actual);

            Assert.IsTrue(actual.Method == Method.POST);
            Assert.IsTrue(actual.Parameters.Count == 2);

            Assert.IsTrue(ContainsParameter(actual.Parameters.Where(x => x.Type.Equals(ParameterType.RequestBody)).DefaultIfEmpty(), testParameter));
        }

        private bool ContainsParameter(IEnumerable<Parameter> parameters, Parameter value)
        {
            foreach (var param in parameters)
            {
                var paramValue = param.Value.ToString();
                var testValue = value.Value.ToString();

                if (paramValue.Equals(testValue) && param.Name.Equals("application/json"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
