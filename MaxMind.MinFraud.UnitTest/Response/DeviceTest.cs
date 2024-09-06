using System;
using System.Text.Json;
using Xunit;
using Device = MaxMind.MinFraud.Response.Device;

namespace MaxMind.MinFraud.UnitTest.Response
{
    public class DeviceTest
    {
        [Fact]
        public void TestDevice()
        {
            var id = "35e5e22c-8bf2-44f8-aa99-716ec7530281";
            var lastSeen = "2016-06-08T14:16:38+00:00";
            var localTime = "2016-06-10T14:19:10-08:00";
            var device = JsonSerializer.Deserialize<Device>(
                @$"
                    {{
                        ""confidence"": 99,
                        ""id"": ""{id}"",
                        ""last_seen"": ""{lastSeen}"",
                        ""local_time"": ""{localTime}""
                    }}
                ")!;

            Assert.Equal(99, device.Confidence);
            Assert.Equal(new Guid(id), device.Id);
            Assert.Equal(lastSeen, device.LastSeen?.ToString("yyyy-MM-ddTHH:mm:ssK"));
            Assert.Equal(localTime, device.LocalTime?.ToString("yyyy-MM-ddTHH:mm:ssK"));
        }
    }
}