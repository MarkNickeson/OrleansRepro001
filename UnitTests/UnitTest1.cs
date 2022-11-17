extern alias V1;
extern alias V2;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Configuration;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var siloV1 = await Host.CreateDefaultBuilder()
                .UseOrleans(builder => builder
                    .UseLocalhostClustering()
                    .Configure<GrainTypeOptions>(options =>
                    {
                        options.Interfaces.Remove(typeof(V2.CommonTypes.IFoo));
                        options.Classes.Remove(typeof(V2.CommonTypes.Foo));
                    })
                    )
                .StartAsync();


            var clientHostV1 = await Host.CreateDefaultBuilder()
                .UseOrleansClient(builder => builder
                    .UseLocalhostClustering()
                    .Configure<GrainTypeOptions>(options =>
                    {
                        options.Interfaces.Remove(typeof(V2.CommonTypes.IFoo));
                        options.Classes.Remove(typeof(V2.CommonTypes.Foo));
                    })
                    ).StartAsync();

            var clientV1 = clientHostV1.Services.GetRequiredService<IClusterClient>();

            var grainV1 = clientV1.GetGrain<V1.CommonTypes.IFoo>("v1"); // failure occurs here due to InvalidCastException

            var rvalV1 = await grainV1.Bar();

            Assert.Equal("version 1", rvalV1);

            await siloV1.StopAsync();
        }
    }
}