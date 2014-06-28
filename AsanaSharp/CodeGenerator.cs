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

		/// <summary>
        /// Accesses the /users endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaUser>> GetUsers(string optFields = null)
		{
			var request = new RestRequest("/users", Method.GET);
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaUser>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Users, null))
				{
					Users = new AsanaReadOnlyCollection<AsanaUser>(resultsCollection);
				}
				else
				{
					Users.Import(resultsCollection);
				}

					//Users = jsonData.Data.ToObject<AsanaReadOnlyCollection<AsanaUser>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Users);
				return Users;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		//[JsonConverter(typeof(AsanaListConverter<AsanaReadOnlyCollection<AsanaWorkspace>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaReadOnlyCollection<AsanaWorkspace>, AsanaWorkspace>))]
		//[JsonProperty(PropertyName = "workspaces")]
		//[JsonProperty(PropertyName = "workspaces", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaReadOnlyCollection<AsanaWorkspace> Workspaces;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "workspaces", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "workspaces")]
		private List<AsanaWorkspace> _deserializedWorkspaces;

		/// <summary>
        /// Accesses the /workspaces endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaReadOnlyCollection<AsanaWorkspace>> GetWorkspaces(string optFields = null)
		{
			var request = new RestRequest("/workspaces", Method.GET);
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaWorkspace>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Workspaces, null))
				{
					Workspaces = new AsanaReadOnlyCollection<AsanaWorkspace>(resultsCollection);
				}
				else
				{
					Workspaces.Import(resultsCollection);
				}

					//Workspaces = jsonData.Data.ToObject<AsanaReadOnlyCollection<AsanaWorkspace>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Workspaces);
				return Workspaces;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		public AsanaUser Me
		{
			get 
			{
				return _remoteMe;
			}
        }
		
		[JsonProperty("me")]
		//[JsonProperty("me"), JsonConverter(typeof(JsonRefedConverter))]
        private AsanaUser _remoteMe
		{
			get 
			{
				return _remoteValueMe;
			}
			set
			{
				if (_remoteValueMe != value)
				{
					_remoteValueMe = value;
					OnPropertyChanged("Me");
				}
			}
		}
		private AsanaUser _remoteValueMe;


		/// <summary>
        /// Accesses the /users/me endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaUser> GetMe(string optFields = null)
		{
			var request = new RestRequest("/users/me", Method.GET);
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

	} // END: public partial class Asana
	//[JsonConverter(typeof(JsonRefConverter))]
	//[JsonConverter(typeof(JsonRefedConverter))]
	//[JsonObject(MemberSerialization.OptIn)]
	public partial class AsanaTeam : AsanaResource
	{
		public async Task<AsanaTeam> Save()
		{
			RestRequest request;

			if (IsLocal) // need to create
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.POST);
			}
			else // update
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.PUT);
			}
            
            request.AddUrlSegment("id", Id.ToString());

            // for each _local
            if (!ReferenceEquals(_localName, null))
                request.AddParameter("name", _localName.ToString());

            // execute the request
            var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string

            // ... 

            if (Projects.BufferCount > 0)
	        {
	            var projectsBuffer = Projects.GetBufferAndPurge();
	            foreach (var project in projectsBuffer.AddBuffer)
	            {
	                await project.Save(); // save if not saved yet
	                AddProject(project);
	            }
	        }


            /*
            return new
            {
                Lala = "test",
                Projects = new[] { "test" }
            };
            */
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}
	} // END class: AsanaTeam

	//[JsonConverter(typeof(JsonRefConverter))]
	//[JsonConverter(typeof(JsonRefedConverter))]
	//[JsonObject(MemberSerialization.OptIn)]
	public partial class AsanaWorkspace : AsanaResource
	{
		public string Name
		{
			get 
			{
				return _localName ?? _remoteName;
			}
			set
			{
				if (_remoteName != value)
					_localName = value;
			}
        }
		
		[JsonProperty("name")]
		//[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		private string _localName;

		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaProject>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaProject>, AsanaProject>))]
		//[JsonProperty(PropertyName = "projects")]
		//[JsonProperty(PropertyName = "projects", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaCollection<AsanaProject> Projects;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "projects", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "projects")]
		private List<AsanaProject> _deserializedProjects;

		/// <summary>
        /// Accesses the /workspaces/{id}/projects endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaProject>> GetProjects(System.Nullable<bool> archived = null, string optFields = null)
		{
			var request = new RestRequest("/workspaces/{id}/projects", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(archived, null))
				request.AddParameter("archived", archived.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaProject>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Projects, null))
				{
					Projects = new AsanaCollection<AsanaProject>(resultsCollection);
				}
				else
				{
					Projects.Import(resultsCollection);
				}

					//Projects = jsonData.Data.ToObject<AsanaCollection<AsanaProject>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Projects);
				return Projects;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}



		/// <summary>
        /// Constructs a new AsanaProject under this class.
        /// </summary>
		public AsanaProject NewProject(AsanaTeam team = null)
		{
			var retVal = new AsanaProject();
			retVal.Team = team;
			return retVal;
		}
        // Name // False // WTF
        // Notes // False // WTF

		/// <summary>
        /// Accesses the /workspaces/{id}/projects endpoint.
        /// </summary>
        /// <returns></returns>
		internal async Task<AsanaProject> CreateProject(AsanaTeam team = null)
		{
			var request = new RestRequest("/workspaces/{id}/projects", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(team, null))
				request.AddParameter("team", team.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaProject>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Projects, null))
				{
					Projects = new AsanaCollection<AsanaProject>(resultsCollection);
				}
				else
				{
					Projects.Import(resultsCollection);
				}

					//Projects = jsonData.Data.ToObject<AsanaCollection<AsanaProject>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Projects);
				return Projects;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaTask>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaTask>, AsanaTask>))]
		//[JsonProperty(PropertyName = "my_tasks")]
		//[JsonProperty(PropertyName = "my_tasks", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaCollection<AsanaTask> MyTasks;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "my_tasks", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "my_tasks")]
		private List<AsanaTask> _deserializedMyTasks;

		/// <summary>
        /// Accesses the /workspaces/{id}/tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTask>> GetMyTasks(System.Nullable<System.DateTime> completedSince = null, System.Nullable<System.DateTime> modifiedSince = null, string optFields = null, bool completedSinceIsNow = false)
		{
			var request = new RestRequest("/workspaces/{id}/tasks", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			request.AddParameter("assignee", "me");
			if (completedSinceIsNow)
				request.AddParameter("completed_since", "now");
			else
			if (!ReferenceEquals(completedSince, null))
				request.AddParameter("completed_since", completedSince.ToString());
			if (!ReferenceEquals(modifiedSince, null))
				request.AddParameter("modified_since", modifiedSince.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(MyTasks, null))
				{
					MyTasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					MyTasks.Import(resultsCollection);
				}

					//MyTasks = jsonData.Data.ToObject<AsanaCollection<AsanaTask>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), MyTasks);
				return MyTasks;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaTask>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaTask>, AsanaTask>))]
		//[JsonProperty(PropertyName = "tasks")]
		//[JsonProperty(PropertyName = "tasks", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaCollection<AsanaTask> Tasks;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "tasks", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "tasks")]
		private List<AsanaTask> _deserializedTasks;

		/// <summary>
        /// Accesses the /workspaces/{id}/tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTask>> GetTasks(System.Nullable<System.DateTime> completedSince = null, System.Nullable<System.DateTime> modifiedSince = null, string optFields = null, bool completedSinceIsNow = false)
		{
			var request = new RestRequest("/workspaces/{id}/tasks", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (completedSinceIsNow)
				request.AddParameter("completed_since", "now");
			else
			if (!ReferenceEquals(completedSince, null))
				request.AddParameter("completed_since", completedSince.ToString());
			if (!ReferenceEquals(modifiedSince, null))
				request.AddParameter("modified_since", modifiedSince.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

					//Tasks = jsonData.Data.ToObject<AsanaCollection<AsanaTask>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Tasks);
				return Tasks;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}



		/// <summary>
        /// Constructs a new AsanaTask under this class.
        /// </summary>
		public AsanaTask NewTask()
		{
			var retVal = new AsanaTask();
			return retVal;
		}
        // Completed // False // WTF
        // CreatedAt // False // WTF
        // Followers // True // WTF
        // AssigneeStatus // False // WTF
        // Projects // True // WTF
        // Name // False // WTF
        // Notes // False // WTF

		/// <summary>
        /// Accesses the /workspaces/{id}/tasks endpoint.
        /// </summary>
        /// <returns></returns>
		internal async Task<AsanaTask> CreateTask()
		{
			var request = new RestRequest("/workspaces/{id}/tasks", Method.POST);
			request.AddUrlSegment("id", Id.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

					//Tasks = jsonData.Data.ToObject<AsanaCollection<AsanaTask>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Tasks);
				return Tasks;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		public async Task<AsanaWorkspace> Save()
		{
			RestRequest request;

			if (IsLocal) // need to create
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.POST);
			}
			else // update
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.PUT);
			}
            
            request.AddUrlSegment("id", Id.ToString());

            // for each _local
            if (!ReferenceEquals(_localName, null))
                request.AddParameter("name", _localName.ToString());

            // execute the request
            var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string

            // ... 

            if (Projects.BufferCount > 0)
	        {
	            var projectsBuffer = Projects.GetBufferAndPurge();
	            foreach (var project in projectsBuffer.AddBuffer)
	            {
	                await project.Save(); // save if not saved yet
	                AddProject(project);
	            }
	        }


            /*
            return new
            {
                Lala = "test",
                Projects = new[] { "test" }
            };
            */
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Projects //
			if (!ReferenceEquals(_deserializedProjects, null))
			{
				if (ReferenceEquals(Projects, null))
				{
					// first deserialization
					Projects = new AsanaCollection<AsanaProject>(_deserializedProjects);
					if (_deserializedProjects.Any())
						OnPropertyChanged("Projects");
				}
				else
				{
					var anythingChanged = Projects.Import(_deserializedProjects);
					if (anythingChanged)
						OnPropertyChanged("Projects");
				}

				// cleanup
				_deserializedProjects = null;
			}
			else if (ReferenceEquals(Projects, null))
			{
				Projects = new AsanaCollection<AsanaProject>();
			}
			// ----------------------------- //

			// Load MyTasks //
			if (!ReferenceEquals(_deserializedMyTasks, null))
			{
				if (ReferenceEquals(MyTasks, null))
				{
					// first deserialization
					MyTasks = new AsanaCollection<AsanaTask>(_deserializedMyTasks);
					if (_deserializedMyTasks.Any())
						OnPropertyChanged("MyTasks");
				}
				else
				{
					var anythingChanged = MyTasks.Import(_deserializedMyTasks);
					if (anythingChanged)
						OnPropertyChanged("MyTasks");
				}

				// cleanup
				_deserializedMyTasks = null;
			}
			else if (ReferenceEquals(MyTasks, null))
			{
				MyTasks = new AsanaCollection<AsanaTask>();
			}
			// ----------------------------- //

			// Load Tasks //
			if (!ReferenceEquals(_deserializedTasks, null))
			{
				if (ReferenceEquals(Tasks, null))
				{
					// first deserialization
					Tasks = new AsanaCollection<AsanaTask>(_deserializedTasks);
					if (_deserializedTasks.Any())
						OnPropertyChanged("Tasks");
				}
				else
				{
					var anythingChanged = Tasks.Import(_deserializedTasks);
					if (anythingChanged)
						OnPropertyChanged("Tasks");
				}

				// cleanup
				_deserializedTasks = null;
			}
			else if (ReferenceEquals(Tasks, null))
			{
				Tasks = new AsanaCollection<AsanaTask>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}
	} // END class: AsanaWorkspace

	//[JsonConverter(typeof(JsonRefConverter))]
	//[JsonConverter(typeof(JsonRefedConverter))]
	//[JsonObject(MemberSerialization.OptIn)]
	public partial class AsanaProject : AsanaResource
	{
		public string Name
		{
			get 
			{
				return _localName ?? _remoteName;
			}
			set
			{
				if (_remoteName != value)
					_localName = value;
			}
        }
		
		[JsonProperty("name")]
		//[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		private string _localName;

		public string Notes
		{
			get 
			{
				return _localNotes ?? _remoteNotes;
			}
			set
			{
				if (_remoteNotes != value)
					_localNotes = value;
			}
        }
		
		[JsonProperty("notes")]
		//[JsonProperty("notes")]
        private string _remoteNotes
		{
			get 
			{
				return _remoteValueNotes;
			}
			set
			{
				if (_remoteValueNotes != value)
				{
					_remoteValueNotes = value;
					OnPropertyChanged("Notes");
				}
			}
		}
		private string _remoteValueNotes;

		private string _localNotes;

		public async Task<AsanaProject> Save()
		{
			RestRequest request;

			if (IsLocal) // need to create
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.POST);
			}
			else // update
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.PUT);
			}
            
            request.AddUrlSegment("id", Id.ToString());

            // for each _local
            if (!ReferenceEquals(_localName, null))
                request.AddParameter("name", _localName.ToString());

            // execute the request
            var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string

            // ... 

            if (Projects.BufferCount > 0)
	        {
	            var projectsBuffer = Projects.GetBufferAndPurge();
	            foreach (var project in projectsBuffer.AddBuffer)
	            {
	                await project.Save(); // save if not saved yet
	                AddProject(project);
	            }
	        }


            /*
            return new
            {
                Lala = "test",
                Projects = new[] { "test" }
            };
            */
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}
	} // END class: AsanaProject

	//[JsonConverter(typeof(JsonRefConverter))]
	//[JsonConverter(typeof(JsonRefedConverter))]
	//[JsonObject(MemberSerialization.OptIn)]
	public partial class AsanaUser : AsanaResource
	{
		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaTask>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaTask>, AsanaTask>))]
		//[JsonProperty(PropertyName = "tasks")]
		//[JsonProperty(PropertyName = "tasks", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaCollection<AsanaTask> Tasks;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "tasks", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "tasks")]
		private List<AsanaTask> _deserializedTasks;

		/// <summary>
        /// Accesses the /tasks endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaTask>> GetTasks(System.Nullable<System.DateTime> completedSince = null, System.Nullable<System.DateTime> modifiedSince = null, string optFields = null, bool completedSinceIsNow = false)
		{
			var request = new RestRequest("/tasks", Method.GET);
			request.AddParameter("workspace", Workspace.Id.ToString());
			request.AddParameter("assignee", Id.ToString());
			if (completedSinceIsNow)
				request.AddParameter("completed_since", "now");
			else
			if (!ReferenceEquals(completedSince, null))
				request.AddParameter("completed_since", completedSince.ToString());
			if (!ReferenceEquals(modifiedSince, null))
				request.AddParameter("modified_since", modifiedSince.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

					//Tasks = jsonData.Data.ToObject<AsanaCollection<AsanaTask>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Tasks);
				return Tasks;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}



		/// <summary>
        /// Constructs a new AsanaTask under this class.
        /// </summary>
		public AsanaTask NewTask()
		{
			var retVal = new AsanaTask();
			return retVal;
		}
        // Completed // False // WTF
        // CreatedAt // False // WTF
        // Followers // True // WTF
        // AssigneeStatus // False // WTF
        // Projects // True // WTF
        // Name // False // WTF
        // Notes // False // WTF

		/// <summary>
        /// Accesses the /tasks endpoint.
        /// </summary>
        /// <returns></returns>
		internal async Task<AsanaTask> CreateTask()
		{
			var request = new RestRequest("/tasks", Method.POST);
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaTask>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Tasks, null))
				{
					Tasks = new AsanaCollection<AsanaTask>(resultsCollection);
				}
				else
				{
					Tasks.Import(resultsCollection);
				}

					//Tasks = jsonData.Data.ToObject<AsanaCollection<AsanaTask>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Tasks);
				return Tasks;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		public AsanaWorkspace Workspace
		{
			get 
			{
				return _localWorkspace ?? _remoteWorkspace;
			}
			set
			{
				if (_remoteWorkspace != value)
					_localWorkspace = value;
			}
        }
		
		[JsonProperty("workspace")]
		//[JsonProperty("workspace"), JsonConverter(typeof(JsonRefedConverter))]
        private AsanaWorkspace _remoteWorkspace
		{
			get 
			{
				return _remoteValueWorkspace;
			}
			set
			{
				if (_remoteValueWorkspace != value)
				{
					_remoteValueWorkspace = value;
					OnPropertyChanged("Workspace");
				}
			}
		}
		private AsanaWorkspace _remoteValueWorkspace;

		private AsanaWorkspace _localWorkspace;

		public string Name
		{
			get 
			{
				return _localName ?? _remoteName;
			}
			set
			{
				if (_remoteName != value)
					_localName = value;
			}
        }
		
		[JsonProperty("name")]
		//[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		private string _localName;

		public async Task<AsanaUser> Save()
		{
			RestRequest request;

			if (IsLocal) // need to create
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.POST);
			}
			else // update
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.PUT);
			}
            
            request.AddUrlSegment("id", Id.ToString());

            // for each _local
            if (!ReferenceEquals(_localName, null))
                request.AddParameter("name", _localName.ToString());

            // execute the request
            var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string

            // ... 

            if (Projects.BufferCount > 0)
	        {
	            var projectsBuffer = Projects.GetBufferAndPurge();
	            foreach (var project in projectsBuffer.AddBuffer)
	            {
	                await project.Save(); // save if not saved yet
	                AddProject(project);
	            }
	        }


            /*
            return new
            {
                Lala = "test",
                Projects = new[] { "test" }
            };
            */
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Tasks //
			if (!ReferenceEquals(_deserializedTasks, null))
			{
				if (ReferenceEquals(Tasks, null))
				{
					// first deserialization
					Tasks = new AsanaCollection<AsanaTask>(_deserializedTasks);
					if (_deserializedTasks.Any())
						OnPropertyChanged("Tasks");
				}
				else
				{
					var anythingChanged = Tasks.Import(_deserializedTasks);
					if (anythingChanged)
						OnPropertyChanged("Tasks");
				}

				// cleanup
				_deserializedTasks = null;
			}
			else if (ReferenceEquals(Tasks, null))
			{
				Tasks = new AsanaCollection<AsanaTask>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}
	} // END class: AsanaUser

	//[JsonConverter(typeof(JsonRefConverter))]
	//[JsonConverter(typeof(JsonRefedConverter))]
	//[JsonObject(MemberSerialization.OptIn)]
	public partial class AsanaTask : AsanaResource
	{
		public System.Nullable<bool> Completed
		{
			get 
			{
				return _localCompleted ?? _remoteCompleted;
			}
			set
			{
				if (_remoteCompleted != value)
					_localCompleted = value;
			}
        }
		
		[JsonProperty("completed")]
		//[JsonProperty("completed")]
        private System.Nullable<bool> _remoteCompleted
		{
			get 
			{
				return _remoteValueCompleted;
			}
			set
			{
				if (_remoteValueCompleted != value)
				{
					_remoteValueCompleted = value;
					OnPropertyChanged("Completed");
				}
			}
		}
		private System.Nullable<bool> _remoteValueCompleted;

		private System.Nullable<bool> _localCompleted;

		public System.Nullable<System.DateTime> CreatedAt
		{
			get 
			{
				return _localCreatedAt ?? _remoteCreatedAt;
			}
			set
			{
				if (_remoteCreatedAt != value)
					_localCreatedAt = value;
			}
        }
		
		[JsonProperty("created_at")]
		//[JsonProperty("created_at")]
        private System.Nullable<System.DateTime> _remoteCreatedAt
		{
			get 
			{
				return _remoteValueCreatedAt;
			}
			set
			{
				if (_remoteValueCreatedAt != value)
				{
					_remoteValueCreatedAt = value;
					OnPropertyChanged("CreatedAt");
				}
			}
		}
		private System.Nullable<System.DateTime> _remoteValueCreatedAt;

		private System.Nullable<System.DateTime> _localCreatedAt;

		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<Dictionary<string, string>>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<Dictionary<string, string>>, Dictionary<string, string>>))]
		//[JsonProperty(PropertyName = "followers")]
		//[JsonProperty(PropertyName = "followers", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaCollection<Dictionary<string, string>> Followers;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "followers", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "followers")]
		private List<Dictionary<string, string>> _deserializedFollowers;
		public string AssigneeStatus
		{
			get 
			{
				return _localAssigneeStatus ?? _remoteAssigneeStatus;
			}
			set
			{
				if (_remoteAssigneeStatus != value)
					_localAssigneeStatus = value;
			}
        }
		
		[JsonProperty("assignee_status")]
		//[JsonProperty("assignee_status")]
        private string _remoteAssigneeStatus
		{
			get 
			{
				return _remoteValueAssigneeStatus;
			}
			set
			{
				if (_remoteValueAssigneeStatus != value)
				{
					_remoteValueAssigneeStatus = value;
					OnPropertyChanged("AssigneeStatus");
				}
			}
		}
		private string _remoteValueAssigneeStatus;

		private string _localAssigneeStatus;

		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaProject>>))]
		//[JsonConverter(typeof(AsanaListConverter<AsanaCollection<AsanaProject>, AsanaProject>))]
		//[JsonProperty(PropertyName = "projects")]
		//[JsonProperty(PropertyName = "projects", ItemConverterType = typeof(JsonRefedConverter))]
		public AsanaCollection<AsanaProject> Projects;

        //[JsonConverter(typeof(JsonRefedConverter))]
		//[JsonProperty(PropertyName = "projects", ItemConverterType = typeof(JsonRefedConverter))]
		[JsonProperty(PropertyName = "projects")]
		private List<AsanaProject> _deserializedProjects;

		/// <summary>
        /// Accesses the /tasks/{id}/projects endpoint.
        /// </summary>
        /// <returns></returns>
		public async Task<AsanaCollection<AsanaProject>> GetProjects(string optFields = null)
		{
			var request = new RestRequest("/tasks/{id}/projects", Method.GET);
			request.AddUrlSegment("id", Id.ToString());
			if (!ReferenceEquals(optFields, null))
				request.AddParameter("opt_fields", optFields.ToString());
			// execute the request
			var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
			var content = response.Content; // raw content as string
			
			using (var stringReader = new StringReader(content))
            using (var reader = new JsonTextReader(stringReader))
            {
				var jsonData = AsanaHost.JsonDeserializer.Deserialize<AsanaArrayResponse>(reader);
				var resultsCollection = jsonData.Data.ToObject<List<AsanaProject>>(AsanaHost.JsonDeserializer);
				
				if (ReferenceEquals(Projects, null))
				{
					Projects = new AsanaCollection<AsanaProject>(resultsCollection);
				}
				else
				{
					Projects.Import(resultsCollection);
				}

					//Projects = jsonData.Data.ToObject<AsanaCollection<AsanaProject>>(AsanaHost.JsonDeserializer);
				//else
					//AsanaHost.JsonDeserializer.Populate(jsonData.Data.CreateReader(), Projects);
				return Projects;
                //var token = JTokenExtensions.DeserializeAndCombineDuplicates(reader);
			}
		}

		public string Name
		{
			get 
			{
				return _localName ?? _remoteName;
			}
			set
			{
				if (_remoteName != value)
					_localName = value;
			}
        }
		
		[JsonProperty("name")]
		//[JsonProperty("name")]
        private string _remoteName
		{
			get 
			{
				return _remoteValueName;
			}
			set
			{
				if (_remoteValueName != value)
				{
					_remoteValueName = value;
					OnPropertyChanged("Name");
				}
			}
		}
		private string _remoteValueName;

		private string _localName;

		public string Notes
		{
			get 
			{
				return _localNotes ?? _remoteNotes;
			}
			set
			{
				if (_remoteNotes != value)
					_localNotes = value;
			}
        }
		
		[JsonProperty("notes")]
		//[JsonProperty("notes")]
        private string _remoteNotes
		{
			get 
			{
				return _remoteValueNotes;
			}
			set
			{
				if (_remoteValueNotes != value)
				{
					_remoteValueNotes = value;
					OnPropertyChanged("Notes");
				}
			}
		}
		private string _remoteValueNotes;

		private string _localNotes;

		public async Task<AsanaTask> Save()
		{
			RestRequest request;

			if (IsLocal) // need to create
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.POST);
			}
			else // update
			{
				request = new RestRequest("/workspaces/{id}/tasks", Method.PUT);
			}
            
            request.AddUrlSegment("id", Id.ToString());

            // for each _local
            if (!ReferenceEquals(_localName, null))
                request.AddParameter("name", _localName.ToString());

            // execute the request
            var response = await AsanaHost.RestClient.ExecuteTaskAsync(request);
            var content = response.Content; // raw content as string

            // ... 

            if (Projects.BufferCount > 0)
	        {
	            var projectsBuffer = Projects.GetBufferAndPurge();
	            foreach (var project in projectsBuffer.AddBuffer)
	            {
	                await project.Save(); // save if not saved yet
	                AddProject(project);
	            }
	        }


            /*
            return new
            {
                Lala = "test",
                Projects = new[] { "test" }
            };
            */
		}

		[OnDeserialized]
		internal void OnDeserializedMethod(StreamingContext context)
		{
			// Load Followers //
			if (!ReferenceEquals(_deserializedFollowers, null))
			{
				if (ReferenceEquals(Followers, null))
				{
					// first deserialization
					Followers = new AsanaCollection<Dictionary<string, string>>(_deserializedFollowers);
					if (_deserializedFollowers.Any())
						OnPropertyChanged("Followers");
				}
				else
				{
					var anythingChanged = Followers.Import(_deserializedFollowers);
					if (anythingChanged)
						OnPropertyChanged("Followers");
				}

				// cleanup
				_deserializedFollowers = null;
			}
			else if (ReferenceEquals(Followers, null))
			{
				Followers = new AsanaCollection<Dictionary<string, string>>();
			}
			// ----------------------------- //

			// Load Projects //
			if (!ReferenceEquals(_deserializedProjects, null))
			{
				if (ReferenceEquals(Projects, null))
				{
					// first deserialization
					Projects = new AsanaCollection<AsanaProject>(_deserializedProjects);
					if (_deserializedProjects.Any())
						OnPropertyChanged("Projects");
				}
				else
				{
					var anythingChanged = Projects.Import(_deserializedProjects);
					if (anythingChanged)
						OnPropertyChanged("Projects");
				}

				// cleanup
				_deserializedProjects = null;
			}
			else if (ReferenceEquals(Projects, null))
			{
				Projects = new AsanaCollection<AsanaProject>();
			}
			// ----------------------------- //

		}

		[OnDeserializing]
		internal void OnDeserializingMethod(StreamingContext context)
		{
			if (ReferenceEquals(AsanaHost, null))
				AsanaHost = ((JsonLinkedContext)context.Context).AsanaHost;
		}
	} // END class: AsanaTask

} // END: namespace

/*
Asana : Object:


AsanaTeam : Object:
/teams/{id}

AsanaWorkspace : Object:
/workspaces/{id}

AsanaProject : Object:
/projects/{id}

AsanaUser : Object:
/users/{id}

AsanaTask : Object:
/tasks/{id}

AsanaCollection`1 : ObservableCollection`1:


AsanaReadOnlyCollection`1 : ObservableCollection`1:


*/