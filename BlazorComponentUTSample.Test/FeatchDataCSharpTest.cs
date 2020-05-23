using System;
using Xunit;
using Bunit;
using Bunit.Mocking.JSInterop;
using static Bunit.ComponentParameterFactory;
using BlazorComponentUTSample.Pages;
using Microsoft.Extensions.DependencyInjection;
using BlazorComponentUTSample.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace BlazorComponentUTSample.Test
{
    public class FeatchDataCSharpTest : TestContext
    {
        [Fact]
        public void LoadingBeforeLoadData()
        {
            // set empty record mock
            var forecasts = new List<WeatherForecast>();
            var mockService = new MockWeatherService();
            mockService.Task.SetResult(forecasts);

            Services.AddSingleton<IWeatherService>(mockService);

            var fetchData = RenderComponent<FetchData>();

            var expectedHtml = @"Loading...";
            fetchData.Find("em").MarkupMatches(expectedHtml);
        }

        [Fact]
        public void ZeroDataCase()
        {
            // set empty record mock
            var forecasts = new List<WeatherForecast>();
            var mockService = new MockWeatherService();
            mockService.Task.SetResult(forecasts);

            Services.AddSingleton<IWeatherService>(mockService);

            var fetchData = RenderComponent<FetchData>();
            // wait until table rendered
            fetchData.WaitForState(() => fetchData.FindAll(".table").Count > 0);

            var expectedHtml = @"<table class=""table"">
                                    <thead>
                                        <tr>
                                            <th>Date</th>
                                            <th>Temp. (C) </th>
                                            <th>Temp. (F) </th>
                                            <th>Summary </th>
                                        </tr>
                                    </thead>
                                <tbody>
                                </tbody>";
            fetchData.Find("table").MarkupMatches(expectedHtml);
        }

        [Fact]
        public void ExistsDataCase()
        {
            // set dummy record mock
            var forecasts = new List<WeatherForecast>();
            forecasts.Add(new WeatherForecast() { Date = new DateTime(2020, 5, 1), TemperatureC = 20, Summary = "Sunny" });
            forecasts.Add(new WeatherForecast() { Date = new DateTime(2020, 5, 2), TemperatureC = 10, Summary = "Rainy" });
            forecasts.Add(new WeatherForecast() { Date = new DateTime(2020, 5, 3), TemperatureC = 14, Summary = "Cloudy" });

            var mockService = new MockWeatherService();
            mockService.Task.SetResult(forecasts);

            Services.AddSingleton<IWeatherService>(mockService);

            var fetchData = RenderComponent<FetchData>();
            // wait until table rendered
            fetchData.WaitForState(() => fetchData.FindAll(".table").Count > 0);

            var expectedHtml = @"<table class=""table"">
                                    <thead>
                                        <tr>
                                            <th>Date</th>
                                            <th>Temp. (C) </th>
                                            <th>Temp. (F) </th>
                                            <th>Summary </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <tr>
                                        <td>2020/05/01</td>
                                        <td>20</td>
                                        <td>67</td>
                                        <td>Sunny</td>
                                    </tr>
                                    <tr>
                                        <td>2020/05/02</td>
                                        <td>10</td>
                                        <td>49</td>
                                        <td>Rainy</td>
                                    </tr>
                                    <tr>
                                        <td>2020/05/03</td>
                                        <td>14</td>
                                        <td>57</td>
                                        <td>Cloudy</td>
                                    </tr>
                            </tbody>";
            fetchData.Find("table").MarkupMatches(expectedHtml);
        }
    }

    internal class MockWeatherService : IWeatherService
    {
        public TaskCompletionSource<IEnumerable<WeatherForecast>> Task { get; } = new TaskCompletionSource<IEnumerable<WeatherForecast>>();

        public Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync()
        {
            return Task.Task;
        }
    }

}
