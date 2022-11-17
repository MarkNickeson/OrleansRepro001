extern alias V1;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace UnitTest_GoodExternAlias
{
    public class UnitTest1
    {

        [Fact]
        public async Task Test1()
        {
            var siloV1 = await Host.CreateDefaultBuilder()
                .UseOrleans(builder => builder
                    .UseLocalhostClustering())
                .StartAsync();

            var clientHostV1 = await Host.CreateDefaultBuilder()
                .UseOrleansClient(builder => builder
                    .UseLocalhostClustering())
                .StartAsync();

            var clientV1 = clientHostV1.Services.GetRequiredService<IClusterClient>();

            var grainV1 = clientV1.GetGrain<V1.CommonTypes.IFoo>("v1"); // failure occurs here

            var rvalV1 = await grainV1.Bar();

            Assert.Equal("version 1", rvalV1);

            await siloV1.StopAsync();
        }
    }
}