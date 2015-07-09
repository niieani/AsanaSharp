using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AsanaSharp.Annotations;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Runtime.Serialization;
using System.Linq;

namespace AsanaSharp
{
	/// <summary>
    /// Generated: Get by ID with the EndpointAttribute.
    /// </summary>
	//[JsonObject(MemberSerialization.OptIn)]
	public partial class Asana
	{

		/// <summary>
        /// Accesses the /teams/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaTeam> GetTeam(long id, string optFields = null)
		{
			var request = new RestRequest("/teams/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaTeam>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /workspaces/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaWorkspace> GetWorkspace(long id, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaWorkspace>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /projects/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaProject> GetProject(long id, string optFields = null)
		{
			var request = new RestRequest("/projects/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaProject>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /projects/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaProject> SaveProject(long id, string optFields = null)
		{
			var request = new RestRequest("/projects/{id}", Method.PUT);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaProject>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /projects/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task DeleteProject(long id)
		{
			var request = new RestRequest("/projects/{id}", Method.DELETE);
			request.AddUrlSegment("id", id.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaProject>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /users/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaUser> GetUser(long id, string optFields = null)
		{
			var request = new RestRequest("/users/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaUser>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaTask> GetTask(long id, string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}", Method.GET);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaTask>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaTask> SaveTask(long id, string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}", Method.PUT);
			request.AddUrlSegment("id", id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaTask>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}


		/// <summary>
        /// Accesses the /tasks/{id} endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task DeleteTask(long id)
		{
			var request = new RestRequest("/tasks/{id}", Method.DELETE);
			request.AddUrlSegment("id", id.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaResponse>(reader);
				return jsonData.Data.ToObject<AsanaTask>(AsanaHost.JsonDeserializer);
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		//[JsonConverter(typeof(AsanaListConverter<AsanaReadOnlyCollection<AsanaUser>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaReadOnlyCollection<AsanaUser>, AsanaUser>))]
		//[JsonProperty(PropertyName = "users")]
		//[JsonProperty(PropertyName = "users", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaReadOnlyCollection<AsanaUser> Users;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "users", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "users")]
		private List<AsanaUser> _deserializedUsers;
