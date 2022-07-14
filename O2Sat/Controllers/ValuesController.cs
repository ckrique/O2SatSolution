using FIWARE.OrionClient.IoTAgent;
using FIWARE.OrionClient.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace O2Sat.Controllers
{
    public class ValuesController : ApiController
    {
        public class Person
        {
            public string name { get; set; }
            public string surname { get; set; }
        }

        // GET api/values
        public async Task<IEnumerable<string>> GetAsync()
        {
            RESTClient<string> restClient = new RESTClient<string>();
            await restClient.PatchAsyncTriggerExhaustorCommand("on");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public async Task<string> PostAsync()
        {            
            RESTClient<string> restClient = new RESTClient<string>();
            string sensorEntityName = "urn:ngsi-ld:sensor:001";
            SensorRootDataCatcher sensor = await restClient.GetMeasurementFromBroker(sensorEntityName, "sensor") as SensorRootDataCatcher;

            restClient = new RESTClient<string>();
            string exhaustorEntityName = "urn:ngsi-ld:fanEhxaustor:001";
            ExhaustorRootDataCatcher exhaustor = await restClient.GetMeasurementFromBroker(exhaustorEntityName, "exhaustor") as ExhaustorRootDataCatcher;

            restClient = new RESTClient<string>();

            if (Convert.ToDouble(sensor.o2Saturation.value) < 19)
            {
                await restClient.PatchAsyncTriggerExhaustorCommand("on");

                restClient = new RESTClient<string>();

                await restClient.PostAsyncTriggerSensorBackDoor("UP");
            }
            else if (Convert.ToDouble(sensor.o2Saturation.value) > 23)
            {
                await restClient.PatchAsyncTriggerExhaustorCommand("off");

                restClient = new RESTClient<string>();

                await restClient.PostAsyncTriggerSensorBackDoor("DOWN");
            }

            string retorno = sensor.o2Saturation.value + "|" + exhaustor.state.value;

            return retorno;

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
