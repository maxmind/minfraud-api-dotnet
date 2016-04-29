using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using Device = MaxMind.MinFraud.Response.Device;

namespace MaxMind.MinFraud.UnitTest.Response
{
    internal class DeviceTest
    {
        [Test]
        public void TestDevice()
        {
            var id = "35e5e22c-8bf2-44f8-aa99-716ec7530281";
            var device = new JObject
            {
                {
                    "id", id
                }
            }.ToObject<Device>();

            Assert.AreEqual(new Guid(id), device.Id);
        }
    }
}