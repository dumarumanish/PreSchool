
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Models;

namespace PreSchool.Application.HelperClasses
{
    public static class HttpClientHelper
    {
        static HttpClient client = new HttpClient();
        public static async Task<O> PostAsync<I, O>(string uri, I input, string token = null)
        {
            var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8,
                         "application/json");

            // Add token if any
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PostAsync(
                uri, content);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<O>();

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Forbidden in external api");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnAuthorizedException("UnAuthorize in external api");

            var errorResponse = await response.Content.ReadAsAsync<ErrorResponse>();

            if (errorResponse != null)
                throw new BaseException(errorResponse.message, errorResponse.description + " from external api", (int)response.StatusCode);

            throw new BaseException("Server error in external api", "", (int)response.StatusCode);

        }

        public static async Task<O> GetAsync<O>(string path, string token = null)
        {
            // Add token if any
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync(path);


            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<O>();

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Forbidden in external api");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnAuthorizedException("UnAuthorize in external api");

            var errorResponse = await response.Content.ReadAsAsync<ErrorResponse>();

            if (errorResponse != null)
                throw new BaseException(errorResponse.message, errorResponse.description + " from external api", (int)response.StatusCode);

            throw new BaseException("Server error in external api", "", (int)response.StatusCode);


        }

        public static async Task<O> UpdateAsync<I, O>(string uri, I input, string token = null)
        {
            // Add token if any
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PutAsJsonAsync(
               uri, input);


            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsAsync<O>();

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Forbidden in external api");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnAuthorizedException("UnAuthorize in external api");

            var errorResponse = await response.Content.ReadAsAsync<ErrorResponse>();

            if (errorResponse != null)
                throw new BaseException(errorResponse.message, errorResponse.description + " from external api", (int)response.StatusCode);

            throw new BaseException("Server error in external api", "", (int)response.StatusCode);



        }

        public static async Task<HttpStatusCode> DeleteAsync(string url, string token = null)
        {
            // Add token if any
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.DeleteAsync(
               url);

            if (response.IsSuccessStatusCode)
                return response.StatusCode;

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Forbidden in external api");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnAuthorizedException("UnAuthorize in external api");

            var errorResponse = await response.Content.ReadAsAsync<ErrorResponse>();

            if (errorResponse != null)
                throw new BaseException(errorResponse.message, errorResponse.description + " from external api", (int)response.StatusCode);

            throw new BaseException("Server error in external api", "", (int)response.StatusCode);

        }


        //private static async void HandleError(HttpResponseMessage response)
        //{
        //    if (response.IsSuccessStatusCode)
        //        return;

        //    if (response.StatusCode == HttpStatusCode.Forbidden)
        //        throw new ForbiddenException("Forbidden in external api");

        //    if (response.StatusCode == HttpStatusCode.Unauthorized)
        //        throw new UnAuthorizedException("UnAuthorize in external api");

        //    var errorResponse = await response.Content.ReadAsAsync<ErrorResponse>();

        //    if (errorResponse != null)
        //        throw new BaseException(errorResponse.message, errorResponse.description + " from external api", (int)response.StatusCode);

        //    throw new BaseException("Server error in external api", "", (int)response.StatusCode);
        //}

    }
}
