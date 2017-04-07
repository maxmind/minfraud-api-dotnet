using Newtonsoft.Json.Linq;
using Xunit;
using System;
using Device = MaxMind.MinFraud.Response.Device;

namespace MaxMind.MinFraud.UnitTest.Response
{
    internal class DeviceTest
    {
        [Fact]
        public void TestDevice()
        {
            var id = "35e5e22c-8bf2-44f8-aa99-716ec7530281";
            var lastSeen = "2016-06-08T14:16:38+00:00";
            var device = new JObject
            {
                {"confidence", 99},
                {"id", id},
                {"last_seen", lastSeen},
            }.ToObject<Device>();

            Assert.Equal(99, device.Confidence);
            Assert.Equal(new Guid(id), device.Id);
            Assert.Equal(lastSeen, device.LastSeen?.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}